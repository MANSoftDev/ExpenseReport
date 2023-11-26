using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExpenseReport
{
    public static class AddExpense
    {
        [FunctionName(Constants.START_FUNCTION_NAME)]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient starter,
            ILogger log)
        {
            var expenses = await req.Content.ReadAsAsync<List<DTO.Expense>>();
            string instanceId = await starter.StartNewAsync(Constants.EXPENSE_ORCHESTRATOR, expenses);
            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
