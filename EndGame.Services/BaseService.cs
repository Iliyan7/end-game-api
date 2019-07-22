using EndGame.DataAccess;
using System;
using System.Linq;
using System.Reflection;

namespace EndGame.Services
{
    public abstract class BaseService
    {
        protected readonly EndGameContext _db;

        public BaseService(EndGameContext db)
        {
            this._db = db;
        }

        protected string[] GetFilledProperties<T>(T obj)
        {
            bool PropertyHasValue(PropertyInfo prop)
            {
                var value = prop.GetValue(obj);
                var type = prop.PropertyType;

                if (IsOfNullableType(type) && value != null)
                    return true;

                if (type == typeof(string) && !string.IsNullOrEmpty((string)value))
                    return true;

                if (type.IsPrimitive || type.IsEnum)
                {
                    var defaultValue = Activator.CreateInstance(type);

                    if (!object.Equals(value, defaultValue))
                        return true;
                }

                return false;
            }

            return obj.GetType()
               .GetProperties()
               .Where(PropertyHasValue)
               .Select(p => p.Name)
               .ToArray();
        }

        protected bool IsOfNullableType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        protected IQueryable<T> CreatePaginatedResult<T>(IQueryable<T> query, int pageIndex, int pageSize)
        {
            return query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }

        protected void UpdateEntry<T>(T entityToUpdate, string[] filledPropertiesToUpdate)
        {
            _db.Attach(entityToUpdate);

            foreach (var property in filledPropertiesToUpdate)
            {
                _db.Entry(entityToUpdate).Property(property).IsModified = true;
            }
        }
    }
}
