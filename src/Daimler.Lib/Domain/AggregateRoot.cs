namespace Daimler.Lib.Domain
{
    // tek domain oldugu icin domainevent implementasyonu yapilmadi.
    public interface IAggregateRoot : IEntity
    {
    }

    public abstract class AggregateRoot : Entity, IAggregateRoot
    {

    }
}