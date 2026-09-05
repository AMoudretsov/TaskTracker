import { Component, inject } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { TextLiteralsService } from "../../services/text-literals.service";
import { LogoComponent } from "./logo/logo.component";

@Component({
  imports: [MatFormFieldModule, MatButtonModule, MatIconModule, MatInputModule, LogoComponent],
  selector: "app-header",
  styleUrl: "./header.component.scss",
  templateUrl: "./header.component.html",
})
export class HeaderComponent {
  private readonly _textLiterals = inject(TextLiteralsService);

  protected readonly SearchPlaceholder = this._textLiterals.SearchPlaceholder;
}
