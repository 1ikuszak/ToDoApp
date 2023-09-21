using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using ToDoApp.Services;

namespace ToDoApp.DataModel;

public class ToDoItem
{
    public int TaskID { get; set; }
    public string Name { get; set; }
    public bool isChecked { get; set; }
}
