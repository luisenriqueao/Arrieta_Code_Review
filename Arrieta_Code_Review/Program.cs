using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrieta_Code_Review
{
    // Use the LogTest for the main validations  
    class Program
    {
        string user;
        string contraseña;

        public void login()
        {
            Console.WriteLine("----------- login -------------");
            Console.WriteLine("Introduce the user");
           user = (Console.ReadLine()); //to enter the user
            Console.WriteLine("Introduce the password");
            contraseña = (Console.ReadLine());
            Console.Clear();
        }

        public void process()
        {
            if (user == "Luis" && contraseña == "1234")
            {
                Console.WriteLine("Welcome" + user);
            }
            else
            {
                Console.WriteLine("Access denied");
            }
            Console.ReadKey();
        }
            


        static void Main(string[] args)
        {
            Program pro = new Program();
            pro.login();
            pro.process();
        }
    }
}
