using ReactiveUI;
using System.Reactive.Linq;
using ReactiveUI;
using System;
using ToDoApp.DataModel;
using ToDoApp.Services;

namespace ToDoApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;
    
    public MainWindowViewModel()
    {
        var service = new ToDoListService();
        ToDoList = new ToDoListViewModel(service.AddItems());
        _contentViewModel = ToDoList;
    }
    
    public ToDoListViewModel ToDoList { get; }

    
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
                }
                ContentViewModel = ToDoList;
            });

        ContentViewModel = addItemViewModel;

    }
}