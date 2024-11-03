using System;
using Domain.Models.Enums;

namespace Domain.Models;

public class WorkItem
{
    public WorkItem(
        int daysAfterToday,
        Priority priority,
        Complexity complexity,
        string title,
        string description,
        bool isCompleted
    )
    {
        DueDate = DateTime.Now.Date.AddDays(daysAfterToday);
        Priority = priority;
        Complexity = complexity;
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
    }


    public WorkItem()
    {
        Random random = new Random();
        Array priorityValues = Enum.GetValues(typeof(Priority));
        Array complexityValues = Enum.GetValues(typeof(Complexity));
        DueDate = DateTime.Now.Date.AddDays(random.Next(3, 10));
        Priority = (Priority)priorityValues.GetValue(random.Next(priorityValues.Length))!;
        Complexity = (Complexity)complexityValues.GetValue(random.Next(complexityValues.Length))!;
        Title = "Default title";
        Description = "Default description";
        IsCompleted = random.Next(1) == 1;
    }

    public DateTime CreationDate = DateTime.Now.Date;
    public DateTime DueDate { get; }
    public Priority Priority { get; }
    public Complexity Complexity;
    public string Title { get; }
    public string Description;
    public bool IsCompleted;

    // Override ToString method
    public override string ToString()
    {
        string priority = Priority.ToString().ToLower();

        if (Priority.Equals(Priority.Urgent) || Priority.Equals(Priority.High))
            priority = Priority.ToString().ToUpper();

        return $"{Title}: due {DueDate:dd.MM.yyyy}, {priority}";
    }
}