using System.Reflection;

namespace CSharpUnions
{
    internal static class Program
    {
        public union StringOrInt(string, int);

        private static void Main(string[] args)
        {
            // Assigning to union var

            StringOrInt x;

            x = "TrumpMcDonaldz";

            Console.WriteLine(x.Value);

            x = 1;

            Console.WriteLine(x.Value);

            // Let's see what methods the union type has

            var methods = typeof(StringOrInt).GetMethods((BindingFlags) (-1));

            Console.WriteLine($"Methods of {nameof(StringOrInt)}:\n");

            foreach (var method in methods)
            {
                Console.WriteLine(method);
            }
        }
    }
}