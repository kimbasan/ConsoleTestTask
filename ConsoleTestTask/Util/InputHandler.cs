namespace ConsoleTestTask.Util
{
    internal class InputHandler
    {
        public static double GetDouble(string message)
        {
            return DoubleConverter.GetValue(message, double.Parse);
        }
        public static float GetFloat(string message)
        {
            return FloatConverter.GetValue(message, float.Parse);
        }

        public static int GetInt(string message, int min = int.MinValue, int max = int.MaxValue)
        {
            return IntConverter.GetValueInRange(message, min, max);
        }

        public static string GetStringNumbers(string message)
        {
            return GetString(message, char.IsDigit, "Можно вводить только цифры.");
        }

        public static string GetStringLetters(string message)
        {
            return GetString(message, char.IsLetter, "Можно вводить только буквы.");
        }

        private static string GetString(string message, Func<char, bool> validation, string validationFailedMessage)
        {
            return StringValidator.GetValueValidated(message, validation, validationFailedMessage);
        }

        public static int GetMenuSelection(string[] messages)
        {
            foreach (string message in messages)
            {
                Console.WriteLine(message);
            }
            return IntConverter.GetValueInRange("Введите номер пункта меню: ", 1, messages.Length);
        }

        public static void PauseForAnyKey()
        {
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadLine();
        }
    }

    class InputConverter<T>
    {
        public static T GetValue(string message, Func<string, T> parse)
        {
            bool repeat = true;
            T value = default;
            do
            {
                Console.WriteLine(message);
                string stringValue = Console.ReadLine();

                try
                {
                    value = parse(stringValue);
                    repeat = false;
                }
                catch
                {
                    Console.WriteLine("Неправильный ввод.");
                    repeat = true;
                }

            } while (repeat);

            return value;
        }
    }

    class DoubleConverter : InputConverter<double>
    {

    }

    class FloatConverter : InputConverter<float>
    {

    }

    class IntConverter : InputConverter<int>
    {
        public static int GetValueInRange(string message, int min, int max)
        {
            int result = 0;
            bool repeat = true;
            do
            {
                result = GetValue(message, int.Parse);
                if (result < min || result > max)
                {
                    Console.WriteLine($"Ввод должен быть между {min} и {max}");
                }
                else
                {
                    repeat = false;
                }
            } while (repeat);

            return result;
        }
    }

    class StringValidator : InputConverter<string>
    {
        public static string GetValue(string message)
        {
            return GetValue(message, (a) => a);
        }

        public static string GetValueValidated(string message, Func<char, bool> validation, string validationFailedMessage)
        {
            bool repeat = true;
            string value = null;
            do
            {
                value = GetValue(message);
                if (value != null && value.Length > 0 && value.All(validation))
                {
                    repeat = false;
                }
                else
                {
                    Console.WriteLine(validationFailedMessage);
                }
            } while (repeat);
            return value;
        }
    }
}
