import { computed, inject } from "@angular/core";
import { tapResponse } from "@ngrx/operators";
import { patchState, signalStore, withComputed, withMethods, withState } from "@ngrx/signals";
import { rxMethod } from "@ngrx/signals/rxjs-interop";
import { debounceTime, delay, distinctUntilChanged, pipe, switchMap, tap } from "rxjs";
import { TasksService } from "../services/tasks.service";
import { TextLiteralsService } from "../services/text-literals.service";
import { AppState } from "./app-state.model";

const initialState: AppState = {
  tasks: [],
  inProgress: false,
  searchTerm: "",
  paging: { limit: 10, cursor: null, hasMore: false },
  error: null,
  tabTitle: "",
};

export const AppStore = signalStore(
  withState(() => ({ ...initialState, tabTitle: inject(TextLiteralsService).TabTitle })),
  withComputed(({ tasks, inProgress, paging }) => ({
    noTasksFound: computed(() => tasks().length === 0),
    canLoadNextPage: computed(() => !inProgress() && paging.hasMore()),
  })),
  withMethods(
    (store, tasksService = inject(TasksService), textLiterals = inject(TextLiteralsService)) => ({
      search: rxMethod<string>(
        pipe(
          debounceTime(500),
          distinctUntilChanged(),
          tap((searchTerm) => patchState(store, { inProgress: true, searchTerm })),
          delay(2000),
          switchMap((searchTerm) => {
            return tasksService
              .listTasks({ search: searchTerm, limit: store.paging.limit(), cursor: null })
              .pipe(
                tapResponse({
                  next: (response) =>
                    patchState(store, {
                      tasks: response.items ?? [],
                      paging: {
                        ...store.paging(),
                        cursor: response.paging.cursor ?? null,
                        hasMore: response.paging.hasMore ?? false,
                      },
                    }),
                  error: () =>
                    patchState(store, { error: { message: textLiterals.SearchFailure } }),
                  finalize: () => patchState(store, { inProgress: false }),
                }),
              );
          }),
        ),
      ),
      loadNextPage: rxMethod<void>(
        pipe(
          tap(() => patchState(store, { inProgress: true })),
          delay(2000),
          switchMap(() => {
            return tasksService
              .listTasks({
                search: store.searchTerm(),
                limit: store.paging.limit(),
                cursor: store.paging.cursor(),
              })
              .pipe(
                tapResponse({
                  next: (response) =>
                    patchState(store, {
                      tasks: [...store.tasks(), ...(response.items ?? [])],
                      paging: {
                        ...store.paging(),
                        cursor: response.paging.cursor ?? null,
                        hasMore: response.paging.hasMore ?? false,
                      },
                    }),
                  error: () =>
                    patchState(store, { error: { message: textLiterals.SearchFailure } }),
                  finalize: () => patchState(store, { inProgress: false }),
                }),
              );
          }),
        ),
      ),
    }),
  ),
);
