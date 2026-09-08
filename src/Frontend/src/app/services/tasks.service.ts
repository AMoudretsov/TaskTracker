import { HttpClient } from "@angular/common/http";
import { inject, Service } from "@angular/core";
import { Observable } from "rxjs";
import { ListResponse } from "../models/list-response.model";
import { SearchQuery } from "../models/search-query.model";
import { Task } from "../models/task.model";
import { HttpUtilsService } from "./http-utils.service";

const DefaultListLimit = 10;

@Service()
export class TasksService {
  private readonly _httpUtils = inject(HttpUtilsService);
  private readonly _api = inject(HttpClient);

  listTasks(query?: SearchQuery): Observable<ListResponse<Task>> {
    const params = this._httpUtils.buildHttpParams(query, { limit: DefaultListLimit });

    return this._api.get<ListResponse<Task>>("/tasks", { params });
  }

  updateTask(task: Task): Observable<Task> {
    return this._api.put<Task>(`/tasks/${task.id}`, task);
  }

  deleteTask(task: Task): Observable<Object> {
    return this._api.delete(`/tasks/${task.id}`);
  }
}
