import { Component, computed, inject, signal } from "@angular/core";
import { form, FormField, maxLength, required } from "@angular/forms/signals";
import { MatButtonModule } from "@angular/material/button";
import { MatDialogModule } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { Task } from "../../models/task.model";
import { TextLiteralsService } from "../../services/text-literals.service";

interface AddModel {
  title: string;
  description: string;
}

@Component({
  imports: [FormField, MatButtonModule, MatDialogModule, MatFormFieldModule, MatInputModule],
  selector: "app-add-dialog",
  styleUrl: "./add-dialog.component.scss",
  templateUrl: "./add-dialog.component.html",
})
export class AddDialogComponent {
  readonly textLiterals = inject(TextLiteralsService);

  addModel = signal<AddModel>({ title: "", description: "" });
  addForm = form(this.addModel, (schemaPath) => {
    required(schemaPath.title, { message: this.textLiterals.TitleRequiredValidationMessage });

    maxLength(schemaPath.title, 128, {
      message: this.textLiterals.TitleMaxLengthValidationMessage,
    });

    maxLength(schemaPath.description, 4096, {
      message: this.textLiterals.DescriptionMaxLengthValidationMessage,
    });
  });
  addTask = computed<Task>(() => ({
    ...this.addModel(),
    id: 0,
    isCompleted: false,
    createdAt: new Date(),
  }));

  hasErrors = computed(() => !this.addForm().valid());
}
