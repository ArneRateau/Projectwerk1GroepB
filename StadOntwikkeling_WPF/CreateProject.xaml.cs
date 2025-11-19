using StadOntwikkeling_BL.Managers;
using StadOntwikkeling_BL.Models;
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
        
        public CreataProject()
        {
            InitializeComponent();
        }

        private void CreeerProject_Click(object sender, RoutedEventArgs e)
        {
            string titel = Titel.Text;
            string status = Status.Text;
            string datum = Datum.Text;
            string wijk = Wijk.Text;
            string straat = Straat.Text;
            string gemeente = Gemeente.Text;
            string postcode = Postcode.Text;
            string huisnummer = Huisnummer.Text;
            string beschrijving = Beschrijving.Text;
            CreateProjectSpecifiek cps = new CreateProjectSpecifiek();
            cps.ShowDialog();
        }
    }
}
