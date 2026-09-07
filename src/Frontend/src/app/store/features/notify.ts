import { patchState, signalStoreFeature, type, withMethods } from "@ngrx/signals";
import { AppState } from "../states/app.state";
import { setError } from "./setters";

export const withNotify = () =>
  signalStoreFeature(
    { state: type<AppState>() },
    withMethods((store) => ({
      notify(message: string): void {
        patchState(store, setError(message));
      },
    })),
  );
