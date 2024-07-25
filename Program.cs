// See https://aka.ms/new-console-template for more information
//ax ^ 2 + bx + c = 0
using NLog;
using QuadraticEquation_Console;
using System.Security.Cryptography.X509Certificates;
Logger logger = LogManager.GetCurrentClassLogger();
int a, b, c;
a = b = c = 0;
int app = 1;

Console.WriteLine("!--- Welcome to QuadraticEquation app! ---!");
while (app==1)
{
    Console.WriteLine("\n###        ###");
    Console.WriteLine("Enter values(int) for quadratic equation \n" +
        "(ax^2 + bx + c = 0 (a != 0)// ax^2 + c = 0 (b = 0)//ax^2 + bx = 0 (c = 0).)");
    //get value from user + check it
    try
    {
        Console.Write("Enter a : ");
        a = Convert.ToInt32(Console.ReadLine());//5
        Console.Write("Enter b : ");
        b = Convert.ToInt32(Console.ReadLine());//4
        Console.Write("Enter c : ");
        c = Convert.ToInt32(Console.ReadLine());//-9
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        logger.Error("Invalid data input.");
        a = b = c = 0;
    }
    QuadraticEquation equals = new QuadraticEquation(a, b, c);

    Console.WriteLine(equals.ToString());
    Console.WriteLine();
    Console.WriteLine();
    equals.SolvingEquation();

    //equals.SerializeToXML(@"data");

    Console.WriteLine("\n\n??? -- Do you want to continue use app? (1 --> yes) : ");
    app = Convert.ToInt32(Console.ReadLine());
}


