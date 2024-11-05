using System;
using Domain.Models.Enums;
using Newtonsoft.Json;

namespace Domain.Models;

public class WorkItem : ICloneable
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

    public Guid Id = Guid.NewGuid();
    public DateTime CreationDate = DateTime.Now.Date;
    public DateTime DueDate;
    public Priority Priority;
    public Complexity Complexity;
    public string Title;
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

    public object Clone()
    {
        return new WorkItem
        {
            CreationDate = CreationDate,
            DueDate = DueDate,
            Priority = Priority,
            Complexity = Complexity,
            Title = Title,
            Description = Description,
            IsCompleted = IsCompleted
        };
    }
}