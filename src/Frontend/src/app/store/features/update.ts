import { inject } from "@angular/core";
import { tapResponse } from "@ngrx/operators";
import { patchState, signalStoreFeature, type, withMethods } from "@ngrx/signals";
import { rxMethod } from "@ngrx/signals/rxjs-interop";
import { concatMap, pipe, tap } from "rxjs";
import { Task } from "../../models/task.model";
import { TasksService } from "../../services/tasks.service";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppState } from "../states/app.state";
import { setError, setInProgress, setTask } from "./setters";

export const withTaskUpdate = () =>
  signalStoreFeature(
    { state: type<AppState>() },
    withMethods(
      (store, tasksService = inject(TasksService), textLiterals = inject(TextLiteralsService)) => ({
        update: rxMethod<Task>(
          pipe(
            tap(() => patchState(store, setInProgress(true))),
            concatMap((task) => {
              return tasksService.updateTask(task).pipe(
                tapResponse({
                  next: (response) => patchState(store, setTask(response)),
                  error: () => patchState(store, setError(textLiterals.UpdateFailure)),
                  finalize: () => patchState(store, setInProgress(false)),
                }),
              );
            }),
          ),
        ),
      }),
    ),
  );
