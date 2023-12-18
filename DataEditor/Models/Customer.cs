using System;

namespace DataEditor.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public int? Age { get; set; }
        public decimal? Sales { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public Customer Clone()
        {
            return new Customer()
            {
                CustomerId = CustomerId,
                FirstName = FirstName,
                LastName = LastName,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                Zip = Zip,
                Phone = Phone,
                Age = Age,
                Sales = Sales,
                CreatedTime = CreatedTime,
                UpdatedTime = UpdatedTime
            };
        }

        public void Update(Customer customer)
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Address1 = customer.Address1;
            Address2 = customer.Address2;
            City = customer.City;
            State = customer.State;
            Zip = customer.Zip;
            Phone = customer.Phone;
            Age = customer.Age;
            Sales = customer.Sales;
            UpdatedTime = DateTime.Now;
        }
    }
}
