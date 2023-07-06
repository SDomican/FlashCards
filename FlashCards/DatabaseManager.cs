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
                    Console.WriteLine("\nType 8 to Debug.");
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

                        case "3":
                            InsertCard(sqlConnection);
                            break;

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

                        case "8":
                            Debug();
                            break;

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

        private void Debug()
        {
            List<FlashCardDTO> cardList = new List<FlashCardDTO>();
            cardList.Add(new FlashCardDTO("Test Card", "Card Value"));
            StackDTO test = new StackDTO("Stack Name", cardList);

            Console.WriteLine(test);
        }

        //Retrieve => CRUD
        static void GetAllRecords(SqlConnection sqlConnection)
        {
            sqlConnection.Open();

            List<StackDTO> stackList = new List<StackDTO>();

            string displayQuery = "SELECT Stack_name, Flashcards.Name,  Flashcards.Value FROM Flashcard_Stack LEFT JOIN Flashcards ON Flashcards.Stack_Id=Flashcard_Stack.Stack_id;";

            SqlCommand displayCommand = new SqlCommand(displayQuery, sqlConnection);
            SqlDataReader dataReader = displayCommand.ExecuteReader();

            while (dataReader.Read())
            {
                string cardName = "N/A";
                string cardValue = "N/A";
                string stackName = "N/A";

                List<FlashCardDTO> cardList = new List<FlashCardDTO>();

                if (!String.IsNullOrEmpty(dataReader.GetValue(0).ToString())) { stackName = dataReader.GetValue(0).ToString(); }

                if (!String.IsNullOrEmpty(dataReader.GetValue(1).ToString())) { cardName = dataReader.GetValue(1).ToString(); }

                if (!String.IsNullOrEmpty(dataReader.GetValue(2).ToString())) { cardValue = dataReader.GetValue(2).ToString(); }

                FlashCardDTO newFlashCardDTO = new FlashCardDTO(cardName, cardValue);
                cardList.Add(newFlashCardDTO);

                StackDTO newStackDTOItem = new StackDTO(stackName, cardList);

                Console.WriteLine(newStackDTOItem.ToString());
                Console.WriteLine("---------------------------");

                //foreach(StackDTO stack in stackList) { Console.WriteLine(stack.ToString()); }

                //Console.WriteLine($"Card Name: {newFlashCardDTO.Name}, Card Value: {newFlashCardDTO.Value}");

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

        //Create => CRUD
        static void InsertCard(SqlConnection sqlConnection)
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
            Console.WriteLine("Enter Stack Name to delete");
            string input = Console.ReadLine();
            string deleteQuery = $"DELETE FROM Flashcard_Stack WHERE Stack_name = '{input}'";
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
