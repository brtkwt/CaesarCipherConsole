using System.Text;

class Program
{
	public static char[] pol_alphabet = { ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'Ą', 'B', 'C', 'Ć', 'D', 'E', 'Ę', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'Ł', 'M', 'N', 'Ń', 'O', 'Ó', 'P', 'Q', 'R', 'S', 'Ś', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Ź', 'Ż',
		'[', '\\', ']', '^', '_', '`', 'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'q', 'r', 's', 'ś', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ź', 'ż',
		'{', '|', '}', '~' };

	public static int GetRandomShift()
	{
		Random random = new Random();
		int randomShift = random.Next(1, pol_alphabet.Length);

		return randomShift;
	}

	public static string Encryption(int shift, string secretText)
	{
		string finishedProduct = "";
		char letter;

		for (int i = 0; i < secretText.Length; i++)
		{
			letter = secretText[i];

			for (int j = 0; j < pol_alphabet.Length; j++)
			{
				if (pol_alphabet[j] == letter)
				{
					if(j + shift < pol_alphabet.Length)
					{
						finishedProduct += pol_alphabet[j + shift];
					}
					else
					{
						finishedProduct += pol_alphabet[j + shift - pol_alphabet.Length];
					}
				}
			}
		}

		return AddHiddenShift(shift, finishedProduct);
	}

	public static string AddHiddenShift(int shift, string plainTextWithNoShift)
	{
		// shift is always hidden as the one before last character
		int position = plainTextWithNoShift.Length - 1;

		char lastLetter = plainTextWithNoShift[position];
		string plainText = plainTextWithNoShift.Substring(0, position);

		plainText += pol_alphabet[shift - 1];
		plainText += lastLetter;

		return plainText;
	}

	public static string Decryption(string PlainText)
	{
		return "";
	}

	private static void Main(string[] args)
	{
		Console.OutputEncoding = Encoding.UTF8;
		bool flag = true;

        Console.WriteLine("CaesarCipherConsole");
		Thread.Sleep(1000);
		Console.Clear();

		while (flag)
        {
			Console.WriteLine("Actions: 1.Encryption / 2.Decryption / 3.Exit ");

			int choice = Convert.ToInt32(Console.ReadLine());
			Console.Clear();

			if (choice == 1)
			{
				Console.WriteLine("Enter the secret text :");
				string secretText = Console.ReadLine();

                Console.WriteLine("1.Enther Shift Manualy / 2.Get random Shift");
				int choice2 = Convert.ToInt32(Console.ReadLine());
				int shift;

				if (choice2 == 1)
				{
					Console.WriteLine("Enter the Shift :");
					shift = Convert.ToInt32(Console.ReadLine());
				}
				else if (choice2 == 2)
				{
					shift = GetRandomShift();
					Console.WriteLine($"Your Shift is {shift}");

					Console.ReadKey();
				}
				else
				{
					break;
				}

                

                string result = Encryption( shift, secretText);
				Console.Clear();
				Console.WriteLine("Your Encoded text is :");
				char endOfLineSign = (char)219;
				Console.WriteLine($"{result}██████");
				Console.WriteLine();
				Console.WriteLine("Press any key to continue");

				Console.ReadKey();
				Console.Clear();

			}
			else if (choice == 2)
			{
				Console.WriteLine("Enter the plain text :");
				string plainText = Console.ReadLine();


			}
			else if (choice == 3)
			{
				flag = false;
				continue;
				// break;
				// Environment.Exit(0);
			}
			else
			{
                Console.WriteLine("Wrong Input !!!");
				Thread.Sleep(3000);
				Console.Clear();
				continue;
			}
		}

		Console.WriteLine("Hello, World!");
	}
}