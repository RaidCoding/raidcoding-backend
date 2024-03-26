using RaidCoding.Data;

namespace RaidCoding.Logic.Exceptions;

public class ValidationException : LogicalException
{
    public ValidationException(EntityName name, IDictionary<string, string[]> errors)
        : base(
            name,
            "Validation",
            "Validation Problem.", //
            "One or more validation problems occured."
        )
    {
        Errors = errors;
    }
    
    public ValidationException(string name, IDictionary<string, string[]> errors)
        : base(
            name,
            "Validation",
            "Validation Problem.",
            "One or more validation problems occured."
        )
    {
        Errors = errors;
    }
    
    public IDictionary<string, string[]> Errors { get;}
}