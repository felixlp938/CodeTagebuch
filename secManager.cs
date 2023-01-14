//(c) 2023, FelixLP; Git: https://github.com/felixlp938

namespace secMgmt
{
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

			if (USERNAME is null || PASSWORD is null) return false;

			if (File.Exists(USERNAME + ".pwd"))
			{
				try
				{
					bool usernameCorrect = false;       //username check for the current user
					bool pwdCorrect = false;            //pwd check for the current user

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
			if (USERNAME is null) return "null";

			try
			{
				if (File.Exists(USERNAME + ".pwd"))
				{
					foreach (string line in File.ReadAllLines(USERNAME + ".pwd"))
					{
						if (line.StartsWith("MOTD=")) return line.Substring(5);
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
}