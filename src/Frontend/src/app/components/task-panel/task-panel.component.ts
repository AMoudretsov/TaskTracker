import { formatDate } from "@angular/common";
import { Component, computed, inject, model } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatChipsModule } from "@angular/material/chips";
import { MatDividerModule } from "@angular/material/divider";
import { MatIcon } from "@angular/material/icon";
import { Task } from "../../models/task.model";
import { TextLiteralsService } from "../../services/text-literals.service";

@Component({
  imports: [MatButtonModule, MatCardModule, MatDividerModule, MatIcon, MatChipsModule],
  selector: "app-task-panel",
  styleUrl: "./task-panel.component.scss",
  templateUrl: "./task-panel.component.html",
})
export class TaskPanel {
  private _textLiterals = inject(TextLiteralsService);

  public task = model.required<Task>();

  protected createdAt = computed(() =>
    formatDate(this.task().createdAt, "yyyy-MM-dd HH:mm:ss", "en-GB"),
  );

  protected done = computed(() => this.task().isCompleted);

  protected status = computed(() =>
    this.task().isCompleted
      ? this._textLiterals.TaskStatusDone
      : this._textLiterals.TaskStatusActive,
  );

  protected toggleStatus() {
    this.task.update((t) => ({ ...t, isCompleted: !t.isCompleted }));
  }
}
