namespace Dima.core.Models.Reports;

public record ExpensesByCategory(string UserId, string Category, int Year, decimal Expenses);