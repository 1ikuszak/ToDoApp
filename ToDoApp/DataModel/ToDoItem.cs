using Avalonia.Styling;

namespace ToDoApp.DataModel;

public class ToDoItem
{
    public string Name { get; set; } = string.Empty;
    public bool isChecked { get; set; }
}