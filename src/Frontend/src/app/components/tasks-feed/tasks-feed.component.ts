import { Component, signal } from "@angular/core";
import { Task } from "../../models/task";
import { TaskPanel } from "../task-panel/task-panel.component";

@Component({
  imports: [TaskPanel],
  selector: "app-tasks-feed",
  styleUrl: "./tasks-feed.component.scss",
  templateUrl: "./tasks-feed.component.html",
})
export class TasksFeedComponent {
  protected tasks = signal<Task[]>([
    {
      id: 1,
      title: "#1 Two Sum",
      description:
        "You are given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target. You may assume that each input would have exactly one solution, and you may not use the same element twice. You can return the answer in any order.",
      isCompleted: false,
      createdAt: new Date("2026-03-07T11:45:00Z"),
    },
    {
      id: 11,
      title: "#11 Container With Most Water",
      description:
        "You are given an integer array height of length n. There are n vertical lines drawn such that the two endpoints of the i-th line are (i, 0) and (i, height[i]). Find two lines that together with the x-axis form a container, such that the container contains the most water. Return the maximum amount of water a container can store.",
      isCompleted: true,
      createdAt: new Date("2026-03-15T10:08:31Z"),
    },
    {
      id: 21,
      title: "#15 3Sum",
      description:
        "Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0. Notice that the solution set must not contain duplicate triplets.",
      isCompleted: false,
      createdAt: new Date("2026-03-28T16:37:22Z"),
    },
    {
      id: 31,
      title: "#20 Valid Parentheses",
      description:
        "Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid. An input string is valid if: 1. Open brackets must be closed by the same type of brackets. 2. Open brackets must be closed in the correct order. 3. Every close bracket has a corresponding open bracket of the same type.",
      isCompleted: true,
      createdAt: new Date("2026-04-15T19:00:18Z"),
    },
    {
      id: 41,
      title: "#21 Merge Two Sorted Lists",
      description:
        "You are given the heads of two sorted linked lists list1 and list2. Merge the two lists into one sorted list. The list should be made by splicing together the nodes of the first two lists. Return the head of the merged linked list.",
      isCompleted: false,
      createdAt: new Date("2026-05-01T19:51:29Z"),
    },
  ]);
}
