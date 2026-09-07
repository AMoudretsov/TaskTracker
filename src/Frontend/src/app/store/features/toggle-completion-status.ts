import { inject } from "@angular/core";
import { tapResponse } from "@ngrx/operators";
import { patchState, signalStoreFeature, type, withMethods } from "@ngrx/signals";
import { rxMethod } from "@ngrx/signals/rxjs-interop";
import { concatMap, pipe, tap } from "rxjs";
import { Task } from "../../models/task.model";
import { TasksService } from "../../services/tasks.service";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppState } from "../states/app.state";
import { setError, setInProgress, setTaskCompletionStatus } from "./setters";

export const withTaskToggleCompletionStatus = () =>
  signalStoreFeature(
    { state: type<AppState>() },
    withMethods(
      (store, tasksService = inject(TasksService), textLiterals = inject(TextLiteralsService)) => ({
        toggleCompletionStatus: rxMethod<Task>(
          pipe(
            tap(() => patchState(store, setInProgress(true))),
            concatMap((task) => {
              return tasksService
                .updateTask({
                  ...task,
                  isCompleted: !task.isCompleted,
                })
                .pipe(
                  tapResponse({
                    next: (response) => patchState(store, setTaskCompletionStatus(response)),
                    error: () => patchState(store, setError(textLiterals.UpdateStatusFailure)),
                    finalize: () => patchState(store, setInProgress(false)),
                  }),
                );
            }),
          ),
        ),
      }),
    ),
  );
