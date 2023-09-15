using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoApp.DataModel;
using ToDoApp.Views;

namespace ToDoApp.ViewModels;

public class ToDoListViewModel:ViewModelBase
{
    public ToDoListViewModel(IEnumerable<ToDoItem> items)
    {
        ListItems = new ObservableCollection<ToDoItem>(items);
    }
    
    public ObservableCollection<ToDoItem> ListItems { get; }
}