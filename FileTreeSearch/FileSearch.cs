using System;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace FileTreeSearch
{
    class FileSearch
    {
        /// <summary>
        /// This method obtains the file name and the current directory,
        /// finds the file desired inside the directory, or informs the user that the file is not present,
        /// seperates the data into users with valid and invalid email addresses
        /// </summary>
        public static void FileTraversal()
        {
            // ask the user for the file name they are searching for. Based on instructions, implied to be .csv
            Console.WriteLine("Please enter file name you wish to find: \n hint: the test files name is called: testFile");

            // record file name
            var fileName = Console.ReadLine();

            // get the current directory. I have created a testFile.csv for the pruposes of this program in the 
            // current directory
            string dir = Directory.GetCurrentDirectory();

            // list of users with valid email addresses
            List<user> validEmails = new List<user>();
            // list of users with invalid email addresses
            List<user> invalidEmails = new List<user>();

            // for every .csv file in the current directory continue code
            foreach (string f in Directory.GetFiles(dir, "*.csv*"))
            {

                // if the file path contains fileName.csv we've found our file
                if (f.Contains(fileName + ".csv"))
                {
                    // using the csvHelper library to make reading and parsing the csv file easier

                    // creating a new streamReader and using it
                    using (var streamReader = new StreamReader(f))
                    {
                        // creating a new CsvReader and using it
                        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                        {
                            // creating a list of the user class to import the data into the project.
                            var records = csvReader.GetRecords<user>().ToList();

                            // iterate through every user in the list
                            for (int i = 0; i < records.Count; i++)
                            {
                                // if the email is valid, add it to the validEmails list
                                if (Validate(records[i].emailAddress))
                                    validEmails.Add(records[i]);
                                // if the email is invalid, add it to the invalidEmails list
                                else
                                    invalidEmails.Add(records[i]);

                            }
                            // prints out the users with valid email addresses
                            Console.WriteLine("These are the valid email users: ");
                            PrintUsers(validEmails);
                            // prints out the users with invalid email addresses
                            Console.WriteLine("These are the invalid email users: ");
                            PrintUsers(invalidEmails);
                        }
                    }
                }
                else
                    // informs the user that the file could not be found in the current directory
                    Console.WriteLine("There is no file with that name in the current directory. \n Current Directory: {0}", dir);
            }
        }

        #region Public Helpers

        /// <summary>
        /// checks the email addresses and returns whether they are valid or not
        /// </summary>
        /// <param name="email"> the email that needs to be validated</param>
        /// <returns></returns>
        static public bool Validate(string email)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);

            return isValid;
        }

        /// <summary>
        /// prints the contents of a list of user
        /// </summary>
        /// <param name="list"> the list of users to be printed</param>
        static public void PrintUsers(List<user> list)
        {
            foreach (user i in list)
            {
                Console.WriteLine(i.firstName + " " + i.lastName + " " + i.emailAddress);
            }
            Console.WriteLine("\n");
        }
        #endregion
    }
}
