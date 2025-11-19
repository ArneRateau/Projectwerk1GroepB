using System.Linq;
using System.Windows;
using System.Collections.Generic;
using StadOntwikkeling_BL.Models;
using System.Windows.Controls;
using StadOntwikkeling_BL.Interfaces;



namespace StadOntwikkeling_WPF
{
    public partial class ZoekVenster : Window
    {
        private readonly IProjectManager _projectManager;
        private List<Project> _alleProjecten = new List<Project>();

        public ZoekVenster(IProjectManager projectManager)
        {
            InitializeComponent();
            _projectManager = projectManager;

            LoadProjecten();

        }

        private void LoadProjecten()
        {
            _alleProjecten = _projectManager.GetProjectsLite();
            DgResultaten.ItemsSource = _alleProjecten;
        }
        private void BtnZoek_Click(object sender, RoutedEventArgs e)
        {
            string naam = TxtNaam.Text.Trim();
            string wijk = TxtWijk.Text.Trim();

            string type = (CmbType.SelectedItem as ComboBoxItem)?.Content.ToString();
            string status = (CmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

            DateTime? start = DpStart.SelectedDate;
            DateTime? einde = DpEinde.SelectedDate;

            var query = _alleProjecten.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(naam))
                query = query.Where(p => p.Titel.Contains(naam, StringComparison.OrdinalIgnoreCase));


            if (!string.IsNullOrWhiteSpace(wijk))
                query = query.Where(p => p.Locatie.Wijk.Contains(wijk, StringComparison.OrdinalIgnoreCase));


            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(p => p.Status.ToString() == status);


            if (start != null)
                query = query.Where(p => p.StartDatum >= start);

            if (einde != null)
                query = query.Where(p => p.StartDatum <= einde);


            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(p =>
                    p.ProjectOnderdelen.Any(o =>
                        (type == "Groene Ruimte" && o is GroenRuimteProject) ||
                        (type == "Stadsontwikkeling" && o is StadsontwikkelingProject) ||
                        (type == "Innovatief Wonen" && o is InnovatiefWonenProject)
                    )
                );
            }

            DgResultaten.ItemsSource = query.ToList();
        }
    }
}
