using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    

    public partial class MainWindow : Window
    {
        List<Player> allPlayers = new List<Player>();
        List<Player> selectedPlayers = new List<Player>();
        int selectedGoalKeepers, selectedDefenders, selectedMidfielders, selectedForwards;

        public MainWindow()
        {
            InitializeComponent();
        }

        private List<Player> CreatePlayers()
        {
            Random r = new Random();
            List<Player> players = new List<Player>();

            string[] firstNames = {
                "Adam", "Amelia", "Ava", "Chloe", "Conor", "Daniel", "Emily",
                "Emma", "Grace", "Hannah", "Harry", "Jack", "James",
                "Lucy", "Luke", "Mia", "Michael", "Noah", "Sean", "Sophie"};

            string[] lastNames = {
                "Brennan", "Byrne", "Daly", "Doyle", "Dunne", "Fitzgerald", "Kavanagh",
                "Kelly", "Lynch", "McCarthy", "McDonagh", "Murphy", "Nolan", "O'Brien",
                "O'Connor", "O'Neill", "O'Reilly", "O'Sullivan", "Ryan", "Walsh"
            };

            Position currentPosition = Position.Goalkeeper;
            //CreatePlayers
            for (int i = 0; i < 18; i++)
            {
                //generate random age
                DateTime date1 = DateTime.Now.AddYears(-30);
                DateTime date2 = DateTime.Now.AddYears(-20);
                TimeSpan t = date2 - date1;
                int noOfDays = t.Days;
                DateTime newDate = date1.AddDays(r.Next(noOfDays));

                //Generate Positions
                

                switch(i)
                {
                    case 2:
                        currentPosition = Position.Defender;
                        break;
                    case 8:
                        currentPosition = Position.Midfielder;
                        break;
                    case 14: 
                        currentPosition = Position.Forward;
                        break;
                }

                //generate random names
                Player p = new Player()
                {
                    FirstName = firstNames[r.Next(firstNames.Length)],
                    LastName = lastNames[r.Next(lastNames.Length)],
                    DateOfBirth = newDate,
                    PreferredPosition = currentPosition
                };
                players.Add(p);
            }
            return players;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Create Players
            allPlayers = CreatePlayers();
            allPlayers.Sort();
            //Display Listox
            lbxAllPlayers.ItemsSource = allPlayers;

            //Sett up ComboBox
            cbxFormation.ItemsSource = new string[] { "4-4-2", "4-3-3", "4-5-1" };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Check spaces
            if (selectedPlayers.Count > 10)
            {
                MessageBox.Show("You have too many players selected");
            }
            else
            {
                //Determine selected player
                Player selected = lbxAllPlayers.SelectedItem as Player;

                //check not null
                if (selected != null)//for making sure a pplayer is slected
                {
                    //check player allowed in formation
                    if(isValidPlayer(selected))
                    {
                        selectedPlayers.Add(selected);
                        allPlayers.Remove(selected);

                        RefreshScreen();
                    }
                    else
                    {
                        MessageBox.Show("Not allowed in formation");
                    }


                    
                }

            }

        }

        private void cbxFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //reset
            foreach(Player player in selectedPlayers)
            {
                allPlayers.Add(player);
            }

            selectedPlayers.Clear();

            selectedMidfielders = 0;
            selectedGoalKeepers = 0;
            selectedForwards = 0;
            selectedDefenders = 0;

            RefreshScreen();
        }

        private void RefreshScreen()
        {
            selectedPlayers.Sort();
            allPlayers.Sort();

            lbxAllPlayers.ItemsSource = null;
            lbxAllPlayers.ItemsSource = allPlayers;

            lbxSelectedPlayers.ItemsSource = null;
            lbxSelectedPlayers.ItemsSource = selectedPlayers;

            tbxSpaces.Text = (11 - selectedPlayers.Count()).ToString();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //Determine selected player
            Player selected = lbxSelectedPlayers.SelectedItem as Player;

            //check not null
            if (selected != null)//for making sure a pplayer is slected
            {
                selectedPlayers.Remove(selected);
                allPlayers.Add(selected);

                RefreshScreen();

                switch(selected.PreferredPosition)
                {
                    case Position.Goalkeeper:
                        selectedGoalKeepers--;
                        break;
                    case Position.Defender:
                        selectedDefenders--;
                        break;
                    case Position.Midfielder:
                        selectedMidfielders--;
                        break;
                    case Position.Forward:
                        selectedForwards--;
                        break;
                }

                
            }
        }

        private bool isValidPlayer(Player player)
        {
            int allowedGoalKeepers = 1;
            bool isValid = false;
            if (selectedGoalKeepers < allowedGoalKeepers)
            {
                selectedGoalKeepers++;
                isValid = true;
            }


            string input = cbxFormation.Text;
            if (input != "")
            {
                string selectedFormation = cbxFormation.SelectedItem as string;
                string[] formation = selectedFormation.Split('-');

                
                int allowedDefenders = int.Parse(formation[0]);
                int allowedMidfielders = int.Parse(formation[1]);
                int allowedForwards = int.Parse(formation[2]);

                switch (player.PreferredPosition)
                {
                    case Position.Defender:
                        if (selectedDefenders < allowedDefenders)
                        {
                            selectedDefenders++;
                            isValid = true;
                        }
                        break;
                    case Position.Midfielder:
                        if (selectedMidfielders < allowedMidfielders)
                        {
                            selectedMidfielders++;
                            isValid = true;
                        }
                        break;
                    case Position.Forward:
                        if (selectedForwards < allowedForwards)
                        {
                            selectedForwards++;
                            isValid = true;
                        }
                        break;
                }

            }
            

            return isValid;
        }
    }
}
