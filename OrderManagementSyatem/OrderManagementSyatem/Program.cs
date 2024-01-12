using OrderManagementSyatem.DAO;
using OrderManagementSyatem.Entity;
using System;
using System.Collections.Generic;
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
                int flag = 0;
                do
                {
                    Console.WriteLine("Welcome to Order Management system");
                    Console.WriteLine("Enter 1 to Create User");
                    Console.WriteLine("Enter 2 to create Product");
                    Console.WriteLine("Enter 3 to Get all from Products");
                    Console.WriteLine("Enter 4 to Place order");
                    Console.WriteLine("Enter 5 to cancle order");
                    Console.WriteLine("Enter 6 to order by customer");
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
                                int a = int.Parse(Console.ReadLine());
                                if (a == 1)
                                {
                                    regcustomers.Role = "Admin";
                                }
                                else if (a == 2)
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
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Product Cannot be added");
                                    Console.ReadLine();
                                }
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
                                        Console.WriteLine($"\n\nProduct id={p.product_id}  Name={p.ProductName}  Price={p.Price}  Description={p.Description}  Type={p.type}");
                                        Console.ReadLine();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No product in the inventory");
                                    Console.ReadLine();
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadLine();
                            }
                            break;
                        case 4:
                            break;


                        case 5:
                            try
                            {
                                Console.WriteLine("To cancle order");
                                Console.WriteLine("Enter User id");
                                int userid = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Order id");
                                int orderid = int.Parse(Console.ReadLine());
                                IOrderManagementRepository cancleorder = new OrderProcessor();
                                bool cancled = cancleorder.cancelOrder(userid, orderid);
                                if (cancled)
                                {
                                    Console.WriteLine("Order Cancled");
                                }
                                else
                                {
                                    Console.WriteLine("Kindly enter valid combination of order id and user id");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;


                        case 6:
                            try
                            {
                                User user = new User();
                                List<Tuple<Orders, Products>> listcustomerOrders = new List<Tuple<Orders, Products>>();
                                Console.WriteLine("Enter user id");
                                user.UserId = int.Parse(Console.ReadLine());
                                IOrderManagementRepository Customerorders = new OrderProcessor();
                                listcustomerOrders = Customerorders.getOrderByUser(user);
                                if (listcustomerOrders != null)
                                {
                                    foreach (var order in listcustomerOrders)
                                    {

                                        Console.WriteLine($"Orderid={order.Item1.Orderid} order date: {order.Item1.Orderdate} Product id: {order.Item2.product_id} Product Name: {order.Item2.ProductName} Description: {order.Item2.Description} Type={order.Item2.Type}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Order detail not found");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;


                        case 7:
                            flag = 1;
                            break;

                        default:
                            Console.WriteLine("Choose a valid choice");
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
