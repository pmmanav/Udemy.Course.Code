using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDesign.LLD.BuilderDesignPattern
{
    public enum CarType
    {
        Sedan,
        Crossover
    }

    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }

    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheelSize(int size);
    }

    public interface IBuildCar 
    {
        public Car Build();
    }

    public class CarBuilder
    {
        private class Impl :
            ISpecifyCarType,
            ISpecifyWheelSize,
            IBuildCar
        {
            private Car car = new Car();
            public Car Build()
            {
                return car;
            }

            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }

            public IBuildCar WithWheelSize(int size)
            {
                switch (car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                        throw new InvalidOperationException("Crossover should have wheel size of 17 or more");
                        break;
                    case CarType.Sedan when size < 15 || size > 18:
                        throw new InvalidOperationException("Sedan should have wheel size of 15 or more");
                        break;
                }
                car.WheelSize = size;
                return this;
            }
        }
        public static ISpecifyCarType Create()
        {
            return new Impl();
        }
    }

    public class StepWiseBuilder
    {
        public static void Main(String[] args)
        {
            Car car = CarBuilder.Create()
                .OfType(CarType.Crossover)
                .WithWheelSize(18)
                .Build();
        }
    }
}
