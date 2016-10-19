using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traffic_Simulator_V3
{
    public class Score
    {
        [Flags]
        public enum UserLevel
        {
            None = 0,
            Beginner = 1,
            Basic = 2,
            Intermediate = 4,
            Advanced = 8,
            Expert = 16
        }

        public string Email { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public UserLevel userLevel;
        
        public Score()
        {
        }

        public Score(string email)
        {
            Email = email;
        }

        public void StartOver()
        {
            Correct = 0;
            Wrong = 0;
        }

        public void UpdateUser()
        {
            if (Correct + Wrong == 10 && Correct >= 9) userLevel = UserLevel.Expert;
            else if (Correct + Wrong == 10 && Correct >= 7) userLevel = UserLevel.Advanced;
            else if (Correct + Wrong == 10 && Correct >= 5) userLevel = UserLevel.Intermediate;
            else if (Correct + Wrong == 10 && Correct >= 3) userLevel = UserLevel.Basic;
            else if (Correct + Wrong == 10 && Correct >= 0) userLevel = UserLevel.Beginner;
            else userLevel = UserLevel.None;
        }
    }
}
