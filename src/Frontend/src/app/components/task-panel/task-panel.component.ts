import { formatDate } from "@angular/common";
import { Component, computed, inject, model, OnChanges, signal } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatChipsModule } from "@angular/material/chips";
import { MatDividerModule } from "@angular/material/divider";
import { MatIcon } from "@angular/material/icon";
import { Task } from "../../models/task.model";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppStore } from "../../store/app.store";

@Component({
  imports: [MatButtonModule, MatCardModule, MatDividerModule, MatIcon, MatChipsModule],
  selector: "app-task-panel",
  styleUrl: "./task-panel.component.scss",
  templateUrl: "./task-panel.component.html",
})
export class TaskPanel implements OnChanges {
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
    this.changeInProgress.set(true);
    this.store.delete(this.task());
  }

  ngOnChanges(): void {
    this.changeInProgress.set(false);
  }
}
