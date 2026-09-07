import { computed, inject } from "@angular/core";
import { tapResponse } from "@ngrx/operators";
import { patchState, signalStore, withComputed, withMethods, withState } from "@ngrx/signals";
import { rxMethod } from "@ngrx/signals/rxjs-interop";
import { concatMap, debounceTime, distinctUntilChanged, pipe, switchMap, tap } from "rxjs";
import { Task } from "../models/task.model";
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
      toggleCompletionStatus: rxMethod<Task>(
        pipe(
          tap(() => patchState(store, { inProgress: true })),
          concatMap((task) => {
            return tasksService
              .updateTask({
                ...task,
                isCompleted: !task.isCompleted,
              })
              .pipe(
                tapResponse({
                  next: (response) =>
                    patchState(store, (state) => ({
                      tasks: state.tasks.map((t) =>
                        t.id === response.id ? { ...t, isCompleted: response.isCompleted } : t,
                      ),
                    })),
                  error: () =>
                    patchState(store, { error: { message: textLiterals.UpdateStatusFailure } }),
                  finalize: () => patchState(store, { inProgress: false }),
                }),
              );
          }),
        ),
      ),
      notify(message: string): void {
        patchState(store, () => ({ error: { message } }));
      },
    }),
  ),
);
