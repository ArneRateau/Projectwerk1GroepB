using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Managers;
using StadOntwikkeling_DL.Repos;
using System.Windows;

namespace StadOntwikkeling_WPF
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private readonly IGebruikerManager _gebruikerManager;
        public MainMenu()
        {
            InitializeComponent();

            _gebruikerManager = new GebruikerManager(new GebruikerRepo());

            addLogin.Click += AddLogin_Click;
        }

        private void AddLogin_Click(object? sender, RoutedEventArgs e)
        {
            GebruikerToevoegen gebruikerToevoegenWindow = new GebruikerToevoegen(_gebruikerManager);
            gebruikerToevoegenWindow.ShowDialog();
        }
    }
}
