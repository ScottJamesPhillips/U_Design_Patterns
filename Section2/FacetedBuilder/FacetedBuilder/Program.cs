using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Faceted Builder approach sees constructing an object using more than one builder
//Sometimes we may have a complex object, and the creational process requires more than one builder class.
//So, what we need to do is to introduce multiple builder classes in such a way, that we can jump from one builder to another while creating our object.
//The faceted Builder approach helps us a lot in that process because we create a facade over our builders and it allows us to use all the builders to create a single object.

//Below is an example in which FootballTeam object uses faceted builder method to construct itself

namespace FacetedBuilder
{
    public class FootballTeam
    {
        // info
        public string Name, City, StreetAddress, Postcode;
        
        //stadium info
        public Stadium Stadium;

        // squads
        public List<Players> Squad;

        //public override string ToString()
        //{
        //    return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        //}
    }

    public class Stadium
    {
        public string Name;
        public string Address;
        public int Capacity;
    }

    public class Players
    {
        public string FirstName;
        public string LastName;
        public int SquadNumber;
        public string Position;
    }


    public class FootballTeamBuilder // facade 
    {
        // the object we're going to build
        protected FootballTeam team = new FootballTeam(); // this is a reference!

        public FootballTeamInfoBuilder Lives => new FootballTeamInfoBuilder(team);
        //public PersonJobBuilder Works => new PersonJobBuilder(team);

        public static implicit operator FootballTeam(FootballTeamBuilder ftb)
        {
            return ftb.team;
        }
    }

    //public class PersonJobBuilder : PersonBuilder
    //{
    //    public PersonJobBuilder(Person person)
    //    {
    //        this.person = person;
    //    }

    //    public PersonJobBuilder At(string companyName)
    //    {
    //        person.CompanyName = companyName;
    //        return this;
    //    }

    //    public PersonJobBuilder AsA(string position)
    //    {
    //        person.Position = position;
    //        return this;
    //    }

    //    public PersonJobBuilder Earning(int annualIncome)
    //    {
    //        person.AnnualIncome = annualIncome;
    //        return this;
    //    }
    //}

    public class FootballTeamInfoBuilder : FootballTeamBuilder
    {
        // might not work with a value type!
        public FootballTeamInfoBuilder(FootballTeam team)
        {
            this.team = team;
        }

        public FootballTeamInfoBuilder At(string streetAddress)
        {
            team.StreetAddress = streetAddress;
            return this;
        }

        public FootballTeamInfoBuilder WithPostcode(string postcode)
        {
            team.Postcode = postcode;
            return this;
        }

        public FootballTeamInfoBuilder In(string city)
        {
            team.City = city;
            return this;
        }

    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var pb = new FootballTeamBuilder();
            FootballTeam person = pb
              .Lives
                .At("123 London Road")
                .In("London")
                .WithPostcode("SW12BC")
              //.Works
              //  .At("Fabrikam")
              //  .AsA("Engineer")
              //  .Earning(123000);

            Console.WriteLine(person);
        }
    }
}
