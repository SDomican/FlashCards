using System.Configuration;
using System.Collections.Specialized;
using FlashCards;

// See https://aka.ms/new-console-template for more information

//string? connectionString;

//connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

//Console.WriteLine("The value of ConnectionString is " + connectionString);

DatabaseManager manager = new DatabaseManager();

manager.Initialise();

