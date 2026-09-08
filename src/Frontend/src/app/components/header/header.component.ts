import { Component, computed, effect, inject, Injector, signal } from "@angular/core";
import { toSignal } from "@angular/core/rxjs-interop";
import { form, FormField } from "@angular/forms/signals";
import { MatButtonModule } from "@angular/material/button";
import { MatDialog } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { Task } from "../../models/task.model";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppStore } from "../../store/app.store";
import { AddDialogComponent } from "../add-dialog/add-dialog.component";
import { LogoComponent } from "./logo/logo.component";

interface SearchModel {
  searchTerm: string;
}

@Component({
  imports: [
    FormField,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    LogoComponent,
  ],
  selector: "app-header",
  styleUrl: "./header.component.scss",
  templateUrl: "./header.component.html",
})
export class HeaderComponent {
  private readonly _injector = inject(Injector);

  private readonly _dialog = inject(MatDialog);

  readonly store = inject(AppStore);

  readonly textLiterals = inject(TextLiteralsService);

  searchModel = signal<SearchModel>({ searchTerm: this.store.searchTerm() });
  searchForm = form(this.searchModel);
  searchTerm = computed(() => this.searchModel().searchTerm);

  constructor() {
    this.store.search(this.searchTerm);
  }

  addTask(): void {
    const addDialog = this._dialog.open<AddDialogComponent, void, Task>(AddDialogComponent, {
      disableClose: true,
      autoFocus: "dialog",
      height: "30rem",
      width: "30rem",
    });

    const addedTaskSignal = toSignal(addDialog.afterClosed(), {
      initialValue: null,
      injector: this._injector,
    });

    effect(
      () => {
        const addedTask = addedTaskSignal();
        if (addedTask) {
          this.store.create(addedTask);
        }
      },
      { injector: this._injector },
    );
  }
}
