import { Task } from "../../models/task.model";
import { AlertState } from "./alert.state";
import { PagingState } from "./paging.state";

export interface AppState {
  tasks: Task[];
  inProgress: boolean;
  searchTerm: string;
  paging: PagingState;
  alert: AlertState | null;
  tabTitle: string;
}
