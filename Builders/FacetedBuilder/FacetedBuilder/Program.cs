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
        public string Name, StripColour;

        //stadium info
        public Stadium Stadium;

        // squads
        public List<Player> Squad;

        public override string ToString()
        {
            return $"Team Name: {Name}, Team Colour: {StripColour}, \n" +
                $"Stadium Info \n" +
                $"Name: {Stadium.Name}, {nameof(Stadium.Capacity)}: {Stadium.Capacity}, {nameof(Stadium.Address)}: {Stadium.Address}, {nameof(Stadium.Postcode)}: {Stadium.Postcode}\n" +
                $"Squad List \n " +
                $"{SquadAsString()}";
        }

        public string SquadAsString()
        {
            string list = "";
            foreach(Player p in Squad)
            {
                list += $"{nameof(p.FirstName)}: {p.FirstName}, {nameof(p.LastName)}: {p.LastName}, {nameof(p.SquadNumber)}: {p.SquadNumber}, {nameof(p.Position)}: {p.Position} \n";
            }
            return list;
        }
    }

    public class Stadium
    {
        public string Name;
        public string Address;
        public string Postcode;
        public int Capacity;
    }

    public class Player
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

        public FootballTeamInfoBuilder Information => new FootballTeamInfoBuilder(team);
        public FootballTeamStadiumBuilder Stadium => new FootballTeamStadiumBuilder(team);
        public FootballTeamSquadBuilder Squad => new FootballTeamSquadBuilder(team);

        public static implicit operator FootballTeam(FootballTeamBuilder ftb)
        {
            return ftb.team;
        }
    }

    public class FootballTeamInfoBuilder : FootballTeamBuilder
    {
        // might not work with a value type!
        public FootballTeamInfoBuilder(FootballTeam team)
        {
            this.team = team;
        }

        public FootballTeamInfoBuilder Name(string name)
        {
            team.Name = name;
            return this;
        }

        public FootballTeamInfoBuilder Strip(string strip)
        {
            team.StripColour = strip;
            return this;
        }

    }

    public class FootballTeamStadiumBuilder:FootballTeamBuilder
    {
        protected Stadium stadium = new Stadium(); 
        public FootballTeamStadiumBuilder(FootballTeam team)
        {
            this.team = team;
            this.team.Stadium = stadium;
        }

        public FootballTeamStadiumBuilder Name(string name)
        {
            stadium.Name = name;
            return this;
        }
        public FootballTeamStadiumBuilder Capacity(int capacity)
        {
            stadium.Capacity= capacity;
            return this;
        }
        public FootballTeamStadiumBuilder Postcode(string postcode)
        {
            stadium.Postcode = postcode;
            return this;
        }
        public FootballTeamStadiumBuilder Address(string address)
        {
            stadium.Address = address;
            return this;
        }
    }

    public class FootballTeamSquadBuilder : FootballTeamBuilder
    {
        protected List<Player> players = new List<Player>();
        protected Player player = new Player();
        public FootballTeamSquadBuilder(FootballTeam team)
        {
            this.team = team;
            this.team.Squad = players; 
        }

        public FootballTeamSquadBuilder PlayerFirstName(string name)
        {
            player.FirstName = name;
            return this;
        }
        public FootballTeamSquadBuilder PlayerLastName(string name)
        {
            player.LastName = name;
            return this;
        }
        public FootballTeamSquadBuilder PlayerSquadNumber(int number)
        {
            player.SquadNumber = number;
            return this;
        }
        public FootballTeamSquadBuilder PlayerPosition(string position)
        {
            player.Position = position;
            return this;
        }
        public FootballTeamSquadBuilder AddPlayer()
        {
            players.Add(player);
            return this;
        }
    }
    public class Demo
    {
        static void Main(string[] args)
        {
            var ftb = new FootballTeamBuilder();
            FootballTeam team = ftb
              .Information
                .Name("Londinium Club de Football")
                .Strip("Blue")
              .Stadium
                .Name("Really Nice Stadium")
                .Capacity(100001)
                .Postcode("SW1 1ST")
                .Address("123 London Street")
            .Squad
                .PlayerFirstName("Roxanne")
                .PlayerLastName("Luis")
                .PlayerSquadNumber(3)
                .PlayerPosition("Left Back")
                .AddPlayer();
            
            Console.WriteLine(team.ToString());
            Console.ReadLine();
        }
    }
}
