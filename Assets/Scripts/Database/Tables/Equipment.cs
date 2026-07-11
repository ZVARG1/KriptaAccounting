using SQLite;
using System;

public class Equipment {
    [PrimaryKey, AutoIncrement] public int EquipmentID { get; set; }
    public int ClientID { get; set; }
    public string Type { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public string Condition { get; set; }
}