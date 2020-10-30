using System;
using System.Collections.Generic;
using System.Text;

namespace first
{
    class Program
    {
        static void Main(string[] args)
        {
            Lesson1 lesson1 = new Lesson1();
            check(lesson1.convert_base("ff", 16, 10), "255"); //255
            check(lesson1.convert_base("ff", 16, 2), "11111111"); //255
            check(lesson1.convert_base("12121221", 3, 16), "1006"); //4102
            check(lesson1.convert_base("abba112", 13, 16), "323F6FE"); //52688638
            check(lesson1.convert_base("12345", 6, 2), "11101001001"); //1865
        }

        static private bool check(string value1, string value2) {
            if (value1 != value2) {
                Console.WriteLine("Test Failed: {0} != {1}", value1, value2);
                return false;
            }
            return true;
        }
    }

    class Lesson1
    {
        private Dictionary<char, int> Symbol_dict;

        public Lesson1() {
            Symbol_dict = new Dictionary<char, int>();
            Symbol_dict.Add('0', 0);
            Symbol_dict.Add('1', 1);
            Symbol_dict.Add('2', 2);
            Symbol_dict.Add('3', 3);
            Symbol_dict.Add('4', 4);
            Symbol_dict.Add('5', 5);
            Symbol_dict.Add('6', 6);
            Symbol_dict.Add('7', 7);
            Symbol_dict.Add('8', 8);
            Symbol_dict.Add('9', 9);
            Symbol_dict.Add('A', 10);
            Symbol_dict.Add('a', 10);
            Symbol_dict.Add('B', 11);
            Symbol_dict.Add('b', 11);
            Symbol_dict.Add('C', 12);
            Symbol_dict.Add('c', 12);
            Symbol_dict.Add('D', 13);
            Symbol_dict.Add('d', 13);
            Symbol_dict.Add('E', 14);
            Symbol_dict.Add('e', 14);
            Symbol_dict.Add('F', 15);
            Symbol_dict.Add('f', 15);
        }

        bool symbol_to_number(char symbol, out int value) {
            return Symbol_dict.TryGetValue(symbol, out value);
        }

        bool number_to_symbol(int number, out char symbol) {
            foreach(KeyValuePair<char, int> entry in Symbol_dict) {
                if (entry.Value == number) {
                    symbol = entry.Key;
                    return true;
                }
            }
            symbol = 'x';
            return false;
        }

        public string convert_base(string input, int base1, int base2) {
            int accum = 0;
            char[] c_array = input.ToCharArray();
            Array.Reverse(c_array);
            for(int i = 0; i < c_array.Length; i++) {
                if (symbol_to_number(c_array[i], out int num)) {
                    accum += (num * (int)Math.Pow((double)base1, (double)i));
                } else {
                    String msg = String.Format("Can't convert {0} to number, invalid symbol", c_array[i]);
                    Console.WriteLine(msg);
                }
            }
            //Console.WriteLine(String.Format("Intermediate: {0}", accum));
            StringBuilder result = new StringBuilder();
            for (int j = 1, num = 0; accum > 0; j++, accum -= num) {
                int denom = (int)(Math.Pow((double)base2, (double)j));
                num = accum % denom;
                number_to_symbol(num / (denom / base2), out char c);
                result.Insert(0, c);
            }
            return result.ToString();
        }
    }
}
