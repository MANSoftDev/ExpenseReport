using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace ExpenseReport
{
    public static class Approve
    {
        [FunctionName("Approve")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [OrchestrationClient]DurableOrchestrationClient client,
            ILogger log)
        {
            var instanceId = await req.ReadAsStringAsync();
            await client.RaiseEventAsync(instanceId, Constants.APPROVAL_EVENT, 1234);
            return new OkResult();
        }
    }
}
