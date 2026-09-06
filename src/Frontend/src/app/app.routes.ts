import { Routes } from "@angular/router";
import { TasksFeedComponent } from "./components/tasks-feed/tasks-feed.component";

export const routes: Routes = [
  { path: "", component: TasksFeedComponent },
  { path: "**", redirectTo: "", pathMatch: "full" },
];
