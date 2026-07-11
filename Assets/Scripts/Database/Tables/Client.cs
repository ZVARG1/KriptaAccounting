using SQLite;
using System;

public class Client {
    [PrimaryKey, AutoIncrement] public int ClientID { get; set; }
    public string OrganizationName { get; set; }
    public string INN { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string ClientType { get; set; }
}