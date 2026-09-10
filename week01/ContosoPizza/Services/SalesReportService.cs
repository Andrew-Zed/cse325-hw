using System.Globalization;
using System.Text;

namespace ContosoPizza.Services;

public static class SalesReportService
{
    public static decimal GenerateSalesSummary(string salesDirectory, string reportPath)
    {
        var salesFiles = Directory.GetFiles(salesDirectory, "sales-*.txt")
            .OrderBy(filePath => filePath, StringComparer.OrdinalIgnoreCase)
            .ToArray();
        var fileTotals = new List<(string FileName, decimal Total)>();

        foreach (var salesFile in salesFiles)
        {
            var fileTotal = decimal.Parse(
                File.ReadAllText(salesFile).Trim(),
                NumberStyles.Currency,
                CultureInfo.InvariantCulture);
            fileTotals.Add((Path.GetFileName(salesFile), fileTotal));
        }

        var totalSales = fileTotals.Sum(file => file.Total);
        var report = new StringBuilder()
            .AppendLine("Sales Summary")
            .AppendLine("----------------------------")
            .AppendLine($" Total Sales: {totalSales:C}")
            .AppendLine()
            .AppendLine(" Details:");

        foreach (var file in fileTotals)
        {
            report.AppendLine($"  {file.FileName}: {file.Total:C}");
        }

        File.WriteAllText(reportPath, report.ToString());
        return totalSales;
    }
}