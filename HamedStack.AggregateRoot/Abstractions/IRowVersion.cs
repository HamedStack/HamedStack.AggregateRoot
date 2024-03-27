// ReSharper disable CommentTypo
namespace HamedStack.TheAggregateRoot.Abstractions;

public interface IRowVersion
{
    byte[] RowVersion { get; set; }
}