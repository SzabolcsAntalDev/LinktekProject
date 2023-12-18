using Caliburn.Micro;
using DataEditor.DataAccess;
using DataEditor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DataEditor.ViewModels
{
    public class EditViewModel : Screen
    {
        public LinkTekTestContext _dbContext;

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                NotifyOfPropertyChange(() => Customer);
            }
        }

        public string Title => $"Edit customer {Customer.FirstName} {Customer.LastName}";

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

        private ObservableCollection<string> _states;
        public ObservableCollection<string> States
        {
            get { return _states; }
            set
            {
                _states = value;
                NotifyOfPropertyChange(() => States);
            }
        }

        public string SelectedState
        {
            get { return Customer.State; }
            set
            {
                Customer.State = value;
                NotifyOfPropertyChange(() => SelectedState);
            }
        }

        public EditViewModel(LinkTekTestContext dbContext, Customer customer, ObservableCollection<string> states)
        {
            ValidateNotNull(dbContext, nameof(dbContext));
            ValidateNotNull(customer, nameof(customer));
            ValidateNotNull(states, nameof(states));

            _dbContext = dbContext;
            Customer = customer;

            States = states;
            if (!States.Any())
                LoadStatesAsync();
        }

        private void ValidateNotNull(object value, string name)
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        private async void LoadStatesAsync()
        {
            IsLoading = true;

            foreach (var state in await GetStatesAsync())
                States.Add(state);

            IsLoading = false;
        }

        private Task<List<string>> GetStatesAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    return _dbContext.Customer.Select(c => c.State).Distinct().ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(
                        $"Could not load states:\n\n" +
                        $"{e.Message}");

                    TryClose(false);
                    return new List<string>();
                }
            });
        }

        public async void Ok()
        {
            await SaveAsync();
        }

        public void Cancel()
        {
            TryClose(false);
        }

        private Task SaveAsync()
        {
            return Task.Run(() =>
            {
                IsLoading = true;

                try
                {
                    var customerFromDb = _dbContext.Customer.Single(c => c.CustomerId == Customer.CustomerId);
                    customerFromDb.Update(Customer);
                    _dbContext.SaveChanges();

                    TryClose(true);
                }
                catch (Exception e)
                {
                    MessageBox.Show(
                        $"Could not save customer {Customer.FirstName} {Customer.LastName}:\n\n" +
                        $"{e.Message}");
                }
                finally
                {
                    IsLoading = false;
                }
            });
        }
    }
}
