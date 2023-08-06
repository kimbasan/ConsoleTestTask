using System.Numerics;
using ConsoleTestTask.Util;

namespace ConsoleTestTask.Solutions
{
    internal class Recursion
    {
        private static readonly double FibonacchiA = (1 + Math.Sqrt(5)) / 2;
        private static readonly double FibonacchiB = (1 - Math.Sqrt(5)) / 2;
        public static void GetNumberFromFibonacchiSequence()
        {
            int number = InputHandler.GetInt("Введите номер числа в последовательности: ", 0, int.MaxValue);

            BigInteger result = GetFibonacchiNumberByIndex(number);
            Console.WriteLine($"Число на позиции {number} в последовательности Фибоначчи: {result}.");
            InputHandler.PauseForAnyKey();
        }

        private static BigInteger GetFibonacchiNumberByIndex(int index)
        {
            return new BigInteger((Math.Pow(FibonacchiA, index) - Math.Pow(FibonacchiB, index)) / Math.Sqrt(5));
        }

        public static void GetNumberInPower()
        {
            float number = InputHandler.GetFloat("Введите число (пример: 1,5): ");
            int power = InputHandler.GetInt("Введите целую степень: ");

            float result = NumberInPower(number, Math.Abs(power));
            if (power < 0)
            {
                result = 1 / result;
            }

            Console.WriteLine($"Число {number} в степени {power}: {result}.");
            InputHandler.PauseForAnyKey();
        }
        private static float NumberInPower(float number, int power)
        {
            if (power == 1)
            {
                return number;
            }
            else if (power == 0)
            {
                return 1;
            }
            else
            {
                return number * NumberInPower(number, power - 1);
            }
        }
    }
}
