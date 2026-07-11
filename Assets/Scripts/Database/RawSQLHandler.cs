using System.Collections.Generic;
using SQLite;


public class RawSQLHandler
{
    private SQLiteConnection _connection;


    public RawSQLHandler(SQLiteConnection connection)
    {
        _connection = connection;
    }


    public List<T> ExecuteQuery<T>(string sql) where T : new()
    {
        return _connection.Query<T>(sql);
    }
}