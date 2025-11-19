using System.Linq;
using System.Windows;
using System.Collections.Generic;
using StadOntwikkeling_BL.Models;
using System.Windows.Controls;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models.DTO_s;



namespace StadOntwikkeling_WPF
{
    public partial class ZoekVenster : Window
    {
        private readonly IProjectManager _projectManager;
        private List<ProjectDTO> _alleProjecten = new List<ProjectDTO>();

        public ZoekVenster(IProjectManager projectManager)
        {
            InitializeComponent();
            _projectManager = projectManager;

            LoadProjecten();
            LoadFilters();

        }

        private void LoadProjecten()
        {
            _alleProjecten = _projectManager.GetProjectsLite();
            DgResultaten.ItemsSource = _alleProjecten;
        }

        void LoadFilters()
        {
            // Wijken
            var wijken = _alleProjecten
                .Select(p => p.Wijk)
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Distinct()
                .OrderBy(w => w)
                .ToList();

            foreach (var w in wijken)
                CmbWijk.Items.Add(new ComboBoxItem { Content = w });

            // Partners
            var partners = _alleProjecten
                .SelectMany(p => p.PartnerNamen)
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            foreach (var p in partners)
                CmbPartner.Items.Add(new ComboBoxItem { Content = p });
        }



        private void BtnZoek_Click(object sender, RoutedEventArgs e)
        {
            var query = _alleProjecten.AsEnumerable();

            // Filter: Wijk
            if (CmbWijk.SelectedItem is ComboBoxItem wijkItem)
            {
                string wijk = wijkItem.Content.ToString();
                if (wijk != "None")
                {
                    query = query.Where(p =>
                        p.Wijk.Equals(wijk, StringComparison.OrdinalIgnoreCase)
                    );
                }
            }


            // Filter: Status
            if (CmbStatus.SelectedItem is ComboBoxItem statusItem)
            {
                string status = statusItem.Content.ToString();
                if (status != "None")
                {
                    query = query.Where(p => p.Status == status);
                }
            }

            // Filter: Type project
            if (CmbType.SelectedItem is ComboBoxItem typeItem)
            {
                string type = typeItem.Content.ToString();
                if (type != "None")
                {
                    query = query.Where(p =>
                        p.ProjectTypes.Any(t =>
                            t.Equals(type, StringComparison.OrdinalIgnoreCase)
                        )
                    );
                }
            }


            // Filter: Naam partner
            if (CmbPartner.SelectedItem is ComboBoxItem partnerItem)
            {
                string partner = partnerItem.Content.ToString();
                if (partner != "None")
                {
                    query = query.Where(p =>
                        p.PartnerNamen.Any(pa =>
                            pa.Equals(partner, StringComparison.OrdinalIgnoreCase)
                        )
                    );
                }
            }


            // Filter: Startdatum >=
            if (DpStart.SelectedDate is DateTime start)
            {
                query = query.Where(p => p.StartDatum >= start);
            }


            // Filter: Startdatum <= einde
            if (DpEind.SelectedDate is DateTime einde)
            {
                query = query.Where(p => p.StartDatum <= einde);
            }

            DgResultaten.ItemsSource = query.ToList();
        }

        private void CmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
