using AcademyG.Week6.Test.Client.ManagementService;
using AcademyG.Week6.Test.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyG.Week6.Test.Client
{
    public static class ClientOperations
    {

        public static async Task Start()
        {
            Console.WriteLine("=== TestWeek6 - WCF Client ===");

            int op;

            do
            {
                Console.Clear();
                Console.WriteLine("Inserire l'operazione che si vuole effettuare");
                Console.WriteLine("0) Visualizzare tutti i cliente\n" +
                                  "1) Visualizzare un cliente\n" +
                                  "2) Aggiungere un cliente\n" +
                                  "3) Modificare un cliente\n" +
                                  "4) Eliminare un cliente\n" +
                                  "-) Uscire");

                if (!int.TryParse(Console.ReadLine(), out op))
                    op = -1;

                switch (op)
                {
                    case 0:
                        await GetAllClients();
                        break;
                    case 1:
                        await GetClientByID();
                        break;
                    case 2:
                        await AddNewClient();
                        break;
                    case 3:
                        await EditClient();
                        break;
                    case 4:
                        await DeleteClient();
                        break;
                    default:
                        Console.WriteLine("Uscita in corso...");
                        break;
                }
            }
            while (op >= 0 && op <= 4);
        }

        public static async Task GetAllClients()
        {
            ManagementServiceClient wcfClient = new ManagementServiceClient();

            //devo mettere tutto il namespace perchè entra in conflitto questo namespace
            IEnumerable<AcademyG.Week6.Test.Client.ManagementService.Client> result = await wcfClient.GetAllClientsAsync();

            foreach (var item in result)
                Console.WriteLine($"{item.Id} - {item.ClientCode} {item.LastName} {item.FirstName}");

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();
        }

        public static async Task GetClientByID()
        {
            ManagementServiceClient wcfClient = new ManagementServiceClient();

            Console.Clear();
            Console.WriteLine("Inserisci l'id dell'ordine che si vuole visualizzare");
            int.TryParse(Console.ReadLine(), out int id);

            //devo mettere tutto il namespace perchè entra in conflitto questo namespace
            AcademyG.Week6.Test.Client.ManagementService.Client result = await wcfClient.GetClientByIdAsync(id);
            
            Console.WriteLine($"{result.Id} - {result.ClientCode} {result.LastName} {result.FirstName}");

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();
        }

        public static async Task AddNewClient()
        {
            ManagementServiceClient wcfClient = new ManagementServiceClient();

            Console.Clear();
            Console.WriteLine("Inserisci il codice cliente");
            string code = Console.ReadLine();
            Console.WriteLine("Inserisci il nome cliente");
            string firstName = Console.ReadLine();
            Console.WriteLine("Inserisci il cognome cliente");
            string lastname = Console.ReadLine();

            AcademyG.Week6.Test.Client.ManagementService.Client newClient = new AcademyG.Week6.Test.Client.ManagementService.Client
            {
                ClientCode = code,
                FirstName = firstName,
                LastName = lastname
            };


            //devo mettere tutto il namespace perchè entra in conflitto questo namespace
            if (await wcfClient.AddNewClientAsync(newClient))
                Console.WriteLine("Operazione completata");

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();

        }

        public static async Task EditClient()
        {
            ManagementServiceClient wcfClient = new ManagementServiceClient();

            Console.Clear();
            Console.WriteLine("Inserisci l'id del cliente");
            int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nInserisci il codice cliente");
            string code = Console.ReadLine();
            Console.WriteLine("Inserisci il nome cliente");
            string firstName = Console.ReadLine();
            Console.WriteLine("Inserisci il cognome cliente");
            string lastname = Console.ReadLine();

            AcademyG.Week6.Test.Client.ManagementService.Client newClient = new AcademyG.Week6.Test.Client.ManagementService.Client
            {
                ClientCode = code,
                FirstName = firstName,
                LastName = lastname
            };


            //devo mettere tutto il namespace perchè entra in conflitto questo namespace
            if (await wcfClient.UpdateClientAsync(newClient, id))
                Console.WriteLine("Operazione completata");

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();
        }

        public static async Task DeleteClient()
        {
            ManagementServiceClient wcfClient = new ManagementServiceClient();

            Console.Clear();
            Console.WriteLine("Inserisci l'id del cliente");
            int.TryParse(Console.ReadLine(), out int id);

            //devo mettere tutto il namespace perchè entra in conflitto questo namespace
            if (await wcfClient.DeleteClientByIdAsync(id))
                Console.WriteLine("Operazione completata");

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();
        }

    }
}
