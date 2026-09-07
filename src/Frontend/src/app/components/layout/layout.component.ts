import { Component, effect, inject, OnInit } from "@angular/core";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { MatSnackBar } from "@angular/material/snack-bar";
import { BrowserService } from "../../services/browser.service";
import { AppStore } from "../../store/app.store";
import { PopupMessageComponent } from "../popup-message/popup-message.component";

@Component({
  imports: [MatProgressBarModule],
  selector: "app-layout",
  styleUrl: "./layout.component.scss",
  templateUrl: "./layout.component.html",
})
export class LayoutComponent implements OnInit {
  private readonly _browserService = inject(BrowserService);
  private readonly _snackBar = inject(MatSnackBar);

  readonly store = inject(AppStore);

  readonly notifications = effect(() => {
    var error = this.store.error();
    if (error) {
      this._snackBar.openFromComponent(PopupMessageComponent, {
        data: { message: error.message },
        duration: 5000,
        horizontalPosition: "end",
        panelClass: ["popup-message-error"],
      });
    }
  });

  ngOnInit(): void {
    this._browserService.setTabTitle(this.store.tabTitle());
  }
}
