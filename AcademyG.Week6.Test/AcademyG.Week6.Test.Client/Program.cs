using AcademyG.Week6.Test.Client.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AcademyG.Week6.Test.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {

            int op;

            do
            {
                Console.WriteLine("Quali operazioni si vogliono eseguire");
                Console.WriteLine("0) Operazioni cliente\n" +
                                  "1) Operazione ordine");
                int.TryParse(Console.ReadLine(), out op);


                if (op == 0)
                    await ClientOperations.Start();
                else
                    await OrderOperations.Start();

            } while (op == 0 || op == 1);
        }
    }
}
