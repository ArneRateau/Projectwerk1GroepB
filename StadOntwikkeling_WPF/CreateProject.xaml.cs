using Microsoft.IdentityModel.Tokens;
using StadOntwikkeling_BL.Managers;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_DL.Repos;
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

namespace StadOntwikkeling_WPF
{
    /// <summary>
    /// Interaction logic for CreataProject.xaml
    /// </summary>
    public partial class CreataProject : Window
    {
        private IProjectManager _projectManager;

        public CreataProject(IProjectManager projectManager)
        {
            InitializeComponent();
            _projectManager = projectManager;
        }

        private void CreeerProject_Click(object sender, RoutedEventArgs e)
        {
            string titel = Titel.Text;
            if(string.IsNullOrWhiteSpace(titel)){
                MessageBox.Show("Titel moet ingevuld zijn.");
                return;
            }

            int status=0;
            if (string.IsNullOrWhiteSpace(Status.Text))
            {
                MessageBox.Show("Status moet ingevuld zijn.");
                return;
            }
            if (Status.Text.Equals("Planning"))
            {
                status = 0;
            }else if (Status.Text.Equals("Uitvoering"))
            {
                status = 1;
            }
            else if (Status.Text.Equals("Afgerond"))
            {
                status = 2;
            }
            
            if (string.IsNullOrWhiteSpace(Datum.Text))
            {
                MessageBox.Show("Geschatte datum moet ingegeven zijn.");
                return;
            }
            DateTime datum = DateTime.Parse(Datum.Text);

            string wijk = Wijk.Text;
            if (string.IsNullOrWhiteSpace(wijk))
            {
                MessageBox.Show("Wijk moet ingevuld zijn.");
                return;
            }

            string straat = Straat.Text;
            if (string.IsNullOrWhiteSpace(straat))
            {
                MessageBox.Show("Straatnaam moet ingevuld zijn.");
                return;
            }

            string gemeente = Gemeente.Text;
            if (string.IsNullOrWhiteSpace(gemeente))
            {
                MessageBox.Show("Gemeente moet ingevuld zijn.");
                return;
            }

            string postcode = Postcode.Text;
            if (string.IsNullOrWhiteSpace(postcode))
            {
                MessageBox.Show("Postcode moet ingevuld zijn.");
                return;
            }
            string huisnummer = Huisnummer.Text;
            if (string.IsNullOrWhiteSpace(huisnummer))
            {
                MessageBox.Show("Huisnummer moet ingevuld zijn.");
                return;
            }
            string beschrijving = Beschrijving.Text;
            if (string.IsNullOrWhiteSpace(beschrijving))
            {
                MessageBox.Show("Beschrijving moet ingevuld zijn, hoe meer hoe liever.");
                return;
            }
            _projectManager.MaakProject(titel, status, datum, wijk, straat, gemeente, postcode, huisnummer, beschrijving);
            CreateProjectSpecifiek cps = new CreateProjectSpecifiek();
            cps.ShowDialog();
        }
    }
}
