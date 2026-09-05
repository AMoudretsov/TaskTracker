import { Service } from "@angular/core";

@Service()
export class TextLiteralsService {
  public readonly AppTitle = "Task Tracker";

  public readonly TabTitle = "Task Tracker";

  public readonly SearchPlaceholder = "Search tasks";

  public readonly TaskStatusActive = "Active";

  public readonly TaskStatusDone = "Done";
}
