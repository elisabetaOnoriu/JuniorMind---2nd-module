namespace TryCatchScenarios
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[] { 1, 2, 3 };
            try
            {
                Console.WriteLine(array[3]);
            }
            catch (IndexOutOfRangeException i)
            {
                Console.WriteLine(i.Message);
            }

            try
            {
                array[1] /= 0;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }

            string stringNumber = "1234562347894354";
            try
            {
                int number = int.Parse(stringNumber);
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}