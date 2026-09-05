import { Component, inject } from "@angular/core";
import { MatIconModule } from "@angular/material/icon";
import { TextLiteralsService } from "../../../services/text-literals.service";

@Component({
  imports: [MatIconModule],
  selector: "app-logo",
  styleUrl: "./logo.component.scss",
  templateUrl: "./logo.component.html",
})
export class LogoComponent {
  private readonly _textLiterals = inject(TextLiteralsService);

  protected readonly AppTitle = this._textLiterals.AppTitle;
}
