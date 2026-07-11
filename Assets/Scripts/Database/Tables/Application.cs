using SQLite;
using System;

public class Application {
    [PrimaryKey, AutoIncrement] public int ApplicationID { get; set; }
    public int ClientID { get; set; }
    public int ServiceID { get; set; }
    public DateTime DateCreated { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
}