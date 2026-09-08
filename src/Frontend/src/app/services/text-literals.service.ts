import { Service } from "@angular/core";

@Service()
export class TextLiteralsService {
  readonly AppTitle = "Task Tracker";

  readonly TabTitle = "Task Tracker";

  readonly SearchPlaceholder = "Search tasks";

  readonly NoTasksFound = "No tasks found";

  readonly TaskStatusActive = "Active";

  readonly TaskStatusDone = "Done";

  readonly EditTaskTitle = "Edit Task";

  readonly TitleField = "Title";

  readonly TitleFieldPlaceholder = "Add title";

  readonly DescriptionField = "Description";

  readonly DescriptionFieldPlaceholder = "Add description";

  readonly TitleRequiredValidationMessage = "Title is required";

  readonly TitleMaxLengthValidationMessage = "Title length must not exceed 128 characters";

  readonly DescriptionMaxLengthValidationMessage = "Title length must not exceed 4096 characters";

  readonly ConfirmTaskDeleteTitle = "Delete Task";

  readonly ConfirmTaskDeleteContent = "Please confirm deletion of the following task:";

  readonly SearchFailure = "Error occurred on retrieving the task list";

  readonly UpdateStatusFailure = "Error occurred on updating the task status";

  readonly UpdateFailure = "Error occurred on updating the task";

  readonly DeleteFailure = "Error occurred on deleting the task";

  readonly NotImplemented = "Functionality not implemented yet. Coming soon.";

  readonly Cancel = "Cancel";

  readonly Save = "Save";

  readonly Delete = "Delete";
}
