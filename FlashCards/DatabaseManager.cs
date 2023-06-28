using System.Configuration;
using System.Data.SqlClient;

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
                    Console.WriteLine("\nType 2 to insert Stack.");
                    Console.WriteLine("\nType 3 to add a Card.");
                    Console.WriteLine("\nType 4 to delete a Stack.");
                    Console.WriteLine("\nType 5 to delete a Card.");
                    Console.WriteLine("\nType 6 to Update a Card.");
                    Console.WriteLine("\nType 7 to Update a Stack.");
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

                        case "2":
                            InsertStack(sqlConnection);
                            break;

                        //case "3":
                        //    InsertCard(sqlConnection);
                        //    break;

                        case "4":
                            DeleteStack(sqlConnection);
                            break;

                        //case "5":
                        //    DeleteCard();
                        //    break;

                        //case "6":
                        //    UpdateCard();
                        //    break;

                        //case "7":
                        //    UpdateStack();
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
            //string displayQuery = "SELECT * FROM Flashcard_Stack INNER JOIN Flashcards ON Flashcards.Flashcard_Id=Flashcard_Stack.Stack_id;";

            string displayQuery = "SELECT * FROM Flashcard_Stack LEFT JOIN Flashcards ON Flashcards.Flashcard_Id = Flashcard_Stack.Stack_id";

            SqlCommand displayCommand = new SqlCommand(displayQuery, sqlConnection);
            SqlDataReader dataReader = displayCommand.ExecuteReader();

            while (dataReader.Read())
            {
                Console.WriteLine($"stack_Id: {dataReader.GetValue(0)}, stack_Name: {dataReader.GetValue(1)}, Flashcard_Id: {dataReader.GetValue(2)}, Flashcard Value: {dataReader.GetValue(3)}");
            }
            dataReader.Close();
            sqlConnection.Close();
        }

        //Create => CRUD
        static void InsertStack(SqlConnection sqlConnection)
        {
            sqlConnection.Open();
            Console.WriteLine("Enter Stack name");
            string stackName = Console.ReadLine();

            if (!string.IsNullOrEmpty(stackName))
            {
                string insertQuery = $"INSERT INTO Flashcard_Stack(stack_name) VALUES('{stackName}')";
                SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Data is succesfully inserted into table!");
            }

            sqlConnection.Close();
        }


        //Delete => CRUD
        static void DeleteStack(SqlConnection sqlConnection)
        {
            sqlConnection.Open();
            Console.WriteLine("Enter Stack id to delete");
            int d_id = int.Parse(Console.ReadLine());
            string deleteQuery = $"DELETE FROM Flashcard_Stack WHERE Stack_id = {d_id.ToString()}";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
            deleteCommand.ExecuteNonQuery();
            Console.WriteLine("Deleted successfully");
            sqlConnection.Close();
        }

        //Delete => CRUD
        static void DeleteCard(SqlConnection sqlConnection)
        {
            sqlConnection.Open();
            Console.WriteLine("Enter id to delete");
            int d_id = int.Parse(Console.ReadLine());
            string deleteQuery = $"DELETE FROM Details WHERE user_id = {d_id.ToString()}";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
            deleteCommand.ExecuteNonQuery();
            Console.WriteLine("Deleted successfully");
            sqlConnection.Close();
        }


    }
}
