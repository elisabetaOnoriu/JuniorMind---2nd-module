using Validator;

namespace JsonValidator
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Pass in the file path as an argument");
                return;
            }

            string text = System.IO.File.ReadAllText(args[0]);
            var jsonText = new Value();
            var match = jsonText.Match(text);
            Console.WriteLine(match.Success() && match.RemainingText() == "");
        }
    }
}
