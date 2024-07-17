using System.Diagnostics;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;

// Rango de fechas
var referenceDay = DateTime.Today.AddMonths(-1);
var beginDate = new DateTime(referenceDay.AddYears(-1).Year, referenceDay.AddMonths(1).Month, 1);
var endDate = new DateTime(referenceDay.Year, referenceDay.Month, DateTime.DaysInMonth(referenceDay.Year, referenceDay.Month));
var hours = Enumerable.Range(0, (int)(endDate - beginDate).TotalHours + 1).Select(d => beginDate.AddHours(d));

// Creamos un conjunto para almacenar los números generados
var random = new Random();
var cups = new HashSet<int>();
while (cups.Count < 100)
{
    int numero = random.Next(1, 101);
    if (!cups.Contains(numero)) cups.Add(numero);
}

var coefficients = cups.SelectMany(c => hours.Select(h => new CoefficientTimeStamp
{
    TimeStamp = h.ToUniversalTime(),
    IdContract = c,
    Value = random.NextDouble()
}));

var hours1 = hours.Count();
var temp1 = coefficients.Count();

var watch = new Stopwatch();
watch.Start();

using var client = new InfluxDBClient("http://localhost:8086", "kqn3Bntiv619ienSJeBrkcpaUwVC7p3vcQJmWrLjVjEDgnulmTFtgwkB77Ds-svCN0AMQl9URQJwsBPyZ1rxLQ==");
using (var writeApi = client.GetWriteApi())
{
    writeApi.WriteMeasurement(coefficients, WritePrecision.Ns, "bucket", "organization");
}

Console.WriteLine($"Insert Finish time: {watch.ElapsedMilliseconds}");
watch.Restart();

var query = "from(bucket:\"bucket\") |> range(start: 2024-01-01T00:00:00Z, stop: 2025-01-01T12:00:00Z)";
var result = await client.GetQueryApi().QueryAsync(query, "organization");

Console.WriteLine($"Aggregate Finish time: {watch.ElapsedMilliseconds}");

[Measurement("coefficient")]
internal class CoefficientTimeStamp
{
    [Column(IsTimestamp = true)] 
    public required DateTime TimeStamp { get; set; }

    [Column("idContract", IsTag = true)]
    public required int IdContract { get; set; }

    [Column("value")]
    public required double Value { get; set; }
}