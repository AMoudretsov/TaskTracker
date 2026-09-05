import { Component, inject, OnInit } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { HeaderComponent } from "./components/header/header.component";
import { LayoutComponent } from "./components/layout/layout.component";
import { BrowserService } from "./services/browser.service";

@Component({
  selector: "app-root",
  imports: [HeaderComponent, LayoutComponent, RouterOutlet],
  templateUrl: "./app.html",
  styleUrl: "./app.scss",
})
export class App implements OnInit {
  private readonly _browserService = inject(BrowserService);

  ngOnInit(): void {
    this._browserService.setTabTitle();
  }
}
