using System;
using System.Collections.Generic;
using System.Linq;

namespace ProportionLogic
{
    class Program
    {
        private static Dictionary<int, string> _entryValues;
        private static int _numberOfItems;
        private static List<int> _userSelections;
        static void Main(string[] args)
        {
            SetEntryValues();
            _userSelections = new List<int>();
            Console.WriteLine("The breakfast machine!!");
            Console.WriteLine("Menu on Offer");
            Console.WriteLine("Input | Item");
            Console.WriteLine("-------------------------------------");
            foreach (var item in _entryValues)
            {
                Console.WriteLine($"{item.Key}     | {item.Value}");
            }
            _numberOfItems = SelectNumberOfItems();
            for (var x = 1; x <= _numberOfItems; x++)
            {
                _userSelections.Add(SelectBreakfastItem(x));
            }
            Console.WriteLine(CalculateProportion());
            Console.ReadLine();
        }
        private static void SetEntryValues()
        {
            _entryValues = new Dictionary<int, string>();
            _entryValues.TryAdd(0, "None Selected");
            _entryValues.TryAdd(1, "Bacon");
            _entryValues.TryAdd(2, "Sausage");
            _entryValues.TryAdd(3, "Egg");
            _entryValues.TryAdd(4, "Beans");
            _entryValues.TryAdd(5, "Toast");
        }
        private static int SelectNumberOfItems()
        {
            Console.WriteLine("How many breakfast items?  ");
            int n;

            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.Clear();
                Console.WriteLine("You entered an invalid number");
                Console.Write("How many breakfast items? ");
            }
            return n;
        }
        private static int SelectBreakfastItem(int currentItem)
        {
            Console.WriteLine($"[{currentItem}/{_numberOfItems}]Which Item?  ");
            int n;

            while (!int.TryParse(Console.ReadLine(), out n) && _entryValues.ContainsKey(n))
            {
                Console.Clear();
                Console.WriteLine("You entered an invalid number");
                Console.Write($"[{currentItem}/{_numberOfItems}]Which Item? ");
            }
            return n;
        }
        private static string CalculateProportion()
        {
            string result = "";
            var proportionResults = new List<Tuple<string, int>>();
            foreach(var value in _entryValues)
            {
                if (value.Key == 0)
                    continue;

                var resultValue = _userSelections.Count(u => u == value.Key);
                proportionResults.Add(Tuple.Create(value.Value, resultValue));
            }

            var nonSelectedCount = _userSelections.Count(u => u == 0);
            var totalValidResults = _userSelections.Count - nonSelectedCount;

            if (totalValidResults == 0)
            {
                return "Not Detected";
            }

            foreach (var proportionResult in proportionResults)
            {
                if (proportionResult.Item2 > 0)
                {
                    double value = (double)proportionResult.Item2 / totalValidResults * 100;
                    result += $"{value.ToString("n2")}% '{proportionResult.Item1}'{Environment.NewLine}";
                }
            }
            return result;
        }
    }
}
