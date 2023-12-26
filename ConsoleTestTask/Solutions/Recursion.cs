using System.Numerics;
using ConsoleTestTask.Util;

namespace ConsoleTestTask.Solutions
{
    internal class Recursion
    {
        public static void GetNumberFromFibonacchiSequence()
        {
            int number = InputHandler.GetInt("Введите номер числа в последовательности: ", 0, int.MaxValue);

            BigInteger result = GetFibonacchiNumberByIndex(number);
            Console.WriteLine($"Число на позиции {number} в последовательности Фибоначчи: {result}.");
            InputHandler.PauseForAnyKey();
        }

        private static BigInteger GetFibonacchiNumberByIndex(int index)
        {
            if (index == 0 || index == 1)
            {
                return index;
            }
            else
            {
                return GetFibonacchiNumberByIndex(index - 1) + GetFibonacchiNumberByIndex(index - 2);
            }
        }

        public static void GetNumberInPower()
        {
            float number = InputHandler.GetFloat("Введите число (пример: 1,5): ");
            int power = InputHandler.GetInt("Введите целую степень: ");

            double result = NumberInPower(number, power);

            Console.WriteLine($"Число {number} в степени {power}: {result}.");
            InputHandler.PauseForAnyKey();
        }
        private static double NumberInPower(float number, int power)
        {
            if (power == 0)
            {
                return 1;
            }
            else if (power > 0)
            {
                return number * NumberInPower(number, power - 1);
            }
            else
            {
                return 1 / NumberInPower(number, -power);
            }
        }
    }
}
