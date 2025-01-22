using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PublicUtilities.Services;
using PublicUtilities.Models;

namespace PublicUtilities
{
    public  class Function1
    {

        private readonly IScrapingService _scrapingService;
        public Function1(IScrapingService scrapingService)
        {
            _scrapingService = scrapingService ?? throw new ArgumentNullException(nameof(scrapingService));
        }

        [FunctionName("Oomapasc")]
        public async Task<IActionResult> GetOomapascInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {

            string accountNumber = req.Query["accountNumber"];
            if (string.IsNullOrEmpty(accountNumber))
            {
                log.LogInformation("Account number is required.");
                return new BadRequestObjectResult("Account number is required.");
            }
            var oomapasc = await _scrapingService.WebScrapingOomapascAsync(accountNumber);

            return new OkObjectResult(oomapasc);
        }




        //[FunctionName("Function1")]
        //public static async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    string name = req.Query["name"];

        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic data = JsonConvert.DeserializeObject(requestBody);
        //    name = name ?? data?.name;

        //    string responseMessage = string.IsNullOrEmpty(name)
        //        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
        //        : $"Hello, {name}. This HTTP triggered function executed successfully.";

        //    return new OkObjectResult(responseMessage);
        //}
    }
}
