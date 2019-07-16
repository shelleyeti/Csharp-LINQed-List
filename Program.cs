using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQed_List
{
    class Program
    {
        static void Main(string[] args)
        {
            //Restriction/Filtering Operations
            // Find the words in the collection that start with the letter 'L'
            List<string> fruits = new List<string>() { "Lemon", "Apple", "Orange", "Lime", "Watermelon", "Loganberry" };

            IEnumerable<string> LFruits = from fruit in fruits
                                          where fruit.StartsWith("l")
                                          select fruit;

            Console.WriteLine("L Fruits");
            foreach (var fruit in LFruits)
            {
                Console.WriteLine(fruit);
            }

            // Which of the following numbers are multiples of 4 or 6
            List<int> numbers = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };

            IEnumerable<int> fourSixMultiples = numbers.Where(x => (x % 4 == 0) || (x % 6 == 0));

            Console.WriteLine("Multiple of 4 and 6");
            foreach (var n in fourSixMultiples)
            {
                Console.WriteLine(n);
            }

            //Ordering Operations
            // Order these student names alphabetically, in descending order (Z to A)
            List<string> names = new List<string>()
            {
                "Heather", "James", "Xavier", "Michelle", "Brian", "Nina",
                "Kathleen", "Sophia", "Amir", "Douglas", "Zarley", "Beatrice",
                "Theodora", "William", "Svetlana", "Charisse", "Yolanda",
                "Gregorio", "Jean-Paul", "Evangelina", "Viktor", "Jacqueline",
                "Francisco", "Tre"
            };

            List<string> descend = names.OrderByDescending(x => x).ToList();

            Console.WriteLine("Decending order of words");
            foreach (var name in descend)
            {
                Console.WriteLine(name);
            }

            // Build a collection of these numbers sorted in ascending order
            List<int> numbers2 = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };
            List<int> ascend = numbers2.OrderBy(x => x).ToList();

            Console.WriteLine("Ascending order of numbers");
            foreach (var n in ascend)
            {
                Console.WriteLine(n);
            }

            //Aggregate Operations
            // Output how many numbers are in this list
            List<int> numbers3 = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };
            Console.WriteLine($"Number of items in a list {numbers3.Count()}");

            // How much money have we made?
            List<double> purchases = new List<double>()
            {
                2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
            };
            Console.WriteLine($"Sum of purhcases {purchases.Sum()}");

            // What is our most expensive product?
            List<double> prices = new List<double>()
            {
                879.45, 9442.85, 2454.63, 45.65, 2340.29, 34.03, 4786.45, 745.31, 21.76
            };
            Console.WriteLine($"Most espensivest {prices.Max()}");


            //Partitioning Operations
            /*
                Store each number in the following List until a perfect square is detected.

                Ref: https://msdn.microsoft.com/en-us/library/system.math.sqrt(v=vs.110).aspx
            */
            List<int> wheresSquaredo = new List<int>()
            {
                66, 12, 8, 27, 82, 34, 7, 50, 19, 46, 81, 23, 30, 4, 68, 14
            };

            var perfectSquare = wheresSquaredo.TakeWhile(n =>
            {
                int num = Convert.ToInt32(Math.Sqrt(n));
                return num * num != n;
            });

            perfectSquare.ToList().ForEach(num => Console.WriteLine(num));

            var altSquaredo = wheresSquaredo.TakeWhile(num => Math.Sqrt(num) % 1 != 0);

            Console.WriteLine("Perfect Square Root");
            foreach (var num in altSquaredo)
            {
                Console.WriteLine(num);
            }

            //Millionaires
            List<Customer> customers = new List<Customer>()
            {
                new Customer(){ Name="Bob Lesman", Balance=80345.66, Bank="FTB"},
                new Customer(){ Name="Joe Landy", Balance=9284756.21, Bank="WF"},
                new Customer(){ Name="Meg Ford", Balance=487233.01, Bank="BOA"},
                new Customer(){ Name="Peg Vale", Balance=7001449.92, Bank="BOA"},
                new Customer(){ Name="Mike Johnson", Balance=790872.12, Bank="WF"},
                new Customer(){ Name="Les Paul", Balance=8374892.54, Bank="WF"},
                new Customer(){ Name="Sid Crosby", Balance=957436.39, Bank="FTB"},
                new Customer(){ Name="Sarah Ng", Balance=56562389.85, Bank="FTB"},
                new Customer(){ Name="Tina Fey", Balance=1000000.00, Bank="CITI"},
                new Customer(){ Name="Sid Brown", Balance=49582.68, Bank="CITI"}
            };

            /*
                Given the same customer set, display how many millionaires per bank.
                Ref: https://stackoverflow.com/questions/7325278/group-by-in-linq

                Example Output:
                WF 2
                BOA 1
                FTB 1
                CITI 1
            */
            var results = from mill in customers
                          where mill.Balance >= 1000000
                          group mill.Name by mill.Bank into g
                          select g.Key + " " + g.Count();

            Console.WriteLine("Millionaiares per bank:");
            foreach (var bank in results)
            {
                Console.WriteLine(bank);
            }

            //Alternative Names of millionaires
            IEnumerable<Customer> millClub = customers.Where(c => c.Balance >= 1000000);
            Console.WriteLine("These people are millionaires");
            foreach (var c in millClub)
            {
                Console.WriteLine(c.Name);
            }

            //Alternative
            IEnumerable<IGrouping<string, Customer>> millPerBank = millClub.GroupBy(customer => customer.Bank);

            Console.WriteLine("List of Millioniares per bank alternative");
            foreach (var m in millPerBank)
            {
                Console.WriteLine($"{m.Key} {m.Count()}");
            }

            /*
                TASK:
                As in the previous exercise, you're going to output the millionaires,
                but you will also display the full name of the bank. You also need
                to sort the millionaires' names, ascending by their LAST name.

                Example output:
                    Tina Fey at Citibank
                    Joe Landy at Wells Fargo
                    Sarah Ng at First Tennessee
                    Les Paul at Wells Fargo
                    Peg Vale at Bank of America
            */
            // Create some banks and store in a List
            List<Bank> banks = new List<Bank>() {
            new Bank(){ Name="First Tennessee", Symbol="FTB"},
            new Bank(){ Name="Wells Fargo", Symbol="WF"},
            new Bank(){ Name="Bank of America", Symbol="BOA"},
            new Bank(){ Name="Citibank", Symbol="CITI"},
        };
            /*
                You will need to use the `Where()`
                and `Select()` methods to generate
                instances of the following class.
            */

            List<ReportItem> millionaireReport = (from customer in customers
                                                  join bank in banks on customer.Bank equals bank.Symbol
                                                  where customer.Balance >= 1000000
                                                  //default splits on the space
                                                  orderby customer.Name.Split()[1]
                                                  select new ReportItem
                                                  {
                                                      CustomerName = customer.Name,
                                                      BankName = bank.Name
                                                  }).ToList();

            Console.WriteLine("Millionaires per bank report");
            foreach (var item in millionaireReport)
            {
                Console.WriteLine($"{item.CustomerName} at {item.BankName}");
            }
        }
    }
}
