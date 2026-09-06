import { ApplicationConfig, provideBrowserGlobalErrorListeners } from "@angular/core";
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from "@angular/material/form-field";
import { provideRouter } from "@angular/router";

import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { routes } from "./app.routes";
import { rootUrlInterceptor } from "./interceptors/root-url.interceptor";
import { AppStore } from "./store/app.store";

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([rootUrlInterceptor])),
    provideRouter(routes),
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: { appearance: "outline" },
    },
    AppStore,
  ],
};
