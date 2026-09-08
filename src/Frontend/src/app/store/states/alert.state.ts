export const AlertTypes = {
  Info: "info",
  Error: "error",
} as const;

export type AlertType = (typeof AlertTypes)[keyof typeof AlertTypes];

export interface AlertState {
  type: AlertType;
  message: string;
}
