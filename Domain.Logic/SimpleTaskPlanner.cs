using DataAccess.Abstractions;
using Domain.Models;

namespace Domain.Logic;

public class SimpleTaskPlanner
{
    private readonly IWorkItemsRepository _repository;

    public SimpleTaskPlanner(IWorkItemsRepository iWorkItemsRepository)
    {
        _repository = iWorkItemsRepository;
    }
    public WorkItem[] CreatePlan()
    {
        return _repository.GetAll().Where(value => true)
            .OrderByDescending(w => w.Priority) 
            .ThenBy(w => w.DueDate)
            .ThenBy(w => w.Title)
            .ToArray();
    }
}