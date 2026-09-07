import { Service } from "@angular/core";

@Service()
export class TextLiteralsService {
  public readonly AppTitle = "Task Tracker";

  public readonly TabTitle = "Task Tracker";

  public readonly SearchPlaceholder = "Search tasks";

  public readonly NoTasksFound = "No tasks found";

  public readonly TaskStatusActive = "Active";

  public readonly TaskStatusDone = "Done";

  public readonly SearchFailure = "Error occurred on retrieving the task list";

  public readonly UpdateStatusFailure = "Error occurred on updating the task status";

  public readonly NotImplemented = "Functionality not implemented yet. Coming soon.";
}
