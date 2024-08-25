using Microsoft.Data.SqlClient;

namespace AdoDapper
{
    public class ReflectionReader<Entity>
        where Entity : class
    {
        public async Task<Entity> ReadSingleAsync(SqlDataReader reader)
        {
            var entity = Activator.CreateInstance<Entity>();
            var properties = entity.GetType().GetProperties();

            while (await reader.ReadAsync())
            {
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    var property = properties.FirstOrDefault(x => x.Name == name);

                    if (property != null && !await reader.IsDBNullAsync(i))
                    {
                        property.SetValue(entity, reader.GetValue(i));
                    }
                }
            }
            return entity;
        }
        public async Task<List<Entity>> ReadListAsync(SqlDataReader reader)
        {
            var list = new List<Entity>();

            while (await reader.ReadAsync())
            {
                var entity = Activator.CreateInstance<Entity>();
                var properties = entity.GetType().GetProperties();

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    var property = properties.FirstOrDefault(x => x.Name == name);

                    if (property != null && !await reader.IsDBNullAsync(i))
                    {
                        property.SetValue(entity, reader.GetValue(i));
                    }
                }

                list.Add(entity);
            }
            return list;
        }
    }
}
