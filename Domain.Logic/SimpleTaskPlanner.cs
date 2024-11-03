using Domain.Models;

namespace Domain.Logic;

public class SimpleTaskPlanner
{
    public WorkItem[] CreatePlan(WorkItem[] items)
    {
        return items
            .OrderByDescending(w => w.Priority) 
            .ThenBy(w => w.DueDate)
            .ThenBy(w => w.Title)
            .ToArray();
    }
}