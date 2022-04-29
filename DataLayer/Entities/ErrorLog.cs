using DataLayer.Interfaces;
using DataLayer.Tools;
using Google.Cloud.Datastore.V1;

namespace DataLayer.Entities
{
    public class ErrorLog : IEntity
    {
        public int Id { get; set; }
        public DateTime LogTime { get; set; } = DateTime.Now;
        public DateTime LogTimeUTC { get; set; } = DateTime.UtcNow;
        public string? Message { get; set; }

        public ErrorLog()
        {

        }

        public ErrorLog(string message)
        {
            Message = message;
        }

        public Key GetKey() => DataTools.GetKey<ErrorLog>(Id);

        public Entity GetEntity()
        {
            Entity entity = new() { Key = GetKey() };
            entity.Properties.Add("LogTime", new Value() { TimestampValue = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(LogTime.ToUniversalTime()) });
            entity.Properties.Add("LogTimeUTC", new Value() { TimestampValue = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(LogTimeUTC) });
            entity.Properties.Add("Message", new Value() { StringValue = Message });
            return entity;
        }

        public IEntity CreateFromEntity(Entity entity)
        {
            return new ErrorLog()
            {
                Id = Convert.ToInt32(entity.Key.Path.First().Id),
                LogTime = entity.Properties["LogTime"].TimestampValue.ToDateTime().ToLocalTime(),
                LogTimeUTC = entity.Properties["LogTimeUTC"].TimestampValue.ToDateTime(),
                Message = entity.Properties["Message"].StringValue
            };
        }

        public ICollection<IEntity> CreateFromEntity(ICollection<Entity> entities) => entities.Select(x => CreateFromEntity(x)).ToList();

        public async Task<bool> Upsert()
        {
            await DataTools.GetDatastoreDb().UpsertAsync(GetEntity());
            return true;
        }

        public async Task Delete() => await DataTools.GetDatastoreDb().DeleteAsync(GetKey());
    }
}