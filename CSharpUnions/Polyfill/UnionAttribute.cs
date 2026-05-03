// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    // https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview3/csharp.md#union-type-support

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public sealed class UnionAttribute: Attribute;

    public interface IUnion
    {
        object? Value { get; }
    }
}