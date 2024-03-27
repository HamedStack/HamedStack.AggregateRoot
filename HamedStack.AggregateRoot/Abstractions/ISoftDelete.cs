namespace HamedStack.TheAggregateRoot.Abstractions;

public interface ISoftDelete
{
    string? DeletedBy { get; set; }
    DateTimeOffset DeletedOn { get; set; }
    bool IsDeleted { get; set; }
}