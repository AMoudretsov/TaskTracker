import { HttpParams } from "@angular/common/http";
import { Service } from "@angular/core";

type Query = Record<string, any>;

@Service()
export class HttpUtilsService {
  buildHttpParams(query?: Query, defaults?: Query): HttpParams {
    if (defaults) {
      query = this.mergeDefaults(query, defaults);
    }

    let params = new HttpParams();

    if (!query) {
      return params;
    }

    for (const [key, value] of Object.entries(query)) {
      if (value !== undefined && value !== null) {
        params = params.set(key, value.toString());
      }
    }

    return params;
  }

  private mergeDefaults(values?: Query, defaults?: Query): Query {
    values ??= {};
    defaults ??= {};

    const result = { ...values, ...defaults };

    for (const key of Object.keys(result)) {
      const value = (values as any)[key];
      const defaultValue = (defaults as any)[key];

      result[key] = value ?? defaultValue;
    }

    return result;
  }
}
