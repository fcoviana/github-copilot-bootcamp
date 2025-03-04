
namespace ReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            // Create sample data
            var sampleData = new List<SalesData>
    {
        new SalesData
        {
            productID = "PROD001",
            quantitySold = 100,
            unitPrice = 29.99,
            baseCost = 15.00
        },
        new SalesData
        {
            productID = "PROD002",
            quantitySold = 75,
            unitPrice = 49.99,
            baseCost = 25.00
        },
        new SalesData
        {
            productID = "PROD003",
            quantitySold = 50,
            unitPrice = 99.99,
            baseCost = 45.00
        }
    };

            // Create dictionary for quarterly data
            var quarterlyData = new Dictionary<string, List<SalesData>>
    {
        { "Q1 2024", sampleData }
    };

            // Display report for each quarter
            foreach (var quarter in quarterlyData)
            {
                if (!program.DisplayReports(quarter.Key, quarter.Value))
                {
                    Console.WriteLine($"Failed to generate report for {quarter.Key}");
                }
            }

        }


        private bool DisplayReports(string quarter, List<SalesData> top3SalesOrders)
        {
            try
            {
                ValidateInputs(quarter, top3SalesOrders);
                DisplayTableHeader(quarter);
                ProcessSalesOrders(top3SalesOrders);
                DisplayTableFooter();

                return true;
            }
            catch (SalesReportException ex)
            {
                LogError($"Sales report generation failed: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                LogError($"Unexpected error in report generation: {ex.Message}");
                return false;
            }
        }

        private void ValidateInputs(string quarter, List<SalesData> salesOrders)
        {
            if (string.IsNullOrEmpty(quarter))
            {
                throw new SalesReportException("Quarter identifier cannot be null or empty");
            }

            if (salesOrders == null)
            {
                throw new SalesReportException("Sales orders list cannot be null");
            }

            if (!salesOrders.Any())
            {
                throw new SalesReportException($"No sales data available for quarter {quarter}");
            }
        }

        private void DisplayTableHeader(string quarter)
        {
            Console.WriteLine($"Top 3 Sales Orders for {quarter}:");
            Console.WriteLine("┌───────────────────────┬───────────────────┬───────────────────┬───────────────────┬───────────────────┬───────────────────┐");
            Console.WriteLine("│      Product ID       │   Quantity Sold   │    Unit Price     │   Total Sales     │      Profit       │ Profit Percentage │");
            Console.WriteLine("├───────────────────────┼───────────────────┼───────────────────┼───────────────────┼───────────────────┼───────────────────┤");
        }

        private void ProcessSalesOrders(List<SalesData> salesOrders)
        {
            foreach (var order in salesOrders)
            {
                try
                {
                    DisplaySalesOrderRow(order);
                }
                catch (Exception ex)
                {
                    LogError($"Error processing order {order.productID}: {ex.Message}");
                }
            }
        }

        private void DisplaySalesOrderRow(SalesData order)
        {
            var (totalSales, profit, profitPercentage) = CalculateOrderMetrics(order);

            Console.WriteLine("│ {0,-22}│ {1,17} │ {2,17} │ {3,17} │ {4,17} │ {5,17} │",
                order.productID ?? "Unknown",
                order.quantitySold,
                order.unitPrice.ToString("C"),
                totalSales.ToString("C"),
                profit.ToString("C"),
                profitPercentage.ToString("F2"));
        }

        private (double totalSales, double profit, double profitPercentage) CalculateOrderMetrics(SalesData order)
        {
            double totalSales = order.quantitySold * order.unitPrice;
            double profit = totalSales - (order.quantitySold * order.baseCost);
            double profitPercentage = totalSales > 0 ? (profit / totalSales) * 100 : 0;

            return (totalSales, profit, profitPercentage);
        }

        private void DisplayTableFooter()
        {
            Console.WriteLine("└───────────────────────┴───────────────────┴───────────────────┴───────────────────┴───────────────────┴───────────────────┘");
            Console.WriteLine();
        }

        private void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }


        public class SalesReportException : Exception
        {
            public SalesReportException(string message) : base(message) { }
            public SalesReportException(string message, Exception inner) : base(message, inner) { }
        }

        public class SalesData
        {
            public string? productID { get; set; }
            public double quantitySold { get; set; }
            public double unitPrice { get; set; }
            public double baseCost { get; set; }
        }
    }
}