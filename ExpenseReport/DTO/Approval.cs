namespace ExpenseReport.DTO
{
    public struct Approval
    {
        public bool IsApproved { get; set; }
        public User? Manager { get; set; }
    }
}
