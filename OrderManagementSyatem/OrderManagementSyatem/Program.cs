﻿using OrderManagementSyatem.DAO;
using OrderManagementSyatem.Entity;
using OrderManagementSyatem.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSyatem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                OrderProcessor validation = new OrderProcessor();
                int flag = 0;
                do
                {
                    Console.WriteLine("Welcome to Order Management system");
                    Console.WriteLine("Enter 1 to Create User");
                    Console.WriteLine("Enter 2 to create Product");
                    Console.WriteLine("Enter 3 to Get all from Products");
                    Console.WriteLine("Enter 4 to cancle order");
                    Console.WriteLine("Enter 5 to order by customer");
                    Console.WriteLine("Enter 6 to Place Order");
                    Console.WriteLine("Enter 7 to Exit");
                    Console.WriteLine("Enter your choice");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                User regcustomers = new User();
                                Console.WriteLine("Welcome to Customers Registration Portal");
                                Console.WriteLine("Enter User name");
                                regcustomers.UserName = Console.ReadLine();
                                Console.WriteLine("Enter Password");
                                regcustomers.Password = Console.ReadLine();
                                Console.WriteLine("Enter 1 for Admin Role or 2 for User");
                                int b = int.Parse(Console.ReadLine());
                                if (b == 1)
                                {
                                    regcustomers.Role = "Admin";
                                }
                                else if (b == 2)
                                {
                                    regcustomers.Role = "User";
                                }
                                else
                                {
                                    throw new Exception("Use a right role choice");
                                }
                                IOrderManagementRepository customerregistration = new OrderProcessor();
                                bool registration = customerregistration.createUser(regcustomers);
                                if (registration == true)
                                {
                                    Console.WriteLine("Customer Registered");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Customer Cannot be registered");
                                    Console.ReadLine();
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadLine();
                            }
                            break;


                        case 2:
                            try
                            {
                                User admin = new User();
                                Products addproduct = new Products();
                                Console.WriteLine("Welcome to product entry portal");
                                Console.WriteLine("enter your customer id");
                                admin.UserId = int.Parse(Console.ReadLine());
                                validation.validateusererexistandisadmin(admin);
                                Console.WriteLine("Enter Product name");
                                addproduct.ProductName = Console.ReadLine();
                                Console.WriteLine("Enter Product price");
                                addproduct.Price = decimal.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Product Description");
                                addproduct.Description = Console.ReadLine();
                                Console.WriteLine("Enter Quantity in Stock ");
                                addproduct.quantityInStock = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Type\n 1 for Electronics\n 2 for Clothings");
                                int type = int.Parse(Console.ReadLine());
                                if (type == 1)
                                {
                                    addproduct.type = "Electronics";
                                }
                                else if (type == 2)
                                {
                                    addproduct.type = "Clothing";
                                }
                                IOrderManagementRepository productaddition = new OrderProcessor();
                                bool product_addition = productaddition.createProduct(admin, addproduct);
                                if (product_addition == true)
                                {
                                    Console.WriteLine("Product Added");
                                }
                                else
                                {
                                    Console.WriteLine("Product Cannot be added");
                                }
                            }
                            catch (UserNotFoundException unf)
                            {
                                Console.WriteLine(unf.Message+" or user is not admin\n");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;


                        case 3:
                            try
                            {
                                Products viewallproducts = new Products();
                                IOrderManagementRepository viewcart = new OrderProcessor();
                                List<Products> products = viewcart.getAllProducts();
                                if (products != null)
                                {
                                    foreach (Products p in products)
                                    {
                                        Console.WriteLine($"\nProduct id={p.product_id}  Name={p.ProductName}  Price={p.Price}  Description={p.Description}  Type={p.type}");
                                        
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No product in the inventory\n");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;



                        case 4:
                            try
                            {
                                Console.WriteLine("To cancle order");
                                Console.WriteLine("Enter User id");
                                int userid = int.Parse(Console.ReadLine());
                                validation.validateuser(userid);
                                Console.WriteLine("Enter Order id");
                                int orderid = int.Parse(Console.ReadLine());
                                IOrderManagementRepository cancleorder = new OrderProcessor();
                                bool cancled = cancleorder.cancelOrder(userid, orderid);
                                if (cancled)
                                {
                                    Console.WriteLine("\nOrder Cancled");
                                }
                                else
                                {
                                    Console.WriteLine("\nKindly enter valid combination of order id and user id");
                                }
                            }
                            catch (UserNotFoundException unf)
                            {
                                Console.WriteLine(unf.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;


                        case 5:
                            try
                            {
                                User user = new User();
                                List<Tuple<Orders, Products>> listcustomerOrders = new List<Tuple<Orders, Products>>();
                                Console.WriteLine("Enter user id");
                                user.UserId = int.Parse(Console.ReadLine());
                                validation.validateuser(user.UserId);
                                IOrderManagementRepository Customerorders = new OrderProcessor();
                                listcustomerOrders = Customerorders.getOrderByUser(user);
                                if (listcustomerOrders != null)
                                {
                                    foreach (var order in listcustomerOrders)
                                    {

                                        Console.WriteLine($"\nOrderid={order.Item1.Orderid} order date: {order.Item1.Orderdate} Product id: {order.Item2.product_id} Product Name: {order.Item2.ProductName} Description: {order.Item2.Description} Type={order.Item2.Type}");
                                    }
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Order detail not found");
                                }
                            }
                            catch (UserNotFoundException unf)
                            {
                                Console.WriteLine(unf.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;



                        case 6:
                            try
                            {
                                int flagplaceorder = 0;
                                User customerplacingorder = new User();
                                Console.WriteLine("Welcome to order placing portal");
                                Console.WriteLine("Enter User id");
                                customerplacingorder.UserId = int.Parse(Console.ReadLine());
                                validation.validateuser(customerplacingorder.UserId);
                                List<Products> orderinglist = new List<Products>();
                                do
                                {
                                    try
                                    {
                                        Products orderproduct = new Products();
                                        Console.WriteLine("Enter Product id");
                                        orderproduct.product_id = int.Parse(Console.ReadLine());
                                        validation.validateProductexistance(orderproduct.product_id);
                                        orderinglist.Add(orderproduct);
                                    }
                                    catch (ProductNotFoundException pnf)
                                    {
                                        Console.WriteLine(pnf.Message);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    Console.WriteLine("To add more Enter 1 else enter any other integer");
                                    int addmore = int.Parse(Console.ReadLine());
                                    if (addmore == 1)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        flagplaceorder = 1;
                                    }
                                } while (flagplaceorder != 1);

                                IOrderManagementRepository placeorder = new OrderProcessor();
                                bool placed = placeorder.createOrder(customerplacingorder, orderinglist);
                                if (placed == true)
                                {
                                    Console.WriteLine("order Placed\n\n");
                                }
                                else
                                {
                                    Console.WriteLine("Order canot be placed\n");
                                }
                            }
                            catch (UserNotFoundException unf)
                            {
                                Console.WriteLine(unf.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case 7:
                            flag = 1;
                            break;

                        default:
                            Console.WriteLine("Please Choose a valid choice");
                            break;
                    }

                } while (flag != 1);
            }
            catch (Exception overall)
            {
                Console.WriteLine(overall.Message);
            }
        }
    }
}
