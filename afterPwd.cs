//(c) 2023, FelixLP; Git: https://github.com/felixlp938

namespace afterPwd
{
	public class loggedIn
	{
		public static string? USERNAME;

		public static void init()
		{
			loggedIn loggedin = new();

			Console.WriteLine($"Willkommen bei deinem Code-Tagebuch, {USERNAME}");
			Console.WriteLine($"{secManager.sendMOTD(USERNAME)}");
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
						application.Runnable.Main();
						break;

					case "settings":
						Console.WriteLine("\n\nEntering settings mode!\n");
						CodeTagebuch.Settings settings = new CodeTagebuch.Settings();
						settings.acces(USERNAME);
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

		public bool chkPwd(string? USERNAME, string? PASSWORD)
		{

			if (USERNAME is null || PASSWORD is null)
			{
				return false;
			}


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

		public static string sendMOTD(string? USERNAME)
		{
			if (USERNAME is null)
			{
				return "null";
			}

			try
			{
				if (File.Exists(USERNAME + ".pwd"))
				{
					foreach (string line in File.ReadAllLines(USERNAME + ".pwd"))
					{
						if (line.StartsWith("MOTD="))
						{
							return line.Substring(5);
						}
					}

					return "null";

				}
				else
				{
					return "null";
				}
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
	}