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
            _gebruikerManager.MaakGebruiker(EmailTextBox.Text, IsAdminCheckBox.IsChecked ?? false, IsPartnerCheckBox.IsChecked ?? false);
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
