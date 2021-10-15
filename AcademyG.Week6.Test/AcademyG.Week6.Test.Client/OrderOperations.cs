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
    public static class OrderOperations
    {
        private static string url = "https://localhost:44310/api/orders";

        public static async Task Start()
        {
            Console.WriteLine("=== TestWeek6 - API Order ===");

            int op;

            do
            {
                Console.Clear();
                Console.WriteLine("Inserire l'operazione che si vuole effettuare");
                Console.WriteLine("0) Visualizzare tutti gli ordini\n" +
                                  "1) Visualizzare un ordine\n" +
                                  "2) Aggiungere un ordine\n" +
                                  "3) Modificare un ordine\n" +
                                  "4) Eliminare un ordine\n" +
                                  "-) Uscire");

                if (!int.TryParse(Console.ReadLine(), out op))
                    op = -1;

                switch (op)
                {
                    case 0:
                        await GetAllOrders();
                        break;
                    case 1:
                        await GetOrderById();
                        break;
                    case 2:
                        await AddOrder();
                        break;
                    case 3:
                        await UpdateOrder();
                        break;
                    case 4:
                        await DeleteOrder();
                        break;
                    default:
                        Console.WriteLine("Uscita in corso...");
                        break;
                }
            }
            while (op >= 0 && op <= 4);
        }

        public static async Task GetAllOrders()
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            Console.Clear();
            if (httpResponse.IsSuccessStatusCode)
            {
                string jsonData = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<OrderContract>>(jsonData);

                foreach (var item in result)
                    Console.WriteLine($"[{item.Id}] {item.OrderCode} {item.OrderDate} {item.ProductCode} {item.Amount}");

            }

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();
        }

        public static async Task GetOrderById()
        {
            bool flag;
            int id;

            HttpClient httpClient = new HttpClient();
            
            do
            {
                Console.Clear();
                Console.WriteLine("Inserisci l'id dell'ordine che si vuole visualizzare");

                flag = int.TryParse(Console.ReadLine(), out id) || id <= 0;

                if (!flag)
                {
                    Console.WriteLine("Errore nell'inserimento dell'id. Riprovare");
                }
            } while (!flag);

            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url + "/" + id)
            };

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            Console.Clear();
            if (httpResponse.IsSuccessStatusCode)
            {
                string jsonData = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrderContract>(jsonData);
                Console.WriteLine($"[{result.Id}] {result.OrderCode} {result.OrderDate} {result.ProductCode} {result.Amount}");

            }
            else
            {
                Console.WriteLine("Ordine non trovato!");
            }

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();
        }

        public static async Task AddOrder()
        {
            HttpClient httpClient = new HttpClient();

            Console.Clear();
            Console.WriteLine("Inserire il codice ordine");
            string code = Console.ReadLine();
            Console.WriteLine("Inserire il codice prodotto");
            string prCode = Console.ReadLine();
            Console.WriteLine("Inserire l'importo");
            decimal.TryParse(Console.ReadLine(), out decimal amount);
            Console.WriteLine("Inserire l'id del cliente");
            int.TryParse(Console.ReadLine(), out int idClient);

            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url)
            };

            OrderContract newOrder = new OrderContract
            {
                OrderDate = DateTime.Now,
                OrderCode = code,
                ProductCode = prCode,
                Amount = amount,
                ClientId = idClient
            };

            string jsonData = JsonConvert.SerializeObject(newOrder);
            httpRequest.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                jsonData = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrderContract>(jsonData);

                Console.WriteLine($"[{result.Id}] {result.OrderCode} {result.OrderDate} {result.ProductCode} {result.Amount}");
            }

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();
        }

        public static async Task UpdateOrder()
        {
            HttpClient httpClient = new HttpClient();

            Console.Clear();

            Console.WriteLine("Inserisci l'id dell'ordine che si vuole visualizzare");
            int.TryParse(Console.ReadLine(), out int id);

            Console.WriteLine("Inserire la data dell'ordine");
            DateTime.TryParse(Console.ReadLine(), out DateTime orderDate);
            Console.WriteLine("Inserire il codice ordine");
            string code = Console.ReadLine();
            Console.WriteLine("Inserire il codice prodotto");
            string prCode = Console.ReadLine();
            Console.WriteLine("Inserire l'importo");
            decimal.TryParse(Console.ReadLine(), out decimal amount);

            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(url + "/" + id)
            };

            OrderContract order = new OrderContract
            {
                OrderDate = orderDate,
                OrderCode = code,
                ProductCode = prCode,
                Amount = amount
            };

            string jsonData = JsonConvert.SerializeObject(order);
            httpRequest.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            if (httpResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Operazione completata");
            }

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();
        }

        public static async Task DeleteOrder()
        {
            HttpClient httpClient = new HttpClient();

            Console.Clear();
            Console.WriteLine("Inserisci l'id dell'ordine che si vuole visualizzare");
            int.TryParse(Console.ReadLine(), out int id);

            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url + "/" + id)
            };

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

            if (httpResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Employee Deleted!");
            }

            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();
        }
    }
}
