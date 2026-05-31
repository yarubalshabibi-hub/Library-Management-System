using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace trying_about_Library_Management_System
{
    internal class FileName
    {
        static string name = "";
        static string phone = "";
        static string email = "";
        static bool isRegistered = false;
        static bool book = false;


        public static void menu()
        {
            Console.WriteLine("System Of Codeline");

            Console.WriteLine("0) Register Member");
            Console.WriteLine("1) Display Member Profile");
            Console.WriteLine("2) Search Book by Title");
            Console.WriteLine("3) Borrow a Book");

        }

        public static void register()
        {
            if (isRegistered == true)
            {
                Console.WriteLine("Already Registred.");
            }
            else
            {
                Console.WriteLine("Enter Your Name: ");
                name = Console.ReadLine(); // 5 length

                Console.WriteLine("Enter Your Phone Number: ");
                phone = Console.ReadLine(); // 8 lemgth

                Console.WriteLine("Enter Your Email: ");
                email = Console.ReadLine(); // contain @


                while (name.Length < 4 || phone.Length != 8 || !email.Contains("@"))
                {
                    Console.WriteLine("Enter Your Name: ");
                    name = Console.ReadLine();

                    Console.WriteLine("Enter Your Phone Number: ");
                    phone = Console.ReadLine();

                    Console.WriteLine("Enter Your Email: ");
                    email = Console.ReadLine();
                }
            }
            
        }

        static void Main(string[] args)
        {
            menu();

            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 0:
                    register();
                    break;
                case 1:
                    if (book == true)
                    {
                        Console.WriteLine("Book Found");
                    }
                    else
                    {
                        Console.WriteLine("Book Not Found");
                    } 
                    
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }


        }



    }
}
