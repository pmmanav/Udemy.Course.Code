using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDesign.LLD.BuilderDesignPattern
{
    public class Person
    {
        public string Name;
        public string Position;

        public class Builder: PersonJobBuilder<Builder>
        {
        }

        public static Builder New => new Builder();
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public class PersonInfoBuilder<SELF>: PersonBuilder
        where SELF: PersonInfoBuilder<SELF>
    {
        protected Person person = new Person();
        public SELF Called(string name) 
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();
        public Person Build()
        {
            return person;
        }
    }

    public class PersonJobBuilder<SELF>: PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF: PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }

    public class FluentBuilderApproach
    {
        public static void Main(String[] args)
        {
            var me = Person.New
                .Called("Dmitri")
                .WorksAsA("Quant")
                .Build();
            Console.WriteLine(me);
        }
    }
}
