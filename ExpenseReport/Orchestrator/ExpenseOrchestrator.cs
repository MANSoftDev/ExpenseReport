using ExpenseReport.DTO;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseReport
{
    public static class ExpenseOrchestrator
    {
        [FunctionName(Constants.EXPENSE_ORCHESTRATOR)]
        public static async Task<bool> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context, ILogger log)
        {
            bool success = false;
            log.LogInformation($"ExpenseOrchestrator executing: {context.CurrentUtcDateTime}");

            var expenses = context.GetInput<List<Expense>>();

            foreach (var expense in expenses)
            {
                var approval = await context.CallActivityAsync<Approval>(Constants.EXPENSE_APPROVAL_ACTIVITY, expense);
                if (approval.Manager != null)
                {
                    await context.CallActivityAsync<Approval>(Constants.EXPENSE_MANAGER_APPROVAL_ACTIVITY, expense);
                    var code = await context.WaitForExternalEvent<int>(Constants.APPROVAL_EVENT);
                }
            }

            return success;
        }
    }
}