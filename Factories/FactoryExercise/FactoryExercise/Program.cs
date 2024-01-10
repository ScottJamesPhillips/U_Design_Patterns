using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryExercise
{
    //You are given a class called Person.The person has two fields: Id , and Name.
    //Please implement a non-static PersonFactory that has a CreatePerson()  method that 
    //takes a person's name.
    //The Id of the person should be set as a 0-based index of the object created.
    //So, the first person the factory makes should have Id= 0, second Id = 1 and so on.
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonFactory
    {
        //Factory needs to initialize counter, create new person, increment on create
        int counter = 0;

        public Person CreatePerson(string name)
        {
            Person p = new Person { Id=counter, Name= name};
            counter++;
            return p;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PersonFactory pf = new PersonFactory();

            Person p1= pf.CreatePerson("Steamboat");
            Person p2 = pf.CreatePerson("Willie");

            Console.WriteLine($"Person 1 called {p1.Name}");
            Console.WriteLine($"Person 2 called {p2.Name}");
            Console.ReadLine();
        }
    }
}
