using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Syncfusion.Data;
using Syncfusion.EJ2;
using Syncfusion.Licensing;

namespace ExpenseTracker.Controllers
{
    public class DashboardController : Controller
    {

        private readonly ApplicationDbContext _context;
        
        public  DashboardController(ApplicationDbContext context)
        {
                    _context = context;
        }    
    

     
        public async Task<ActionResult> Index()
        {
            //last 7 days
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

          List<Transaction> SelectedTransactions = await _context.Transactions
               .Include(x => x.Category)
               .Where(y => y.Date >= StartDate && y.Date <= EndDate)
               .ToListAsync();

            //Total Income
            int TotalIncome = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("C0");

            //Total Expense
            int TotalExpense = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

            //Balance
            int Balance = TotalIncome - TotalExpense;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(culture, "{0:c0}", Balance);

            //Doughnut chart - Expense By Category
            ViewBag.DoughnutChartData = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.Icon+""+ k.First().Category.Title,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = k.Sum(j => j.Amount).ToString("C0"),
                })
                .OrderByDescending(l=>l.amount)
                .ToList();

            //Spline Chart - TotalIncome vs Expense

            //Income
            List<SplineChartdata> IncomeSummary = SelectedTransactions
                 .Where(i => i.Category.Type == "Income")
                 .GroupBy(j => j.Date)
                 .Select(k => new SplineChartdata()
                 {
                     day = k.First().Date.ToString("dd-MMM"),
                     income = k.Sum(l => l.Amount)
                 })
                 .ToList();

            //Expense
            List<SplineChartdata> ExpenseSummary = SelectedTransactions
                 .Where(i => i.Category.Type == "Expense")
                 .GroupBy(j => j.Date)
                 .Select(k => new SplineChartdata()
                 {
                     day = k.First().Date.ToString("dd-MMM"),
                     expense = k.Sum(l => l.Amount)
                 })
                 .ToList();

            //Combine Income & Expense
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = from day in Last7Days
                                      join income in IncomeSummary on day equals income.day into DayIncomeJoined
                                      from income in DayIncomeJoined.DefaultIfEmpty()
                                      join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,

                                      };
            //Recent Transactions
            ViewBag.RecentTransactions = await _context.Transactions
                .Include(i => i.Category)
                .OrderByDescending(j => j.Date)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }

    public class SplineChartdata
    {
        public string day;
        public int Income;
        public int Expense;
        internal int income;
        internal int expense;
    }

}
