using TodoWebApp.Models;

namespace TodoWebApp.Services;

public sealed class TodoStore
{
    private readonly Lock _lock = new();
    private readonly List<TodoItem> _items =
    [
        new() { Id = 1, Title = "Review the ASP.NET Core request pipeline" },
        new() { Id = 2, Title = "Build the Contacts API" },
        new() { Id = 3, Title = "Inspect the OpenAPI document" }
    ];
    private int _nextId = 4;

    public IReadOnlyList<TodoItem> GetAll()
    {
        lock (_lock)
        {
            return _items
                .Select(item => new TodoItem
                {
                    Id = item.Id,
                    Title = item.Title,
                    IsComplete = item.IsComplete
                })
                .ToList();
        }
    }

    public void Add(string title)
    {
        lock (_lock)
        {
            _items.Add(new TodoItem
            {
                Id = _nextId++,
                Title = title.Trim()
            });
        }
    }

    public void Toggle(int id)
    {
        lock (_lock)
        {
            var item = _items.FirstOrDefault(candidate => candidate.Id == id);
            if (item is not null)
            {
                item.IsComplete = !item.IsComplete;
            }
        }
    }

    public void Delete(int id)
    {
        lock (_lock)
        {
            _items.RemoveAll(item => item.Id == id);
        }
    }
}
