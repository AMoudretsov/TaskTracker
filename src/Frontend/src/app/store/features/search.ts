import { inject } from "@angular/core";
import { tapResponse } from "@ngrx/operators";
import { patchState, signalStoreFeature, type, withMethods } from "@ngrx/signals";
import { rxMethod } from "@ngrx/signals/rxjs-interop";
import { debounceTime, distinctUntilChanged, pipe, switchMap, tap } from "rxjs";
import { TasksService } from "../../services/tasks.service";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppState } from "../states/app.state";
import { setError, setInProgress, setPaging, setSearchTerm, setTasks } from "./setters";

export const withTaskSearch = () =>
  signalStoreFeature(
    { state: type<AppState>() },
    withMethods(
      (store, tasksService = inject(TasksService), textLiterals = inject(TextLiteralsService)) => ({
        search: rxMethod<string>(
          pipe(
            debounceTime(500),
            distinctUntilChanged(),
            tap((searchTerm) => patchState(store, setInProgress(true), setSearchTerm(searchTerm))),
            switchMap((searchTerm) => {
              return tasksService
                .listTasks({ search: searchTerm, limit: store.paging.limit(), cursor: null })
                .pipe(
                  tapResponse({
                    next: (response) =>
                      patchState(store, setTasks(response.items), setPaging(response.paging)),
                    error: () => patchState(store, setError(textLiterals.SearchFailure)),
                    finalize: () => patchState(store, setInProgress(false)),
                  }),
                );
            }),
          ),
        ),
      }),
    ),
  );
