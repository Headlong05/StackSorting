using StackSorting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace StackSorting
{
    public class Item
    {
        public int value = 0;
        public Item next = null;
        public Item(int value = 0, Item next = null) { this.value = value; this.next = next; } // Time complexity: O(1)
    }
    public class Stack
    {
        private Item top = null;
        public bool IsEmpty() // Time complexity: O(1)
        {
            return top == null; // O(1)
        }
        public void Push(Item item) // Time complexity: O(1)
        {
            if (!IsEmpty()) item.next = top; // O(1)
            top = item; // O(1)
        }
        public Item Pop() // Time complexity: O(1)
        {
            if (IsEmpty()) throw new Exception("Stack empty"); // O(1)
            Item result = top; // O(1)
            top = top.next; // O(1)

            result.next = null; // O(1)
            return result; // O(1)
        }
        public void Print() // Time complexity: O(n)
        {
            Item current = top; // O(1)
            while (current != null) // O(n)
            {
                Console.Write(current.value + " "); // O(1)
                current = current.next; // O(1)
            }
            Console.WriteLine(); // O(1)
        }
        public int Get(int pos, Stack helperStack) // Time complexity: O(n)
        {
            if (IsEmpty())
            {
                throw new Exception("Stack empty");
            }
            for (int i = 0; i < pos; i++) // O(n)
            {
                helperStack.Push(Pop()); // O(1)
            }
            int result = top.value; // O(1)
            while (!helperStack.IsEmpty()) // O(n)
            {
                Push(helperStack.Pop()); // O(1)
            }
            return result; // O(1)
        }
        public void Set(int pos, int newvalue, Stack helperStack) // Time complexity: O(n)
        {
            if (IsEmpty())
            {
                throw new Exception("Stack empty");
            }
            for (int i = 0; i < pos; i++) // O(n)
            {
                helperStack.Push(Pop()); // O(1)
            }
            top.value = newvalue; // O(1)
            while (!helperStack.IsEmpty()) // O(n)
            {
                Push(helperStack.Pop()); // O(1)
            }
        }
        public int this[int index] // Time complexity: O(n)
        {
            get => Get(index, new Stack());
            set => Set(index, value, new Stack());
        }
        public int SortStack(IComparer<Item> comparer = null)
        {
            int N_op = 0; // Operation counter
            Stack helperStack = new Stack();
            while (!IsEmpty()) // O(n)
            {
                Item element = Pop(); // O(1), increment N_op by 1
                N_op++;
                while (!helperStack.IsEmpty() && (comparer?.Compare(helperStack.top, element) > 0 || (comparer == null && helperStack.top.value > element.value))) // O(n)
                {
                    Push(helperStack.Pop()); // O(1), increment N_op by 1
                    N_op++;
                }
                helperStack.Push(element); // O(1), increment N_op by 1
                N_op++;
            }
            while (!helperStack.IsEmpty()) // O(n)
            {
                Push(helperStack.Pop()); // O(1), increment N_op by 1
                N_op++;
            }
            return N_op; // Return the number of basic operations
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        int[] nValues = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

        for (int i = 0; i < nValues.Length; i++)
        {
            int n = nValues[i];
            var stack = new StackSorting.Stack();
            HashSet<int> uniqueValues = new HashSet<int>();
            int stackSize = 0; // Variable to track the stack size
            int N_op = 0; // Variable to count the number of basic operations
            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine($"{i + 1}.");

            // Clear the stack before each new sort
            while (!stack.IsEmpty())
            {
                stack.Pop();
                N_op++; // Count the Pop() operation
            }
            stackSize = 0;

            stopwatch.Start();
            // Fill the stack with random unique values
            while (uniqueValues.Count < n)
            {
                int randomValue = new Random().Next(0, n * 2);
                if (!uniqueValues.Contains(randomValue))
                {
                    stack.Push(new Item(randomValue));
                    N_op += 3; // Count the operations of creating Item, Push(), and adding to HashSet
                    uniqueValues.Add(randomValue);
                    stackSize++; // Increase the stack size when adding a new element
                }
            }

            // Sort in ascending order
            N_op += stack.SortStack(); // Count the operations in the SortStack() method
            Console.Write("Stack after sorting: ");
            stack.Print();
            N_op += 2; // Count the operations of calling Print() and Console.WriteLine()

            // Sort in descending order
            N_op += stack.SortStack(Comparer<Item>.Create((x, y) => y.value.CompareTo(x.value))); // Count the operations in the SortStack() method
            Console.Write("Stack after sorting in descending order: ");
            stack.Print();
            N_op += 2; // Count the operations of calling Print() and Console.WriteLine()

            stopwatch.Stop();

            Console.WriteLine($"Stack size: {stackSize}");
            Console.WriteLine($"Sorting time: {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine($"Number of basic operations: {N_op}");
            Console.WriteLine();
        }

        Console.ReadLine();
    }
}





