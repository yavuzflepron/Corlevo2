using Google.Cloud.Datastore.V1;

namespace DataLayer.Interfaces
{
    public interface IEntity
    {
        Key GetKey();

        Entity GetEntity();

        IEntity CreateFromEntity(Entity entity);

        ICollection<IEntity> CreateFromEntity(ICollection<Entity> entities);

        Task<bool> Upsert();
    }
}