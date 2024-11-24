using Microsoft.VisualBasic;
using System;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using static Module7.Programm;
using System.Text.RegularExpressions;

namespace Module7
{
    class Programm
    {
        class Base
        {
            public void Display<T>(T arg)
            {
                Console.WriteLine(arg);
            }
        }

        public static class Check
        {
            public static bool IsValidAddress(object s)
            {
                Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]$");
                return regex.IsMatch((string)s);
            }
        }
                 
        abstract class Address
        {
            public string City;
            public string Street;
            public Address(string city, string street)
            {
                City = city;
                Street = street;
            }         
        }

        class Catalog
        {
            private TypeGroupProduct typeGroupProduct;
            public Catalog()
            {
                typeGroupProduct = new TypeGroupProduct();
            }
        }

        class TypeGroupProduct
        {
            public object GroupProduct;

            public void Display()
            {
                Base obj = (Base)GroupProduct;
                obj.Display<object>(GroupProduct);
            }
        }

        class Product : TypeGroupProduct
        {
            public static string Name;
            public static int Price;
        }

        class ComplectProducts
        {
            private Product product;
            public ComplectProducts(Product product)
            {
                this.product = product;
            }
        }

        class Buyer
        {
            public string Name;
            public int NumberPhone;
            public Buyer(string name, int numberPhone)
            {
                this.Name = name;
                this.NumberPhone = numberPhone;
            }
        }

        class TotalPrice
        {
            public int Price = Product.Price;
            public static TotalPrice operator + (TotalPrice a, TotalPrice b) 
            {
                return new TotalPrice
                {
                    Price = a.Price + b.Price,
                };
            }

            public static implicit operator int(TotalPrice v)
            {
                throw new NotImplementedException();
            }
        }

        abstract class Delivery 
        {
            internal object Buyer;
            internal object Product;
            internal abstract int TotalPrice { get; set; }
            public Delivery(Buyer buyer, Product product, TotalPrice totalPrice)
            {
                Buyer = buyer;
                Product = product;
                TotalPrice = totalPrice;
            }
            
            public abstract void Display();
        }

        class HomeDelivery : Delivery
        {
            protected static object Address;
            public HomeDelivery(Address address, Buyer buyer, Product product, TotalPrice totalPrice) : base(buyer, product, totalPrice)
            {
                Address = address;
            }

            internal override int TotalPrice
            {
                get
                {
                    return TotalPrice;
                }
                set
                {
                    if (value < 500)
                    {
                        Console.WriteLine("Сумма заказа для доставки на дом должна быть не менее 500 руб.");
                    }
                    else
                    {
                        TotalPrice = value;
                    }
                }
            }

            public override void Display()
            {
                Console.WriteLine(Check.IsValidAddress(Address));
                Console.WriteLine("Товар {0} на сумму {1} будет доставлен на дом по адресу {2}, покупатель {3}", Product, TotalPrice, Address, Buyer);
            }           
        }

        class PickPointDelivery : Delivery
        {
            
            class PickPoin
            {
                public static string NamePoint;
                public static string AddressPoint;
            }
            class PickPoinList
            {
                private PickPoin[] list;
                public PickPoinList(PickPoin[] list)
                {
                    this.list = list;
                }

                public PickPoin this[int index]
                {
                    get
                    {
                        return list[index];
                    }
                    set
                    {
                        list[index] = value;
                    }
                }                
            }

            internal override int TotalPrice
            {
                get
                {
                    return TotalPrice;
                }
                set
                {
                    if (value < 500)
                    {
                        Console.WriteLine("Сумма заказа для доставки в пункт выдачи должна быть не менее 500 руб.");
                    }
                    else
                    {
                        TotalPrice = value;
                    }
                }
            }

            string NamePoint = PickPoin.NamePoint;
            string AddressPoint = PickPoin.AddressPoint;

            public PickPointDelivery(Buyer buyer, Product product, TotalPrice totalPrice) : base(buyer, product, totalPrice)
            {
            }

            public override void Display()
            {
                Console.WriteLine("Товар {0} на сумму {1} будет доставлен в {2} по адресу {3}, покупатель {4}", Product, TotalPrice, NamePoint, AddressPoint, Buyer);
            }

        }

        class ShopDelivery : Delivery
        {
            class Shop
            {
                public static string NameShop;
                public static string AddressShop;
            }
            class ShopList
            {
                private Shop[] list;
                public ShopList(Shop[] list)
                {
                    this.list = list;
                }

                public Shop this[int index]
                {
                    get
                    {
                        return list[index];
                    }
                    set
                    {
                        list[index] = value;
                    }
                }

            }

            internal override int TotalPrice
            {
                get
                {
                    return TotalPrice;
                }
                set
                {
                    TotalPrice = value;
                }
            }

            string NameShop = Shop.NameShop;
            string AddressShop = Shop.AddressShop;

            public ShopDelivery(Buyer buyer, Product product, TotalPrice totalPrice) : base(buyer, product, totalPrice)
            {
            }

            public override void Display()
            {
                Console.WriteLine("Товар {0} на сумму {1} будет доставлен в {2} по адресу {3}, покупатель {4}", Product, TotalPrice, NameShop, AddressShop, Buyer);
            }
        }

        class Order<TDelivery, 
        TStructOrder> where TDelivery : Delivery
        {
            public TDelivery Delivery;
            public int Number;
            public string Description;
            public void DisplayAddress()
            {
                Console.WriteLine(Number);
            }

        }

        class MyOrder<TDelivery,
        TStructOrder> : Order<TDelivery,
        TStructOrder> where TDelivery : Delivery
        {         
            public void DisplayMyOrder()
            {
                Console.WriteLine("Мой заказ {0} на сумму {1}", Delivery.Product, Delivery.TotalPrice);
            }
        }

        static void Main(string[] args)
        {
           Console.ReadKey();
        }
    }
}