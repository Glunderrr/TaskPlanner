using DataAccess.Abstractions;
using Domain.Models;
using Newtonsoft.Json;

namespace DataAccess;

public class FileWorkItemsRepository : IWorkItemsRepository
{
    private const string FilePath = "D:\\Programing\\С#\\TaskPlanner\\DataAccess\\work-items.json";
    private Dictionary<Guid, WorkItem> _workItems = new();

    public FileWorkItemsRepository()
    {
        using StreamReader r = new StreamReader(FilePath);
        string json = r.ReadToEnd();
        WorkItem[]? workItems = JsonConvert.DeserializeObject<WorkItem[]>(json);
        if (workItems != null)
            foreach (var item in workItems)
            {
                _workItems.Add(item.Id, item);
            }
    }


    public Guid Add(WorkItem workItem)
    {
        _workItems.Add(workItem.Id, workItem);
        return workItem.Id;
    }

    public WorkItem Get(Guid id)
    {
        return _workItems[id];
    }

    public WorkItem[] GetAll()
    {
        return _workItems.Values.ToArray();
    }

    public bool Update(WorkItem workItem)
    {
        _workItems[workItem.Id] = workItem;
        return true;
    }

    public bool Remove(Guid id)
    {
        return _workItems.Remove(id);
    }

    public void SaveChanges()
    {
        var values = _workItems.Values.ToArray();
        string json = JsonConvert.SerializeObject(values);
        File.WriteAllText(FilePath, json);
    }
}