using ConsoleTestTask.Solutions;
using ConsoleTestTask.Util;

public class Runner
{
    public static void Main(String[] args)
    {
        var menuItems = new string[] {
            "1 - 1a: Нахождение суммы чисел главной диагонали матрицы",
            "2 - 1b: Нахождение суммы чисел кратных 3 в матрице",
            "3 - 2a: Число Фибоначчи по номеру в последовательности",
            "4 - 2b: Возведение в степень",
            "5 - 3: Справочник контактов",
            "6 - 4: Геометрические фигуры",
            "7 - Выход"
        };
        bool repeat = true;
        do
        {
            int choice = InputHandler.GetMenuSelection(menuItems);
            
            switch (choice)
            {
                case 1: Matrix.GetMainDiagonalSum(); break;
                case 2: Matrix.GetSumOfNumbersMultipleOfThree(); break;
                case 3: Recursion.GetNumberFromFibonacchiSequence(); break;
                case 4: Recursion.GetNumberInPower(); break;
                case 5: new PhoneDictionary().Start(); break;
                case 6: new Shapes().Start(); break;
                case 7: repeat = false; break;
            }
        } while (repeat);
    }
}