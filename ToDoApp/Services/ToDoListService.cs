using System.Collections;
using System.Collections.Generic;
using ToDoApp.DataModel;

namespace ToDoApp.Services;

public class ToDoListService
{
    public IEnumerable<ToDoItem> AddItems() => new[]
    {
        new ToDoItem{ Name = "3 kupy" , isChecked = true},
        new ToDoItem{ Name = "umyź sie"},
        new ToDoItem{ Name = "silka z ksychem i shrapem"}
    };
}