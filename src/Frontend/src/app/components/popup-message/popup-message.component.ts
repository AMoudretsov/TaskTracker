import { Component, inject, signal } from "@angular/core";
import { MatIconModule } from "@angular/material/icon";
import { MAT_SNACK_BAR_DATA } from "@angular/material/snack-bar";
import { AlertState, AlertTypes } from "../../store/states/alert.state";

export interface PopupMessageData {
  alert: AlertState;
}

@Component({
  imports: [MatIconModule],
  selector: "app-popup-message",
  styleUrl: "./popup-message.component.scss",
  templateUrl: "./popup-message.component.html",
})
export class PopupMessageComponent {
  data = inject<PopupMessageData>(MAT_SNACK_BAR_DATA);

  iconType = signal(PopupMessageComponent.getIconType(this.data.alert));

  static popupPanelClasses(alert: AlertState) {
    switch (alert.type) {
      case AlertTypes.Info:
        return "popup-message-info";
      case AlertTypes.Error:
        return "popup-message-error";
      default:
        throw new Error(`Unknown alert type ${alert.type}`);
    }
  }

  private static getIconType(alert: AlertState) {
    switch (alert.type) {
      case AlertTypes.Info:
        return "feedback";
      case AlertTypes.Error:
        return "cancel";
      default:
        throw new Error(`Unknown alert type ${alert.type}`);
    }
  }
}
