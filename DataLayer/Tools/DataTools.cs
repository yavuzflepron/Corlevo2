using DataLayer.Interfaces;
using Google.Cloud.Datastore.V1;

namespace DataLayer.Tools
{
    public partial class DataTools
    {
        private static readonly string ProjectId = "corlevo-test";

        internal static DatastoreDb GetDatastoreDb()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS")))
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Directory.GetFiles(Environment.CurrentDirectory, "datastore.json", SearchOption.AllDirectories).First());
            return DatastoreDb.Create(ProjectId);
        }

        internal static Key GetKey<T>(int id) where T : IEntity => GetDatastoreDb().CreateKeyFactory(typeof(T).Name).CreateKey(id);

        internal static Key GetKey<T>(Guid? id = null) where T : IEntity => GetDatastoreDb().CreateKeyFactory(typeof(T).Name).CreateKey((id ?? Guid.NewGuid()).ToString());
    }

    public partial class DataTools
    {
        public static async Task<bool> Delete<T>(Guid id) where T : IEntity
        {
            await GetDatastoreDb().DeleteAsync(GetKey<T>(id));
            return true;
        }
    }
}