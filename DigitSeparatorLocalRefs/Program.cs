using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitSeparatorLocalRefs
{
    class Program
    {
        static void Main(string[] args)
        {
            UsingDigitSeparators();
            BinaryLiterals();
            ReturnByReference();
            
            
            Console.ReadKey();
        }

        static ref int ReturnByReference()
        {
            int[] arr = { 1 };
            ref int x = ref arr[0];
            Console.WriteLine( "x:{0} | arr[0]: {1}",x,arr[0]);
            return ref x;
        }

        static void BinaryLiterals()
        {
            // Creating binary literals  
            // by prefixing with 0b 
            var num1 = 0b1001;
            var num2 = 0b01000011;

            Console.WriteLine("Value of Num1 is: " + num1);
            Console.WriteLine("Value of Num2 is: " + num2);
            Console.WriteLine("Char value of Num2 is: {0}",
                                     Convert.ToChar(num2));
        }

        static void UsingDigitSeparators()
        {

            const int assumedPopulation = 1_122_339_654;
            Console.WriteLine(assumedPopulation);
            
        }
    }
}
