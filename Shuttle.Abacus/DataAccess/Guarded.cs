using System;

namespace Shuttle.Abacus.DataAccess
{
    public static class Guarded
    {
        public static T Entity<T>(T instance, Guid id) where T : class
        {
            if (instance == null)
            {
                throw Exceptions.MissingEntity(typeof(T).Name, id);
            }

            return instance;
        }

        public static void Entity<T>(object instance, Guid id) where T : class
        {
            if (instance != null)
            {
                return;
            }

            throw Exceptions.MissingEntity(typeof(T).Name, id);
        }
    }
}