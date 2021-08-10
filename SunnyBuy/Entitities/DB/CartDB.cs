﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using SunnyBuy.Services.CartServices.Models;

namespace SunnyBuy.Entitities.DB
{
    public class CartDB
    {
        string path = @"C:\Users\debora.maciel\Desktop\Techer Projects C#\SunnyBuy\Files\Cart.csv";

        string header = "";

        public List<Cart> CartsListDB()
        {
            List<Cart> cartsList = new List<Cart>();

            try
            {
                var carts = File.ReadAllLines(path, Encoding.UTF8);

                header = carts[0];

                carts.Skip(1)
                    .ToList()
                    .ForEach(p => {

                        var fields = p.Split(';');

                        var cart = new Cart();

                        cart.CartId = int.Parse(fields[0]);
                        cart.ProductId = int.Parse(fields[1]);
                        cart.ClientId = int.Parse(fields[2]);
                        cart.DateInclude = DateTime.Parse(fields[3]);
                        cart.Deleted = bool.Parse(fields[4]);

                        cartsList.Add(cart);
                    });
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocurred an error");
                Console.WriteLine(e.Message);
            }
            return cartsList;
        }

        public bool PostCartEntity(PostModel model)
        {
            try
            {
                var carts = CartsListDB();

                var modelEntity = new Cart();

                modelEntity.CartId = carts.Count() + 1;
                modelEntity.ProductId = model.ProductId;
                modelEntity.ClientId = model.UserId;
                modelEntity.DateInclude = model.DateInclude;
                modelEntity.Deleted = model.Deleted;

                carts.Add(modelEntity);

                var lines = new List<string>();

                lines.Add(header);

                foreach (var item in carts)
                {
                    var aux = new string[] { item.CartId.ToString(), item.ProductId.ToString(), item.ClientId.ToString(), item.DateInclude.ToString(), item.Deleted.ToString() };
                    lines.Add(String.Join(";", aux));
                }

                File.WriteAllLines(path, lines);
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocurred an error");
                Console.WriteLine(e.Message);
            }
            return true;
        }

        public bool PutCartEntity(PutModel model)
        {
            try
            {
                var carts = CartsListDB();

                var index = carts.FindIndex(a => a.CartId == model.CartId);
                carts[index].Deleted = model.Deleted;

                var lines = new List<string>();

                lines.Add(header);

                foreach (var item in carts)
                {
                    var aux = new string[] { item.CartId.ToString(), item.ProductId.ToString(), item.ClientId.ToString(), item.DateInclude.ToString(), item.Deleted.ToString() };
                    lines.Add(String.Join(";", aux));
                }

                File.WriteAllLines(path, lines);
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocurred an error");
                Console.WriteLine(e.Message);
            }
            return true;
        }
    }
}
