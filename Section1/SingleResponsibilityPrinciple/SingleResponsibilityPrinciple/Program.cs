using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//PRINCIPLE - a class should have one, and only one, reason to change. 
namespace SingleResponsibilityPrinciple
{
    //1 - To keep to Single Responsibility Rule keep Journal class as read, write related
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;
        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        
    }
    //2 - Second class is created for saving/loading 
    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite=false)
        {
            if(overwrite||!File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("Morning Diary");
            j.AddEntry("I am hungry");
            Console.WriteLine(j);

            var p = new Persistence();
            //var filename = ("@c:\temp.txt");
            //p.SaveToFile(j, filename);
            //Process.Start(filename);
        }
    }
}
