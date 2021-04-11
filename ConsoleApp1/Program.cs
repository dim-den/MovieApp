using System;
using System.IO;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace ConsoleApp1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            UserDataService u = new UserDataService();
            User user = u.GetByUsername("dimden").Result;
            Console.WriteLine(user.Email);
                string fName = @"D:\7.png";
                FileStream stream = new FileStream(fName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(stream);
                user.ImageData = br.ReadBytes((int)stream.Length);
            await u.Update(user.ID, user);
            Console.ReadKey();
        }
    }
}
