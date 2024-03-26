using RaidCoding.Data;

namespace RaidCoding.Logic.Exceptions;

public class LogicalException : Exception
{
    public LogicalException(EntityName entityName, string error, string title, string message) : base(message)
    {
        EntityName = entityName.ToString();
        Error = error;
        Title = title;
    }


    public LogicalException(string entityName, string error, string title, string message) : base(message)
    {
        EntityName = entityName;
        Error = error;
        Title = title;
    }

    public string EntityName { get; }
    public string Error { get; }
    public string Title { get; }
}