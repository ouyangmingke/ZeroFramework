using System.Diagnostics.CodeAnalysis;

namespace Zero.Core.Modularity;

public interface IDependedTypesProvider
{
    Type[] GetDependedTypes();
}
