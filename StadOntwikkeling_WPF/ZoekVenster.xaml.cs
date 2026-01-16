using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Managers;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_BL.Models.DTO_s;
using StadOntwikkeling_WPF.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;



namespace StadOntwikkeling_WPF
{
	public partial class ZoekVenster : Window
	{
		private IProjectManager _projectManager;
		private IPartnerManager _partnerManager;
		private ILocatieManager _locatieManager;
		private IProjectPartnerManager _projectPartnerManager;
		private List<ProjectDTO> _alleProjecten = new();
		public ZoekVenster(IProjectManager projectManager, IPartnerManager partnerManager, ILocatieManager locatieManager, IProjectPartnerManager projectPartnerManager)
		{
			InitializeComponent();
			_projectManager = projectManager;
			_partnerManager = partnerManager;
			_locatieManager = locatieManager;
			_projectPartnerManager = projectPartnerManager;


			LoadProjecten();
			LoadFilters();
		}

		private void LoadProjecten()
		{
			_alleProjecten = _projectManager.GetProjectsLite();
			DgResultaten.ItemsSource = null;
			DgResultaten.ItemsSource = _alleProjecten;
		}

		private ProjectDTO? GetSelectedProject()
		{
			return DgResultaten.SelectedItem as ProjectDTO;
		}
		private void OpenEditWindow(ProjectDTO project)
		{
			Project fullProject = _projectManager.GetProjectById(project.Id);
			
			var win = new ProjectWindow(fullProject, _projectManager, _partnerManager, _locatieManager, _projectPartnerManager);
			
			win.ShowDialog();

			LoadProjecten();

			this.Close();
		}

		private void DgResultaten_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var project = GetSelectedProject();
			if (project == null) return;

			OpenEditWindow(project);
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
        private void ToonInfoProject_Click(object sender, RoutedEventArgs e)
        {
            ProjectDTO res = (ProjectDTO)DgResultaten.SelectedItem;
            InfoProjectWindow info = new InfoProjectWindow(_projectManager, _partnerManager, _locatieManager, _projectPartnerManager, res.Id);
            info.ShowDialog();
        }

		private void VerwijderProject_Click(object sender, RoutedEventArgs e)
		{
			var project = (ProjectDTO)DgResultaten.SelectedItem;

			if (project == null)
			{
				MessageBox.Show("Selecteer eerst een project.");
				return;
			}

			var bevestiging = MessageBox.Show(
				$"Weet u zeker dat u het project '{project.Title}' wilt verwijderen?",
				"Bevestiging",
				MessageBoxButton.YesNo,
				MessageBoxImage.Warning);

			if (bevestiging == MessageBoxResult.Yes)
			{
				// VOORBEELD — verwijder logica
				_projectManager.VerwijderProject(project.Id);

				// UI verversen
				BtnZoek_Click(null, null);
			}
		}
	}
}
