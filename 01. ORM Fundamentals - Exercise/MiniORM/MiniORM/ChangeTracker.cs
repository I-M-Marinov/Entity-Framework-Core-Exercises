using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;

namespace MiniORM
{
    public class ChangeTracker<T>
        where T : class, new()
    {
        private readonly List<T> _allEntities;
        private readonly List<T> _added;
        private readonly List<T> _removed;

        public ChangeTracker(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            this._added = new List<T>();
            this._removed = new List<T>();
            this._allEntities = CloneEntities(entities);

        }

        public IReadOnlyCollection<T> AllEntities => this._allEntities.AsReadOnly();
        public IReadOnlyCollection<T> Added => this._added.AsReadOnly();
        public IReadOnlyCollection<T> Removed => this._removed.AsReadOnly();

        private static List<T> CloneEntities(IEnumerable<T> entities)
        {

            var copiedEntities = new List<T>();

            var propertiesToClone = typeof(T).GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();



            foreach (var entity in entities)
            {
                var clonedEntity = Activator.CreateInstance<T>();

                foreach (var property in propertiesToClone)
                {
                    var value = property.GetValue(entity);
                    property.SetValue(clonedEntity, value);
                }

                copiedEntities.Add(clonedEntity);
            }

            return copiedEntities;
        }

        private void Add(T item)
        {
            _added.Add(item);
        }

        private void Remove(T item)
        {
            _removed.Remove(item);
        }

        public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet)
        {
            var modifiedEntities = new List<T>();
            var primaryKeys = typeof(T).GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>()).ToArray();

            foreach (var proxyEntity in AllEntities)
            {

                var primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray();
                var entity = dbSet.Entities.Single(e => GetPrimaryKeyValues(primaryKeys, e))
                    .SequenceEqual(primaryKeyValues);

                var isModified = IsModified(proxyEntity, entity);

                if (isModified)
                {
                    modifiedEntities.Add(entity);
                }

            }

            return modifiedEntities;
        }

        private static bool IsModified(T entity, T proxyEntity)
        {
            var monitoredProperties = typeof(T).GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType));

            var modifiedProperties = monitoredProperties
                .Where(pi => !Equals(pi.GetValue(entity), pi.GetValue(proxyEntity))).ToArray();

            var isModified = modifiedProperties.Any();
            return isModified;
        }

        private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, T entity)
        {
            return primaryKeys.Select(pk => pk.GetValue(entity));
        }
    }
}
        