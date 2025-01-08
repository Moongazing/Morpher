using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Morpher.Kernel;

/// <summary>
/// Central manager for handling type mappings and executing transformations.
/// </summary>
public class MorpherManager
{
    private static readonly ConcurrentDictionary<(Type Source, Type Destination), Delegate> Mappings = new();

    /// <summary>
    /// Registers a mapping between two types.
    /// </summary>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Destination type.</typeparam>
    /// <param name="mappingExpression">Mapping logic as a lambda expression.</param>
    public static void RegisterMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression)
    {
        if (mappingExpression == null)
            throw new ArgumentNullException(nameof(mappingExpression));

        Mappings[(typeof(TSource), typeof(TDestination))] = mappingExpression.Compile();
    }

    /// <summary>
    /// Executes a mapping for the given source object.
    /// </summary>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Destination type.</typeparam>
    /// <param name="source">Source object to map from.</param>
    /// <returns>Mapped destination object.</returns>
    public static TDestination Map<TSource, TDestination>(TSource source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        if (Mappings.TryGetValue((typeof(TSource), typeof(TDestination)), out var mapping))
        {
            var func = (Func<TSource, TDestination>)mapping;
            return func(source);
        }

        return AutoMap<TSource, TDestination>(source);
    }

    /// <summary>
    /// Automatically maps two types using reflection.
    /// </summary>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Destination type.</typeparam>
    /// <param name="source">Source object to map from.</param>
    /// <returns>Mapped destination object.</returns>
    private static TDestination AutoMap<TSource, TDestination>(TSource source)
    {
        var destination = Activator.CreateInstance<TDestination>();
        var sourceType = typeof(TSource);
        var destinationType = typeof(TDestination);

        foreach (var sourceProp in sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var destinationProp = destinationType.GetProperty(sourceProp.Name, BindingFlags.Public | BindingFlags.Instance);

            if (destinationProp != null && destinationProp.CanWrite)
            {
                destinationProp.SetValue(destination, sourceProp.GetValue(source));
            }
        }

        return destination;
    }
}
