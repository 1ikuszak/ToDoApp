using System.Collections;
using System.Collections.Generic;
using ToDoApp.DataModel;

namespace ToDoApp.Services;

public class ToDoListService
{
    private ToDoItemRepository _repository;
    
    public ToDoListService(ToDoItemRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<ToDoItem> AddItems() => _repository.GetAllTasks();
}