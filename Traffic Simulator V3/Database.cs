using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Traffic_Simulator_V3
{
    class Database
    {
        public static List<User> allUsers;
        public static List<Score> allScores;
        //
        // reading from a file
        //
        static public void LoadAll()
        {
            allUsers = new List<User>();
            allScores = new List<Score>();
            User tempUser;
            Score tempScore;
            String lineUsers,
                   lineScores;

            try
            {
                using (StreamReader srUsers = new StreamReader("users.txt"));
                using (StreamReader srScores = new StreamReader("scores.txt"));
            }
            catch (FileNotFoundException)
            {
                for (int i = 15; i > 0; i--)
                {
                    Console.Clear();
                    Console.WriteLine("Go to ...\\Projects\\Traffic Simulator V3\\Traffic Simulator V3\\bin\\Release");
                    Console.WriteLine("Check and fix following files: users.txt, scores.txt");
                    Console.WriteLine("System reboot in: {0}", i);
                    Thread.Sleep(1000);
                }
            }         
            finally
            {
                using (StreamReader srUsers = new StreamReader("users.txt"))
                {
                    using (StreamReader srScores = new StreamReader("scores.txt"))
                    {
                        while ((lineUsers = srUsers.ReadLine()) != null & (lineScores = srScores.ReadLine()) != null)
                        {
                            tempUser = new User();
                            tempScore = new Score();
                            User.numberOfUsers++;

                            string[] valuesUser = lineUsers.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            string[] valuesScore = lineScores.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            tempUser.Name = valuesUser[0];
                            tempUser.Surname = valuesUser[1];
                            tempUser.Email = valuesUser[2];

                            tempScore.Email = valuesScore[0];
                            tempScore.Correct = Int32.Parse(valuesScore[1]);
                            tempScore.Wrong = Int32.Parse(valuesScore[2]);

                            allUsers.Add(tempUser);

                            tempScore.UpdateUser();
                            allScores.Add(tempScore);
                        }
                    }
                }
                
            }
            
        }

        static public void DisplayAllProfiles()
        {
            var profiles =
                    allUsers.GroupJoin(allScores,
                                     user => user.Email,
                                     score => score.Email,
                                     (user, score) => new
                                         {
                                             User = user,
                                             Score = score
                                         });

            Console.WriteLine("Displaying all profiles:");
            foreach (var profile in profiles)
            {
                Console.Write(profile.User.Name + ", " + profile.User.Surname + ", " + profile.User.Email + ", ");
                foreach (var score in profile.Score)
                {
                    Console.WriteLine(score.Correct + ", " + score.Wrong);
                }
            }
        }
        
        
        static public User LoadUser(string email)
        {
            User loadUser = null;
            
            foreach (User user in allUsers)
            {
                if (user.Email == email)
                {
                    loadUser = user;
                    break;
                }
            }
            
            return loadUser;
        }

        static public Score LoadScore(string email)
        {
            Score loadScore = null;

            foreach (Score score in allScores)
            {
                if (score.Email == email)
                {
                    loadScore = score;
                    break;
                }
            }
            return loadScore;
        }

        static public void DeleteProfile(string email)
        {
            foreach (Score score in allScores)
            {
                if (score.Email == email)
                {
                    allScores.Remove(score);
                }
            }

            foreach (User user in allUsers)
            {
                if (user.Email == email)
                {
                    allUsers.Remove(user);
                }
            }
        }

        static public void Save(User addUser, Score addScore)
        {
            allUsers.Add(addUser);
            allScores.Add(addScore);

            System.IO.File.WriteAllText("users.txt", string.Empty);
            foreach (User user in allUsers)
            {
                System.IO.File.AppendAllText("users.txt", user.Name + ", " + user.Surname + ", " + user.Email + "\n");
            }
            
            System.IO.File.WriteAllText("scores.txt", string.Empty);
            foreach (Score score in allScores)
            {
                System.IO.File.AppendAllText("scores.txt", score.Email + ", " + score.Correct + ", " + score.Wrong + "\n");
            }
        }

        public static void GetListOfLowProgress()
        {
            var lowProgress =
                from user in allUsers
                join score in allScores
                on user.Email equals score.Email
                where score.userLevel == Score.UserLevel.None || score.userLevel == Score.UserLevel.Beginner || score.userLevel == Score.UserLevel.Basic
                select new { Name = user.Name, Surname = user.Surname, Email = user.Email, Correct = score.Correct, Wrong = score.Wrong };

            Console.WriteLine("Low Progress Profiles:");
            foreach (var profile in lowProgress)
            {
                Console.WriteLine(profile.Name + ", " + profile.Surname + ", " + profile.Email + ", " + profile.Correct + ", " + profile.Wrong);
            }
        }

        public static void GetListOfHighProgress()
        {
            var highProgress =
                from user in allUsers
                join score in allScores
                on user.Email equals score.Email
                where score.userLevel == Score.UserLevel.Intermediate || score.userLevel == Score.UserLevel.Advanced || score.userLevel == Score.UserLevel.Expert
                select new { Name = user.Name, Surname = user.Surname, Email = user.Email, Correct = score.Correct, Wrong = score.Wrong };

            Console.WriteLine("High Progress Profiles:");
            foreach (var profile in highProgress)
            {
                Console.WriteLine(profile.Name + ", " + profile.Surname + ", " + profile.Email + ", " + profile.Correct + ", " + profile.Wrong);
            }
        }

        public static bool CheckForPassword(string password)
        {
            if (password == "s")
                return true;
            else
                return false;
        }
    }
}
