using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
using System.Linq;

namespace Company.Function
{
    public static class TempApi
    {
        [FunctionName("TempApi")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //Detta ska ligga som ett Attribute unde http.. i metoden ovan. Ändra namn på tabellen weatherData och connectionen
            //connection ska också in i local settings

            //[Table("Temperatures", "1", Connection = "TemperaturesDatabaseConnection")] CloudTable table,
            
            
            log.LogInformation("C# HTTP trigger function processed a request.");

            var apiResponse = new ApiResponse(
                    11.2,
                    14.0
                );
            
            return new OkObjectResult(apiResponse);

            // var query = table.CreateQuery<TableData>();
            // query.TakeCount = 5;

            
            // // Här höll vi på ocj dribblade med Ienumerable vs tolist. antar att inparametern i metoden var Ienumerable å inte CludTable då

            // var result = (await table.ExecuteQuerySegmentedAsync(query,null)).ToList();

            // if(result.Any())
            // {
            //     var apiResponse = new ApiResponse(
            //         result.First().Temperature,
            //         result.Average(temp => temp.Temperature)
            //     );

            //     return new OkObjectResult(apiResponse);
            // }

            // return new OkResult();
        }
    }
    public record ApiResponse(double CurrentTemperature, double AverageTemperature);

    public class TableData : TableEntity
    {
        public double Temperature { get; set; }
    }
}
