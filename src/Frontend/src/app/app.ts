import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { HeaderComponent } from "./components/header/header.component";
import { LayoutComponent } from "./components/layout/layout.component";

@Component({
  selector: "app-root",
  imports: [HeaderComponent, LayoutComponent, RouterOutlet],
  templateUrl: "./app.html",
  styleUrl: "./app.scss",
})
export class App {}
