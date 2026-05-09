using System.Reflection;

namespace CSharpUnions
{
    internal static class Program
    {
        private union StringOrInt(string, int);

        private union StringOrIntOrBool(StringOrInt, bool);

        private static void Main(string[] args)
        {
            // Assigning to union var

            StringOrIntOrBool x;

            x = "TrumpMcDonaldz";

            Console.WriteLine(x.Value);

            x = 1;

            Console.WriteLine(x.Value);

            x = true;

            Console.WriteLine(x.Value);

            // Let's see what methods the union type has

            var methods = typeof(StringOrIntOrBool).GetMethods((BindingFlags) (-1));

            Console.WriteLine($"Methods of {nameof(StringOrInt)}:\n");

            foreach (var method in methods)
            {
                Console.WriteLine(method);
            }
        }
    }
}