using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsAgainstHumanity
{
    public class UI : IUI
    {
        private LimitedList<string> _messageLog;

        public int Height => Console.WindowHeight;
        public int Width => Console.WindowWidth;
        public int HalfWay => Width / 2;

        public UI()
        {
            _messageLog = new LimitedList<string>(Height);
        }

        public void Write(string message) { Console.Write(message); }

        public void WriteLine(string message) { Console.WriteLine(message); }

        public ConsoleKey ReadKey() => Console.ReadKey(intercept: true).Key;

        public string ReadLine()
        {
            string input;
            do
            {
                input = Console.ReadLine()!.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    AddToMessageLog("Input a valid option.");
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        public bool AskForBool(List<string> menu)
        {
            bool input = false;
            int choice = MenuPaged(2, menu);
            if (choice == 1)
                input = true;
            else if (choice == 2)
                input = false;
            return input;
        }

        public string AskForString(string prompt)
        {
            bool fail = true;
            string input;
            do
            {
                AddToMessageLog($"{prompt}: ");
                input = Console.ReadLine()!.Trim();
                if (string.IsNullOrWhiteSpace(input))
                    AddToMessageLog($"You have to supply a valid {prompt.ToLower()}");
                else
                    fail = false;
            } while (fail);
            return input;
        }

        public int AskForInt(string prompt)
        {
            do
            {
                string given = AskForString(prompt);
                if (int.TryParse(given, out int result))
                    return result;
            } while (true);
        }

        public void Clear()
        {
            string blanker = new string(' ', HalfWay - 1);
            ResetPosition();
            for (int i = 0; i < Height; i++)
            {
                WriteLine(blanker);
            }
            ResetPosition();
        }

        public void Initialize()
        {
            Console.CursorVisible = false;
            PrintMessageLog();
        }

        public void ResetPosition()
        {
            Console.SetCursorPosition(0, 0);
        }

        public void MenuHighlight()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void MenuNotSelected()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void PrintMenu(int choice, List<string> menu)
        {
            ResetPosition();
            for (int i = 1; i < menu.Count + 1; i++)
            {
                if (i == choice)
                {
                    MenuHighlight();
                    WriteLine(menu[i - 1]);
                    MenuNotSelected();
                }
                else
                {
                    WriteLine(menu[i - 1]);
                }
            }
        }

        public int MenuPaged(int choice, List<string> menu)
        {
            bool notChosen = true;
            int max = Height - 1;
            int currentPage = 1;
            int pages = (menu.Count / max) + 1;
            int lastPage = menu.Count % max;
            Manager Manager = new Manager(this);
            if (lastPage == 0)
            {
                lastPage = max;
                pages--;
            }
            Clear();
            AddToMessageLog("Choose an option using the up & down arrow keys & enter:");
            AddToMessageLog($"Page {currentPage} of {pages}.");
            do
            {
                if (currentPage == pages)
                    PrintMenu(choice, menu.GetRange((currentPage - 1) * max, lastPage));
                else
                    PrintMenu(choice, menu.GetRange((currentPage - 1) * max, max));
                ConsoleKey keyPress = ReadKey();
                switch (keyPress)
                {
                    case ConsoleKey.UpArrow:
                        choice--;
                        if (choice == 0)
                        {
                            if (currentPage == 1)
                            {
                                currentPage = pages;
                                choice = lastPage;
                            }
                            else
                            {
                                choice = max;
                                currentPage--;
                            }
                            AddToMessageLog($"Page {currentPage} of {pages}.");
                            Clear();
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        choice++;
                        if (currentPage == pages && choice == lastPage + 1)
                        {
                            choice = 1;
                            currentPage = 1;
                            AddToMessageLog($"Page {currentPage} of {pages}.");
                            Clear();
                        }
                        if (choice == max + 1)
                        {
                            choice = 1;
                            currentPage++;
                            AddToMessageLog($"Page {currentPage} of {pages}.");
                            Clear();
                        }
                        break;
                    case ConsoleKey.Enter:
                        notChosen = false;
                        break;
                    case ConsoleKey.Escape:
                        Manager.MainMenu();
                        break;
                }
            } while (notChosen);
            return ((currentPage - 1) * Height) + choice - 1;
        }

        public void PrintMessageLog()
        {
            PrintDivider();
            int i = 0;
            foreach (string message in _messageLog)
            {
                Console.SetCursorPosition(HalfWay + 2, i);
                WriteLine(message + new string(' ', HalfWay - message.Length - 2));
                i++;
            }
            Clear();
        }

        private void PrintDivider()
        {
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(HalfWay, i);
                Console.Write(" ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void AddToMessageLog(string message)
        {
            _messageLog.Add(message);
            PrintMessageLog();
        }

        public void Quit()
        {
            System.Environment.Exit(0);
        }
    }
}
