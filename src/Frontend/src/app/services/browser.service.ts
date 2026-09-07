import { inject, Service } from "@angular/core";
import { Title } from "@angular/platform-browser";

@Service()
export class BrowserService {
  private readonly _titleService = inject(Title);

  setTabTitle(title: string): void {
    this._titleService.setTitle(title);
  }
}
