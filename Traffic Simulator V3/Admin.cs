using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traffic_Simulator_V3
{
    class Admin
    {
        public void AdminMain()
        {
            int caseSwitch = 0;

            while (caseSwitch != 4)
            {
                Console.Clear();
                string input = "0";
                string optionInput;

                Console.WriteLine("\tMENIU:");
                Console.WriteLine("1. Display all profiles.");
                Console.WriteLine("2. Compare two profiles");
                Console.WriteLine("3. Delete profile.");
                Console.WriteLine("4. Display list of low progressing profiles.");
                Console.WriteLine("5. Display list of high progressing profiles.");
                Console.WriteLine("6. Get number of profiles");
                Console.WriteLine("7. Save and Exit.");
                Console.Write("Enter Your Choice: ");
    
                input = Console.ReadLine();

                Console.WriteLine();

                caseSwitch = Convert.ToInt32(input);
    
                switch (caseSwitch)
                {
                    case 1:
                        Database.DisplayAllProfiles();
                        break;
                    case 2:
                        
                        break;
                    case 3:
                        Console.Write("Enter email of profile to delete: ");
                        optionInput = Console.ReadLine();
                        Database.DeleteProfile(optionInput);
                        break;
                    case 4:
                        Database.GetListOfLowProgress();
                        break;
                    case 5:
                        Database.GetListOfHighProgress();
                        break;
                    case 6:
                        Console.WriteLine("Total of profiles: {0}", User.numberOfUsers);
                        break;
                    case 7:
                        break;
                    default:
                        Console.WriteLine("Wrong symbol entered. Try again.");
                        break;
                }
                Console.Write("\nPress any key to continue... ");
                Console.ReadKey();
            }
        Console.WriteLine("Looking forward to see You!");
        }
        
    }
}
