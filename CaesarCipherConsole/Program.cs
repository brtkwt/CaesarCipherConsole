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
		int randomShift = random.Next(1, pol_alphabet.Length - 1);

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
		return plainTextWithNoShift.Insert(plainTextWithNoShift.Length - 1, pol_alphabet[shift - 1].ToString());
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
		char character = plainText[^2];
		return Array.IndexOf(pol_alphabet, character) + 1;
	}

	public static void WrongInputBlock(string reason = "")
	{
		Console.WriteLine($"Wrong Input - {reason}!!!");
		Console.WriteLine("Press any key to continue ...");
		Console.ReadKey();
	} 

	public static void TextOutputBlock(string result, string vertion = "")
	{
		Console.WriteLine();
		Console.WriteLine($"Your {vertion} text is :");
		Console.WriteLine($"{result}███");
		Console.WriteLine();
		Console.WriteLine("Press any key to continue ...");
		Console.ReadKey();
	}

	private static void Main(string[] args)
	{
		Console.OutputEncoding = Encoding.Unicode;
		Console.InputEncoding = Encoding.Unicode;
		
		bool flag = true;

        while (flag)
        {
			Console.Clear();
			Console.WriteLine("---------------------------------------------------");
			Console.WriteLine("             Caesar Cipher Console");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Select action: 1.Encryption / 2.Decryption / 3.Exit ");
			Console.WriteLine("---------------------------------------------------");

			int choice;
			bool success = int.TryParse(Console.ReadLine(), out choice);

			Console.WriteLine();

			if (!success)
			{
				WrongInputBlock("Enter a number");
				continue;
			}

            if (choice == 1)
			{
				Console.WriteLine("Enter the secret text :");
				string secretText = Console.ReadLine();

				if (string.IsNullOrEmpty(secretText))
				{
					WrongInputBlock("Empty");
					continue;
				}

				Console.WriteLine();
				Console.WriteLine("1.Enther Shift Manually / 2.Get random Shift");
				int choice2;
				bool success2 = int.TryParse(Console.ReadLine(), out choice2);

				if (!success2)
				{
                    Console.WriteLine();
                    WrongInputBlock("Enter a number");
					continue;
				}

				int shift;
				Console.WriteLine();

				if (choice2 == 1)
				{
					Console.WriteLine("Enter the Shift (number 1 - 112):");
					bool success3 = int.TryParse(Console.ReadLine(), out shift);

					if (!success3)
					{
						Console.WriteLine();
						WrongInputBlock("Shift must be a number");
						continue;
					}
					
					if(shift <  1 || shift > 112)
					{
                        Console.WriteLine();
                        WrongInputBlock("Shift must be in 1 - 112 range");
						continue;
					}
				}
				else if (choice2 == 2)
				{
					shift = GetRandomShift();
					Console.WriteLine($"Your Shift is {shift}");
				}
				else
				{
					WrongInputBlock("Wrong number selected");
					continue;
				}

				TextOutputBlock(Encryption(shift, secretText), "Encrypted");
			}
			else if (choice == 2)
			{
				Console.WriteLine("Enter the plain text :");
				string plainText = Console.ReadLine();

				if (string.IsNullOrEmpty(plainText) || plainText.Length == 1)
				{
					Console.WriteLine();
					WrongInputBlock("Empty or to short (minimum length for decryption is 2 characters)");
					continue;
				}

				TextOutputBlock(Decryption(plainText), "Decrypted");
			}
			else if (choice == 3)
			{
				flag = false;
				continue;
			}
			else
			{
				WrongInputBlock("Incorrect number");
				continue;
			}
		}
	}
}