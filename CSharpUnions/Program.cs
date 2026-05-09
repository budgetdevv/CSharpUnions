using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSharpUnions
{
    internal static class Program
    {
        public union ForwardedString(string);

        public union StringOrInt(string, int);

        public struct StringOrIntContainer
        {
            [JsonConverter(typeof(ValueUnionConverter))]
            public StringOrInt Value { get; set; }
        }

        private class ValueUnionConverter: JsonConverter<StringOrInt>
        {
            public override StringOrInt Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                try
                {
                    return JsonSerializer.Deserialize<string>(ref reader);
                }

                catch { }
                try
                {
                    return JsonSerializer.Deserialize<int>(ref reader);
                }

                catch { }

                throw new Exception("Failed to deserialize union");
            }

            public override void Write(Utf8JsonWriter writer, StringOrInt value, JsonSerializerOptions options)
            {
                Type[] unionTypes = [ typeof(string), typeof(int) ];

                var type = value.Value.GetType();

                foreach (var unionType in unionTypes)
                {
                    if (type == unionType)
                    {
                        JsonSerializer.Serialize(writer, value, options);
                        return;
                    }
                }

                throw new Exception("Failed to serialize union");
            }
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

            var container = JsonSerializer.Deserialize<StringOrIntContainer>(JsonSerializer.Serialize(containerDyn));

            Console.WriteLine($"Deserialized value: {container.Value.Value}");
        }
    }
}