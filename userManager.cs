//(c) 2023, FelixLP; Git: https://github.com/felixlp938

namespace userManager
{
    public class deleteUser
    {
        public static void deleteUserfile(string? USERNAME)
        {
            if (USERNAME != null) return;

            Thread.Sleep(100);
            Console.WriteLine("[Critical ] Benutzer löschen");

            Console.Write("Den Username eingeben: ");

            string? inputUserToDelete = Console.ReadLine();

            if (inputUserToDelete is null)
            {
                Console.WriteLine("Der eingegebene Username ist nicht vorhanden!");
                afterPwd.loggedIn.init();
            }

            if (inputUserToDelete.ToLower() == USERNAME)
            {
                Console.WriteLine("Du kannst dich in deiner eigenen Session nicht löschen!");
                afterPwd.loggedIn.init();
            }

            if (File.Exists(inputUserToDelete + ".pwd"))
            {
                //delete file with two factor auth here
            }


        }
    }

    public class createUser
    {
        //implement code here to create user
    }
}