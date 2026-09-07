import { Service } from "@angular/core";

@Service()
export class TextLiteralsService {
  readonly AppTitle = "Task Tracker";

  readonly TabTitle = "Task Tracker";

  readonly SearchPlaceholder = "Search tasks";

  readonly NoTasksFound = "No tasks found";

  readonly TaskStatusActive = "Active";

  readonly TaskStatusDone = "Done";

  readonly ConfirmTaskDeleteTitle = "Delete Task";

  readonly ConfirmTaskDeleteContent = "Please confirm deletion of the following task:";

  readonly SearchFailure = "Error occurred on retrieving the task list";

  readonly UpdateStatusFailure = "Error occurred on updating the task status";

  readonly DeleteFailure = "Error occurred on deleting the task";

  readonly NotImplemented = "Functionality not implemented yet. Coming soon.";

  readonly Cancel = "Cancel";

  readonly Delete = "Delete";
}
