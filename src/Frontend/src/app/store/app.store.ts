import { computed, inject } from "@angular/core";
import { signalStore, withComputed, withState } from "@ngrx/signals";
import { TextLiteralsService } from "../services/text-literals.service";
import { withTaskDelete } from "./features/delete";
import { withTaskLoadNextPage } from "./features/load-next-page";
import { withNotify } from "./features/notify";
import { withTaskSearch } from "./features/search";
import { withTaskToggleCompletionStatus } from "./features/toggle-completion-status";
import { AppState } from "./states/app.state";

const initialState: AppState = {
  tasks: [],
  inProgress: false,
  searchTerm: "",
  paging: { limit: 10, cursor: null, hasMore: false },
  error: null,
  tabTitle: "",
};

export const AppStore = signalStore(
  withState(() => ({ ...initialState, tabTitle: inject(TextLiteralsService).TabTitle })),
  withTaskSearch(),
  withTaskLoadNextPage(),
  withTaskToggleCompletionStatus(),
  withTaskDelete(),
  withNotify(),
  withComputed(({ tasks, inProgress, paging }) => ({
    noTasksFound: computed(() => tasks().length === 0),
    canLoadNextPage: computed(() => !inProgress() && paging.hasMore()),
  })),
);
