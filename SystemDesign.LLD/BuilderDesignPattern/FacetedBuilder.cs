using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDesign.LLD.BuilderDesignPattern.FacetedBuider
{
    public class Person
    { 
        public string StreetAddress, Postcode, City;
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $""; //TODO
        }
    }


    public class PersonBuilder  //facade
    {
        protected Person person = new Person();
        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonAddressBuilder: PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder: PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string comanyName)
        {
            person.CompanyName = comanyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class FacetedBuilder
    {
        
        public static void Main(String[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb.Works
                .At("Fabrikam")
                .AsA("Engineer")
                .Earning(123000)
                .Lives
                    .At("123 London Road")
                    .In("London")
                    .WithPostcode("SW12AC");
        }
    }
}
