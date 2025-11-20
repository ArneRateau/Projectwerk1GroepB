using Microsoft.IdentityModel.Tokens;
using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Managers;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_WPF.Model;
using System;
using System.Collections.Generic;
using System.Formats.Nrbf;
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
    /// Interaction logic for ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window
    {
        private readonly ProjectManager _projectManager;
        private readonly PartnerManager _partnerManager;
        private Project p;

        public ProjectWindow(Project project, ProjectManager projectManager, PartnerManager partnerManager)
        {
            InitializeComponent();
            p = project;
            _projectManager = projectManager;
            _partnerManager = partnerManager;



            FillCurrentValues();
            FillNewValues();
            LoadPartners();
        }

        // ============================
        // 1. HUIDIGE WAARDES
        // ============================
        private void FillCurrentValues()
        {
            TextBlock_Id.Text = p.Id.ToString();
            TextBlock_Titel.Text = p.Titel;
            TextBlock_Datum.Text = p.StartDatum.ToString("dd/MM/yyyy");
            TextBlock_Status.Text = p.Status.ToString();
            TextBlock_Beschrijving.Text = p.Beschrijving;

            TextBlock_Locatie_Straat.Text = p.Locatie.Straat;
            TextBlock_Locatie_Huisnummer.Text = p.Locatie.Huisnummer;
            TextBlock_Locatie_Postcode.Text = p.Locatie.Postcode;
            TextBlock_Locatie_Gemeente.Text = p.Locatie.Gemeente;
            TextBlock_Locatie_Wijk.Text = p.Locatie.Wijk;
        }

        // ============================
        // 2. NIEUWE WAARDES (prefill)
        // ============================
        private void FillNewValues()
        {
            TextBox_New_Id.Text = p.Id.ToString();
            TextBox_New_Titel.Text = p.Titel;
            DatePicker_New_Datum.SelectedDate = p.StartDatum;

            ComboBox_New_Status.SelectedItem =
                ComboBox_New_Status.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(i => i.Content.ToString() == p.Status.ToString());

            TextBox_New_Beschrijving.Text = p.Beschrijving;

            TextBox_New_Straat.Text = p.Locatie.Straat;
            TextBox_New_Huisnummer.Text = p.Locatie.Huisnummer;
            TextBox_New_Postcode.Text = p.Locatie.Postcode;
            TextBox_New_Gemeente.Text = p.Locatie.Gemeente;
            TextBox_New_Wijk.Text = p.Locatie.Wijk;
        }

        // ============================
        // 3. PARTNERS LIST
        // ============================
        private void LoadPartners()
        {
            // Clear both lists
            ListBox_Partners_Current.Items.Clear();
            ListBox_Partners_All.Items.Clear();

            // -----------------------------
            // 1. CURRENT PROJECT PARTNERS
            // -----------------------------
            foreach (var pp in p.Projecten)
            {
                ListBox_Partners_Current.Items.Add(
                    $"Naam: {pp.Partner.Naam}, Rol: {pp.Rol}"
                );
            }

            // -----------------------------
            // 2. ALL OTHER PARTNERS
            // -----------------------------
            // Load all partners from DB
            var allPartners = _partnerManager.GetAllPartners();

            // Get IDs of partners already linked to project
            var linkedIds = p.Projecten
                             .Select(pp => pp.Partner.Id)
                             .ToHashSet();

            // Filter: all partners that are NOT yet linked
            var remainingPartners = allPartners
                                    .Where(pa => !linkedIds.Contains(pa.Id))
                                    .OrderBy(pa => pa.Naam)
                                    .ToList();

            foreach (var partner in remainingPartners)
            {
                ListBox_Partners_All.Items.Add(partner.Naam);
            }
        }



        // ============================
        // 4. OPSLAAN
        // ============================
        private void Button_Opslaan(object sender, RoutedEventArgs e)
        {
            try
            {
                // Titel
                if (!string.IsNullOrWhiteSpace(TextBox_New_Titel.Text))
                    p.Titel = TextBox_New_Titel.Text;

                // Datum
                if (DatePicker_New_Datum.SelectedDate.HasValue)
                    p.StartDatum = DatePicker_New_Datum.SelectedDate.Value;

                // Status
                if (ComboBox_New_Status.SelectedItem is ComboBoxItem item)
                    p.Status = Enum.Parse<Status>(item.Content.ToString());

                // Beschrijving
                if (!string.IsNullOrWhiteSpace(TextBox_New_Beschrijving.Text))
                    p.Beschrijving = TextBox_New_Beschrijving.Text;

                // Locatie
                p.Locatie.Straat = TextBox_New_Straat.Text;
                p.Locatie.Huisnummer = TextBox_New_Huisnummer.Text;
                p.Locatie.Postcode = TextBox_New_Postcode.Text;
                p.Locatie.Gemeente = TextBox_New_Gemeente.Text;
                p.Locatie.Wijk = TextBox_New_Wijk.Text;

                // Save to DB
                _projectManager.UpdateProject(p);

                MessageBox.Show("Project opgeslagen.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout: {ex.Message}");
            }
        }

        private void Button_PartnerKoppelen(object sender, RoutedEventArgs e)
        {
            PartnerPopup popup = new PartnerPopup();
            popup.ShowDialog();

            // After closing the popup, reload the partners
            p = _projectManager.GetProjectById(p.Id);
            LoadPartners();
        }
    }
}
