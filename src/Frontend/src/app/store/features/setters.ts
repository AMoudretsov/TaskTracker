import { PagingResponse } from "../../models/list-response.model";
import { Task } from "../../models/task.model";
import { AppState } from "../states/app.state";

export const setTasks = (tasks: Task[]) => ({ tasks: tasks ?? [] });

export const appendTasks = (tasks: Task[]) => (state: AppState) => ({
  tasks: [...state.tasks, ...(tasks ?? [])],
});

export const setTask = (task: Task) => (state: AppState) => ({
  tasks: state.tasks.map((t) => (t.id === task.id ? { ...task } : t)),
});

export const setTaskCompletionStatus = (task: Task) => (state: AppState) => ({
  tasks: state.tasks.map((t) => (t.id === task.id ? { ...t, isCompleted: task.isCompleted } : t)),
});

export const deleteTask = (task: Task) => (state: AppState) => ({
  tasks: state.tasks.filter((t) => t.id !== task.id),
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

export const setError = (message: string) => ({ error: { message } });
