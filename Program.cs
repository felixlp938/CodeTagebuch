//(c) 2023, FelixLP; Git: https://github.com/felixlp938

namespace application
{
	public class Runnable
	{

		//build version format = DDMMYYYY
		[AllowNull]
		public string BUILD = "14012023";

		public static void Main()
		{
			Console.OutputEncoding = Encoding.UTF8;
			Runnable runnable = new();
			secManager secManager = new();

			secManager.init();

			Console.Title = $"Code-Tagebuch Build {runnable.BUILD}";

			Console.WriteLine("Wilkommen beim Code-Tagebuch! Build " + runnable.BUILD);
			Console.WriteLine(" ");
			Console.WriteLine("Bitte gebe deine Anmeldungs-Informationen ein:");
			Console.Write("Username: ");
			string? username = Console.ReadLine();
			Thread.Sleep(100);
			Console.Write("Password: ");
			string password = GetPassword();

			Console.WriteLine(" ");
			Console.WriteLine(" ");


			//check if username is NULL => true: return to MAIN();
			if (username is null)
			{
				Console.WriteLine("Du kannst dich nicht als NULL anmelden! Wie soll das Funktionieren?");
				Main();
			}

			//check if user exists and password is correct
			if (secManager.chkPwd(username, password))
			{
				afterPwd.loggedIn.USERNAME = username;
				afterPwd.loggedIn.init();
			}
			else
			{
				Console.WriteLine();
				Console.WriteLine("Das hat leider nicht geklappt!");
				Main();
			}
		}

		//not called in Program.cs
		public static string GetPassword()
		{
			var pass = string.Empty;
			ConsoleKey key;
			do
			{
				var keyInfo = Console.ReadKey(intercept: true);
				key = keyInfo.Key;

				if (key == ConsoleKey.Backspace && pass.Length > 0)
				{
					Console.Write("\b \b");
					pass = pass[0..^1];
				}
				else if (!char.IsControl(keyInfo.KeyChar))
				{
					Console.Write("*");
					pass += keyInfo.KeyChar;
				}
			} while (key != ConsoleKey.Enter);
			return pass;
		}
	}
}