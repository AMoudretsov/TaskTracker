import { inject } from "@angular/core";
import { tapResponse } from "@ngrx/operators";
import { patchState, signalStoreFeature, type, withMethods } from "@ngrx/signals";
import { rxMethod } from "@ngrx/signals/rxjs-interop";
import { concatMap, pipe, tap } from "rxjs";
import { TasksService } from "../../services/tasks.service";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppState } from "../states/app.state";
import { appendTasks, setError, setInProgress, setPaging } from "./setters";

export const withTaskLoadNextPage = () =>
  signalStoreFeature(
    { state: type<AppState>() },
    withMethods(
      (store, tasksService = inject(TasksService), textLiterals = inject(TextLiteralsService)) => ({
        loadNextPage: rxMethod<void>(
          pipe(
            tap(() => patchState(store, setInProgress(true))),
            concatMap(() => {
              return tasksService
                .listTasks({
                  search: store.searchTerm(),
                  limit: store.paging.limit(),
                  cursor: store.paging.cursor(),
                })
                .pipe(
                  tapResponse({
                    next: (response) =>
                      patchState(store, appendTasks(response.items), setPaging(response.paging)),
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
