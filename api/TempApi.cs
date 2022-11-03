using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos.Table;
using System.Linq;

namespace Company.Function
{
    public static class TempApi
    {
        [FunctionName("TempApi")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, 
            [Table("Temperatures", "1", Connection = "TemperaturesDatabaseConnection")] CloudTable table,
            ILogger log)
        {
            var query = table.CreateQuery<TableData>();
            query.TakeCount = 5;

            var result = (await table.ExecuteQuerySegmentedAsync(query,null)).ToList();

            if(result.Any())
            {
                var apiResponse = new ApiResponse(
                    result.First().WeightedTemperature,
                    result.Average(temp => temp.WeightedTemperature)
                );

                return new OkObjectResult(apiResponse);
            }

            return new OkResult();
        }
    }
    public record ApiResponse(double CurrentTemperature, double AverageTemperature);

    public class TableData : TableEntity
    {
        public double WeightedTemperature { get; set; }
    }
}
