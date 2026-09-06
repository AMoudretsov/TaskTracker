import { HttpEvent, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

export const rootUrlInterceptor: HttpInterceptorFn = (
  request: HttpRequest<unknown>,
  next: HttpHandlerFn,
): Observable<HttpEvent<unknown>> => {
  const isAbsoluteUrl = request.url.startsWith("http://") || request.url.startsWith("https://");
  if (isAbsoluteUrl) {
    return next(request);
  }

  const apiUrl = `${environment.apiUrl}${request.url.startsWith("/") ? "" : "/"}${request.url}`;
  request = request.clone({ url: apiUrl });

  return next(request);
};
