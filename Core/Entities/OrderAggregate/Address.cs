using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    // Order Entity Owns this address, not related to it but owing it
    //doesnt have id 
    public class Address
    {
        public Address() //EF requires empty ctor to make success migration
        {

        }

        public Address(string firstName, string lastName, string street, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}
