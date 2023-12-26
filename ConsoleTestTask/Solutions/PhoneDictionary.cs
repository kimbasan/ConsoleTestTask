using ConsoleTestTask.Util;

namespace ConsoleTestTask.Solutions
{
    internal class PhoneDictionary
    {
        private static Dictionary<int, List<Person>> phoneBook;

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
                    case 5: FindByName(); break;
                    case 6: repeat = false; break;
                }
            } while (repeat);
        }

        private static void PopulatePhoneBook()
        {
            phoneBook = new Dictionary<int, List<Person>>();
            AddOrUpdate(new Person("Иван Иванович Иванов", "8029445511"));
            AddOrUpdate(new Person("Иван Иванович Иванов", "8453445711"));
            AddOrUpdate(new Person("Сергей Сергеевич Сергеев", "802934356123"));
            AddOrUpdate(new Person("Петр Петрович Петров", "802911231231"));
            AddOrUpdate(new Person("Иванов Иванович Иван", "8786545511"));
        }

        private static string GetNameFromInput()
        {
            string name = InputHandler.GetStringLetters("Введите имя:");
            string middle = InputHandler.GetStringLetters("Введите отчество:");
            string surname = InputHandler.GetStringLetters("Введите Фамилию: ");
            return string.Format($"{name} {middle} {surname}");
        }

        private static void FindByName()
        {
            string name = GetNameFromInput();
            PrintSearch(name, FindPersonByName(name));
        }

        private static List<Person> FindPersonByName(string name)
        {
            List<Person> result = phoneBook[name.GetHashCode()];
            return result.FindAll(person => person.GetName().Equals(name));
        }

        private static void PrintSearch(string name, List<Person> searchResults)
        {
            if (searchResults != null && searchResults.Count > 0)
            {
                Console.WriteLine($"Для ФИО {name} найдено:");
                foreach (var person in searchResults)
                {
                    Console.WriteLine($"номер: {person.GetPhone()}");
                }
            }
            else
            {
                Console.WriteLine($"Для ФИО {name} номер не найден.");
            }
            InputHandler.PauseForAnyKey();
        }

        private static int SelectPerson(string name, List<Person> searchResults)
        {
            if (searchResults.Count == 1)
            {
                return 0;
            }
            else
            {
                Console.WriteLine($"Для ФИО {name} найдено:");
                int i = 0;
                foreach (var person in searchResults)
                {
                    Console.WriteLine($"{i} - {person.GetName()} номер: {person.GetPhone()}");
                    i++;
                }
                int select = InputHandler.GetInt("Выберите запись: ", 0, i - 1);
                return select;
            }
        }


        private static void PrintBook()
        {
            Console.WriteLine("Справочник:");
            Console.WriteLine("______________________");
            foreach (var pair in phoneBook)
            {
                foreach (var person in pair.Value)
                {
                    Console.WriteLine($"|{person.GetName()}: {person.GetPhone()}|");
                }
            }
            Console.WriteLine("______________________");
        }
        private static void AddRecord()
        {
            string newName = GetNameFromInput();
            string phone = InputHandler.GetStringNumbers("Введите телефон: ");
            AddOrUpdate(new Person(newName, phone));
            Console.WriteLine("Запись добавлена.");

            InputHandler.PauseForAnyKey();
        }

        private static void DeleteRecord()
        {
            string name = GetNameFromInput();
            List<Person> dictionaryList = phoneBook[name.GetHashCode()];
            if (dictionaryList != null && dictionaryList.Count > 0)
            {
                List<Person> searchResult = FindPersonByName(name);
                if (searchResult.Count > 0)
                {
                    int personToDelete = SelectPerson(name, searchResult);
                    dictionaryList.RemoveAt(personToDelete);
                    phoneBook[name.GetHashCode()] = dictionaryList;
                    Console.WriteLine("Запись удалена.");
                }
                else
                {
                    Console.WriteLine("Для введенной ФИО запись не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Для введенной ФИО запись не найдена.");
            }
            InputHandler.PauseForAnyKey();
        }

        private static void EditRecord()
        {
            string name = GetNameFromInput();
            List<Person> dictionaryList = phoneBook[name.GetHashCode()];
            if (dictionaryList != null && dictionaryList.Count > 0)
            {
                List<Person> searchResult = FindPersonByName(name);
                if (searchResult.Count > 0)
                {
                    int personToEdit = SelectPerson(name, searchResult);

                    Console.WriteLine($"Текущий номер телефона: {searchResult[personToEdit].GetPhone()}");
                    var newNumber = InputHandler.GetStringNumbers("Введите новый номер телефона: ");
                    searchResult[personToEdit].SetPhoneNumber(newNumber);

                    Console.WriteLine("Запись изменена.");
                }
                else
                {
                    Console.WriteLine("Для введенной ФИО запись не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Для введенной ФИО запись не найдена.");
            }
            InputHandler.PauseForAnyKey();
        }

        private static void AddOrUpdate(Person person)
        {
            var hash = person.GetName().GetHashCode();
            if (phoneBook.ContainsKey(hash))
            {
                phoneBook[hash].Add(person);
            }
            else
            {
                phoneBook.Add(hash, new List<Person> { person });
            }
        }
    }

    internal class Person
    {
        private string name;

        private string phoneNumber;

        public Person(string name, string phone)
        {
            this.name = name;
            this.phoneNumber = phone;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetPhone()
        {
            return phoneNumber;
        }

        public void SetPhoneNumber(string phone)
        {
            phoneNumber = phone;
        }

        public override bool Equals(object? obj)
        {
            return obj is Person person &&
                   name == person.name &&
                   phoneNumber == person.phoneNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, phoneNumber);
        }

        public override string ToString()
        {
            return string.Format($"{name} {phoneNumber}");
        }
    }
}
