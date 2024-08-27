using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DapperExtension
{
    public static class ReflectionReader
    {
        private static readonly Dictionary<Type, PropertyInfo[]> PropertyCache 
            = new Dictionary<Type, PropertyInfo[]>();

        public static Entity ReadEntityFromReader<Entity>(SqlDataReader reader) where Entity : class
        {
            var entity = Activator.CreateInstance<Entity>();
            var entityType = typeof(Entity);

            if (!PropertyCache.TryGetValue(entityType, out var properties))
            {
                properties = entityType.GetProperties();
                PropertyCache[entityType] = properties;
            }

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                var property = properties.FirstOrDefault(x => x.Name == name);

                if (property != null && !reader.IsDBNull(i))
                {
                    property.SetValue(entity, reader.GetValue(i));
                }
            }
            return entity;
        }

    }
}
