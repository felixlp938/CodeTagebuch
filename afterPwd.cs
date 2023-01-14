//(c) 2023, FelixLP; Git: https://github.com/felixlp938

namespace afterPwd
{
	public class loggedIn
	{
		public static string? USERNAME;

		//init class
		public static void init()
		{
			loggedIn loggedin = new();

			Console.WriteLine($"Willkommen bei deinem Code-Tagebuch, {USERNAME}");
			Console.WriteLine($"{secMgmt.secManager.sendMOTD(USERNAME)}");
			Console.WriteLine("Eine Liste von allen Commands: settings; read; write; delete; modify; exit; logout");

			loggedin.perfomCommand(Console.ReadLine());
		}

		//like command promt interface (get command)
		public void perfomCommand(string? COMMAND)
		{
			if (COMMAND is null) return;

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


	//different modes: create, write

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