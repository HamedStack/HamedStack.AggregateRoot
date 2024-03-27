namespace HamedStack.TheAggregateRoot.Abstractions;

public interface IAudit
{
    string? CreatedBy { get; set; }
    DateTimeOffset CreatedOn { get; set; }
    string? ModifiedBy { get; set; }
    DateTimeOffset? ModifiedOn { get; set; }
}