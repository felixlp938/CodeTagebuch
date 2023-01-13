//(c) 2023, FelixLP; Git: https://github.com/felixlp938

namespace CodeTagebuch
{
	public class Settings
	{
		//settings class

		//main function to acces --> check if user is allowed to do this (allow to change settings)
		public void acces(string? USERNAME)
		{
			if (USERNAME is null) return;

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
					return;
				}

				if (allowed.ToLower().Equals("true"))
				{
					Console.Write("Settings: [username (den Username ändern); pwd (das Passwort ändern); settings (Einstellungen erlauben oder verbieten); motd (Message-Of-The-Day ändern)] ");
					string? command_input = Console.ReadLine();

					if (command_input is null)
					{
						Console.WriteLine("Der Command kann nicht NULL sein, das Programm wird nun beendet!");
						afterPwd.loggedIn.init();
						return;
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
							acces(USERNAME);
							break;
					}
				}
				else
				{
					Console.WriteLine("Du darfst die Settings leider nicht öffnen! (Um diese Abfrage zu umgehen, gehe in deine Profil-File und ändere den Wert SETTINGS zu true)");
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

		//change the pwd
		public void changePassword(string? USERNAME)
		{
			if (USERNAME is null) return;

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
				string? pwd_1 = application.Runnable.GetPassword();
				Thread.Sleep(100);
				Console.WriteLine("Gebe das Passwort erneut ein:");
				string? pwd_2 = application.Runnable.GetPassword();

				if (pwd_1 is null || pwd_2 is null)
				{
					Console.WriteLine("Das hat leider nicht geklappt! Die Passwörter dürfen nicht NULL sein!");
					afterPwd.loggedIn.init();
				}

				if (pwd_1 == pwd_2)
				{
					StreamWriter writer = new StreamWriter(USERNAME + ".pwd");
					writer.WriteLine($"USERNAME={config_USERNAME}");
					writer.WriteLine($"PWD={pwd_1}");
					writer.WriteLine($"SETTINGS={config_SETTINGS}");
					writer.WriteLine($"MOTD={changeMOTD}");

					writer.Close();

					afterPwd.loggedIn.init();
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
		public void changePerms(string? USERNAME)
		{
			if (USERNAME is null) return;

			try
			{
				string? config_USERNAME = null;
				string? config_PWD = null;
				string? config_MOTD = null;         //never used

				foreach (string line in File.ReadAllLines(USERNAME + ".pwd"))
				{
					if (line.StartsWith("USERNAME="))
					{
						config_USERNAME = line.Substring(9);
					}

					if (line.StartsWith("PWD="))
					{
						config_PWD = line.Substring(4);
					}

					if (line.StartsWith("MOTD="))
					{
						config_MOTD = line.Substring(5);
					}
				}

				File.Delete(USERNAME + ".pwd");

				Console.WriteLine("Sollen die Einstellungen erlaubt werden? Falls ja, gebe true ein[!] anderen Falls false[!]:");
				string? allow_1 = Console.ReadLine();
				Thread.Sleep(100);
				Console.WriteLine("Gebe den Wert erneut ein:");
				string? allow_2 = Console.ReadLine();

				if (allow_1 is null || allow_2 is null)
				{
					Console.WriteLine("Das hat leider nicht geklappt! Die Usernames dürfen nicht NULL sein!");
					afterPwd.loggedIn.init();
				}

				if (allow_1 == allow_2)
				{
					StreamWriter writer = new StreamWriter(USERNAME + ".pwd");
					writer.WriteLine($"USERNAME={config_USERNAME}");
					writer.WriteLine($"PWD={config_PWD}");
					writer.WriteLine($"SETTINGS={allow_1}");
					writer.WriteLine($"MOTD={changeMOTD}");

					writer.Close();

					afterPwd.loggedIn.init();

				}
				else
				{
					Console.WriteLine("Das hat leider nicht geklappt! Die Werte stimmen nicht überein!");
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

		//change the username
		public void changeUsername(string? USERNAME)
		{
			if (USERNAME is null)
			{
				return;
			}

			try
			{
				string? config_SETTINGS = null;
				string? config_PWD = null;
				string? config_MOTD = null;         //never used

				foreach (string line in File.ReadAllLines(USERNAME + ".pwd"))
				{
					if (line.StartsWith("SETTINGS="))
					{
						config_SETTINGS = line.Substring(9);
					}

					if (line.StartsWith("PWD="))
					{
						config_PWD = line.Substring(4);
					}

					if (line.StartsWith("MOTD="))
					{
						config_MOTD = line.Substring(5);
					}
				}

				File.Delete(USERNAME + ".pwd");

				Console.WriteLine("Gebe deinen neuen Username ein:");
				string? username_1 = Console.ReadLine();
				Thread.Sleep(100);
				Console.WriteLine("Gebe den Username erneut ein:");
				string? username_2 = Console.ReadLine();

				if (username_1 is null || username_2 is null)
				{
					Console.WriteLine("Das hat leider nicht geklappt! Die Usernames dürfen nicht NULL sein!");
					afterPwd.loggedIn.init();
				}

				if (username_1 == username_2)
				{
					StreamWriter writer = new StreamWriter(USERNAME + ".pwd");
					writer.WriteLine($"USERNAME={username_1}");
					writer.WriteLine($"PWD={config_PWD}");
					writer.WriteLine($"SETTINGS={config_SETTINGS}");
					writer.WriteLine($"MOTD={changeMOTD}");

					writer.Close();

					afterPwd.loggedIn.init();

				}
				else
				{
					Console.WriteLine("Das hat leider nicht geklappt! Die Usernames stimmen nicht überein!");
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


		//change the motd => message of the day
		public void changeMOTD(string? USERNAME)
		{
			if (USERNAME is null)
			{
				return;
			}

			try
			{
				string? config_SETTINGS = null;
				string? config_PWD = null;
				string? config_USERNAME = null;    

				foreach (string line in File.ReadAllLines(USERNAME + ".pwd"))
				{
					if (line.StartsWith("SETTINGS="))
					{
						config_SETTINGS = line.Substring(9);
					}

					if (line.StartsWith("PWD="))
					{
						config_PWD = line.Substring(4);
					}

					if (line.StartsWith("USERNAME="))
					{
						config_USERNAME = line.Substring(9);
					}
				}

				File.Delete(USERNAME + ".pwd");

				Console.WriteLine("Gebe deine neue MOTD ein:");
				string? motd = Console.ReadLine();
				Thread.Sleep(100);
				
				StreamWriter writer = new StreamWriter(USERNAME + ".pwd");
				writer.WriteLine($"USERNAME={config_USERNAME}");
				writer.WriteLine($"PWD={config_PWD}");
				writer.WriteLine($"SETTINGS={config_SETTINGS}");
				writer.WriteLine($"MOTD={motd}");
				writer.Close();

				afterPwd.loggedIn.init();
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e.Message}");
				Console.ReadKey();
				afterPwd.loggedIn.init();
			}
		}
	}
}
