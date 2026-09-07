import { Component, inject } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MAT_DIALOG_DATA, MatDialogModule } from "@angular/material/dialog";
import { TextLiteralsService } from "../../services/text-literals.service";

export interface ConfirmDeleteDialogInput {
  dialogTitle: string;
  dialogMessage: string;
  itemToDelete: string;
}

@Component({
  imports: [MatButtonModule, MatDialogModule],
  selector: "app-confirm-delete-dialog",
  styleUrl: "./confirm-delete-dialog.component.scss",
  templateUrl: "./confirm-delete-dialog.component.html",
})
export class ConfirmDeleteDialogComponent {
  readonly textLiterals = inject(TextLiteralsService);

  readonly dialogInput = inject<ConfirmDeleteDialogInput>(MAT_DIALOG_DATA);
}
