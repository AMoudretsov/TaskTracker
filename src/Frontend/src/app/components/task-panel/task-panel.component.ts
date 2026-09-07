import { formatDate } from "@angular/common";
import {
  Component,
  computed,
  effect,
  inject,
  Injector,
  model,
  OnChanges,
  signal,
} from "@angular/core";
import { toSignal } from "@angular/core/rxjs-interop";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatChipsModule } from "@angular/material/chips";
import { MatDialog } from "@angular/material/dialog";
import { MatDividerModule } from "@angular/material/divider";
import { MatIcon } from "@angular/material/icon";
import { Task } from "../../models/task.model";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppStore } from "../../store/app.store";
import { ConfirmDeleteDialogComponent } from "../confirm-delete-dialog/confirm-delete-dialog.component";

@Component({
  imports: [MatButtonModule, MatCardModule, MatDividerModule, MatIcon, MatChipsModule],
  selector: "app-task-panel",
  styleUrl: "./task-panel.component.scss",
  templateUrl: "./task-panel.component.html",
})
export class TaskPanel implements OnChanges {
  private readonly _injector = inject(Injector);

  private readonly _dialog = inject(MatDialog);

  task = model.required<Task>();

  readonly store = inject(AppStore);

  readonly textLiterals = inject(TextLiteralsService);

  changeInProgress = signal(false);

  createdAt = computed(() => formatDate(this.task().createdAt, "yyyy-MM-dd HH:mm:ss", "en-GB"));

  done = computed(() => this.task().isCompleted);

  toggleDisabled = computed(() => this.changeInProgress());

  editDisabled = computed(() => this.done() || this.changeInProgress());

  deleteDisabled = computed(() => this.done() || this.changeInProgress());

  status = computed(() =>
    this.task().isCompleted ? this.textLiterals.TaskStatusDone : this.textLiterals.TaskStatusActive,
  );

  toggleStatus() {
    this.changeInProgress.set(true);
    this.store.toggleCompletionStatus(this.task());
  }

  editTask(): void {
    this.store.notify(this.textLiterals.NotImplemented);
  }

  deleteTask(): void {
    const confirmDialog = this._dialog.open(ConfirmDeleteDialogComponent, {
      data: {
        dialogTitle: this.textLiterals.ConfirmTaskDeleteTitle,
        dialogMessage: this.textLiterals.ConfirmTaskDeleteContent,
        itemToDelete: this.task().title,
      },
      disableClose: true,
      panelClass: ["confirm-delete-dialog"],
      autoFocus: "dialog",
      height: "15rem",
      width: "25rem",
    });

    const confirmed = toSignal(confirmDialog.afterClosed(), {
      initialValue: null,
      injector: this._injector,
    });

    effect(
      () => {
        if (confirmed()) {
          this.changeInProgress.set(true);
          this.store.delete(this.task());
        }
      },
      { injector: this._injector },
    );
  }

  ngOnChanges(): void {
    this.changeInProgress.set(false);
  }
}
