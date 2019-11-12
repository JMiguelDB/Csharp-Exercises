using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Car myCar = new Car {Make = "Oldmobile", Model = "Cutlas Supreme", Year = 1998, Color = "Yellow" };
            Console.WriteLine("{0} {1} {2} {3}", myCar.Make, myCar.Model, myCar.Year, myCar.Color);
            Console.WriteLine("{0:C}", myCar.DetermineMarketValue());

            //Collection initializer
            List<Car> myList = new List<Car>(){
                new Car {Make = "Oldmobile", Model = "Cutlas Supreme", Year = 1998, Color = "Yellow" },
                new Car {Make = "Toyota", Model = "F1 Zero", Year = 2015, Color = "Grey" },
                new Car {Make = "Toyota", Model = "F2", Year = 2018, Color = "Red" }
            };
            Console.WriteLine(myList[0].Make);

            // LINQ query
            /*
            var toyotas = from car in myList
                          where car.Make == "Toyota"
                          select car;

            */
            var orderedCars = from car in myList
                              orderby car.Year descending
                              select car;

            var newCars = from car in myList
                          where car.Make == "Toyota"
                          && car.Year == 2015
                          select new { car.Make, car.Model };
            // LINQ method
            var toyotas = myList.Where(car => car.Make == "Toyota" && car.Year == 2018);
            var firstToyota = myList.OrderByDescending(car => car.Year).First(car => car.Make == "Toyota");

            myList.ForEach(car => Console.WriteLine("{0} {1}", car.Make, car.Model));
            Console.WriteLine(myList.Exists(car => car.Model == "F2"));

            foreach(var car in toyotas)
            {
                Console.WriteLine("{0} {1}", car.Model, car.Color);
            }

            Console.WriteLine(myList.GetType());

            Console.ReadLine();
        }
    }

    class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        public Car()
        {
            Make = "Nissan";
        }

        public decimal DetermineMarketValue()
        {
            decimal carValue;
            if (Year > 1990)
                carValue = 10000;
            else
                carValue = 2000;

            return carValue;
        }
    }
}
