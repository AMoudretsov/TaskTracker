import { Component, computed, inject, signal } from "@angular/core";
import { form, FormField } from "@angular/forms/signals";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppStore } from "../../store/app.store";
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
  protected readonly store = inject(AppStore);

  protected readonly textLiterals = inject(TextLiteralsService);

  protected searchModel = signal<SearchModel>({ searchTerm: this.store.searchTerm() });
  protected searchForm = form(this.searchModel);
  protected searchTerm = computed(() => this.searchModel().searchTerm);

  constructor() {
    this.store.search(this.searchTerm);
  }
}
