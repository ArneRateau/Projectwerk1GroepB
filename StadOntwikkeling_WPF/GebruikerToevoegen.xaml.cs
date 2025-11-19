using StadOntwikkeling_BL.Interfaces;
using System;
using System.Windows;

namespace StadOntwikkeling_WPF
{
    /// <summary>
    /// Interaction logic for GebruikerToevoegen.xaml
    /// </summary>
    public partial class GebruikerToevoegen : Window
    {
        private  IGebruikerManager _gebruikerManager;


        public GebruikerToevoegen(IGebruikerManager gebruikerManager)
        {
            InitializeComponent();
            _gebruikerManager = gebruikerManager;
            SaveButton.Click += SaveButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Email mag niet leeg zijn.");
                return;
            }
            if(!_gebruikerManager.IsGeldigEmail(EmailTextBox.Text))
            {
                MessageBox.Show("Ongeldig email formaat.");
                return;
            }
            if(_gebruikerManager.ZoekGebruikerMetEmail(EmailTextBox.Text) != null)
            {
                MessageBox.Show("Deze email is al in gebruik.");
                return;
            }
            
           
                string email = EmailTextBox.Text;
                bool isAdmin = IsAdminCheckBox.IsChecked ?? false;
                bool isPartner = IsPartnerCheckBox.IsChecked ?? false;
                _gebruikerManager.MaakGebruiker(email, isAdmin, isPartner);
                MessageBox.Show("Gebruiker succesvol toegevoegd.");
            
                
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = false;
            }
            catch
            {
            }
            Close();
        }
    }
}
