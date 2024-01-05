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

//Fluent interface is an interface that allows you to chain several calls by returning a reference of the object you are working with
//eg
//public class TestClass
//{
//    public TestClass exampleFunction()
//    {
//        return this;
//    }
//}
//Can then do the following:
//var builder = new TestClass()
//TestClass().exampleFunction().exampleFunction()
    

namespace FunctionalBuilder
{
    public enum BreadType
    {
        Sandwich,
        Roll,
        Panini,
        Baguette
    }

    public class Sandwich
    {
        public string Name;
        public bool Vegetarian;
        public bool Vegan;
        public BreadType BreadType;
    }

    //sealed means cannot inherit from it, you must use extension methods. Will preserve list of mutation functions
    //The below builder used to build up object, instead of having a field for storing person, do this in build step
    public sealed class SandwichBuilder
    {
        public readonly List<Action<Sandwich>> Actions
          = new List<Action<Sandwich>>();

        //Add an action to a person and turn it into a func to preseve fluent interface
        public SandwichBuilder Called(string name)
        {
            //Add new action to be applied to sandwich
            Actions.Add(p => { p.Name = name; });
            return this;
        }

        public Sandwich Build()
        {
            var s = new Sandwich();
            Actions.ForEach(a => a(s));
            return s;
        }
    }

    //Keeping to OpenClose principle, we create an extension class for functions that are to be added
    public static class SandwhichBuilderExtension
    {
        //passing in SandwichBuilder as param in order to extend & use functions builder
        public static SandwichBuilder Vegetarian
          (this SandwichBuilder builder, bool veggie)
        {
            builder.Actions.Add(s =>
            {
                //If vegan, must be veggie
                s.Vegetarian = s.Vegan?true:false;
            });
            return builder;
        }

        public static SandwichBuilder IsVegan
          (this SandwichBuilder builder, bool vegan)
        {
            builder.Actions.Add(s =>
            {
                s.Vegan = vegan;
            });
            return builder;
        }

        public static SandwichBuilder WhichBreadType
          (this SandwichBuilder builder, BreadType breadType)
        {
            builder.Actions.Add(s =>
            {
                s.BreadType = breadType;
            });
            return builder;
        }
    }

    public class FunctionalBuilder
    {
        public static void Main(string[] args)
        {
            var sb = new SandwichBuilder();
            var sandwhich = sb.Called("The Whopper").IsVegan(true).Vegetarian(false).WhichBreadType(BreadType.Panini).Build();
        }
    }
}