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

		for (int i = 0; i < secretText.Length; i++)
		{

			for (int j = 0; j < pol_alphabet.Length; j++)
			{
				if (pol_alphabet[j] == secretText[i])
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

	public static string Decryption(string plainText)
	{
		int shift = ReadTheShiftFromPlainText(plainText);

		string plainTextNoShift = plainText.Substring(0, plainText.Length - 2) + plainText.Substring(plainText.Length - 1);

		string decryptedText = "";

		for (int i = 0; i < plainTextNoShift.Length; i++)
		{
			for (int j = 0; j < pol_alphabet.Length; j++)
			{
				if (pol_alphabet[j] == plainTextNoShift[i])
				{
					if (j - shift >= 0)
					{
						decryptedText += pol_alphabet[j - shift];
					}
					else
					{
						decryptedText += pol_alphabet[j - shift + pol_alphabet.Length];
					}
				}
			}
		}

		return decryptedText;
	}

	public static int ReadTheShiftFromPlainText(string plainText)
	{
		char character = plainText[plainText.Length - 2];
		int shift = 0;

		for (int i = 0; i < pol_alphabet.Length; i++)
		{
			if(pol_alphabet[i] == character)
			{
				shift = i + 1;
			}
		}

		return shift;
	}

	private static void Main(string[] args)
	{
		Console.OutputEncoding = Encoding.Unicode;
		Console.InputEncoding = Encoding.Unicode;
		
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
				Console.WriteLine($"{result}███");
				Console.WriteLine();
				Console.WriteLine("Press any key to continue");

				Console.ReadKey();
				Console.Clear();

			}
			else if (choice == 2)
			{
				Console.WriteLine("Enter the plain text :");
				string decryptedText = Console.ReadLine();

				Console.WriteLine($"{Decryption(decryptedText)}███");

				Console.ReadKey();
				Console.Clear();
			}
			else if (choice == 3)
			{
				//flag = false;
				//continue;
				// break;
				Environment.Exit(0);
			}
			else
			{
                Console.WriteLine("Wrong Input !!!");
				Thread.Sleep(3000);
				Console.Clear();
				continue;
			}
		}

	}
}