using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;

namespace Traffic_Simulator_V3
{
    /// <summary>
    /// Ši klasė:
    /// 1. Turi tingų inicializavimą;
    /// 4. Turi standartinių įvykių;
    /// 5. Turi nestandartines išimtis;
    /// 10. Panaudoja app ir user konfigūracinį failą;
    /// 11. Prasmingai panaudoja dependency injection;
    /// </summary>

    delegate void MyThreadDelegate();

    public partial class MainWindow : Form
    {
        Lazy<Admin> admin = new Lazy<Admin>();
        User currentUser;
        Score currentScore;
        string loadEmail,
               tempName,
               tempSurname,
               tempEmail;
        IIntroductionAction action = null;

        public MainWindow()
        {
            switch (rnd.Next(0, 4))
            {
                case 0:
                    AboutAuthor aboutAuthor = new AboutAuthor();
                    Intro(aboutAuthor);
                    break;
                case 1:
                    AboutProgram aboutProgram = new AboutProgram();
                    Intro(aboutProgram);
                    break;
                case 2:
                    Donation donation = new Donation();
                    Intro(donation);
                    break;
                case 3:
                    History history = new History();
                    Intro(history);
                    break;
            }
                      

            InitializeComponent();
            Database.LoadAll();            
        }
        
        private void Intro(IIntroductionAction specificAction)
        {
            this.action = specificAction;

            //Thread introThread = new Thread(action.Introduce);
            //introThread.Start();
            MyThreadDelegate introThread = delegate () { (new Thread(action.Introduce)).Start(); };
            introThread.Invoke(); 
        }

        // 
        // create new profile
        //
        private void createNewBox1_TextChanged(object sender, EventArgs e)
        {
            tempName = createNewBox1.Text;
        }

        private void createNewBox2_TextChanged(object sender, EventArgs e)
        {
            tempSurname = createNewBox2.Text;
        }

        private void createNewBox3_TextChanged(object sender, EventArgs e)
        {
            tempEmail = createNewBox3.Text;
        }

        //
        // input validation
        //
        private bool CheckForName(string name, TextBox box)
        {
            Regex r = new Regex("^[A-Z][a-zA-Z]*$");

            if (name == null || !r.IsMatch(name))
            {
                box.BackColor = Color.OrangeRed;
                return false;
            }
            else if (r.IsMatch(name))
            {
                box.BackColor = Color.YellowGreen;
                return true;
            }
            return false;
        }

        private bool CheckForEmail(string tempEmail, TextBox box)
        {
            Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (tempEmail == null || !r.IsMatch(tempEmail))
            {
                box.BackColor = Color.OrangeRed;
                return false;
            }
            else if (r.IsMatch(tempEmail))
            {
                box.BackColor = Color.YellowGreen;
                return true;
            }
            return false;
        }

        
        private void submitNewButton_Click(object sender, EventArgs e)
        {
            if (CheckForName(tempName, createNewBox1) == false |
                CheckForName(tempSurname, createNewBox2) == false |
                CheckForEmail(tempEmail, createNewBox3) == false) return;

            currentUser = new User(tempName, tempSurname, tempEmail);
            currentScore = new Score(tempEmail);
            nameLabel.Text = currentUser.Name;

            LoggedIn();
        }

        //
        // load existing profile
        //
        private void loadBox1_TextChanged(object sender, EventArgs e)
        {
            loadEmail = loadBox1.Text;
        }

        private void submitLoadButton_Click(object sender, EventArgs e)
        {
            if (Database.CheckForPassword(loadEmail) == true)
            {
                Thread adminThread = new Thread(admin.Value.AdminMain);
                adminThread.Start();
            }
                
            if (CheckForEmail(loadEmail, loadBox1) == false) return;
            currentUser = Database.LoadUser(loadEmail);
            currentScore = Database.LoadScore(loadEmail);
            nameLabel.Text = currentUser.Name;
            UpdateScoreBoxes();
            questionsAsked = currentScore.Correct + currentScore.Wrong;

            LoggedIn();
        }

        private void LoggedIn()
        {
            UpdateScoreBoxes();

            StartQuestioning(); 

            label9.Visible = true;
            answerBox.Visible = true;
            submitAnswerButton.Visible = true;

            nextQuestionButton.Visible = true;
            nameLabel.Visible = true;
            label10.Visible = true;
            label7.Visible = true;
            label8.Visible = true;

            progressBox.Visible = true;
            correctBox.Visible = true;
            wrongBox.Visible = true;

            startOverButton.Visible = true;
            saveAndExitButton.Visible = true;
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If cars go at the same time, use \"n\"." 
                + "\n\n"
                + "Example: 12n43"
                + "\n", "Help", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void creditsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author: \tKazimieras Senvaitis\n"
                + "\t+370 662 17281\n"
                + "\tturbo407@gmail.com\n"
                + "Version: \t" + AppSettings.Default.majorVersion.ToString() + "." + AppSettings.Default.minorVersion.ToString() + "." + AppSettings.Default.patchNumber.ToString() 
                + "\n", "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            while (true)
            {
                switch (MessageBox.Show("Current number of questions: " + UserSettings.Default.numberOfQuestions.ToString()
                    + "\nIncrease = Yes"
                    + "\nDecrease = No",
                    "Settings", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        {
                            UserSettings.Default.numberOfQuestions++;
                            break;
                        }
                    case DialogResult.No:
                        {
                            UserSettings.Default.numberOfQuestions--;
                            break;
                        }
                    case DialogResult.Cancel:
                        {
                            UserSettings.Default.Save();
                            return;
                        }
                }

                try
                {
                    if (UserSettings.Default.numberOfQuestions > AppSettings.Default.maxNumberOfQuestions)
                        throw new TestOverloadException();
                }
                catch(TestOverloadException)
                {
                    for (int i = 15; i > 0; i--)
                    {
                        Console.Clear();
                        Console.WriteLine("Restoring factory settings in: {0}", i);
                        Thread.Sleep(1000);
                    }

                    UserSettings.Default.numberOfQuestions = 10;
                }
                
            }            
        }
        
        
        //
        // save and exit
        //
        private void saveAndExitButton_Click(object sender, EventArgs e)
        {
            Database.Save(currentUser, currentScore);
            Dispose();
        }

        private void startOverButton_Click(object sender, EventArgs e)
        {
            questionsAsked = 0;

            Reset();
            StartQuestioning();

            
            currentScore.StartOver();
            UpdateScoreBoxes();
        }

        private void UpdateScoreBoxes()
        {
            progressBox.Text = (currentScore.Correct + currentScore.Wrong).ToString();
            correctBox.Text = currentScore.Correct.ToString();
            wrongBox.Text = currentScore.Wrong.ToString();
        }
    }
}
