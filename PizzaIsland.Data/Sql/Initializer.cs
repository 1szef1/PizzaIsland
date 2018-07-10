using PizzaIsland.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaIsland.Data.Extensions;

namespace PizzaIsland.Data.Sql
{
    public class Initializer
    {
        public static void CreateDatabaseWithData()
        {
            using (var context = new PIContext())
            {
                ProductType pt1 = new ProductType();
                pt1.TypeNr = (int)ProductEnum.Pizza;
                pt1.Name = ProductEnum.Pizza.GetDescription();

                if (!context.Products.Any(x => x.Name == "Margheritta"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt1,
                        Name = "Margheritta",
                        Price = 20,
                    });
                }
                if (!context.Products.Any(x => x.Name == "Vegetariana"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt1,
                        Name = "Vegetariana",
                        Price = 22,
                    });
                }
                if (!context.Products.Any(x => x.Name == "Tosca"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt1,
                        Name = "Tosca",
                        Price = 25,
                    });
                }
                if (!context.Products.Any(x => x.Name == "Venecia"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt1,
                        Name = "Venecia",
                        Price = 25,
                    });
                }

                ProductType pt2 = new ProductType();
                pt2.TypeNr = (int)ProductEnum.PizzaAddOn;
                pt2.Name = ProductEnum.PizzaAddOn.GetDescription();
                pt2.Price = 2;

                if (!context.Products.Any(x => x.Name == "Podwójny ser"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt2,
                        Name = "Podwójny ser",
                    });
                }
                if (!context.Products.Any(x => x.Name == "Salami"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt2,
                        Name = "Salami",
                    });
                }
                if (!context.Products.Any(x => x.Name == "Szynka"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt2,
                        Name = "Szynka",
                    });
                }
                if (!context.Products.Any(x => x.Name == "Pieczarki"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt2,
                        Name = "Pieczarki",
                    });
                }

                ProductType pt3 = new ProductType();
                pt3.TypeNr = (int)ProductEnum.MainDish;
                pt3.Name = ProductEnum.MainDish.GetDescription();

                if (!context.Products.Any(x => x.Name == "Schabowy z frytkami/ryżem/ziemniakami"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt3,
                        Name = "Schabowy z frytkami/ryżem/ziemniakami",
                        Price = 30,
                    });
                }
                if (!context.Products.Any(x => x.Name == "Ryba z frytkami"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt3,
                        Name = "Ryba z frytkami",
                        Price = 28,
                    });
                }
                if (!context.Products.Any(x => x.Name == "Placek po węgiersku"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt3,
                        Name = "Placek po węgiersku",
                        Price = 27,
                    });
                }

                ProductType pt4 = new ProductType();
                pt4.TypeNr = (int)ProductEnum.MainDishAddOn;
                pt4.Name = ProductEnum.MainDishAddOn.GetDescription();

                if (!context.Products.Any(x => x.Name == "Bar sałatkowy "))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt4,
                        Name = "Bar sałatkowy ",
                        Price = 5,
                    });
                }
                if (!context.Products.Any(x => x.Name == "Zestaw sosów"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt4,
                        Name = "Zestaw sosów",
                        Price = 6,
                    });
                }

                ProductType pt5 = new ProductType();
                pt5.TypeNr = (int)ProductEnum.Soup;
                pt5.Name = ProductEnum.Soup.GetDescription();

                if (!context.Products.Any(x => x.Name == "Pomidorowa"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt5,
                        Name = "Pomidorowa",
                        Price = 12,
                    });
                }
                if (!context.Products.Any(x => x.Name == "Rosół"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt5,
                        Name = "Rosół",
                        Price = 10,
                    });
                }

                ProductType pt6 = new ProductType();
                pt6.TypeNr = (int)ProductEnum.Drink;
                pt6.Name = ProductEnum.Drink.GetDescription();
                pt6.Price = 5;

                if (!context.Products.Any(x => x.Name == "Kawa"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt6,
                        Name = "Kawa",
                    });
                }
                if (!context.Products.Any(x => x.Name == "Herbata"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt6,
                        Name = "Herbata",
                    });
                }
                if (!context.Products.Any(x => x.Name == "Cola"))
                {
                    context.Products.Add(new Product()
                    {
                        Type = pt6,
                        Name = "Cola",
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
