using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDesign.LLD.BuilderDesignPattern.FunctionalBuilder
{
    public class Person
    {
        public string Name, Position;
    }

    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf: FunctionalBuilder<TSubject, TSelf>
        where TSubject: new()
    {
        private readonly List<Func<Person, Person>> actions
            = new List<Func<Person, Person>>();


        public TSelf Do(Action<Person> action)
        {
            return AddAction(action);
        }

        public Person Build()
        {
            return actions.Aggregate(new Person(), (p, f) => f(p));
        }
        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });
            return (TSelf)this;
        }
    }

    public sealed class PersonBuilder2 : FunctionalBuilder<Person, PersonBuilder2>
    {
        public PersonBuilder2 Called (string name) 
            => Do(p => p.Name = name);
    }

    public sealed class PersonBuilder
    {
        private readonly List<Func<Person, Person>> actions 
            = new List<Func<Person, Person>>();

        public PersonBuilder Called(string name) => Do(p => p.Name = name);

        public PersonBuilder Do(Action<Person> action)
        {
            return AddAction(action);
        }

        public Person Build()
        {
            return actions.Aggregate(new Person(), (p, f) => f(p));
        }
        private PersonBuilder AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });
            return this;
        }
    }

    public static class PersonBuilderExtension
    {
        public static PersonBuilder WorksAs(this PersonBuilder builder, string position) 
        { 
            return builder.Do(p => p.Position = position);
        }
    }

    public class FunctionalBuilder
    {
        public static void Main(String[] args)
        {
            var person = new PersonBuilder()
                .Called("Dmitri")
                .WorksAs("Developer")
                .Build();
            var person2 = new PersonBuilder2()
                .Called("Dmitri")
                .Build();
        }
    }
}
