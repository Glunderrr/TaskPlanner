using System.Text;
using DataAccess;
using Domain.Logic;
using Domain.Models;
using Domain.Models.Enums;

namespace TaskPlanner
{
    internal static class Program
    {
        static void Main()
        {
            FileWorkItemsRepository repository = new();

            Console.OutputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nОберіть операцію:");
                Console.WriteLine("[A]dd work item");
                Console.WriteLine("[B]uild a plan");
                Console.WriteLine("[M]ark work item as completed");
                Console.WriteLine("[R]emove a work item");
                Console.WriteLine("[Q]uit the app");
                Console.Write("Ваш вибір: ");

                var input = Console.ReadLine()?.ToUpper();
                switch (input)
                {
                    case "A":
                        AddWorkItem(repository);
                        break;
                    case "B":
                        BuildPlan(repository);
                        break;
                    case "M":
                        MarkAsCompleted(repository);
                        break;
                    case "R":
                        RemoveWorkItem(repository);
                        break;
                    case "Q":
                        repository.SaveChanges();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        private static void AddWorkItem(FileWorkItemsRepository repository )
        {
            bool flag;

            Console.WriteLine("Хочете згенерувати елемент автоматично?");
            while (!bool.TryParse(Console.ReadLine(), out flag))
            {
                Console.Write("Невірний формат. Введіть 'true' або 'false': ");
            }

            if (flag)
            {
                Console.Write("Введіть кількість елементів, які хочете згенерувати: ");
                int count;
                while (!int.TryParse(Console.ReadLine(), out count))
                {
                    Console.Write("Невірний формат числа. Будь ласка, спробуйте ще раз: ");
                }

                for (int i = 0; i < count; i++)
                {
                    var newItem = new WorkItem();
                    Console.Write("Створенно новий елемент:\n" + newItem + "\n");
                    repository.Add(newItem);
                }

                Console.WriteLine("Усі завдання додано!\n");
            }
            else
            {
                Console.WriteLine("Введіть назву завдання (Title): ");
                string title = Console.ReadLine();

                Console.WriteLine("Введіть опис завдання (Description): ");
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
                    Console.Write(
                        $"Невірне значення пріоритету. Будь ласка, введіть {string.Join(", ", Enum.GetNames(typeof(Priority)))}: ");
                }

                Console.Write($"Введіть складність {string.Join(", ", Enum.GetNames(typeof(Complexity)))}: ");
                Complexity complexity;
                while (!Enum.TryParse(Console.ReadLine(), true, out complexity))
                {
                    Console.Write(
                        $"Невірне значення складності. Будь ласка, введіть  {string.Join(", ", Enum.GetNames(typeof(Complexity)))}: ");
                }

                Console.Write("Чи виконане завдання? (true/false): ");
                bool isCompleted;
                while (!bool.TryParse(Console.ReadLine(), out isCompleted))
                {
                    Console.Write("Невірний формат. Введіть 'true' або 'false': ");
                }

                repository.Add(new WorkItem(
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

        private static void BuildPlan(FileWorkItemsRepository repository)
        {
            SimpleTaskPlanner planner = new SimpleTaskPlanner(repository);
            WorkItem[] sortedWorkItems = planner.CreatePlan();
            Console.WriteLine(sortedWorkItems.Length);
            Console.WriteLine("\nВідсортовані завдання:");
            foreach (var workItem in sortedWorkItems)
            {
                Console.WriteLine(workItem);
            }
        }

        private static void MarkAsCompleted(FileWorkItemsRepository repository)
        {
            Console.Write("Введіть назву завдання для позначення як виконаного: ");
            string title = Console.ReadLine();
            WorkItem item = new WorkItem();
            foreach (var element in repository.GetAll())
            {
                if (element.Title.Equals(title))
                    item = element;
            }

            Console.WriteLine(repository.Update(item)
                ? $"Завдання '{title}' позначено як виконане."
                : "Завдання не знайдено.");
        }

        private static void RemoveWorkItem(FileWorkItemsRepository repository)
        {
            Console.Write("Введіть назву завдання для видалення: ");
            string title = Console.ReadLine();
            WorkItem item = new WorkItem();
            foreach (var element in repository.GetAll())
            {
                if (element.Title.Equals(title))
                    item = element;
            }

            Console.WriteLine(repository.Remove(item.Id) ? $"Завдання '{title}' видалено." : "Завдання не знайдено.");
        }
    }
}