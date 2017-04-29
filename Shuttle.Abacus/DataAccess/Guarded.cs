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
    }
}