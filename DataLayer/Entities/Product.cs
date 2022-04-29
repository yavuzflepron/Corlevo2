using DataLayer.Interfaces;
using DataLayer.Tools;
using Google.Cloud.Datastore.V1;

namespace DataLayer.Entities
{
    public partial class Product : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public double Price { get; set; }

        public Key GetKey() => DataTools.GetKey<Product>(Id);

        public Entity GetEntity()
        {
            Entity entity = new() { Key = GetKey() };
            entity.Properties.Add("Name", new Value() { StringValue = Name });
            entity.Properties.Add("Price", new Value() { DoubleValue = Price });
            return entity;
        }

        public IEntity CreateFromEntity(Entity entity)
        {
            return new Product()
            {
                Id = Guid.Parse(entity.Key.Path.First().Name),
                Name = entity.Properties["Name"].StringValue,
                Price = entity.Properties["Price"].DoubleValue
            };
        }

        public ICollection<IEntity> CreateFromEntity(ICollection<Entity> entities) => entities.Select(x => CreateFromEntity(x)).ToList();

        public async Task<bool> Upsert()
        {
            await DataTools.GetDatastoreDb().UpsertAsync(GetEntity());
            return true;
        }
    }

    public partial class Product
    {
        public static async Task<List<Product>?> GetList(string searchText)
        {
            Query query = new Query("Product");
            var rawData = await DataTools.GetDatastoreDb().RunQueryAsync(query);
            var data = rawData != null && rawData.Entities != null && rawData.Entities.Count > 0 ? rawData.Entities.Select(x => (Product)new Product().CreateFromEntity(x)).ToList() : null;
            if (data != null && !string.IsNullOrEmpty(searchText))
                data = data?.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList();
            return data;
        }

        public static async Task<Product?> GetById(string id) => await GetById(Guid.Parse(id));

        public static async Task<Product?> GetById(Guid id)
        {
            var data = await DataTools.GetDatastoreDb().LookupAsync(DataTools.GetKey<Product>(id));
            return data != null ? (Product)new Product().CreateFromEntity(data) : default;
        }
    }
}