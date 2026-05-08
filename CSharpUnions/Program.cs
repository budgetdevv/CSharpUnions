using System.Reflection;
using System.Text.Json;

namespace CSharpUnions
{
    internal static class Program
    {
        public union ForwardedString(string);

        public union StringOrInt(string, int);

        public struct StringOrIntContainer
        {
            public StringOrInt Value { get; set; }
        }

        private static void Main(string[] args)
        {
            // Assigning to union var

            StringOrInt x;

            x = "TrumpMcDonaldz";

            Console.WriteLine(x.Value);

            x = 1;

            Console.WriteLine(x.Value);

            // Let's see what methods the union type has

            var methods = typeof(ForwardedString).GetMethods((BindingFlags) (-1));

            Console.WriteLine($"Methods of {nameof(ForwardedString)}:\n");

            foreach (var method in methods)
            {
                Console.WriteLine(method);
            }

            // Can we serialize / deserialize?

            var containerDyn = new
            {
                Value = "TrumpMcDonaldz"
            };

            // This throws, there's pending support for deserializing unions: https://github.com/dotnet/runtime/issues/127299
            var container = JsonSerializer.Deserialize<StringOrIntContainer>(JsonSerializer.Serialize(containerDyn));

            Console.WriteLine($"Deserialized value: {container.Value}");
        }
    }
}