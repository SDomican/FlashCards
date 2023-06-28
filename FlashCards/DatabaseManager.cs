using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards
{
    internal class DatabaseManager
    {

        private string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public void Initialise()
        {

            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                Console.WriteLine($"Connection established successfully: {connectionString}");
                Console.Clear();

                bool closeApp = false;

                while (closeApp == false)
                {
                    Console.WriteLine("\n\nMAIN MENU");
                    Console.WriteLine("\nType 0 to Close Application.");
                    Console.WriteLine("\nType 1 to View All Records.");
                    Console.WriteLine("\nType 2 to Insert Record.");
                    Console.WriteLine("\nType 3 to Delete Record.");
                    Console.WriteLine("\nType 4 to Update Record.");
                    Console.WriteLine("\n---------------------------------------\n");

                    string? commandInput = Console.ReadLine();

                    switch (commandInput)
                    {
                        case "0":
                            Console.WriteLine("\nGoodbye\n");
                            closeApp = true;
                            break;

                        case "1":
                            GetAllRecords(sqlConnection);
                            break;

                        //case "2":
                        //    Insert();
                        //    break;

                        //case "3":
                        //    Delete();
                        //    break;

                        //case "4":
                        //    Update();
                        //    break;

                        default:
                            Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                            break;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Retrieve => CRUD
        static void GetAllRecords(SqlConnection sqlConnection)
        {
            sqlConnection.Open();
            string displayQuery = "SELECT * FROM Flashcard_Stack INNER JOIN Flashcards ON Flashcards.Flashcard_Id=Flashcard_Stack.stack_id;";
            SqlCommand displayCommand = new SqlCommand(displayQuery, sqlConnection);
            SqlDataReader dataReader = displayCommand.ExecuteReader();

            while (dataReader.Read())
            {
                Console.WriteLine($"stack_Id: {dataReader.GetValue(0)}, stack_Name: {dataReader.GetValue(1)}, Flashcard_Id: {dataReader.GetValue(2)}, Flashcard Value: {dataReader.GetValue(3)}");
            }
            dataReader.Close();
        }

    }
}
