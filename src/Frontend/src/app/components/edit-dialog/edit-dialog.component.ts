import { Component, computed, inject, OnInit, signal } from "@angular/core";
import { form, FormField, maxLength, required } from "@angular/forms/signals";
import { MatButtonModule } from "@angular/material/button";
import { MAT_DIALOG_DATA, MatDialogModule } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { Task } from "../../models/task.model";
import { TextLiteralsService } from "../../services/text-literals.service";

export interface EditDialogData {
  task: Task;
}

interface EditModel {
  title: string;
  description: string;
}

@Component({
  imports: [FormField, MatButtonModule, MatDialogModule, MatFormFieldModule, MatInputModule],
  selector: "app-edit-dialog",
  styleUrl: "./edit-dialog.component.scss",
  templateUrl: "./edit-dialog.component.html",
})
export class EditDialogComponent implements OnInit {
  readonly textLiterals = inject(TextLiteralsService);

  readonly data = inject<EditDialogData>(MAT_DIALOG_DATA);

  editModel = signal<EditModel>({ title: "", description: "" });
  editForm = form(this.editModel, (schemaPath) => {
    required(schemaPath.title, { message: this.textLiterals.TitleRequiredValidationMessage });

    maxLength(schemaPath.title, 128, {
      message: this.textLiterals.TitleMaxLengthValidationMessage,
    });

    maxLength(schemaPath.description, 4096, {
      message: this.textLiterals.DescriptionMaxLengthValidationMessage,
    });
  });
  editTask = computed<Task>(() => ({ ...this.data.task, ...this.editModel() }));

  hasErrors = computed(() => !this.editForm().valid());

  ngOnInit(): void {
    this.editModel.set({
      title: this.data.task.title,
      description: this.data.task.description ?? "",
    });
  }
}
