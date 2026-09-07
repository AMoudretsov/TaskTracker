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
  public task = model.required<Task>();

  protected readonly store = inject(AppStore);

  protected textLiterals = inject(TextLiteralsService);

  protected changeInProgress = signal(false);

  protected createdAt = computed(() =>
    formatDate(this.task().createdAt, "yyyy-MM-dd HH:mm:ss", "en-GB"),
  );

  protected done = computed(() => this.task().isCompleted);

  protected toggleDisabled = computed(() => this.changeInProgress());

  protected editDisabled = computed(() => this.done() || this.changeInProgress());

  protected deleteDisabled = computed(() => this.done() || this.changeInProgress());

  protected status = computed(() =>
    this.task().isCompleted ? this.textLiterals.TaskStatusDone : this.textLiterals.TaskStatusActive,
  );

  protected toggleStatus() {
    this.changeInProgress.set(true);
    this.store.toggleCompletionStatus(this.task());
  }

  protected editTask(): void {
    this.store.notify(this.textLiterals.NotImplemented);
  }

  protected deleteTask(): void {
    this.changeInProgress.set(true);
    this.store.delete(this.task());
  }

  ngOnChanges(): void {
    this.changeInProgress.set(false);
  }
}
