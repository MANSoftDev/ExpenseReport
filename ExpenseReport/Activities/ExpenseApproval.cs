using ExpenseReport.DTO;
using ExpenseReport.Helpers;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace ExpenseReport
{
    public static class ExpenseApproval
    {
        [FunctionName(Constants.EXPENSE_APPROVAL_ACTIVITY)]
        public static async Task<Approval> Run([ActivityTrigger] Expense expense,
            ILogger log)
        {
            var approval = new Approval();

            if (expense.Amount < 500.00) // Hard-coded for demo. Should come from configuration
            {
                approval.IsApproved = true;
                approval.Manager = null;
            }
            else
            {
                approval.IsApproved = true;
                approval.Manager = UserManagement.GetManager(1);
            }

            // Simulate processing
            await Task.Delay(new TimeSpan(0, 1, 0));
            return approval;
        }
    }
}
