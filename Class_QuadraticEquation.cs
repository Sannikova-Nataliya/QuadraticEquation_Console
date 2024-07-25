using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QuadraticEquation_Console
{
    [Serializable]
    public class QuadraticEquation
    {
        static int count;
        Logger logger = LogManager.GetCurrentClassLogger();

        //Constructors
        QuadraticEquation() : this(0, 0, 0) { }
        public QuadraticEquation(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
            if (a != 0 && b != 0 && c != 0)
                mType = 1;
            else if (a != 0 && b == 0 && c != 0)
                mType = 2;
            else if (a != 0 && b != 0 && c == 0)
                mType = 3;
            else if (a != 0 && b == 0 && c == 0)
                mType = 4;
            count++;
            Roots = "";
        }
        //Parameters
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        private int mType;//показує тип рівняння -- повне/відсутня b/ відсутня с/ відсутні b and c
        public string Roots;//розв'язки рівняння
        //
        public override string ToString()
        {
            switch (mType)
            {
                case 1:
                    {
                        if (B > 0 && C > 0)
                            return $"\n{A}x^2 + {B}x + {C} = 0";
                        else if (B > 0 && C < 0)
                            return $"\n{A}x^2 + {B}x - {C * (-1)} = 0";
                        else if (B < 0 && C > 0)
                            return $"\n{A}x^2 - {B * (-1)}x + {C} = 0";
                        else if (B < 0 && C < 0)
                            return $"\n{A}x^2 - {B * (-1)}x - {C * (-1)} = 0";
                    }
                    break;
                case 2:
                    {
                        if (C > 0)
                            return $"\n{A}x^2 + {C} = 0";
                        else
                            return $"\n{A}x^2 - {C * (-1)} = 0";
                    }
                    break;
                case 3:
                    {
                        if (B > 0)
                            return $"\n{A}x^2 + {B}x = 0";
                        else
                            return $"\n{A}x^2 - = 0";
                    }
                    break;
                case 4:
                    {
                        return $"\n{A}x^2 = 0";
                    }
                    break;
            }
            //Return an error
            logger.Error("Data problem when trying to convert to string. ");
            return "Error";
        }
        public void AssignFrom(QuadraticEquation other)
        {
            this.A = other.A;
            this.B = other.B;
            this.C = other.C;
            this.Roots = other.Roots;
            if (A != 0 && B != 0 && C != 0)
                mType = 1;
            else if (A != 0 && B == 0 && C != 0)
                mType = 2;
            else if (A != 0 && B != 0 && C == 0)
                mType = 3;
            else if (A != 0 && B == 0 && C == 0)
                mType = 4;
            else mType = 0;
            count++;
        }
        public void mTypeReset()
        {
            if (A != 0 && B != 0 && C != 0)
                mType = 1;
            else if (A != 0 && B == 0 && C != 0)
                mType = 2;
            else if (A != 0 && B != 0 && C == 0)
                mType = 3;
            else if (A != 0 && B == 0 && C == 0)
                mType = 4;
            else mType = 0;
            Console.WriteLine("mType reset successfully.");
        }
        //public function for solving the equation
        public void SolvingEquation()
        {
            switch (mType)
            {
                case 1:
                    {
                        Type_1_Solving();
                    }
                    break;
                case 2:
                    {
                        Type_2_Solving();
                    }
                    break;
                case 3:
                    {
                        Type_3_Solving();
                    }
                    break;
                case 4:
                    {
                        Type_4_Solving();
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Error!");
                        logger.Error($"Problem at the stage of solving the equation \"{ToString()}\". The parameters did not match any of the conditions.");
                    }
                    break;
            }
        }
        //
        private void Type_1_Solving()//a,b,c != 0
        {
            //find D
            var D = (Math.Pow(B, 2)) - (4 * A * C);
            Console.WriteLine($"D = b^2 - 4ac;\n" +
            $"D = {B}^2 - 4*{A}*{C} =" + (((4 * A * C) > 0) ? $" {Math.Pow(B, 2)} - {4 * A * C};\n" : $" {Math.Pow(B, 2)} - ({4 * A * C});\n") +
            $"D = {D}");
            var SqrtD = Math.Sqrt(D);
            if (D > 0)
            {
                Console.WriteLine(//"Sqrt of a discriminant : method \"Math.Sqrt()\"\n" +
                    $"√D = {SqrtD};");
            }
            //x
            if (D > 0)
            {
                Console.WriteLine("\n" +
                    "\t -b+√D\n" +
                    "x(1)  =  -----\n" +
                    "\t  2a");

                Console.WriteLine("\n" +
                    "\t -b-√D\n" +
                    "x(2)  =  -----\n" +
                    "\t  2a");

                var x1 = ((B * (-1)) + SqrtD) / (2 * A);
                var x2 = ((B * (-1)) - SqrtD) / (2 * A);
                Console.WriteLine($"Result x(1) = {x1}; x(2) = {x2}");
                Roots = $"x(1) = {x1}; x(2) = {x2}";
            }
            else if (D == 0)
            {
                Console.WriteLine(//"x : \n" +
                    "     -b\n" +
                "x = ----\n" +
                "     2a\n\n" +
               $"     {B * (-1)}\n" +
                "x = ----\n" +
                $"     2 * {A}\n");
                float x = (B * (-1));
                x /= (A * 2);
                Console.WriteLine($"Result x = {x}");
                Roots = $"x = {x}";
            }
            else
            {
                Console.WriteLine("The equation has no roots");
                logger.Info($"Equation \"{ToString()}\" has no roots");
                Roots = "The equation has no roots";
            }

        }
        private void Type_2_Solving()//b=0
        {
            Console.WriteLine($"{A} * x^2 + {C} = 0\n" +
                $"{A} * x^2 = {C * (-1)}\n" +
                $"x^2={C}/{A}\n" +
                $"x = ±√{C}/{A}");
            var x1 = (Math.Pow(C / A, 2));
            var x2 = (Math.Pow(C / A, 2)) * (-1);
            Console.WriteLine($"Result x(1) = {x1}; x(2) = {x2}");
            Roots = $"x(1) = {x1}; x(2) = {x2}";
        }
        private void Type_3_Solving()//c=0
        {
            Console.WriteLine((B > 0) ? $"{A}x^2 + {B}x = 0\n" : $"{A}x^2  {B}x = 0\n" +
                $"x({A}x+{B}) = 0\n" +
                $"x = 0; {A}x + b = 0\n" +
                $"\t {A}x = {B * (-1)}\n");
            var x1 = 0;
            var x2 = (-1 * (B / A));
            Console.WriteLine($"Result x(1) = {x1}; x(2) = {x2}");
            Roots = $"x(1) = {x1}; x(2) = {x2}";
        }
        private void Type_4_Solving()//b=c=0
        {
            Console.WriteLine($"{A}x^2 = 0");
            var x = 0;
            Console.WriteLine($"Result x = {x}");
            Roots = $"x = {x}";
        }
        // save to file
        public void SerializeToXML(string path)
        {
            path += @"/equation" + count + ".xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(QuadraticEquation));
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    xmlSerializer.Serialize(fs, this);
                    Console.WriteLine($"Your data serialized to \"{path}\"");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error($"Problem while serializing to xml : {ex.Message}");
            }
        }
        public void SaveToTXT(string path)//path in form @"\folder"
        {
            string data = $"{ToString()}\n{Roots}";

            path += @"/equation" + count + ".txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(data);
            }
        }

        //read from file
        public void DeserializeFromXML(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(QuadraticEquation));
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    QuadraticEquation q = ((QuadraticEquation)xmlSerializer.Deserialize(fs));
                    AssignFrom(q);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error($"Problem on stage deserialization from \"{path}\" : {ex.Message}");
            }
        }
    }
}
