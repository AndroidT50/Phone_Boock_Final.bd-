using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp5
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            People people = new People();
            Contact contact = new Contact();
            people.Name = new List<string>();
            Console.WriteLine("Ввести имя");
            people.Name.Add(Console.ReadLine());
            Console.WriteLine("Ввести имя2");
            people.Name.Add(Console.ReadLine());
            var nametest = string.Join(",", people.Name);
            Console.WriteLine("Введите отчество");
            people.FerstName = Console.ReadLine();
            Console.WriteLine("Введите фамилию");
            people.LastName = Console.ReadLine();
            Console.WriteLine("Ввести возраст");
            people.Age = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Введите дату рождения");
            people.DataBith = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Введите адресс");
            contact.Adress = Console.ReadLine();

            Console.WriteLine("Введите Email");
            contact.Email = new List<string>
            {
                Console.ReadLine()
            };
            var emailist = string.Join(",", contact.Email);

            Console.WriteLine("Введите номер тефонф");
            contact.PhoneNamber = new List<double> {  double.Parse(Console.ReadLine()) };
            double[] doubles = contact.PhoneNamber.ToArray();
            var phone = string.Join(",", doubles);

            string sqlExpression = $"INSERT INTO Users (Name,Age,LastName,FerstName,DataBith,Adres,Email,PhoneNumber) VALUES ('{nametest}',{people.Age},'{people.LastName}','{people.FerstName}',{people.DataBith},'{contact.Adress}','{emailist}',{phone})";
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\arhan\source\repos\dbtest\dbtest\testdb.mdf;Integrated Security=True"))
            {

                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression,connection);



                SqlParameter lastnamepametr = new SqlParameter("@lastname", people.LastName);
                command.Parameters.Add(lastnamepametr);
                SqlParameter ageparametr = new SqlParameter("@age",people.Age);
                command.Parameters.Add(ageparametr);
                SqlParameter nameParameter = new SqlParameter("@name", nametest);
                command.Parameters.Add(nameParameter);
                SqlParameter ferstParametr = new SqlParameter("@ferstname", people.FerstName);
                command.Parameters.Add(ferstParametr);
                SqlParameter dataParametr = new SqlParameter("@databith", people.DataBith);
                command.Parameters.Add(dataParametr);
                SqlParameter adressParametr = new SqlParameter("@adres", contact.Adress);
                command.Parameters.Add(adressParametr);
                SqlParameter emailParametr = new SqlParameter("@email",emailist);
                command.Parameters.Add(emailParametr);
                SqlParameter phoneParametr = new SqlParameter("@phonenamber", phone);
                command.Parameters.Add(phoneParametr);



                int number = await command.ExecuteNonQueryAsync();
                Console.WriteLine($"Добавлено объектов: {number}");
                Console.WriteLine("Подключение закрыто");
            }
            
            Console.Read();
        }
    }
}
