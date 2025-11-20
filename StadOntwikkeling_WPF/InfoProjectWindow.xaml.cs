using Microsoft.Win32;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for InfoProjectWindow.xaml
    /// </summary>
    public partial class InfoProjectWindow : Window
    {
        private IProjectManager _projectManager;
        private List<ProjectPartner> _projectPartnerList;
        public InfoProjectWindow(IProjectManager projectManager, int projectId)
        {
            InitializeComponent();
            _projectManager = projectManager;
            Project project = _projectManager.GetProjectById(projectId);
            DataGridInfoProjecten.ItemsSource = new List<Project> { project };

            var projectPartnerList = _projectManager.GetProjectPartners(projectId);
            DataGridPartners.ItemsSource = projectPartnerList;
        }
        private void ExportToCSV_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridInfoProjecten.SelectedItems.Count == 0)
            {
                MessageBox.Show("Geen projecten geselecteerd.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = "geselecteerde_projecten.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                var geselecteerdeInfoProjectenUI = DataGridInfoProjecten.SelectedItems;
                ExportProjectsToCsv(geselecteerdeInfoProjectenUI, dialog.FileName);
                MessageBox.Show("Export voltooid!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ExportProjectsToCsv(System.Collections.IList projecten, string bestandPad)
        {
            using (var writer = new StreamWriter(bestandPad))
            {
                foreach (var item in projecten)
                {
                    if (item is Project project)
                    {
                        string status = project.Status.ToString();
                        string straat = EscapeCsvValue(project.Locatie.Straat);
                        string postcode = EscapeCsvValue(project.Locatie.Postcode);
                        string gemeente = EscapeCsvValue(project.Locatie.Gemeente);
                        string wijk = EscapeCsvValue(project.Locatie.Wijk);
                        string huisnummer = EscapeCsvValue(project.Locatie.Huisnummer);


                        string partners = EscapeCsvValue(string.Join(";", project.Projecten.Select(pp => pp.Partner.Naam + "(" + pp.Rol + ")")));
                        string onderdelen = EscapeCsvValue(string.Join(";", project.ProjectOnderdelen.Select(po => po.ProjectOnderdeelId.ToString())));

                        writer.WriteLine($"{project.Id},{EscapeCsvValue(project.Titel)},{project.StartDatum:yyyy-MM-dd},{status},{EscapeCsvValue(project.Beschrijving)},{straat},{postcode},{gemeente},{wijk},{huisnummer},{partners},{onderdelen}");
                    }
                }
            }
        }
        private string EscapeCsvValue(string value)
        {
            if (value.Contains("\""))
                value = value.Replace("\"", "\"\"");

            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                value = $"\"{value}\"";

            return value;
        }
        private void ToonProjectWindow_Click(object sender, RoutedEventArgs e)
        {
            /*ProjectWindow projectWindow = new ProjectWindow(project, _projectManager);
            info.ShowDialog();*/
        }
    }
}
