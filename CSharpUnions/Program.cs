using System.Reflection;

namespace CSharpUnions
{
    internal static class Program
    {
        public union ForwardedString(string);

        public union StringOrInt(string, int);

        private static void Main(string[] args)
        {
            StringOrInt x;

            x = "TrumpMcDonaldz";

            Console.WriteLine(x.Value);

            x = 1;

            Console.WriteLine(x.Value);

            var methods = typeof(ForwardedString).GetMethods((BindingFlags) (-1));

            Console.WriteLine($"Methods of {nameof(ForwardedString)}:\n");

            foreach (var method in methods)
            {
                Console.WriteLine(method);
            }
        }
    }
}