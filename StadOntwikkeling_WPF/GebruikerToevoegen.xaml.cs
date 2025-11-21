using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Managers;
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
        private IPartnerManager _partnerManager;


        public GebruikerToevoegen(IGebruikerManager gebruikerManager, IPartnerManager partnerManager)
        {
            InitializeComponent();
            _gebruikerManager = gebruikerManager;
            _partnerManager = partnerManager;

            SaveButton.Click += SaveButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NaamTextBox.Text))
            {
                MessageBox.Show("Naam mag niet leeg zijn.");
                return;
            }
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
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
            string naam = NaamTextBox.Text;
            int newGebruikerId = _gebruikerManager.MaakGebruiker(naam, email, isAdmin, isPartner);

            if (isPartner)
            {
                _partnerManager.MaakPartner(naam, email);
            }
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
