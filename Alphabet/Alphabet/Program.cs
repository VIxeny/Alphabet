using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphabet
{
	internal class Program
	{

		static Random rand = new Random();
		static string input;

		private static int score = 0;
		private static int maxScore = 5;

		private static int place;

		private static List<char> letters;
		static void Main(string[] args)
		{
			ReadFile();

			List<char> russian = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray().ToList();
			List<char> english = "abcdefghijklmnopqrstuvwxyz".ToCharArray().ToList();

			Console.Clear();
			letters = ChooseLanguageAndExit(russian, english);
			ChooseTest();
		}

		static void ReadFile()
		{
			{

				string fileName = "ini.txt";
				string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
				try
				{
					using (StreamReader reader = new StreamReader(filePath))
					{
						string line = reader.ReadLine();

						if (line.Length >= 11)
						{
							if(int.TryParse(line.Substring(11), out int value)){
								maxScore = value;
								Console.WriteLine("нажмите любую клавишу чтобы продолжить");
							}
							else
							{
								Console.WriteLine("Проверьте файл в нём должна быть одна строка 'attmepts = x'" +
								" где x это целочисленное число и без пробелов в конце");
								Environment.Exit(0);
							}
							
						}
						else
						{
							Console.WriteLine("Проверьте файл в нём должна быть одна строка 'attmepts = x'" +
								" где x это целочисленное число и без пробелов в конце");
							Environment.Exit(0);
						}
					}
				}
				catch (FileNotFoundException)
				{
					Console.WriteLine("Файл не найден.");
					Environment.Exit(0);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
					Environment.Exit(0);
				}
				Console.ReadKey();
			}
		}

		static bool CheckInputInt(string input)
		{
			if (int.TryParse(input, out int userAnswerChar))
				return true;
			else
			{
				Console.WriteLine("Неверный тип данных ввода");
				return false;
			}
		}

		static bool CheckInputChar(string input)
		{
			if (char.TryParse(input, out char userAnswerChar))
				return true;
			else
			{
				Console.WriteLine("Неверный тип данных ввода");
				return false;
			}
		}

		static bool CheckInputLength(string input, int length)
		{
			if (input.Length >= length)
				return true;
			else
			{
				Console.WriteLine("мало букв");
				return false;
			}
		}

		static private List<char> ChooseLanguageAndExit(List<char> russian, List<char> english)
		{
			while (true)
			{
				Console.Write("Чтобы выйти пропишите \"выход\"");
				Console.SetCursorPosition(0, 3);

				Console.WriteLine("Выберите Язык:\n" +
					"	\"русский\" или \"английский\"\n");
				string language = Console.ReadLine();

				switch (language)
				{
					case "русский":
						return russian;
					case "английский":
						return english;
					case "выход":
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("Неверно введён язык");
						Console.WriteLine("Попробуйте ещё раз");
						Console.ReadKey();
						Console.Clear();
						break;
				}
			}

		}

		static private void ChooseTest()
		{
			while (true)
			{
				Console.Clear();
				Console.SetCursorPosition(0, 5);

				Console.WriteLine("Выберите тест (1, 2, 3):\n\n");

				Console.WriteLine($"  1:\n" +
					"	На каком месте стоит буква или на какая буква стоит на этом месте\n\n");
				Console.WriteLine("  2:\n" +
					"	Какая буква стоит перед этой буквой и какая после\n\n");
				Console.WriteLine("  3:\n" +
					"	Какие две буквы стоят перед этой буквой и какие две после\n\n");
				Console.WriteLine("  выход:\n" +
					"	если хотите закончить изучение алфавита\n\n");

				Console.Write("номер: ");
				input = Console.ReadLine();
				if (input == "выход")
					Environment.Exit(0);

				if (int.TryParse(input, out int number))
				{
					switch (number)
					{
						case 1:
							Test1(letters);
							break;
						case 2:
							Test2(letters);
							break;
						case 3:
							Test3(letters);
							break;

						default:
							Console.WriteLine("Неверно введён номер");
							break;
					}
				}
				else
				{
					Console.WriteLine("Попробуйте ещё раз");
				}
			}
		}

		private static void AddScore(bool isAnswerRight)
		{
			if (isAnswerRight)
			{
				Console.WriteLine("Поздравляю вы правы!");
				score++;
			}
			else
			{
				Console.WriteLine("К сожалению вы ошиблись");
			}
		}

		private static void Result()
		{
			Console.WriteLine("Ваш счёт равен: " + score);
			if (score == 5)
				Console.WriteLine("Потрясающий результат");
		}

		private static bool Restart()
		{
			while (true)
			{
				Console.WriteLine("попробовать ещё раз? (да/нет)");
				input = Console.ReadLine();
				if (input == "да")
				{
					return true;
				}
				else if (input == "нет")
				{
					return false;
				}
				else
				{
					Console.WriteLine("нераспознаная инструкция, попробуйте ещё раз");
				}
			}
		}

		private static void Test1(List<char> letters)
		{
			Console.Clear();
			score = 0;
			for (int i = 0; i < maxScore; i++)
			{
				place = rand.Next(letters.Count);

				switch (rand.Next(2))
				{
					case 0:
						Console.WriteLine("На каком месте стоит эта буква: " + letters[place]);
						input = Console.ReadLine();
						if (CheckInputInt(input))
							AddScore(Convert.ToInt32(input) == place);
						break;
					case 1:
						Console.WriteLine("Какая буква стоит на: " + (place + 1));
						input = Console.ReadLine();
						if (CheckInputChar(input))
							AddScore(Convert.ToInt32(input) == letters[place]);
						break;

				}
			}
			Result();
			if (Restart())
				Test1(letters);
		}

		private static void Test2(List<char> letters)
		{
			Console.Clear();
			score = 0;
			Console.WriteLine("Ответ должен состоять из двух букв написанных слитно первая буква котороая стоит" +
				"перед буквой про которую спросили а другая после. Пример \"йл\" такой был бы ответ если" +
				"бы спросили про букву  к");
			for (int i = 0; i < maxScore; i++)
			{
				place = rand.Next(1, letters.Count - 1);
				Console.WriteLine("Какая буква стоит: перед, после - " + letters[place]);
				input = Console.ReadLine();
				if (CheckInputLength(input, 2))
					AddScore(input[0] == letters[place - 1] && input[1] == letters[place + 1]);
			}
			Result();
			if (Restart())
				Test2(letters);
		}

		private static void Test3(List<char> letters)
		{
			Console.Clear();
			score = 0;
			Console.WriteLine("Ответ должен состоять из двух букв написанных слитно первая буква котороая стоит" +
				"перед буквой про которую спросили а другая после. Пример \"ийлм\" такой был бы ответ если" +
				"бы спросили про букву  к");
			for (int i = 0; i < maxScore; i++)
			{
				place = rand.Next(2, letters.Count - 2);
				Console.WriteLine("Какая буква стоит: перед, после - " + letters[place]);
				input = Console.ReadLine();
				if (CheckInputLength(input, 4))
					AddScore(input[0] == letters[place - 2] && input[1] == letters[place - 1] &&
					input[2] == letters[place + 1] && input[3] == letters[place + 2]);
			}
			Result();
			if (Restart())
				Test2(letters);
		}
	}
}
