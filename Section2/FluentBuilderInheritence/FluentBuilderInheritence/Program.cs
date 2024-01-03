using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBuilderInheritence
{
    public class Person
    {
        public string Name;
        public Meal Meal;

        public class Builder : PersonDishCuisineBuilder<Builder>
        {

        }
        public static Builder New => new Builder();

        //The override modifier is required to extend or modify the abstract or virtual implementation of an inherited method, property, indexer, or event.
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Meal.Dish)}: {Meal.Dish.Name}({Meal.Dish.Cuisine}), {nameof(Meal.Drink)}: {Meal.Drink}";
        }
    }
    public class Meal
    {
       public Dish Dish;
       public string Drink;
    }

    public class Dish
    {
        public string Name;
        public string Cuisine;
    }

    //Recursive generics can be used to infinitely allow a derived class to pass information about return type back to it's base class (pass drink and dish info back to PersonInfoBuilder class)
    public class PersonBuilder
    {
        protected Person person = new Person();
        public Person Build()
        {
            return person;
        }
    }

    //class Foo:Bar<Foo>, so our derived class is going to derive from itself
    public class PersonInfoBuilder<SELF>:PersonBuilder where SELF:PersonInfoBuilder<SELF>
    {
        //protected as using inheritence
        //meaning person can only be accessed by classes derived from PersonInfoBuilder
        protected Person person = new Person();

        //fluent builder approach
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF) this;
        }
    }

    public class PersonDishBuilder<SELF>:PersonInfoBuilder<PersonDishBuilder<SELF>> where SELF: PersonDishBuilder<SELF>
    {
        public SELF WantsToEat(string dish)
        {
            person.Dish.Name = dish;
            //person.Dish.Cuisine = cuisine;
            return (SELF) this;
        }
    }

    public class PersonDrinkBuilder<SELF> : PersonInfoBuilder<PersonDrinkBuilder<SELF>> where SELF : PersonDrinkBuilder<SELF>
    {
        public SELF WantsToDrink(string drink)
        {
            person.Drink = drink;
            return (SELF) this;
        }
    }

    public class PersonDishCuisineBuilder<SELF> : PersonDishBuilder<PersonDishCuisineBuilder<SELF>> where SELF : PersonDishCuisineBuilder<SELF>
    {
        public SELF OfThisCuisine(string cuisine)
        {
            person.Dish.Cuisine = cuisine;
            return (SELF)this;
        }
    }




    // internal - Only accessible within the same assembly.
    internal class Program
    {
        public static void Main(string[] args)
        {
            Person.New.Called("Scott").WantsToEat("Sweet & Sour").OfThisCuisine("Chinese").Build();
        }
    }
}
