//(c) 2023, FelixLP; Git: https://github.com/felixlp938

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace application
{
	public class Runnable
	{
		[AllowNull]
		public string BUILD = "02012023";

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

			if (username is null)
			{
				Console.WriteLine("Du kannst dich nicht als NULL anmelden! Wie soll das Funktionieren?");
				Main();
				return;
			}


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

	namespace afterPwd
	{
		public class loggedIn
		{
			public static string? USERNAME;

			public static void init()
			{
				loggedIn loggedin = new();

				Console.WriteLine($"Willkommen bei deinem Code-Tagebuch, {USERNAME}");
				Console.WriteLine("Eine Liste von allen Commands: settings; read; write; delete; modify; exit; logout");

				loggedin.perfomCommand(Console.ReadLine());
			}

			public void perfomCommand(string? COMMAND)
			{
				if (COMMAND is null)
				{
					return;
				}

				switch (COMMAND.ToLower())
				{
					case "read":
						Console.WriteLine("\n\nEntering read mode!\n");
						FileMgmt.readMode();
						break;

					case "write":
						Console.WriteLine("\n\nEntering write mode!\n");
						break;

					case "delete":
						Console.WriteLine("\n\nEntering delete mode!\n");
						break;

					case "modify":
						Console.WriteLine("\n\nEntering modify mode!\n");
						break;

					case "exit":
						Environment.Exit(0);
						break;

					case "logout":
						Console.Clear();
						Runnable.Main();
						break;

					case "settings":
						Console.WriteLine(new NotImplementedException());
						break;

					default:
						Console.Clear();
						init();
						break;
				}
			}
		}

		public class FileMgmt
		{
			public static void readMode()
			{
				Console.Write("Dateinamen zum öffnen eingeben: ");
				string? filename = Console.ReadLine();

				if (File.Exists(filename))
				{
					Console.WriteLine(File.ReadAllLines(filename));
				}
				else
				{
					Console.WriteLine("Das hat leider nicht geklappt! Existiert die Datei?");
				}

				Console.Clear();
				afterPwd.loggedIn.init();
			}
			public static void writeMode()
			{
				Console.Write("Dateinamen zum öffnen eingeben: ");
				string? filename = Console.ReadLine();
				Console.Write("\nSoll etwas an die Datei ANGEÄNGT werden? [ TRUE | FALSE ] ");
				string? append = Console.ReadLine();

				bool append_bool = false;
			
				if (append != null)
				{
					if (append.ToLower() == "true")
					{
						append_bool = true;
					}
					else if (append.ToLower() == "false")
					{
						append_bool = false;
					}
				}

				if (File.Exists(filename))
				{
					StreamWriter write = new StreamWriter(filename, append_bool);


					write.WriteLine(Console.ReadLine());

					write.Close();

				}
				else
				{
					Console.WriteLine("Das hat leider nicht geklappt! Ist die Datei vorhanden und hat keinen Schreibschutz?");
				}

				Console.Clear();
				afterPwd.loggedIn.init();
			}
		}
	}

	public class secManager
	{
		public void init()
		{
			if (!File.Exists("default.pwd"))
			{
				try
				{
					StreamWriter writer = new StreamWriter("default.pwd");
					writer.WriteLine("USERNAME=default");
					writer.WriteLine("PWD=");
					writer.WriteLine("SETTINGS=true");
					writer.WriteLine("MOTD=Das ist ein default Account!");
					writer.Close();
				}
				catch (Exception e)
				{
					Console.WriteLine($"Das erstellen des DeafultUsers hat leider nicht geklappt! Error: {e.Message}");
				}
			}
		}

		public bool chkPwd(string USERNAME, string PASSWORD)
		{
			if (File.Exists(USERNAME + ".pwd"))
			{
				try
				{
					bool usernameCorrect = false;		//username check for the current user
					bool pwdCorrect = false;			//pwd check for the current user

					foreach (string line in File.ReadLines(USERNAME + ".pwd"))
					{
						if (line.StartsWith("USERNAME="))
						{
							if (line.Substring(9) == USERNAME)
							{
								usernameCorrect = true;
							}
							else
							{
								usernameCorrect = false;
							}
						}
						else if (line.StartsWith("PWD="))
						{
							if (line.Substring(4) == PASSWORD)
							{
								pwdCorrect = true;
							}
							else
							{
								pwdCorrect = false;
							}
						}
					}

					if (usernameCorrect == true && pwdCorrect == true)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine($"Das lesen der Userdatei hat leider nicht geklappt! Error: {e.Message}");
					return false;
				}
			}
			else
			{
				return false;
			}
		}
	}

	public class Settings
	{
		//settings class


		//old default class
		public void IMPLEMENT_CODE_HERE()
		{


			Console.WriteLine(new NotImplementedException());
		}

		//main function to access --> check if user is allowed to do this (allow to change settings)
		public void access(string USERNAME)
		{
			try
			{
				string? allowed = null;

				foreach (string line in File.ReadAllLines(USERNAME + ".pwd"))
				{
					if (line.StartsWith("SETTINGS="))
					{
						allowed = line.Substring(9);
					}
				}

				if (allowed is null)
				{
					Console.WriteLine("Der Wert SETTINGS in der USERDATEI ist NULL (also nicht festgelegt)");
					afterPwd.loggedIn.init();
				}

				if (allowed.ToLower().Equals("true"))
				{
					Console.Write("Settings: [username (den Username ändern); pwd (das Passwort ändern); settings (Einstellungen erlauben oder verbieten); motd (Message-Of-The-Day ändern)] ");
					string? command_input = Console.ReadLine();

					if (command_input.ToLower() is null)
					{
						Console.WriteLine("Der Command kann nicht NULL sein, das Programm wird nun beendet!");
					}

					switch (command_input.ToLower())
					{
						case "username":
							changeUsername(USERNAME);
							break;

						case "pwd":
							changePassword(USERNAME);
							break;

						case "settings":
							changePerms(USERNAME);
							break;

						case "motd":
							changeMOTD(USERNAME);
							break;

						default:
							break;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e.Message}");
				Console.ReadKey();
				afterPwd.loggedIn.init();
			}
		}

		//change the pwd
		public void changePassword(string USERNAME)
		{
			try
			{
				string? config_USERNAME = null;
				string? config_SETTINGS = null;
				string? config_MOTD = null;         //never used

				foreach (string line in File.ReadAllLines(USERNAME + ".pwd"))
				{
					if (line.StartsWith("USERNAME="))
					{
						config_USERNAME = line.Substring(9);
					}

					if (line.StartsWith("SETTINGS="))
					{
						config_SETTINGS = line.Substring(9);
					}

					if (line.StartsWith("MOTD="))
					{
						config_MOTD = line.Substring(5);
					}
				}

				File.Delete(USERNAME + ".pwd");

				Console.WriteLine("Gebe dein neues Passowort ein:");
				string? pwd_1 = Runnable.GetPassword();
				Thread.Sleep(100);
				Console.WriteLine("Gebe das Passwort erneut ein:");
				string? pwd_2 = Runnable.GetPassword();

				if (pwd_1 is null || pwd_2 is null)
				{
					Console.WriteLine("Das hat leider nicht geklappt! Die Passwörter dürfen nicht NULL sein!");
					afterPwd.loggedIn.init();
				}

				if (pwd_1 == pwd_2)
				{
					StreamWriter writer = new StreamWriter(USERNAME + ".pwd");
					writer.WriteLine($"USERNAME={config_USERNAME}");
					writer.WriteLine($"PWD=");
					writer.WriteLine($"SETTINGS={config_SETTINGS}");
					writer.WriteLine($"MOTD={changeMOTD}");

					writer.Close();
				}
				else
				{
					Console.WriteLine("Das hat leider nicht geklappt! Die Passwörter stimmen nicht überein!");
					afterPwd.loggedIn.init();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e.Message}");
				Console.ReadKey();
				afterPwd.loggedIn.init();
			}
		}

		//allow or disallow the user to access settings
		public void changePerms(string USERNAME)
		{
			Console.WriteLine(new NotImplementedException());
		}

		//change the username
		public void changeUsername(string USERNAME)
		{
			Console.WriteLine(new NotImplementedException());
		}


		//change the motd => message of the day
		public void changeMOTD(string USERNAME)
		{
			//implement this later!

			Console.WriteLine(new NotImplementedException());
		}
	}
}