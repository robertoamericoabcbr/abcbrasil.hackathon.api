using System.Collections.Generic;

namespace ABCBrasil.Hackthon.Api.Domain.Mappings
{
    public static class AutoMapperExtensions
    {
        public static T MapTo<T>(this object value)
        {
            return MappingProfile.Mapper.Map<T>(value);
        }

        public static IEnumerable<T> IEnumerableTo<T>(this object value)
        {
            return MappingProfile.Mapper.Map<IEnumerable<T>>(value);
        }

        public static IList<T> IListTo<T>(this object value)
        {
            return MappingProfile.Mapper.Map<IList<T>>(value);
        }

        public static List<T> ListTo<T>(this object value)
        {
            return MappingProfile.Mapper.Map<List<T>>(value);
        }

        public static ICollection<T> ICollectionTo<T>(this object value)
        {
            return MappingProfile.Mapper.Map<ICollection<T>>(value);
        }

        #region Para Merges de Objetos

        public static void MapMergesWhithRefTo<TDestination, TResource>(ref TDestination destination, params TResource[] sources)
        {
            foreach (TResource source in sources)
                MappingProfile.Mapper.Map(source, destination);
        }

        public static void MapMergesWhithOutTo<TDestination, TResource>(out TDestination destinationOut, params TResource[] sources) where TDestination : class, new()
        {
            TDestination destination = new();
            foreach (TResource source in sources)
                MappingProfile.Mapper.Map(source, destination);
            destinationOut = destination;
        }

        public static void MapMergeWhithRefTo<TDestination, TResource>(ref TDestination destination, TResource source)
        {
            MappingProfile.Mapper.Map(source, destination);
        }

        public static void MapMergeWhithOutTo<TDestination, TResource>(out TDestination destinationOut, TResource source) where TDestination : class, new()
        {
            TDestination destination = new();
            MappingProfile.Mapper.Map(source, destination);
            destinationOut = destination;
        }

        #endregion Para Merges de Objetos
    }
}