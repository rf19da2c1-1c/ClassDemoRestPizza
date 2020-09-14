﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaLib.model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassDemoRestPizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        // data
        private static List<Pizza> _data = new List<Pizza>()
        {
            new Pizza(1, "tomat, basilikum, ost", false, 45),
            new Pizza(2, "tomat, oregano, ost", true, 55),
            new Pizza(3, "tomat, skinke, ost", false, 45)
        };

        // GET: api/<PizzasController>
        [HttpGet]
        public IEnumerable<Pizza> Get()
        {
            return _data;
        }

        // GET api/<PizzasController>/5
        [HttpGet]
        [Route("{nr}")]
        public Pizza Get(int nr)
        {
            return _data.Find(p => p.Nr == nr);
        }


        // GET: api/<PizzasController>
        [HttpGet]
        [Route("Search")]
        public IEnumerable<Pizza> Get([FromQuery] QueryPizza query)
        {
            List<Pizza> tmpList = null;
            if (query.Topping != null)
            {
                tmpList = _data.FindAll(p => p.Desciption.Contains(query.Topping));
            }
            else
            {
                tmpList = _data;
            }


            List<Pizza> tmpList2 = null;
            if (query.Family != null)
            {
                bool familyPizza = (query.Family == "true");
                tmpList2 = _data.FindAll(p => p.FamilyPizza == familyPizza);
            }
            else
            {
                tmpList2 = tmpList;
                    ;
            }

            return tmpList2;
        }

        // POST api/<PizzasController>
        [HttpPost]
        public void Post([FromBody] Pizza value)
        {
            _data.Add(value);
        }

        // PUT api/<PizzasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Pizza value)
        {
            Pizza pizza = Get(id);
            if (pizza != null)
            {
                pizza.Nr = value.Nr;
                pizza.Desciption = value.Desciption;
                pizza.FamilyPizza = value.FamilyPizza;
                pizza.Price = value.Price;
            }
        }


        // DELETE api/<PizzasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Pizza pizza = Get(id);
            if (pizza != null)
            {
                _data.Remove(pizza);
            }
        }
    }
}
