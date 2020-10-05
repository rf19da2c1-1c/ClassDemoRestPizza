using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PizzaLib.model;

namespace PizzaConsumer
{
    internal class RestWorker
    {
        private const String URI = "http://localhost:54395/api/Pizzas/";


        public RestWorker()
        {
        }

        public async void Start()
        {
            PrintHeader("Henter alle Pizzaer");
            List<Pizza> allPizzas = await GetAllPizzas();
            foreach (Pizza pizza in allPizzas)
            {
                Console.WriteLine(pizza);
            }


            PrintHeader("Henter Pizza nr 18");
            try
            {
                Pizza pizza1 = await GetOnePizza(18);
                Console.WriteLine("pizza= " + pizza1);
            }
            catch (KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }

            PrintHeader("Henter Pizza nr 2");
            try
            {
                Pizza pizza1 = await GetOnePizza(2);
                Console.WriteLine("pizza= " + pizza1);
            }
            catch (KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }


            PrintHeader("Opretter en  Pizza");
            Pizza nyPizza = new Pizza(101, "bare en pizza", false, 55);
            await OpretNyPizza(nyPizza);

            // udskriver alle pizzaer
            allPizzas = await GetAllPizzas();
            foreach (Pizza pizza in allPizzas)
            {
                Console.WriteLine(pizza);
            }


            PrintHeader("Ændre Pizza nr 101");
            nyPizza.Desciption = "En dajli pizza";
            await OpdaterPizza(nyPizza);

            // udskriver alle pizzaer
            allPizzas = await GetAllPizzas();
            foreach (Pizza pizza in allPizzas)
            {
                Console.WriteLine(pizza);
            }

            PrintHeader("Sletter Pizza nr 101");
            await SletPizza(101);

            // udskriver alle pizzaer
            allPizzas = await GetAllPizzas();
            foreach (Pizza pizza in allPizzas)
            {
                Console.WriteLine(pizza);
            }




        }




        private void PrintHeader(string txt)
        {
            Console.WriteLine("=========================");
            Console.WriteLine(txt);
            Console.WriteLine("=========================");

        }

        public async Task<List<Pizza>> GetAllPizzas()
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(URI);
                List<Pizza> pizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);
                return pizzas;
            }

        }

        private async Task<Pizza> GetOnePizza(int nr)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(URI + nr);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    Pizza pizza = JsonConvert.DeserializeObject<Pizza>(json);
                    return pizza;
                }

                throw new KeyNotFoundException(
                    $"Fejl code={resp.StatusCode} message={await resp.Content.ReadAsStringAsync()}");
            }

        }

        private async Task OpretNyPizza(Pizza pizza)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(pizza),
                    Encoding.UTF8,
                    "application/json");

                HttpResponseMessage resp = await client.PostAsync(URI, content);
                if (resp.IsSuccessStatusCode)
                {
                    return;
                }

                throw new ArgumentException("opret fejlede");
            }

        }


        private async Task OpdaterPizza(Pizza pizza)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(pizza),
                    Encoding.UTF8,
                    "application/json");

                HttpResponseMessage resp = await client.PutAsync(URI + pizza.Nr, content);
                if (resp.IsSuccessStatusCode)
                {
                    return;
                }

                throw new ArgumentException("opdater fejlede");
            }
        }


        private async Task SletPizza(int nr)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.DeleteAsync(URI + nr);
                if (resp.IsSuccessStatusCode)
                {
                    return;
                }

                throw new ArgumentException("slet fejlede");
            }
        }

    }

}
