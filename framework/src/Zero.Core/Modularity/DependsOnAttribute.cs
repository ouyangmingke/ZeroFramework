using JetBrains.Annotations;

using System.Diagnostics.CodeAnalysis;

namespace Zero.Core.Modularity;

/// <summary>
/// Used to define dependencies of a type.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependsOnAttribute : Attribute, IDependedTypesProvider
{
    [NotNull]
    private Type[] DependedTypes { get; }

    public DependsOnAttribute(params Type[] dependedTypes)
    {
        DependedTypes = dependedTypes ?? new Type[0];
    }

    public virtual Type[] GetDependedTypes()
    {
        return DependedTypes;
    }
}
