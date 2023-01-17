using System.Reflection;

namespace CautareMotoreta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("va doriti un motocross?");
            string raspuns = Console.ReadLine();
            if (raspuns == "da")
            {
                Console.WriteLine("care este bugetul dvs?");
                int buget = Convert.ToInt32(Console.ReadLine());
                if (buget <= 200)
                {
                    Console.WriteLine("Optiuni:\nYamaha\ncan am\nktm\nAlegeti unul");
                    string model = Console.ReadLine();
                    switch (model)
                    {
                        case "yamaha":
                            Console.WriteLine("https://www.yamahamotorsports.com/motocross");
                            break;
                        case "can am":
                            Console.WriteLine("https://www.bing.com/search?q=motocross+can+am&cvid=80970fbaba4644dab73ab55c39e30650&aqs=edge.0.69i59j0l7j69i64.5478j0j9&FORM=ANAB01&PC=EDGEDB");
                            break;
                        case "ktm":
                            Console.WriteLine("https://www.ktm.com/en-us/models/mx.html");
                            break;
                        default:
                            Console.WriteLine("Nu ati introdus un model valid");
                            break;

                    }
                }
                else if (buget > 200 && buget <= 500)
                {
                    Console.WriteLine("Optiuni:\nHonda\ncan am\nktm\nYamaha\nAlegeti unul");
                    string model = Console.ReadLine();
                    switch (model)
                    {
                        case "honda":
                            Console.WriteLine("https://www.bing.com/search?q=honda+motocross&cvid=bbee533c273f4478a58076e56b0b001c&aqs=edge.0.0l9.2796j0j4&FORM=ANAB01&PC=EDGEDB");
                            break;
                        case "yamaha":
                            Console.WriteLine("https://www.yamahamotorsports.com/motocross");
                            break;
                        case "can am":
                            Console.WriteLine("https://www.bing.com/search?q=motocross+can+am&cvid=80970fbaba4644dab73ab55c39e30650&aqs=edge.0.69i59j0l7j69i64.5478j0j9&FORM=ANAB01&PC=EDGEDB");
                            break;
                        case "ktm":
                            Console.WriteLine("https://www.ktm.com/en-us/models/mx.html");
                            break;
                        default:
                            Console.WriteLine("Nu ati introdus un model valid");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Optiuni:\ndirt bike\nHonda\ncan am\nktm\nYamaha\nAlegeti unul");
                    string model = Console.ReadLine();
                    switch (model)
                    {
                        case "dirt byke":
                            Console.WriteLine("https://www.dirtbikes.com/");
                            break;
                        case "honda":
                            Console.WriteLine("https://www.bing.com/search?q=honda+motocross&cvid=bbee533c273f4478a58076e56b0b001c&aqs=edge.0.0l9.2796j0j4&FORM=ANAB01&PC=EDGEDB");
                            break;
                        case "yamaha":
                            Console.WriteLine("https://www.yamahamotorsports.com/motocross");
                            break;
                        case "can am":
                            Console.WriteLine("https://www.bing.com/search?q=motocross+can+am&cvid=80970fbaba4644dab73ab55c39e30650&aqs=edge.0.69i59j0l7j69i64.5478j0j9&FORM=ANAB01&PC=EDGEDB");
                            break;
                        case "ktm":
                            Console.WriteLine("https://www.ktm.com/en-us/models/mx.html");
                            break;
                        default:
                            Console.WriteLine("Nu ati introdus un model valid");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("La revedere!");
            }
        }
    }
}