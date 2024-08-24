using System.Text;

class Program
{
	public static char[] pol_alphabet = { ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'Ą', 'B', 'C', 'Ć', 'D', 'E', 'Ę', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'Ł', 'M', 'N', 'Ń', 'O', 'Ó', 'P', 'Q', 'R', 'S', 'Ś', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Ź', 'Ż',
		'[', '\\', ']', '^', '_', '`', 'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'q', 'r', 's', 'ś', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ź', 'ż',
		'{', '|', '}', '~' };   // array length - 113

	public static string Encryption(int shift, string secretText)
	{
		string encodedText = TextTransformation(shift, secretText);

		// shift is always hidden as the one before last character
		return encodedText.Insert(encodedText.Length - 1, pol_alphabet[shift - 1].ToString());
	}

	public static string Decryption(string plainText)
	{
		int shift = (-1) * (Array.IndexOf(pol_alphabet, plainText[^2]) + 1);

		string plainTextNoShift = plainText.Remove(plainText.Length - 2, 1);

		return TextTransformation(shift, plainTextNoShift);
	}

	public static string TextTransformation(int shift, string text)
	{
		string finishedProduct = "";

		foreach(char letter in text)
		{
			int position = Array.IndexOf(pol_alphabet, letter) + shift;

			if (position < 0)
				finishedProduct += pol_alphabet[position + pol_alphabet.Length];

			else if (position > 112)
				finishedProduct += pol_alphabet[position - pol_alphabet.Length];
			
			else
				finishedProduct += pol_alphabet[position];
		}

		return finishedProduct;
    }

	public static void WrongInputBlock(string reason = "")
	{
		Console.WriteLine($"Wrong Input - {reason}!!!");
		Console.WriteLine("Press enter to continue ...");
		while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
	} 

	public static void TextOutputBlock(string result, string vertion = "")
	{
		Console.WriteLine();
		Console.WriteLine($"Your {vertion} text is :");
		Console.WriteLine($"{result}███");
		Console.WriteLine();
		Console.WriteLine("Press enter to continue ...");
		while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
	}

	private static void Main(string[] args)
	{
		Console.OutputEncoding = Encoding.Unicode;
		Console.InputEncoding = Encoding.Unicode;
		
        while (true)
        {
			Console.Clear();
			Console.WriteLine("---------------------------------------------------");
			Console.WriteLine("             Caesar Cipher Console");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Select action: 1.Encryption / 2.Decryption / 3.Exit ");
			Console.WriteLine("---------------------------------------------------");

			bool success = int.TryParse(Console.ReadLine(), out int choice);

			Console.WriteLine();

			if (!success)
			{
				WrongInputBlock("Enter a integer number");
				continue;
			}

			if (choice == 1)
			{
				Console.WriteLine("Enter the secret text :");
				string? secretText = Console.ReadLine();

				if (string.IsNullOrEmpty(secretText))
				{
					WrongInputBlock("Empty");
					continue;
				}

				Console.WriteLine();
				Console.WriteLine("1.Enther Shift Manually / 2.Get random Shift");
				bool success2 = int.TryParse(Console.ReadLine(), out int choice2);

				if (!success2)
				{
					Console.WriteLine();
					WrongInputBlock("Enter a integer number");
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
						WrongInputBlock("Shift must be a integer number");
						continue;
					}

					if (shift < 1 || shift > 112)
					{
						Console.WriteLine();
						WrongInputBlock("Shift must be in 1 - 112 range");
						continue;
					}
				}
				else if (choice2 == 2)
				{
					Random random = new Random();
					shift = random.Next(1, pol_alphabet.Length - 1);
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
				string? plainText = Console.ReadLine();

				if (string.IsNullOrEmpty(plainText) || plainText.Length == 1)
				{
					Console.WriteLine();
					WrongInputBlock("Empty or to short (minimum length for decryption is 2 characters)");
					continue;
				}

				TextOutputBlock(Decryption(plainText), "Decrypted");
			}
			else if (choice == 3)
				break;
			else
			{
				WrongInputBlock("Incorrect number");
				continue;
			}
		}
	}
}