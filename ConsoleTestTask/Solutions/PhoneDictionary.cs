using ConsoleTestTask.Util;

namespace ConsoleTestTask.Solutions
{
    internal class PhoneDictionary
    {
        private static Dictionary<Person, string> phoneBook;

        public static void Start()
        {
            PopulatePhoneBook();

            bool repeat = true;
            var menuItems = new string[] {
                "1 - Вывести записи",
                "2 - Добавить запись",
                "3 - Изменить запись",
                "4 - Удалить запись",
                "5 - Найти запись по ФИО",
                "6 - Выход"
            };
            do
            {
                PrintBook();
                int choice = InputHandler.GetMenuSelection(menuItems);
                switch (choice)
                {
                    case 1: PrintBook(); break;
                    case 2: AddRecord(); break;
                    case 3: EditRecord(); break;
                    case 4: DeleteRecord(); break;
                    case 5: FindRecordByPerson(); break;
                    case 6: repeat = false; break;
                }

            } while (repeat);
        }

        private static void PopulatePhoneBook()
        {
            phoneBook = new Dictionary<Person, string>
            {
                { new Person("Иван", "Иванович", "Иванов"), "8029445511" },
                { new Person("Сергей", "Сергеевич", "Сергеев"), "802934356123" },
                { new Person("Петр", "Петрович", "Петров"), "802911231231" }
            };
        }

        private static void FindRecordByPerson()
        {
            var person = CreatePersonFromInput();
            string result = null;

            if (phoneBook.TryGetValue(person, out result))
            {
                Console.WriteLine($"Для ФИО {person} Найден номер: {result}.");

            }
            else
            {
                Console.WriteLine($"Для ФИО {person} номер не найден.");
            }
            InputHandler.PauseForAnyKey();
        }

        private static Person CreatePersonFromInput()
        {
            string firstName = InputHandler.GetStringLetters("Введите имя: ");
            string surname = InputHandler.GetStringLetters("Введите отчество: ");
            string lastName = InputHandler.GetStringLetters("Введите фамилию: ");
            return new Person(firstName, surname, lastName);
        }

        private static void PrintBook()
        {
            Console.WriteLine("Справочник:");
            Console.WriteLine("______________________");
            foreach (var pair in phoneBook)
            {
                Console.WriteLine($"|{pair.Key}: {pair.Value}|");
            }
            Console.WriteLine("______________________");
        }
        private static void AddRecord()
        {
            var newPerson = CreatePersonFromInput();
            string phone = InputHandler.GetStringNumbers("Введите телефон: ");
            if (phoneBook.TryAdd(newPerson, phone))
            {
                Console.WriteLine("Запись добавлена.");
            }
            else
            {
                Console.WriteLine("Для данной ФИО уже есть запись.");
            }
            InputHandler.PauseForAnyKey();
        }

        private static void DeleteRecord()
        {
            var searchPerson = CreatePersonFromInput();
            if (phoneBook.Remove(searchPerson))
            {
                Console.WriteLine("Запись удалена.");
            }
            else
            {
                Console.WriteLine("Для введенной ФИО запись не найдена.");
            }
            InputHandler.PauseForAnyKey();
        }

        private static void EditRecord()
        {
            var searchPerson = CreatePersonFromInput();
            string phoneNumber = null;
            if (phoneBook.TryGetValue(searchPerson, out phoneNumber) && phoneNumber != null) 
            {
                Console.WriteLine($"Текущий номер телефона: {phoneNumber}");
                var newNumber = InputHandler.GetStringNumbers("Введите новый номер телефона: ");
                phoneBook[searchPerson] = newNumber;
                Console.WriteLine("Запись обновлена.");
            }
            else
            {
                Console.WriteLine("Для введенной ФИО запись не найдена.");
            }
            InputHandler.PauseForAnyKey();
        }
    }

    internal class Person
    {
        private string firstName;
        private string surname;
        private string lastName;

        public Person(string firstName, string surname, string lastName)
        {
            this.firstName = firstName;
            this.surname = surname;
            this.lastName = lastName;
        }

        public void SetFirstName(string newFirstName)
        {
            firstName = newFirstName;
        }

        public void SetSurname(string newSurname)
        {
            surname = newSurname;
        }

        public void SetLastName(string newLastName)
        {
            lastName = newLastName;
        }

        public override bool Equals(object? obj)
        {
            return obj is Person person &&
                   firstName == person.firstName &&
                   surname == person.surname &&
                   lastName == person.lastName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(firstName, surname, lastName);
        }

        public override string ToString()
        {
            return string.Format($"{firstName} {surname} {lastName}");
        }
    }
}
