import { inject } from "@angular/core";
import { tapResponse } from "@ngrx/operators";
import { patchState, signalStoreFeature, type, withMethods } from "@ngrx/signals";
import { rxMethod } from "@ngrx/signals/rxjs-interop";
import { concatMap, pipe, tap } from "rxjs";
import { Task } from "../../models/task.model";
import { TasksService } from "../../services/tasks.service";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppState } from "../states/app.state";
import { deleteTask, setError, setInProgress } from "./setters";

export const withTaskDelete = () =>
  signalStoreFeature(
    { state: type<AppState>() },
    withMethods(
      (store, tasksService = inject(TasksService), textLiterals = inject(TextLiteralsService)) => ({
        delete: rxMethod<Task>(
          pipe(
            tap(() => patchState(store, setInProgress(true))),
            concatMap((task) => {
              return tasksService.deleteTask(task).pipe(
                tapResponse({
                  next: () => patchState(store, deleteTask(task)),
                  error: () => patchState(store, setError(textLiterals.DeleteFailure)),
                  finalize: () => patchState(store, setInProgress(false)),
                }),
              );
            }),
          ),
        ),
      }),
    ),
  );
