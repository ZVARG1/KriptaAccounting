using SQLite;
using System;

public class Employee {
    [PrimaryKey, AutoIncrement] public int EmployeeID { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}