import { computed, inject } from "@angular/core";
import { signalStore, withComputed, withState } from "@ngrx/signals";
import { TextLiteralsService } from "../services/text-literals.service";
import { withTaskCreate } from "./features/create";
import { withTaskDelete } from "./features/delete";
import { withTaskLoadNextPage } from "./features/load-next-page";
import { withTaskSearch } from "./features/search";
import { withTaskToggleCompletionStatus } from "./features/toggle-completion-status";
import { withTaskUpdate } from "./features/update";
import { AppState } from "./states/app.state";

const initialState: AppState = {
  tasks: [],
  inProgress: false,
  searchTerm: "",
  paging: { limit: 10, cursor: null, hasMore: false },
  alert: null,
  tabTitle: "",
};

export const AppStore = signalStore(
  withState(() => ({ ...initialState, tabTitle: inject(TextLiteralsService).TabTitle })),
  withTaskSearch(),
  withTaskLoadNextPage(),
  withTaskToggleCompletionStatus(),
  withTaskCreate(),
  withTaskUpdate(),
  withTaskDelete(),
  withComputed(({ inProgress, paging }) => ({
    canLoadNextPage: computed(() => !inProgress() && paging.hasMore()),
  })),
);
