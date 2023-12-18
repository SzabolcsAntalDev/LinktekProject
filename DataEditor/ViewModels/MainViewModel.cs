using Caliburn.Micro;
using DataEditor.Common;
using DataEditor.DataAccess;
using DataEditor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DataEditor.ViewModels
{
    public class MainViewModel : Screen
    {
        private LinkTekTestContext _dbContext;
        private readonly Paginator _paginator = new Paginator();
        private readonly ObservableCollection<string> _states = new ObservableCollection<string>();

        private IWindowManager _windowManager;
        private IWindowManager WindowManager => _windowManager ?? (_windowManager = new WindowManager());

        private readonly IDictionary<string, Func<Customer, object>> _keySelectorsByColumnHeaderText =
            new Dictionary<string, Func<Customer, object>>()
        {
            { "First Name", c => c.FirstName },
            { "Last Name", c => c.LastName },
            { "Address 1", c => c.Address1 },
            { "Address 2", c => c.Address2 },
            { "City", c => c.City },
            { "State", c => c.State },
            { "Zip Code", c => c.Zip },
            { "Phone Number", c => c.Phone },
            { "Age", c => c.Age },
            { "Sales", c => c.Sales },
            { "Created Time", c => c.CreatedTime },
            { "Updated Time", c => c.UpdatedTime },
        };

        #region Pagination
        public int? TotalNumberOfCustomers
        {
            get { return _paginator.TotalNumberOfCustomers; }
            set
            {
                _paginator.TotalNumberOfCustomers = value;
                NotifyOfPropertyChange(() => TotalNumberOfCustomers);
                NotifyOfPropertyChange(() => TotalNumberOfPages);
                NotifyOfPropertyChange(() => CanFirstPage);
                NotifyOfPropertyChange(() => CanPrevPage);
                NotifyOfPropertyChange(() => CanNextPage);
                NotifyOfPropertyChange(() => CanLastPage);
            }
        }

        public int? TotalNumberOfPages
        {
            get { return _paginator.TotalNumberOfPages; }
            set
            {
                _paginator.TotalNumberOfPages = value;
                NotifyOfPropertyChange(() => TotalNumberOfPages);
            }
        }

        public int PageSize => Paginator.PageSize;

        public int CurrentPageIndex
        {
            get { return _paginator.CurrentPageIndex; }
            set
            {
                _paginator.CurrentPageIndex = value;
                NotifyOfPropertyChange(() => CurrentPageIndex);
                Load();
                NotifyOfPropertyChange(() => CanFirstPage);
                NotifyOfPropertyChange(() => CanPrevPage);
                NotifyOfPropertyChange(() => CanNextPage);
                NotifyOfPropertyChange(() => CanLastPage);
            }
        }

        // header text of the column by which the view is sorted
        public string SortByHeaderText
        {
            get { return _paginator.SortByHeaderText; }
            set
            {
                _paginator.SortByHeaderText = value;
                NotifyOfPropertyChange(() => SortByHeaderText);
                NotifyOfPropertyChange(() => IsAscending);
                Load();
            }
        }

        public bool IsAscending
        {
            get { return _paginator.IsAscending; }
            set { _paginator.IsAscending = value; }
        }
        #endregion Pagination

        #region Properties
        private IList<Customer> _customers;
        public IList<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                NotifyOfPropertyChange(() => Customers);
            }
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                NotifyOfPropertyChange(() => SelectedCustomer);
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                NotifyOfPropertyChange(() => IsLoading);
            }
        }
        #endregion Properties

        public MainViewModel()
        {
            InitDbContext();
            Load();
        }

        private void InitDbContext()
        {
            try
            {
                _dbContext = new LinkTekTestContext();
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    $"Could not connect to the database with the connection string:\n\n" +
                    $"{ConfigurationManager.ConnectionStrings["LinkTekTest"].ConnectionString}\n\n" +
                    $"{e.Message}");
                Environment.Exit(0);
            }
        }

        public async void Load()
        {
            await LoadAsync();
        }

        private Task LoadAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    IsLoading = true;

                    // set number of costumers only at first load
                    // depending on the requirements this can be changed
                    SetTotalNumberOfCustomers();

                    Customers = _paginator.GetPage(_dbContext, _keySelectorsByColumnHeaderText);
                }
                catch (Exception e)
                {
                    MessageBox.Show(
                        $"Could not load customers:\n\n" +
                        $"{e.Message}");
                }
                finally
                {
                    IsLoading = false;
                }
            });
        }

        private void SetTotalNumberOfCustomers()
        {
            if (TotalNumberOfCustomers == null)
                TotalNumberOfCustomers = _dbContext.Customer.Count();
        }

        public bool CanFirstPage => _paginator.CanPrevPage;
        public void FirstPage()
        {
            CurrentPageIndex = 0;
        }

        public bool CanPrevPage => _paginator.CanPrevPage;
        public void PrevPage()
        {
            CurrentPageIndex--;
        }

        public bool CanNextPage => _paginator.CanNextPage;
        public void NextPage()
        {
            CurrentPageIndex++;
        }

        public bool CanLastPage => _paginator.CanNextPage;
        public void LastPage()
        {
            CurrentPageIndex = TotalNumberOfPages.Value;
        }

        public void EditSelectedCustomer()
        {
            try
            {
                // should not get into this use case
                if (SelectedCustomer == null)
                {
                    MessageBox.Show("No customer selected.");
                    return;
                }

                var result = OpenEditView();

                // reload the view if modifications were saved
                if (result == true)
                    Load();
            }
            catch (Exception e)
            {
                MessageBox.Show(
                        $"Could not edit customer:\n\n" +
                        $"{e.Message}");
            }
        }

        private bool? OpenEditView()
        {
            // send a copy of the customer so the data in the actual view
            // is not modified when editing the customer data in the edit view
            var editViewModel = new EditViewModel(_dbContext, SelectedCustomer.Clone(), _states);
            return WindowManager.ShowDialog(editViewModel, null, null);
        }

        protected override void OnDeactivate(bool close)
        {
            _dbContext.Dispose();
            base.OnDeactivate(close);
        }
    }
}
