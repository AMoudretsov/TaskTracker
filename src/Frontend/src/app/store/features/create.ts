import { inject } from "@angular/core";
import { tapResponse } from "@ngrx/operators";
import { patchState, signalStoreFeature, type, withMethods } from "@ngrx/signals";
import { rxMethod } from "@ngrx/signals/rxjs-interop";
import { concatMap, pipe, tap } from "rxjs";
import { ErrorResponse } from "../../models/error-response.model";
import { Task } from "../../models/task.model";
import { TasksService } from "../../services/tasks.service";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppState } from "../states/app.state";
import { addTask, errorAlert, infoAlert, meetSearch, setInProgress } from "./setters";

export const withTaskCreate = () =>
  signalStoreFeature(
    { state: type<AppState>() },
    withMethods(
      (store, tasksService = inject(TasksService), textLiterals = inject(TextLiteralsService)) => ({
        create: rxMethod<Task>(
          pipe(
            tap(() => patchState(store, setInProgress(true))),
            concatMap((task) => {
              return tasksService.createTask(task).pipe(
                tapResponse({
                  next: (response) =>
                    meetSearch(store.searchTerm(), response)
                      ? patchState(store, addTask(response))
                      : patchState(store, infoAlert(textLiterals.NewTaskFilteredOutBySearch)),
                  error: (error: ErrorResponse) =>
                    patchState(store, errorAlert(textLiterals.SaveFailure, error.error?.detail)),
                  finalize: () => patchState(store, setInProgress(false)),
                }),
              );
            }),
          ),
        ),
      }),
    ),
  );
