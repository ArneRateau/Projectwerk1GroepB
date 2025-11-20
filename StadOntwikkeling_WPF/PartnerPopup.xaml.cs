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
using System.Windows.Shapes;
using StadOntwikkeling_BL.Models;


namespace StadOntwikkeling_WPF
{
    public partial class PartnerPopup : Window
    {
        public string PartnerNaam { get; private set; }
        public string PartnerRol { get; private set; }

        public PartnerPopup()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            

            if (string.IsNullOrWhiteSpace(TxtRol.Text))
            {
                MessageBox.Show("Rol is verplicht.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PartnerRol = TxtRol.Text.Trim();

            DialogResult = true;   // belangrijk zodat ShowDialog() == true wordt
            Close();
        }

        private void BtnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void TxtNaam_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
