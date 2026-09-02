using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace CruzadorBankGit.Viewer
{
    internal class ConsoleUI
    {
        public void Head(string message, bool clear = true)
        {
            if (clear) Console.Clear();
            Console.WriteLine("===========================================================================");
            Console.WriteLine($"{message} \t\t\t\t\t {DateTime.Today.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}");
            Console.WriteLine("___________________________________________________________________________\n");
        }
        public int SetAndSelectionEnumOption<TKeyEnum, TValue>(Dictionary<TKeyEnum, TValue> mainDictionary, bool clear = false)
        {
            if (clear) Console.Clear();
            if (!typeof(TKeyEnum).IsEnum) throw new NotSupportedException("TKeyEnum should be an enum");

            foreach (var option in mainDictionary) Console.WriteLine($"[{Convert.ToInt32(option.Key)}] \t {option.Value}");

            Console.Write("\nEnter the chosen option: ");
            return Convert.ToInt32(Console.ReadLine());
        }
        public void SpecialMessage (string message, ConsoleColor color = ConsoleColor.Red, bool clear = true)
        {
            if (clear) Console.Clear();
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message), "message should be a valid, not null, empty or white Space message");
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.WriteLine("---------------------------------------------------------------------------\n");
            Console.ResetColor();
            Console.ReadKey();
        }
        public int GetInt(string message, bool clear = false)
        {
            if (clear) Console.Clear();
            Console.Write(message);
            return Convert.ToInt32(Console.ReadLine());
        }
        public decimal GetDecimal (string message,  bool clear = false)
        {
            if (clear) Console.Clear();
            Console.Write(message);
            return Convert.ToDecimal(Console.ReadLine());
        }
        public string GetString(string message, bool clear = false)
        {
            if (clear) Console.Clear();
            Console.Write(message);
            return Console.ReadLine();
        }
        public void ShowAccountData(ArrayList data, bool clear = false)
        {
            if (clear) Console.Clear();
            Console.WriteLine($"Name: \t\t\t\t\t {data[1]}");
            Console.WriteLine($"Account Id: \t\t\t\t {data[0]}");
            Console.WriteLine($"Balance: \t\t\t\t {data[2].ToString()}");
            Console.ReadKey();
        }
        public void ShowBalance (decimal value)
        {
            Console.Write("Current balance: . . . . . . ");
            Console.ForegroundColor = ConsoleColor.Green;
            if (value <  0) Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value);
            Console.ResetColor();
        }
    }
}
