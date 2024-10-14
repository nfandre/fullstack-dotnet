namespace Dima.core.Requests.Transactions;

public class GetTransactionByPeriodRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}