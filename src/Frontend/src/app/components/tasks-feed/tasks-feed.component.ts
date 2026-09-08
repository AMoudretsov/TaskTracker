import { AfterViewInit, Component, ElementRef, inject, OnDestroy, viewChild } from "@angular/core";
import { TextLiteralsService } from "../../services/text-literals.service";
import { AppStore } from "../../store/app.store";
import { TaskPanel } from "../task-panel/task-panel.component";

@Component({
  imports: [TaskPanel],
  selector: "app-tasks-feed",
  styleUrl: "./tasks-feed.component.scss",
  templateUrl: "./tasks-feed.component.html",
})
export class TasksFeedComponent implements AfterViewInit, OnDestroy {
  private _intersectionObserver!: IntersectionObserver;

  readonly store = inject(AppStore);

  readonly textLiterals = inject(TextLiteralsService);

  nextPageIndicator = viewChild<ElementRef>("nextPageIndicator");

  ngAfterViewInit(): void {
    this.initIntersectionObserver();
  }

  ngOnDestroy(): void {
    this._intersectionObserver?.disconnect();
  }

  private initIntersectionObserver(): void {
    this._intersectionObserver = new IntersectionObserver(
      (entries) => {
        const entry = entries[0];

        if (entry.isIntersecting && this.store.canLoadNextPage()) {
          this.store.loadNextPage();
        }
      },
      { threshold: 0.01 },
    );

    this._intersectionObserver.observe(this.nextPageIndicator()!.nativeElement);
  }
}
