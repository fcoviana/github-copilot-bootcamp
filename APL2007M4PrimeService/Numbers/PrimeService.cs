
namespace System.Numbers
{
    public class PrimeService
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        public bool IsPrime(int candidate)
        {
            if (candidate < 2)
            {
                return false;
            }

            for (int divisor = 2; divisor <= Math.Sqrt(candidate); ++divisor)
            {
                if (candidate % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
