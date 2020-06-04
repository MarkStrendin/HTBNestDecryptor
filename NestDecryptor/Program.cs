using System;
using System.Linq;

namespace NestDecryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Decryptor decryptor = new Decryptor();
            Console.Write("Enter encrypted string: ");
            string cyphertext = Console.ReadLine();
            if (!string.IsNullOrEmpty(cyphertext))
            {
                Console.WriteLine(decryptor.DecryptString2(cyphertext));
            } else
            {
                Console.WriteLine("No input detected! Quitting!");
            }
            
        }
    }
}
