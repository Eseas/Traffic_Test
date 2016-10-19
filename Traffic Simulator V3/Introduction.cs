using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Traffic_Simulator_V3
{
    class AboutAuthor : IIntroductionAction
    {
        public void Introduce()
        {
            if (MessageBox.Show("Kazimieras Senvaitis is 2nd grade Software Engineering student at Vilnius University.\n"
                + "He has particular interest in car simulation games and in cars in general.\n"
                + "Find out more?", "About Author", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("http://www.youtube.com/snwtz");
            }
        }
    }

    class AboutProgram : IIntroductionAction
    {
        public void Introduce()
        {
            if (MessageBox.Show("This Traffic Test has been programmed using C# programming language and Microsoft Visual Studio .\n"
                + "The code written by Kazimieras Senvaitis reaches lenght of 2000 rows and it is approximately 130000 symbols.\n"
                + "Find out more?", "About Program", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://www.visualstudio.com/en-us/visual-studio-homepage-vs.aspx");
            }
        }
    }

    class Donation : IIntroductionAction
    {
        public void Introduce()
        {
            if (MessageBox.Show("This Software has taken approximately 200 houdred work hours of Kazimieras Senvaitis. .\n"
                + "It is free and always will be.\n"
                + "Would you like to donate 1€?", "Donation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("http://senvaitis.blogspot.lt/");
            }
        }
    }

    class History : IIntroductionAction
    {
        public void Introduce()
        {
            MessageBox.Show("This project has been started in 1st grade of Software Engineering studies as a Java programming language project. .\n"
                + "However, project did not reach the production because of too sophisticated vision of grafical user interface and unrelated school programming requirements.\n"
                + "Anyway, success came in 2nd grade as a C# programming project with vision of the program simplified and with less strict school requirements.\n"
                + "Kazimieras Senvaitis also responded of C# as more intuitive programming language than Java.\n"
                , "Short History", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    internal interface IIntroductionAction
    {
        void Introduce();
    }

}
