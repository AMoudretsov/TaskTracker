import { inject, Service } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { TextLiteralsService } from "./text-literals.service";

@Service()
export class BrowserService {
  private readonly _titleService = inject(Title);
  private readonly _textLiterals = inject(TextLiteralsService);

  public setTabTitle(title?: string): void {
    title ??= this._textLiterals.TabTitle;
    this._titleService.setTitle(title);
  }
}
