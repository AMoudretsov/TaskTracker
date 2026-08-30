using Microsoft.EntityFrameworkCore;
using TaskTracker.Core.Entities;

namespace TaskTracker.Infrastructure.Seeding;

public static class TaskItemSeeding
{
    /// <summary>
    /// If TaskItem entity table is empty, seed it with test tasks, taken from LeetCode.
    /// </summary>
    public static void Seed(DbContext context, bool hasMigrationChanges)
    {
        ArgumentNullException.ThrowIfNull(context);

        var taskItems = context.Set<TaskItem>();
        if (taskItems.Any())
        {
            return;
        }

        taskItems.AddRange(LeetCodeTaskItems);
        context.SaveChanges();
    }

    /// <summary>
    /// If TaskItem entity table is empty, seed it with test tasks, taken from LeetCode.
    /// </summary>
    public static async Task SeedAsync(
        DbContext context,
        bool hasMigrationChanges,
        CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);

        var taskItems = context.Set<TaskItem>();

        var hasTasks = await taskItems.AnyAsync(cancelToken);
        if (hasTasks)
        {
            return;
        }

        taskItems.AddRange(LeetCodeTaskItems);
        await context.SaveChangesAsync(cancelToken);
    }

    private static IEnumerable<TaskItem> LeetCodeTaskItems =>
        [
            new()
            {
                Title = "#1 Two Sum",
                Description = "You are given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target. You may assume that each input would have exactly one solution, and you may not use the same element twice. You can return the answer in any order."
            },
            new()
            {
                Title = "#11 Container With Most Water",
                Description = "You are given an integer array height of length n. There are n vertical lines drawn such that the two endpoints of the i-th line are (i, 0) and (i, height[i]). Find two lines that together with the x-axis form a container, such that the container contains the most water. Return the maximum amount of water a container can store."
            },
            new()
            {
                Title = "#15 3Sum",
                Description = "Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0. Notice that the solution set must not contain duplicate triplets."
            },
            new()
            {
                Title = "#20 Valid Parentheses",
                Description = "Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid. An input string is valid if: 1. Open brackets must be closed by the same type of brackets. 2. Open brackets must be closed in the correct order. 3. Every close bracket has a corresponding open bracket of the same type."
            },
            new()
            {
                Title = "#21 Merge Two Sorted Lists",
                Description = "You are given the heads of two sorted linked lists list1 and list2. Merge the two lists into one sorted list. The list should be made by splicing together the nodes of the first two lists. Return the head of the merged linked list."
            },
            new()
            {
                Title = "#49 Group Anagrams",
                Description = "Given an array of strings strs, group the anagrams together. You can return the answer in any order."
            },
            new()
            {
                Title = "#53 Maximum Subarray",
                Description = "Given an integer array nums, find the with the largest sum, and return its sum."
            },
            new()
            {
                Title = "#70 Climbing Stairs",
                Description = "You are climbing a staircase. It takes n steps to reach the top. Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?"
            },
            new()
            {
                Title = "#121 Best Time to Buy and Sell Stock",
                Description = "You are given an array prices where prices[i] is the price of a given stock on the i-th day. You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock. Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0."
            },
            new()
            {
                Title = "#125 Valid Palindrome",
                Description = "A phrase is a palindrome if, after converting all uppercase letters into lowercase letters and removing all non-alphanumeric characters, it reads the same forward and backward. Alphanumeric characters include letters and numbers. Given a string s, return true if it is a palindrome, or false otherwise."
            },
            new()
            {
                Title = "#206 Reverse Linked List",
                Description = "Given the head of a singly linked list, reverse the list, and return the reversed list."
            },
            new()
            {
                Title = "#226 Invert Binary Tree",
                Description = "Given the root of a binary tree, invert the tree, and return its root."
            },
            new()
            {
                Title = "#238 Product of Array Except Self",
                Description = "Given an integer array nums, return an array answer such that answer[i] is equal to the product of all the elements of nums except nums[i]. The product of any prefix or suffix of nums is guaranteed to fit in a 32-bit integer. You must write an algorithm that runs in O(n) time and without using the division operation."
            },
            new()
            {
                Title = "#242 Valid Anagram",
                Description = "Given two strings s and t, return true if t is an of s, and false otherwise."
            },
            new()
            {
                Title = "#704 Binary Search",
                Description = "Given an array of integers nums which is sorted in ascending order, and an integer target, write a function to search target in nums. If target exists, then return its index. Otherwise, return -1. You must write an algorithm with O(log n) runtime complexity."
            }
        ];
}
