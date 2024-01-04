using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//A functional builder is a design pattern in C# that allows you to construct complex objects in a functional and fluent manner. 
//It leverages a set of builder classes, each responsible for constructing a specific part of the final object. 
//These builders are chained together to compose the complete object.
//Use this but try and stay as close to open close principle as poss

//Will not be using inheritence here, will be using extension 

namespace FunctionalBuilder
{
    public class Person
    {
        public string Name, Position;
    }

    //sealed means cannot inherit from it, you must use extension methods. Will preserve list of mutation functions
    //The below builder used to build up object, instead of having a field for storing person, do this in build step
    public sealed class PersonBuilder
    {
        public readonly List<Action<Person>> Actions
          = new List<Action<Person>>();

        //Add an action to a person and turn it into a func to preseve fluent interface
        public PersonBuilder Called(string name)
        {
            //Add new action to be applied to person
            Actions.Add(p => { p.Name = name; });
            return this;
        }

        public Person Build()
        {
            var p = new Person();
            Actions.ForEach(a => a(p));
            return p;
        }
    }

    //Keeping to OpenClose principle, we create an extension class
    public static class PersonBuilderExtensions
    {
        //passing in PersonBuilder as param in order to extend & use functions builder
        public static PersonBuilder WorksAsA
          (this PersonBuilder builder, string position)
        {
            builder.Actions.Add(p =>
            {
                p.Position = position;
            });
            return builder;
        }
    }

    public class FunctionalBuilder
    {
        public static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            var person = pb.Called("Dmitri").WorksAsA("Programmer").Build();
        }
    }
}