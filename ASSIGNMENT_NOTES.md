# W01 Assignment Notes

## Part 1: Web API Evidence

The API uses `PizzaController` to implement CRUD operations. The original records are `Classic Italian` and `Veggie`. The additional record I added is:

```json
{
    "id": 3,
    "name": "Hawaiian",
    "isGlutenFree": false
}
```

The following requests were tested against `http://localhost:5270`:

| Operation | Request | Result |
| --- | --- | --- |
| GET | `GET /pizza/` | `200 OK`; returned the pizza list, including Hawaiian. |
| POST | `POST /pizza/` with `{"name":"Margherita","isGlutenFree":false}` | `201 Created`; returned the new pizza with its generated ID. |
| PUT | `PUT /pizza/4` with `{"id":4,"name":"Margherita Special","isGlutenFree":false}` | `204 No Content`; updated the pizza. |
| DELETE | `DELETE /pizza/4` | `204 No Content`; deleted the pizza. |

The runnable request examples are also in `Week01/ContosoPizza/ContosoPizza.http`.

## Part 2: Sales Summary Function

This function reads every `sales-*.txt` file, calculates the actual total, and writes a detailed text report file.

```csharp
using System.Globalization;
using System.Text;

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
```