using AcademyG.Week6.Test.WCF;
using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyG.Week6.Test.SelfHostingWCF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ManagementService)))
            {
                host.Open();

                Console.WriteLine("=== Library WCF Running ===");
                Console.WriteLine("Press any key to end");
                Console.ReadKey();
            }
        }
    }
}
