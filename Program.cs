using System.Text;
using Domain.Logic;
using Domain.Models;
using Domain.Models.Enums;

namespace TaskPlanner;

internal static class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        List<WorkItem> workItems = new List<WorkItem>();
        bool flag;

        Console.WriteLine("Згенерувати автоматично? (true/false):");
        while (!bool.TryParse(Console.ReadLine(), out flag))
        {
            Console.Write("Невірний формат. Введіть 'true' або 'false': ");
        }

        if (flag)
        {
            Console.WriteLine("Введіть кількість елементів:");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count))
            {
                Console.Write("Невірний формат числа. Будь ласка, спробуйте ще раз: ");
            }

            for (int i = 0; i < count; i++)
            {
                workItems.Add(new WorkItem());
            }
        }
        else
        {
            Console.WriteLine(
                "Введіть дані для створення завдань (WorkItem). Для завершення введення введіть порожній рядок.");

            while (true)
            {
                Console.Write("Введіть назву завдання (Title): ");
                string title = Console.ReadLine();
                if (string.IsNullOrEmpty(title)) break;

                Console.Write("Введіть опис завдання (Description): ");
                string description = Console.ReadLine();

                Console.Write("Введіть кількість днів від сьогодні для завершення (daysAfterToday): ");
                int daysAfterToday;
                while (!int.TryParse(Console.ReadLine(), out daysAfterToday))
                {
                    Console.Write("Невірний формат числа. Будь ласка, спробуйте ще раз: ");
                }


                Console.Write($"Введіть пріоритет {string.Join(", ", Enum.GetNames(typeof(Priority)))}: ");
                Priority priority;
                while (!Enum.TryParse(Console.ReadLine(), true, out priority))
                {
                    Console.Write("Невірне значення пріоритету. Будь ласка, введіть Low, Medium або High: ");
                }

                Console.Write($"Введіть складність {string.Join(", ", Enum.GetNames(typeof(Complexity)))}: ");
                Complexity complexity;
                while (!Enum.TryParse(Console.ReadLine(), true, out complexity))
                {
                    Console.Write("Невірне значення складності. Будь ласка, введіть Easy, Medium або Hard: ");
                }

                Console.Write("Чи виконане завдання? (true/false): ");
                bool isCompleted;
                while (!bool.TryParse(Console.ReadLine(), out isCompleted))
                {
                    Console.Write("Невірний формат. Введіть 'true' або 'false': ");
                }
                
                workItems.Add(new WorkItem(
                    daysAfterToday,
                    priority,
                    complexity,
                    title,
                    description,
                    isCompleted
                ));

                Console.WriteLine("Завдання додано!\n");
            }
        }
        SimpleTaskPlanner planner = new SimpleTaskPlanner();
        WorkItem[] sortedWorkItems = planner.CreatePlan(workItems.ToArray());
        
        Console.WriteLine("\nПочатковий масив:");
        foreach (var workItem in workItems)
        {
            Console.WriteLine(workItem);
        }
        
        Console.WriteLine("\nВідсортовані завдання:");
        foreach (var workItem in sortedWorkItems)
        {
            Console.WriteLine(workItem);
        }
    }
}