import { Component, inject } from "@angular/core";
import { MatIconModule } from "@angular/material/icon";
import { MAT_SNACK_BAR_DATA } from "@angular/material/snack-bar";

export interface PopupMessageData {
  message: string;
}

@Component({
  imports: [MatIconModule],
  selector: "app-popup-message",
  styleUrl: "./popup-message.component.scss",
  templateUrl: "./popup-message.component.html",
})
export class PopupMessageComponent {
  data = inject<PopupMessageData>(MAT_SNACK_BAR_DATA);
}
