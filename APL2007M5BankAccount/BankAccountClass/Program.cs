namespace BankAccountApp;

static public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var simulator = new AccountSimulator();
            simulator.RunSimulation();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Simulation failed: {ex.Message}");
        }
    }
}