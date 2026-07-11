using System;


public class DatabaseQuery
{
    public string Name;

    public string SQL;

    public Type ResultType;


    public DatabaseQuery(
        string name,
        string sql,
        Type resultType)
    {
        Name = name;
        SQL = sql;
        ResultType = resultType;
    }
}