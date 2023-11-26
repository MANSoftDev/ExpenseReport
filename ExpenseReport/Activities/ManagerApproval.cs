using ExpenseReport.DTO;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace ExpenseReport.Activities
{
    public static class ManagerApproval
    {
        [FunctionName(Constants.EXPENSE_MANAGER_APPROVAL_ACTIVITY)]
        public static async Task<Approval> Run([ActivityTrigger] DurableActivityContext context,
                    ILogger log)
        {
            // Get the input data from the context
            var expense = context.GetInput<Expense>();

            // Simulate sending an email or other type of notiifcation
            var approval = new Approval()
            {
                IsApproved = true
            };

            // Simulate processing
            await Task.Delay(new TimeSpan(0, 1, 0));
            return approval;
        }
    }
}
