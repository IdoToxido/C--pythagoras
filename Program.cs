using System.Net;
class Program
{
    public static void Main()
    {
        HttpListener listener = new();
        listener.Prefixes.Add("http://*:5000/");
        listener.Start();

        Console.WriteLine("Server started. Listening for requests...");
        Console.WriteLine("Main page on http://localhost:5000/website/index.html");

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string rawPath = request.RawUrl!;
            string absPath = request.Url!.AbsolutePath;


            Console.WriteLine($"Received a request with path: " + rawPath);

            string filePath = "." + absPath;
            bool isHtml = request.AcceptTypes!.Contains("text/html");

            if (File.Exists(filePath))
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                if (isHtml) { response.ContentType = "text/html; charset=utf-8"; }
                response.OutputStream.Write(fileBytes);
            }
            else if (isHtml)
            {
                response.StatusCode = (int)HttpStatusCode.Redirect;
                response.RedirectLocation = "/website/404.html";
            }

            response.Close();

        }
    }
}

class Programos
{
    public static bool Function_All_Done(double[] Lengths, double[] Angles)
    {
        foreach (double var in Lengths)
        {
            if (var == 0)
            {
                return false;
            }
        }
        foreach (double var in Angles)
        {
            if (var == 0)
            {
                return false;
            }
        }
        return true;
    }
    public static double[] Function_Measurements()
    {
        Console.WriteLine("You should only give 3 parameters in order to get the most accurate and correct result,");
        Console.WriteLine("Also make sure you provide a correct triangle according to the following orders");
        Console.WriteLine("if any value isn't given, enter 0 instead");

        Console.WriteLine("Sides:");
        Console.Write("Side X (Second Biggest): ");
        double A_Side = double.Parse(Console.ReadLine()!);
        Console.Write("Side Y (Smallest/Equal to X): ");
        double B_Side = double.Parse(Console.ReadLine()!);
        Console.Write("Side Z (Biggest): ");
        double C_Side = double.Parse(Console.ReadLine()!);


        Console.WriteLine("Angles:");
        Console.Write("Angle A (Biggest AND opposite to side Z) : ");
        double Angle_A = double.Parse(Console.ReadLine()!);
        Console.Write("Angle B (Smallest AND opposite to side Y): ");
        double Angle_B = double.Parse(Console.ReadLine()!);
        Console.Write("Angle C (Second Biggest AND opposite to side X): ");
        double Angle_C = double.Parse(Console.ReadLine()!);

        double[] arr = { A_Side, B_Side, C_Side, Angle_A, Angle_B, Angle_C };
        return arr;
    }
    public static void Main()
    {
        double[] Measurements = Function_Measurements();

        double[] LengthsArray = { Measurements[0], Measurements[1], Measurements[2] };
        double[] AnglesArray = { Measurements[3], Measurements[4], Measurements[5] };
        double[] L = { 12.0, 5.0, 0 };
        double[] A = { 80, 0, 0 };

        while (!Function_All_Done(L, A))
        {
            A = Function_Complete_Angle(A);
            // 90* Triangle
            if (A[0] == 90)
            {
                // Pythagoras
                L = Function_Pythagoras(L);
                // Angles Trigo 90*
                A = Function_90_Degrees_Angle_Cal_By_Sin_Cosin_Tan(A, L);
                // Sides Trigo 90*
                L = Function_90_Degrees_Side_Cal_By_Sin_Cosin_Tan(A, L);
            }
            // Complete to 180*
            A = Function_Complete_Angle(A);
            // Non 90* triangle
            if (A[0] != 90)
            {
                A = Function_Degrees_Angle_Cal_By_CoSine(A, L);
                A = Function_Non_90_Degrees_Angle_Cal_By_Sin(A, L);
                L = Function_Non_90_Degrees_Side_Cal_By_CoSine(A, L);
                L = Function_Non_90_Degrees_Side_Cal_By_Sin(A, L);
            }
        }
        L = Function_Round_Sides(L);
        A = Function_Round_Angles(A);

        if (A[0] > 90)
        {
            Function_Display_Obtuse_Triangle_By_Concept(A, L);
        }
        else if (A[0] < 90)
        {
            Function_Display_Acute_Triangle_By_Concept(A, L);
        }
        else if (A[0] == 90)
        {
            Function_Display_90_Degrees_Triangle_By_Concept(A, L);
        }

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Side 0 is: " + LengthsArray[0]);
        Console.WriteLine("Side 1 is: " + LengthsArray[1]);
        Console.WriteLine("Side 2 is: " + LengthsArray[2]);
        Console.WriteLine();
        Console.WriteLine("Angle 0 is: " + AnglesArray[0]);
        Console.WriteLine("Angle 1 is: " + AnglesArray[1]);
        Console.WriteLine("Angle 2 is: " + AnglesArray[2]);
    }
    public static double[] Function_Pythagoras(double[] L)
    {
        if (L[0] == 0 && L[2] != 0 && L[1] != 0)
        {
            L[0] = Math.Sqrt(Math.Pow(L[2], 2) - Math.Pow(L[1], 2));
        }
        if (L[1] == 0 && L[2] != 0 && L[0] != 0)
        {
            L[1] = Math.Sqrt(Math.Pow(L[2], 2) - Math.Pow(L[0], 2));
        }
        if (L[2] == 0 && L[1] != 0 && L[0] != 0)
        {
            L[2] = Math.Sqrt(Math.Pow(L[1], 2) + Math.Pow(L[0], 2));
        }
        return L;

    }
    public static double[] Function_Complete_Angle(double[] A)
    {
        if (A[0] == 0 && A[1] != 0 && A[2] != 0)
        {
            A[0] = 180 - A[1] - A[2];
        }

        if (A[1] == 0 && A[0] != 0 && A[2] != 0)
        {
            A[1] = 180 - A[0] - A[2];
        }
        if (A[2] == 0 && A[0] != 0 && A[1] != 0)
        {
            A[2] = 180 - A[0] - A[1];
        }
        return A;
    }
    public static double[] Function_90_Degrees_Angle_Cal_By_Sin_Cosin_Tan(double[] A, double[] L)
    {
        // sin
        if (A[1] == 0 && L[1] != 0 && L[2] != 0)
        {
            A[1] = Math.Asin(L[1] / L[2] * (180.0 / Math.PI));
        }
        if (A[2] == 0 && L[0] != 0 && L[2] != 0)
        {
            A[2] = Math.Asin(L[0] / L[2] * (180.0 / Math.PI));
        }
        // cosin
        if (A[1] == 0 && L[0] != 0 && L[2] != 0)
        {
            A[1] = Math.Acos(L[0] / L[2] * (180.0 / Math.PI));
        }
        if (A[2] == 0 && L[1] != 0 && L[2] != 0)
        {
            A[2] = Math.Acos(L[1] / L[2] * (180.0 / Math.PI));
        }
        // tan
        if (A[1] == 0 && L[0] != 0 && L[1] != 0)
        {
            A[1] = Math.Atan(L[1] / L[0] * (180.0 / Math.PI));
        }
        if (A[2] == 0 && L[0] != 0 && L[1] != 0)
        {
            A[2] = Math.Atan(L[0] / L[1] * (180.0 / Math.PI));
        }
        return A;
    }
    public static double[] Function_90_Degrees_Side_Cal_By_Sin_Cosin_Tan(double[] A, double[] L)
    {
        //sin
        if (L[0] == 0 && L[2] != 0 && A[2] != 0)
        {
            L[0] = L[2] * Math.Sin(A[2] * (180.0 / Math.PI));
        }
        if (L[1] == 0 && L[2] != 0 && A[1] != 0)
        {
            L[1] = L[2] * Math.Sin(A[1] * (180.0 / Math.PI));
        }
        if (L[2] == 0 && L[0] != 0 && A[2] != 0)
        {
            L[2] = L[0] / Math.Sin(A[2] * (180.0 / Math.PI));
        }
        if (L[2] == 0 && L[1] != 0 && A[1] != 0)
        {
            L[2] = L[1] / Math.Sin(A[1] * (180.0 / Math.PI));
        }
        // cosin
        if (L[0] == 0 && L[2] != 0 && A[1] != 0)
        {
            L[0] = L[2] * Math.Cos(A[1] * (180.0 / Math.PI));
        }
        if (L[1] == 0 && L[2] != 0 && A[2] != 0)
        {
            L[1] = L[2] * Math.Cos(A[2] * (180.0 / Math.PI));
        }
        if (L[2] == 0 && L[0] != 0 && A[1] != 0)
        {
            L[2] = L[0] / Math.Cos(A[1] * (180.0 / Math.PI));
        }
        if (L[2] == 0 && L[1] != 0 && A[2] != 0)
        {
            L[2] = L[1] / Math.Cos(A[2] * (180.0 / Math.PI));
        }
        // tan
        if (L[0] == 0 && L[1] != 0 && A[2] != 0)
        {
            L[0] = L[1] * Math.Tan(A[2] * (180.0 / Math.PI));
        }
        if (L[1] == 0 && L[0] != 0 && A[1] != 0)
        {
            L[1] = L[0] * Math.Tan(A[1] * (180.0 / Math.PI));
        }
        if (L[0] == 0 && L[1] != 0 && A[1] != 0)
        {
            L[0] = L[1] / Math.Tan(A[1] * (180.0 / Math.PI));
        }
        if (L[1] == 0 && L[0] != 0 && A[2] != 0)
        {
            L[1] = L[0] / Math.Tan(A[2] * (180.0 / Math.PI));
        }

        return L;
    }
    public static double[] Function_Non_90_Degrees_Side_Cal_By_Sin(double[] A, double[] L)
    {
        // find L[0]
        if (L[0] == 0 && L[1] != 0 && A[2] != 0 && A[1] != 0)
        {
            L[0] = L[1] * Math.Sin(A[2] * (180.0 / Math.PI)) / Math.Sin(A[1] * (180.0 / Math.PI));
        }
        if (L[0] == 0 && L[2] != 0 && A[2] != 0 && A[0] != 0)
        {
            L[0] = L[2] * Math.Sin(A[2] * (180.0 / Math.PI)) / Math.Sin(A[0] * (180.0 / Math.PI));
        }
        // find L[1]
        if (L[1] == 0 && L[0] != 0 && A[1] != 0 && A[2] != 0)
        {
            L[1] = L[0] * Math.Sin(A[1] * (180.0 / Math.PI)) / Math.Sin(A[2] * (180.0 / Math.PI));
        }
        if (L[1] == 0 && L[2] != 0 && A[1] != 0 && A[0] != 0)
        {
            L[1] = L[2] * Math.Sin(A[1] * (180.0 / Math.PI)) / Math.Sin(A[0] * (180.0 / Math.PI));
        }
        // find L[2]
        if (L[2] == 0 && L[0] != 0 && A[0] != 0 && A[2] != 0)
        {
            L[2] = L[0] * Math.Sin(A[0] * (180.0 / Math.PI)) / Math.Sin(A[2] * (180.0 / Math.PI));
        }
        if (L[2] == 0 && L[1] != 0 && A[0] != 0 && A[1] != 0)
        {
            L[2] = L[1] * Math.Sin(A[0] * (180.0 / Math.PI)) / Math.Sin(A[1] * (180.0 / Math.PI));
        }

        return L;
    }
    public static double[] Function_Non_90_Degrees_Angle_Cal_By_Sin(double[] A, double[] L)
    {
        A = Function_Complete_Angle(A);
        if (A[0] == 0 && L[2] != 0 && A[1] != 0 && L[1] != 0)
        {
            A[0] = Math.Asin(L[2] * Math.Sin(A[1] * (180.0 / Math.PI)) / L[1] * (180.0 / Math.PI));
        }
        A = Function_Complete_Angle(A);
        if (A[0] == 0 && L[2] != 0 && A[2] != 0 && L[0] != 0)
        {
            A[0] = Math.Asin(L[2] * Math.Sin(A[2] * (180.0 / Math.PI)) / L[0] * (180.0 / Math.PI));
        }
        A = Function_Complete_Angle(A);
        if (A[1] == 0 && L[1] != 0 && A[0] != 0 && L[2] != 0)
        {
            A[1] = Math.Asin(L[1] * Math.Sin(A[0] * (180.0 / Math.PI)) / L[2] * (180.0 / Math.PI));
        }
        A = Function_Complete_Angle(A);
        if (A[1] == 0 && L[1] != 0 && A[2] != 0 && L[0] != 0)
        {
            A[1] = Math.Asin(L[1] * Math.Sin(A[2] * (180.0 / Math.PI)) / L[0] * (180.0 / Math.PI));
        }
        A = Function_Complete_Angle(A);
        if (A[2] == 0 && L[0] != 0 && A[1] != 0 && L[1] != 0)
        {
            A[2] = Math.Asin(L[0] * Math.Sin(A[1] * (180.0 / Math.PI)) / L[1] * (180.0 / Math.PI));
        }
        A = Function_Complete_Angle(A);
        if (A[2] == 0 && L[0] != 0 && A[0] != 0 && L[2] != 0)
        {
            A[2] = Math.Asin(L[0] * Math.Sin(A[0] * (180.0 / Math.PI)) / L[2] * (180.0 / Math.PI));
        }
        A = Function_Complete_Angle(A);
        return A;
    }
    public static double[] Function_Non_90_Degrees_Side_Cal_By_CoSine(double[] A, double[] L)
    {
        if (L[0] == 0 && L[1] != 0 && L[2] != 0 && A[2] != 0)
        {
            L[0] = Math.Sqrt(L[1] * L[1] + L[2] * L[2] - 2 * L[1] * L[2] * Math.Cos(A[2]) * (180.0 / Math.PI));
        }
        if (L[1] == 0 && L[0] != 0 && L[2] != 0 && A[1] != 0)
        {
            L[1] = Math.Sqrt(L[0] * L[0] + L[2] * L[2] - 2 * L[0] * L[2] * Math.Cos(A[1]) * (180.0 / Math.PI));
        }
        if (L[2] == 0 && L[0] != 0 && L[1] != 0 && A[0] != 0)
        {
            L[2] = Math.Sqrt(L[0] * L[0] + L[1] * L[1] - 2 * L[0] * L[1] * Math.Cos(A[0]) * (180.0 / Math.PI));
        }
        return L;
    }
    public static double[] Function_Degrees_Angle_Cal_By_CoSine(double[] A, double[] L)
    {
        if (L[0] != 0 && L[1] != 0 && L[2] != 0)
        {
            if (A[0] == 0)
            {
                double PowSum = L[0] * L[0] + L[1] * L[1] - L[2] * L[2];
                A[0] = Math.Acos(PowSum / (2 * L[0] * L[1])) * (180 / Math.PI);
            }

            if (A[1] == 0)
            {
                double PowSum = L[0] * L[0] + L[2] * L[2] - L[1] * L[1];
                A[1] = Math.Acos(PowSum / (2 * L[0] * L[2])) * (180 / Math.PI);
            }

            if (A[2] == 0)
            {
                double PowSum = L[1] * L[1] + L[2] * L[2] - L[0] * L[0];
                A[2] = Math.Acos(PowSum / (2 * L[1] * L[2])) * (180 / Math.PI);
            }
        }
        return A;
    }
    public static double[] Function_Round_Sides(double[] L)
    {
        double[] Rounded_Sides = { };

        for (int i = 0; i < 3; i++)
        {
            if (L[i].ToString().Contains('.'))
            {
                if (L[i].ToString().Split('.')[1].Length > 3)
                {
                    L[i] = Math.Round(L[i], 3); ;
                }
            }
            Rounded_Sides = Rounded_Sides.Append(L[i]).ToArray();
        }

        return Rounded_Sides;
    }
    public static double[] Function_Round_Angles(double[] A)
    {
        double[] Rounded_Angles = { };
        for (int i = 0; i < 3; i++)
        {
            if (A[i].ToString().Contains('.'))
            {
                if (A[i].ToString().Split('.')[1].Length > 3)
                {
                    A[i] = Math.Round(A[i], 3); ;
                }
            }
            Rounded_Angles = Rounded_Angles.Append(A[i]).ToArray();
        }

        return Rounded_Angles;
    }
    public static void Function_Display_90_Degrees_Triangle_By_Concept(double[] A, double[] L)
    {
        Console.WriteLine();
        for (int j = 0; j < L[0].ToString().Length; j++)
        {
            Console.Write(" ");
        }
        Console.WriteLine(A[1]);
        for (int i = 0; i < Math.Round(L[0]); i++)
        {

            if (Math.Round(L[0] / 2) == i + 1)
            {
                Console.Write(L[0] + " |");
                for (int j = 0; j < i; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("\\ ");
                Console.WriteLine(L[2]);

            }
            else
            {
                for (int j = 0; j < L[0].ToString().Length + 1; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("|");
                if (i == Math.Round(L[0]) - 1)
                {
                    for (int j = 0; j < Math.Round(L[0]) - 1; j++)
                    {
                        Console.Write("_");
                    }
                    Console.WriteLine("\\");
                    for (int j = 0; j < L[0].ToString().Length - 1; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("90");
                    for (int j = 0; j < Math.Round(L[0]) - 1; j++)
                    {
                        Console.Write(" ");
                        if (Math.Round((L[0] - 1) / 2) == j + 1)
                        {
                            Console.Write(L[1]);
                            for (int k = 0; k < Math.Round(L[0] - 1 / 2) - L[1].ToString().Length - 2; k++)
                            {
                                Console.Write(" ");
                            }
                            Console.WriteLine(A[2]);
                        }
                    }
                }
                for (int j = 0; j < i; j++)
                {
                    Console.Write(" ");
                }
                if (i != Math.Round(L[0]) - 1)
                {
                    Console.WriteLine("\\");
                }
            }
        }
    }
    public static void Function_Display_Obtuse_Triangle_By_Concept(double[] A, double[] L)
    {


        Console.WriteLine();
        for (int j = 0; j < 2; j++)
        {
            Console.Write(" ");
        }
        Console.WriteLine(A[1]);

        for (int i = 0; i < Math.Round(L[0]); i++)
        {
            if (Math.Round(L[0] / 2) == i)
            {
                for (int j = 0; j < A[0].ToString().Length + i - L[0].ToString().Length; j++)
                {
                    Console.Write(" ");
                }
                Console.Write(L[0] + " \\");
                for (int j = 0; j < i; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("\\ ");
                Console.WriteLine(L[2]);

            }
            else
            {
                for (int j = 0; j < A[0].ToString().Length + i + 1; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("\\");
                if (i == Math.Round(L[0]) - 1)
                {
                    for (int j = 0; j < Math.Round(L[0]) - 1; j++)
                    {
                        Console.Write("_");
                    }
                    Console.WriteLine("\\");
                    for (int j = 0; j < L[0].ToString().Length - 1; j++)
                    {
                        Console.Write(" ");
                    }
                    for (int j = 0; j < i + A[0].ToString().Length; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(A[0]);
                    for (int j = 0; j < Math.Round(L[0]) - 1; j++)
                    {
                        Console.Write(" ");
                        if (Math.Round((L[0] - 1) / 2) - L[1].ToString().Length == j)
                        {
                            Console.Write(L[1]);
                            for (int k = 0; k < Math.Round((L[0] - 1) / 2); k++)
                            {
                                Console.Write(" ");
                            }
                            Console.WriteLine(A[2]);
                        }
                    }
                }
                for (int j = 0; j < i; j++)
                {
                    Console.Write(" ");
                }
                if (i != Math.Round(L[0]) - 1)
                {
                    Console.WriteLine("\\");
                }
            }
        }
    }
    public static void Function_Display_Acute_Triangle_By_Concept(double[] A, double[] L)
    {
        int idfk = int.Parse(Math.Round(L[0]).ToString()) / 4;
        Console.WriteLine();
        for (int i = 0; i < L[0] - 1; i++)
        {
            Console.Write(' ');
        }
        Console.WriteLine(A[1]);
        for (int i = 0; i < Math.Round(L[0]); i++)
        {
            if (Math.Round(L[0] / 2) == i)
            {
                for (int j = 0; j < A[0].ToString().Length - i + L[0] - idfk - L[0].ToString().Length; j++)
                {
                    Console.Write(' ');
                }

                Console.Write(L[0] + " /");

                for (int j = 0; j < i * 2; j++)
                {
                    Console.Write(' ');
                }
                Console.Write("\\ ");
                Console.WriteLine(L[1]);
            }

            else
            {
                for (int j = 0; j < L[0] - i; j++)
                {
                    Console.Write(' ');
                }
                Console.Write('/');

                if (i == L[0] - 1)
                {
                    for (int j = 0; j < (L[0] - 1) * 2; j++)
                    {
                        Console.Write('_');
                    }
                    Console.WriteLine('\\');
                    Console.Write(A[0]);
                    for (int j = 0; j < L[0] - 2 - A[1].ToString().Length; j++)
                    {
                        Console.Write(' ');
                    }
                    Console.Write(L[2]);
                    for (int j = 0; j < L[0] - L[2].ToString().Length; j++)
                    {
                        Console.Write(' ');
                    }
                    Console.Write(A[2]);
                }
                else
                {
                    for (int j = 0; j < i * 2; j++)
                    {
                        Console.Write(' ');
                    }
                    Console.WriteLine('\\');
                }
            }
        }
    }
}