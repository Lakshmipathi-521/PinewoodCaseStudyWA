namespace DataAccess.CRMSystem;

public interface IRepository<EntityType, EntityKey> : IDisposable
{
    IEnumerable<EntityType> GetEntities();
    EntityType GetEntityByKey(EntityKey entityKey);
    bool AddEntity(EntityType entityType);
    EntityType UpdateEntity(EntityType entityType);
    bool DeleteEntity(EntityType entityType);
}
