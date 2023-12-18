using DataEditor.DataAccess;
using DataEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataEditor.Common
{
    public class Paginator
    {
        public const int PageSize = 300;

        private int? _totalNumberOfCustomers;
        public int? TotalNumberOfCustomers
        {
            get { return _totalNumberOfCustomers; }
            set
            {
                _totalNumberOfCustomers = value;
                TotalNumberOfPages = _totalNumberOfCustomers / PageSize;
            }
        }

        public int? TotalNumberOfPages { get; set; }

        public int CurrentPageIndex { get; set; } = 0;

        // header text of the column by which the view is sorted
        private string _sortByHeaderText;
        public string SortByHeaderText
        {
            get { return _sortByHeaderText; }
            set
            {
                // reverse sort order of actual column
                if (_sortByHeaderText == value)
                    IsAscending = !IsAscending;
                // sort ascending by new column
                else
                    IsAscending = true;

                _sortByHeaderText = value;
            }
        }

        public bool IsAscending { get; set; }

        public bool CanPrevPage => CurrentPageIndex > 0;
        public bool CanNextPage => CurrentPageIndex < TotalNumberOfPages;

        public IList<Customer> GetPage(
            LinkTekTestContext dbContext,
            IDictionary<string, Func<Customer, object>> keySelectorsByColumnHeaderText)
        {
            // skip the previous pages to get to the current page
            var customersToSkip = CurrentPageIndex * PageSize;
            var viewIsSorted = SortByHeaderText != null;

            if (!viewIsSorted)
                return dbContext.Customer.Skip(customersToSkip).Take(PageSize).ToList();

            // the field of the Customer to order by
            var orderBySelector = keySelectorsByColumnHeaderText[SortByHeaderText];
            var orderedCustomers = IsAscending
                    ? dbContext.Customer.OrderBy(orderBySelector)
                    : dbContext.Customer.OrderByDescending(orderBySelector);

            return orderedCustomers.Skip(customersToSkip).Take(PageSize).ToList();
        }
    }
}
