using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoAssignment_2
{
    class Program
    {
        public static string decision;
        static void Main(string[] args)
        {

            SqlConnection sqlConnection = null;
            try
            {

                sqlConnection = new SqlConnection("data source=.; database=Company; integrated security=SSPI");
                string value = "create table dbo.Sales(OrderId int primary key not null,OrderDate date not null, Quantity int not null,Price money not null,Status nChar(20) not null)";

                SqlCommand sqlCommand = new SqlCommand(value, sqlConnection);


                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("table successfully created");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
             
            finally
            {
                sqlConnection.Close();
            }
            do
            {
                Console.WriteLine("enter the order id:");
                int orderId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter the order date:");
                DateTime orderDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter the order quantity:");
                int quantity = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter the price:");
                int price = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter the order status:pending or success");
                string status = Console.ReadLine();

                string value1 = "Insert into Sales(OrderId,OrderDate,Quantity,Price,Status) values(@id,@orderdate,@quantity,@price,@status)";
                SqlCommand sqlCommand1 = new SqlCommand(value1, sqlConnection);

                sqlCommand1.Parameters.AddWithValue("@id", orderId);
                sqlCommand1.Parameters.AddWithValue("@orderdate", orderDate);
                sqlCommand1.Parameters.AddWithValue("@quantity", quantity);
                sqlCommand1.Parameters.AddWithValue("@price", price);
                sqlCommand1.Parameters.AddWithValue("@status", status);

                try
                {
                    sqlConnection.Open();
                    sqlCommand1.ExecuteNonQuery();
                    Console.WriteLine("values successfully added");

                    Console.WriteLine("do you continue:yes or no");
                     decision= Console.ReadLine();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                finally
                {
                    sqlConnection.Close();
                }
            } while (decision!="no");

            try
            {
                sqlConnection.Open();
               
                string value2="Select s.OrderId from Sales s where s.Status ='Pending'";
                SqlCommand sqlCommand2 = new SqlCommand(value2, sqlConnection);
                SqlDataReader sqlDataReader=sqlCommand2.ExecuteReader();

                if(sqlDataReader.HasRows)
                {
                    while(sqlDataReader.Read())
                    {
                        Console.WriteLine("Order Id :{0} is pending", sqlDataReader[0]);
                    }
                    

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                sqlConnection.Close();
            }


            try
            {
                sqlConnection.Open();

                string value3 = "exec YearlySales 7";
                SqlCommand sqlCommand3 = new SqlCommand(value3, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand3.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine("Sales {0} is above target", sqlDataReader[1]);
                    }
                    Console.ReadLine();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                sqlConnection.Close();
            }



        }
    }
}
    

