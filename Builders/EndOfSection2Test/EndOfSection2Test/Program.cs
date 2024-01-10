using EndOfSection2Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndOfSection2Test
{
    //Abstract cannot be instantiated. 
    abstract class ClassPropInfo
    {
        protected string Name;
        protected string Type;

        public abstract override string ToString();
    }

    class PropInfoBuilder:ClassPropInfo
    {
        public PropInfoBuilder(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
        public override string ToString()
        {
            return $"  public {Type} {Name};\n";
        }
    }

    class ClassInfoBulder : ClassPropInfo
    {
        List<PropInfoBuilder> props = new List<PropInfoBuilder>();

        public ClassInfoBulder(string name)
        {
            this.Name = name;
        }

        public void AddField(string name, string type)
        {
            props.Add(new PropInfoBuilder(name, type));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"public class {Name}\n{{\n");
            foreach(PropInfoBuilder pib in props)
            {
                sb.Append(pib);
            }
            sb.Append("}");
            return sb.ToString();
        }
    }

    public class CodeBuilder
    {
        ClassInfoBulder cib;
        public CodeBuilder(string name)
        {
            cib = new ClassInfoBulder(name);
        }

        public CodeBuilder AddField(string name, string type)
        {
            cib.AddField(name, type);
            return this;
        }

        public override string ToString()
        {
            return cib.ToString();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
            Console.ReadLine();
        }
    }
}

