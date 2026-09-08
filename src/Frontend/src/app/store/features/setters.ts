import { PagingResponse } from "../../models/list-response.model";
import { Task } from "../../models/task.model";
import { AlertTypes } from "../states/alert.state";
import { AppState } from "../states/app.state";

export const setTasks = (tasks: Task[]) => ({ tasks: tasks ?? [] });

export const appendTasks = (tasks: Task[]) => (state: AppState) => ({
  tasks: [...state.tasks, ...(tasks ?? [])],
});

export const addTask = (task: Task) => (state: AppState) => ({ tasks: [task, ...state.tasks] });

export const setTask = (task: Task) => (state: AppState) => ({
  tasks: state.tasks.map((t) => (t.id === task.id ? { ...task } : t)),
});

export const setTaskCompletionStatus = (task: Task) => (state: AppState) => ({
  tasks: state.tasks.map((t) => (t.id === task.id ? { ...t, isCompleted: task.isCompleted } : t)),
});

export const deleteTask = (task: Task) => (state: AppState) => ({
  tasks: state.tasks.filter((t) => t.id !== task.id),
});

export const refreshTask = (id: number) => (state: AppState) => ({
  tasks: state.tasks.map((t) => (t.id === id ? { ...t } : t)),
});

export const setPaging = (paging: PagingResponse) => (state: AppState) => ({
  paging: {
    ...state.paging,
    cursor: paging.cursor ?? null,
    hasMore: paging.hasMore ?? false,
  },
});

export const setSearchTerm = (searchTerm: string) => ({ searchTerm });

export const setInProgress = (inProgress: boolean) => ({ inProgress });

export const infoAlert = (message: string) => ({ alert: { type: AlertTypes.Info, message } });

export const errorAlert = (message: string, detail?: string) =>
  detail
    ? { alert: { type: AlertTypes.Error, message: `${message}\n${detail}` } }
    : { alert: { type: AlertTypes.Error, message } };

export const meetSearch = (searchTerm: string, task: Task) => {
  if (!searchTerm) {
    return true;
  }

  searchTerm = searchTerm.toLowerCase();
  return (
    task.title.toLowerCase().includes(searchTerm) ||
    (task.description?.toLowerCase().includes(searchTerm) ?? false)
  );
};
