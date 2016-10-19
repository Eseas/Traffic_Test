using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace Traffic_Simulator_V3
{
    /// <summary>
    /// Ši klasė:
    /// 3. Turi 1 delegatą;
    /// 4. Turi nestandartinį įvykį;
    /// 8. Turi 1 lambda išraišką;
    /// 9. Turi 2 anoniminius metodus;
    /// 10. Panaudoja user konfigūracinius failus
    /// </summary>

    public partial class MainWindow : Form
    {
        int questionsAsked = 0;
        int creation = 0;

        static Random rnd = new Random();
        Crossroad cr;
        Vehicle veh0,
                veh1,
                veh2,
                veh3;
        List<Vehicle> vehicles;
        delegate void MyDelegate(string message);
        delegate void VisibilityFalseDelegate(PictureBox pic);

        public void StartQuestioning()
        {
            cr = new Crossroad();
            cr.CrossroadCreated += OnCrossroadCreated;
            cr.CreateCrossroad();

            vehicles = new List<Vehicle>();

            if (rnd.Next(0, 100) < AppSettings.Default.carProbability)
            {
                veh0 = new Vehicle();
                veh0.VehicleCreated += OnVehicleCreated;
                veh0.CreateVehicle(0, rnd.Next(0, 4));
                vehicles.Add(veh0);
                veh0.NumberCreated = creation;

                switch (creation)
                {
                    case 0:
                        north_0.Visible = true;
                        break;
                    default:
                        break;
                }
                creation++;
            }
            if (rnd.Next(0, 100) < AppSettings.Default.carProbability)
            {
                veh1 = new Vehicle();
                veh1.VehicleCreated += OnVehicleCreated;
                veh1.CreateVehicle(1, rnd.Next(0, 4));
                vehicles.Add(veh1);
                veh1.NumberCreated = creation;

                switch (creation)
                {
                    case 0:
                        east_0.Visible = true;
                        break;
                    case 1:
                        east_1.Visible = true;
                        break;
                    default:
                        break;
                }
                creation++;
            }
            if (rnd.Next(0, 100) < AppSettings.Default.carProbability)
            {
                veh2 = new Vehicle();
                veh2.VehicleCreated += OnVehicleCreated;
                veh2.CreateVehicle(2, rnd.Next(0, 4));
                vehicles.Add(veh2);
                veh2.NumberCreated = creation;

                switch (creation)
                {
                    case 0:
                        south_0.Visible = true;
                        break;
                    case 1:
                        south_1.Visible = true;
                        break;
                    case 2:
                        south_2.Visible = true;
                        break;
                    default:
                        break;
                }
                creation++;
            }
            if (rnd.Next(0, 100) < AppSettings.Default.carProbability)
            {
                veh3 = new Vehicle();
                veh3.VehicleCreated += OnVehicleCreated;
                veh3.CreateVehicle(3, rnd.Next(0, 4));
                vehicles.Add(veh3);
                veh3.NumberCreated = creation;

                switch (creation)
                {
                    case 0:
                        west_0.Visible = true;
                        break;
                    case 1:
                        west_1.Visible = true;
                        break;
                    case 2:
                        west_2.Visible = true;
                        break;
                    case 3:
                        west_3.Visible = true;
                        break;
                    default:
                        break;
                }
                creation++;
            }
            creation = 0;

        }

        private void submitAnswerButton_Click(object sender, EventArgs e)
        {
            questionsAsked++;

            if (GetAnswer() == answerBox.Text) // if correct
            {
                correctPic.Visible = true;
                currentScore.Correct++;
            }
            else // wrong
            {
                wrongPic.Visible = true;
                currentScore.Wrong++;
            }
            // updating score boxes
            progressBox.Text = (currentScore.Correct + currentScore.Wrong).ToString();
            correctBox.Text = currentScore.Correct.ToString();
            wrongBox.Text = currentScore.Wrong.ToString();
        }

        private void nextQuestionButton_Click(object sender, EventArgs e)
        {
            if (questionsAsked == UserSettings.Default.numberOfQuestions - 1)
            {
                nextQuestionButton.Text = "Finish Test";
            }

            if (questionsAsked == UserSettings.Default.numberOfQuestions)
            {
                if ((currentScore.Correct / UserSettings.Default.numberOfQuestions * 100) >= UserSettings.Default.passPercentage)
                {
                    VisibilityFalseDelegate falseSetter = delegate (PictureBox pic) { pic.Visible = false;  };
                    falseSetter.Invoke(correctPic);
                    falseSetter.Invoke(wrongPic);


                    //correctPic.Visible = false;
                    //wrongPic.Visible = false;
                    passedPic.Visible = true;
                    
                }
                else
                {
                    correctPic.Visible = false;
                    wrongPic.Visible = false;
                    failedPic.Visible = true;
                }
                return;
            }
            Reset();
            StartQuestioning();
        }

        public void Reset()
        {
            // cleaning up graphics
            #region
            answerBox.Text = "";

            correctPic.Visible = false;
            wrongPic.Visible = false;

            passedPic.Visible = false;
            failedPic.Visible = false;

            MINI_left_east.Visible = false;
            MINI_left_north.Visible = false;
            MINI_left_south.Visible = false;
            MINI_left_west.Visible = false;
            MINI_right_east.Visible = false;
            MINI_right_north.Visible = false;
            MINI_right_south.Visible = false;
            MINI_right_west.Visible = false;
            MINI_straight_east.Visible = false;
            MINI_straight_north.Visible = false;
            MINI_straight_south.Visible = false;
            MINI_straight_west.Visible = false;
            FER_left_east.Visible = false;
            FER_left_north.Visible = false;
            FER_left_south.Visible = false;
            FER_left_west.Visible = false;
            FER_right_east.Visible = false;
            FER_right_north.Visible = false;
            FER_right_south.Visible = false;
            FER_right_west.Visible = false;
            FER_straight_east.Visible = false;
            FER_straight_north.Visible = false;
            FER_straight_south.Visible = false;
            FER_straight_west.Visible = false;
            GTO_left_east.Visible = false;
            GTO_left_north.Visible = false;
            GTO_left_south.Visible = false;
            GTO_left_west.Visible = false;
            GTO_right_east.Visible = false;
            GTO_right_north.Visible = false;
            GTO_right_south.Visible = false;
            GTO_right_west.Visible = false;
            GTO_straight_east.Visible = false;
            GTO_straight_north.Visible = false;
            GTO_straight_south.Visible = false;
            GTO_straight_west.Visible = false;
            MB_left_east.Visible = false;
            MB_left_north.Visible = false;
            MB_left_south.Visible = false;
            MB_left_west.Visible = false;
            MB_right_east.Visible = false;
            MB_right_north.Visible = false;
            MB_right_south.Visible = false;
            MB_right_west.Visible = false;
            MB_straight_east.Visible = false;
            MB_straight_north.Visible = false;
            MB_straight_south.Visible = false;
            MB_straight_west.Visible = false;
            north_0.Visible = false;
            north_1.Visible = false;
            north_2.Visible = false;
            north_3.Visible = false;
            north_4.Visible = false;
            east_0.Visible = false;
            east_1.Visible = false;
            east_2.Visible = false;
            east_3.Visible = false;
            east_4.Visible = false;
            south_0.Visible = false;
            south_1.Visible = false;
            south_2.Visible = false;
            south_3.Visible = false;
            south_4.Visible = false;
            west_0.Visible = false;
            west_1.Visible = false;
            west_2.Visible = false;
            west_3.Visible = false;
            west_4.Visible = false;
            greenEast.Visible = false;
            greenNorth.Visible = false;
            greenSouth.Visible = false;
            greenWest.Visible = false;
            redEast.Visible = false;
            redNorth.Visible = false;
            redSouth.Visible = false;
            redWest.Visible = false;
            #endregion
        }

        // subscriber
        public void OnVehicleCreated(object source, VehicleCreatedEventArgs e)
        {
            // vehicles drawing switch
            #region
            switch (e.skin)
            {
                case "FER":
                    switch (e.source)
                    {
                        case 0:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    FER_left_north.Visible = true;
                                    break;
                                case 0:
                                    FER_straight_north.Visible = true;
                                    break;
                                case 1:
                                    FER_right_north.Visible = true;
                                    break;
                            }
                            break;
                        case 1:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    FER_left_east.Visible = true;
                                    break;
                                case 0:
                                    FER_straight_east.Visible = true;
                                    break;
                                case 1:
                                    FER_right_east.Visible = true;
                                    break;
                            }
                            break;
                        case 2:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    FER_left_south.Visible = true;
                                    break;
                                case 0:
                                    FER_straight_south.Visible = true;
                                    break;
                                case 1:
                                    FER_right_south.Visible = true;
                                    break;
                            }
                            break;
                        case 3:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    FER_left_west.Visible = true;
                                    break;
                                case 0:
                                    FER_straight_west.Visible = true;
                                    break;
                                case 1:
                                    FER_right_west.Visible = true;
                                    break;
                            }
                            break;
                    }
                    break;
                case "GTO":
                    switch (e.source)
                    {
                        case 0:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    GTO_left_north.Visible = true;
                                    break;
                                case 0:
                                    GTO_straight_north.Visible = true;
                                    break;
                                case 1:
                                    GTO_right_north.Visible = true;
                                    break;
                            }
                            break;
                        case 1:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    GTO_left_east.Visible = true;
                                    break;
                                case 0:
                                    GTO_straight_east.Visible = true;
                                    break;
                                case 1:
                                    GTO_right_east.Visible = true;
                                    break;
                            }
                            break;
                        case 2:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    GTO_left_south.Visible = true;
                                    break;
                                case 0:
                                    GTO_straight_south.Visible = true;
                                    break;
                                case 1:
                                    GTO_right_south.Visible = true;
                                    break;
                            }
                            break;
                        case 3:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    GTO_left_west.Visible = true;
                                    break;
                                case 0:
                                    GTO_straight_west.Visible = true;
                                    break;
                                case 1:
                                    GTO_right_west.Visible = true;
                                    break;
                            }
                            break;
                    }
                    break;
                case "MB":
                    switch (e.source)
                    {
                        case 0:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    MB_left_north.Visible = true;
                                    break;
                                case 0:
                                    MB_straight_north.Visible = true;
                                    break;
                                case 1:
                                    MB_right_north.Visible = true;
                                    break;
                            }
                            break;
                        case 1:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    MB_left_east.Visible = true;
                                    break;
                                case 0:
                                    MB_straight_east.Visible = true;
                                    break;
                                case 1:
                                    MB_right_east.Visible = true;
                                    break;
                            }
                            break;
                        case 2:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    MB_left_south.Visible = true;
                                    break;
                                case 0:
                                    MB_straight_south.Visible = true;
                                    break;
                                case 1:
                                    MB_right_south.Visible = true;
                                    break;
                            }
                            break;
                        case 3:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    MB_left_west.Visible = true;
                                    break;
                                case 0:
                                    MB_straight_west.Visible = true;
                                    break;
                                case 1:
                                    MB_right_west.Visible = true;
                                    break;
                            }
                            break;
                    }
                    break;
                case "MINI":
                    switch (e.source)
                    {
                        case 0:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    MINI_left_north.Visible = true;
                                    break;
                                case 0:
                                    MINI_straight_north.Visible = true;
                                    break;
                                case 1:
                                    MINI_right_north.Visible = true;
                                    break;
                            }
                            break;
                        case 1:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    MINI_left_east.Visible = true;
                                    break;
                                case 0:
                                    MINI_straight_east.Visible = true;
                                    break;
                                case 1:
                                    MINI_right_east.Visible = true;
                                    break;
                            }
                            break;
                        case 2:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    MINI_left_south.Visible = true;
                                    break;
                                case 0:
                                    MINI_straight_south.Visible = true;
                                    break;
                                case 1:
                                    MINI_right_south.Visible = true;
                                    break;
                            }
                            break;
                        case 3:
                            switch (e.turnSignal)
                            {
                                case -1:
                                    MINI_left_west.Visible = true;
                                    break;
                                case 0:
                                    MINI_straight_west.Visible = true;
                                    break;
                                case 1:
                                    MINI_right_west.Visible = true;
                                    break;
                            }
                            break;
                    }
                    break;
            }
            #endregion  // switch
        }

        public void OnCrossroadCreated(object source, CrossroadCreatedEventArgs e)
        {
            if (e.northSouth)
            {
                greenNorth.Visible = true;
                greenSouth.Visible = true;
                redWest.Visible = true;
                redEast.Visible = true;
            }
            else if (e.westEast)
            {
                redNorth.Visible = true;
                redSouth.Visible = true;
                greenWest.Visible = true;
                greenEast.Visible = true;
            }
        }

        public string GetAnswer()
        {
            int position = 0;
            string answer = "";

            bool synchroL = false;
            bool synchroSR = false;

            MyDelegate printer1 = (string message) => { Console.WriteLine(message); };
            printer1.Invoke("northSouth is " + cr.northSouth.ToString());

            MyDelegate printer2 = (delegate (string message) { Console.WriteLine(message); });
            printer2.Invoke("westEast is " + cr.westEast);                  
            
            // žalioji dalis
            foreach (Vehicle veh in vehicles)
            {
                if (cr.CheckIfGreen(veh) && (veh.TurnSignal == 0 || veh.TurnSignal == 1)) // jei dega žalia ir važiuoja tiesiai arba į dešinę?
                {
                    if (synchroSR == false)
                    {
                        veh.Position = position++;
                        answer += veh.NumberCreated.ToString();
                        synchroSR = true;
                    }
                    else if (synchroSR == true)
                    {
                        veh.Position = position - 1;
                        answer += "n" + veh.NumberCreated.ToString();
                        synchroSR = false;
                    }
                }
            }
            synchroSR = false;

            foreach (Vehicle veh in vehicles)
            {
                if (cr.CheckIfGreen(veh) && (veh.TurnSignal == -1)) // jei dega žalia ir važiuoja į kairę
                {
                    if (synchroL == false)
                    {
                        veh.Position = position++;
                        answer += veh.NumberCreated.ToString();
                        synchroL = true;
                    }
                    else if (synchroL == true)
                    {
                        veh.Position = position - 1;
                        answer += "n" + veh.NumberCreated.ToString();
                        synchroL = false;
                    }
                }
            }
            synchroL = false;

            // raudonoji dalis
            foreach (Vehicle veh in vehicles)
            {
                if (!cr.CheckIfGreen(veh) && (veh.TurnSignal == 0 || veh.TurnSignal == 1))
                {
                    if (synchroSR == false)
                    {
                        veh.Position = position++;
                        answer += veh.NumberCreated.ToString();
                        synchroSR = true;
                    }
                    else if (synchroSR == true)
                    {
                        veh.Position = position - 1;
                        answer += "n" + veh.NumberCreated.ToString();
                        synchroSR = false;
                    }
                }
            }
            synchroSR = false;

            foreach (Vehicle veh in vehicles)
            {
                if (!cr.CheckIfGreen(veh) && (veh.TurnSignal == -1))
                {
                    if (synchroL == false)
                    {
                        veh.Position = position++;
                        answer += veh.NumberCreated.ToString();
                        synchroL = true;
                    }
                    else if (synchroL == true)
                    {
                        veh.Position = position - 1;
                        answer += "n" + veh.NumberCreated.ToString();
                        synchroL = false;
                    }
                }
            }
            synchroL = false;

            foreach (var veh in vehicles)
            {
                Console.WriteLine("Mano source: {0}; Mano posukis {1}; Man dega: {2}; Mano Position: {3}", veh.Source, veh.TurnSignal, cr.CheckIfGreen(veh), veh.Position);
            }

            Console.WriteLine("And the answer is: {0}", answer);
            return answer;
        }
    }
}
