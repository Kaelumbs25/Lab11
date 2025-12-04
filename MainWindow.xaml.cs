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

            //Display Listox
            lbxAllPlayers.ItemsSource = allPlayers;

        }
    }
}
