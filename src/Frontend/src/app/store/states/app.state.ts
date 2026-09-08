import { Task } from "../../models/task.model";
import { ErrorState } from "./error.state";
import { PagingState } from "./paging.state";

export interface AppState {
  tasks: Task[];
  inProgress: boolean;
  searchTerm: string;
  paging: PagingState;
  error: ErrorState | null;
  tabTitle: string;
}
