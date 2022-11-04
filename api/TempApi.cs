using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos.Table;
using System.Linq;
using System.Collections.Generic;

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
            query.TakeCount = 100;

            var result = (await table.ExecuteQuerySegmentedAsync(query,null)).ToList();

            int index = 0;

            List<double> fetchedTemperatures = new List<double>();

            if(result.Any())
            {
                foreach(var item in result)
                {
                    fetchedTemperatures.Add(item.WeightedTemperature);
                    index++;
                    if(index == 100)
                        break;  
                }

                var apiResponse = new ApiResponse(
                    result.First().WeightedTemperature,
                    fetchedTemperatures.Average()
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
