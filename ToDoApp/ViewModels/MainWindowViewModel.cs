using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
using ToDoApp.DataModel;
using ToDoApp.Services;

namespace ToDoApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;
    private readonly ToDoItemRepository _toDoItemRepository;
    
    public ToDoListViewModel ToDoList { get; }
    
    public MainWindowViewModel()
    {
        // Database Initialization
        string connectionString = "Data Source=D:\\code\\c#\\ToDoApp\\ToDoApp\\myDB.db;Version=3;";
        DbConnection dbConnection = new DbConnection(connectionString);
        // create table if it has not been created
        dbConnection.CreateTasksTable();
        
        // initialize object _toDoItemRepository with the access to DB
        _toDoItemRepository = new ToDoItemRepository(dbConnection);
        var service = new ToDoListService(_toDoItemRepository);
        // get ToDoItems data and it to ToDoListViewModel
        ToDoList = new ToDoListViewModel(service.AddItems());
        // set current view to ToDoList
        _contentViewModel = ToDoList;
    }
    
    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public void AddItem()
    {
        AddItemViewModel addItemViewModel = new();
        
        Observable.Merge(
                addItemViewModel.OkCommand,
                addItemViewModel.CancelCommand.Select(_ => (ToDoItem?)null))
            .Take(1)
            .Subscribe(newItem =>
            {
                if (newItem != null)
                {
                    ToDoList.ListItems.Add(newItem );
                    _toDoItemRepository.AddTask(newItem);
                }
                ContentViewModel = ToDoList;
            });

        ContentViewModel = addItemViewModel;
    }
    
    public void DeleteItems()
    {
        var checkedItems = ToDoList.ListItems.Where(item => item.isChecked).ToList();
        
        foreach (var item in checkedItems)
        {
            try
            {
                _toDoItemRepository.DeleteTask(item);
                ToDoList.ListItems.Remove(item);
            }
            catch (Exception ex)
            {
                // Log the exception, and consider adding more detailed logging
                Console.WriteLine($"Error deleting item with ID {item.TaskID}: {ex.Message}");
            }
        }
    }
}