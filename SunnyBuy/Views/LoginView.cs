﻿using System;
using SunnyBuy.Services.UsersServices;
using SunnyBuy.Services.UsersServices.Models;

namespace SunnyBuy.Views
{
    public class LoginView
    {
        public void ShowLoginView()
        {
            UserService userService = new UserService();
            var user = new LoginModel();

            Console.WriteLine("       ___________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("       --------------------------------------------   Login  --------------------------------------------");
            Console.WriteLine("       ___________________________________________________________________________________________________\n");

            Console.Write("       *           E mail: ");
            user.Email = Console.ReadLine();
            Console.WriteLine();
            
            while (!user.EmailValidate(user.Email))
            {
                Console.WriteLine();
                Console.Write("       *           Type a valid email: ");
                user.Email = Console.ReadLine();
            }

            if (userService.Login(user.Email))
            {
                var userP = new LoginModel();
                Console.Write("       *           Password: ");
                userP.Password = string.Empty;
                ConsoleKey key;

                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && userP.Password.Length > 0)
                    {
                        Console.Write("\b \b");
                        userP.Password = userP.Password[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        userP.Password += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
            }
            else
            {
                Console.WriteLine("                   This email doesnt exist.");
                Console.WriteLine("                   Sign Up ?");
            }

        }
    }
}