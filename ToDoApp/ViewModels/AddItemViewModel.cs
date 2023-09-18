using System.Reactive;
using ReactiveUI;
using ToDoApp.DataModel;

namespace ToDoApp.ViewModels;

public class AddItemViewModel:ViewModelBase
{
    private string _name = string.Empty;
    
    public ReactiveCommand<Unit, ToDoItem> OkCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public AddItemViewModel()
    {
        var isValidObservable = this.WhenAnyValue
        (
            x => x.Name,
            x=> !string.IsNullOrWhiteSpace(x)
        );
        
        OkCommand = ReactiveCommand.Create(
            () => new ToDoItem { Name = Name }, isValidObservable);
        CancelCommand = ReactiveCommand.Create((() => {}));
    }
    
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
}