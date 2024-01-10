using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepwiseBuilder
{

    //The Step Builder pattern is a creational design pattern used to construct a complex object step by step.
    //It provides a fluent interface to create an object with a large number of possible configurations, making the code more readable and reducing the need for multiple constructors or setter methods.
    //An extension of the Builder pattern that fully guides the user through the creation of the object with no chances of confusion.
    //The user experience will be much more improved by the fact that he will only see the next step methods available, NO build method until is the right time to build the object.
    //Imagine you are building a configuration object for a new user.The user has various optional parameters such as host, port, username, password, and others. 
    //Using the Step Builder pattern, you can set these parameters in a clean and readable way:

    //The Step Builder pattern allows you to construct complex objects by breaking down the construction process into a series of steps.Each step corresponds to setting a particular attribute or configuration option of the object. 
    //This results in more readable and maintainable code, especially when dealing with objects that have numerous configuration options

    public class User
    {
        public string Name;
        public string Email;
        public string Password;
        public string ConfirmPassword;
        public bool ReceiveOffers;
    }

    //ISpecifyEmail is first method called, all others then cascade down
    public interface ICreateUser
    {
        ISpecifyEmail Email(string Email);
    }
    public interface ISpecifyEmail
    {
        ISpecifyPassword Password(string password);
    }
    public interface ISpecifyPassword
    {
        ISpecifyConfirmPassword CheckPassword(string confirmPassword);
    }
    public interface ISpecifyConfirmPassword
    {
        IReceiveOffers ReceiveOffers(bool receiveOffers);
    }
    public interface IReceiveOffers
    {
        User CreateUser();
    }


    public class UserBuilder
    {
        //private implementation class 
        private class Impl :
            ICreateUser,
            ISpecifyEmail,
            ISpecifyPassword,
            ISpecifyConfirmPassword,
            IReceiveOffers
        {
            private User user = new User();

            public User CreateUser()
            {
                return user;
            }

            public ISpecifyEmail Email(string email)
            {
                if (!email.Contains('@'))
                    throw new Exception("Invalid email");
                user.Email = email;
                return this;
            }

            public ISpecifyPassword Password(string password)
            {
                if (password.Length < 6)
                    throw new Exception("Password too short");
                user.Password = password;
                return this;
            }

            public ISpecifyConfirmPassword CheckPassword(string confirmPassword)
            {
                if (confirmPassword != user.Password)
                    throw new Exception("Passwords do not match");
                user.ConfirmPassword = confirmPassword;
                return this;
            }

            public IReceiveOffers ReceiveOffers(bool receiveOffers)
            {
                user.ReceiveOffers = receiveOffers;
                return this;
            }
        }

        public static ICreateUser Initialize()
        {
            return new Impl();
        }
    }

        class Program
        {
            static void Main(string[] args)
            {
                try
                {
                    var user = UserBuilder.Initialize().Email("@").Password("Arse1994").CheckPassword("Arse1994").ReceiveOffers(true).CreateUser();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ReadLine();
                }
            }
        }
    }