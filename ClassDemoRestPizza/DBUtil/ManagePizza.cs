using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PizzaLib.model;

namespace ClassDemoRestPizza.DBUtil
{
    public class ManagePizza
    {
        private const String connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=ClassDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";



        private const String GET_ALL = "select * from Pizza";

        public IEnumerable<Pizza> Get()
        {
            List<Pizza> liste = new List<Pizza>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pizza item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }
            return liste;
        }

        private Pizza ReadNextElement(SqlDataReader reader)
        {
            Pizza pizza = new Pizza();

            pizza.Nr = reader.GetInt32(0);
            pizza.Desciption = reader.GetString(1);
            pizza.FamilyPizza = reader.GetBoolean(2);
            pizza.Price = reader.GetInt32(3);


            return pizza;
        }
    }
}
