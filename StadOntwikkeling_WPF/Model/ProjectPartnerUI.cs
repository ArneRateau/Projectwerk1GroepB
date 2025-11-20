using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_WPF.Model
{
	public class ProjectPartnerUI : INotifyPropertyChanged
	{
		private PartnerUI _partner;
		private ProjectUI _project;
		private string _rol;

		public ProjectPartnerUI(int id, PartnerUI partner, ProjectUI project, string rol)
		{
			Id = id;
			Partner = partner;
			Project = project;
			Rol = rol;
		}

		public ProjectPartnerUI(PartnerUI partner, ProjectUI project, string rol)
		{
			Partner = partner;
			Project = project;
			Rol = rol;
		}

		public int Id { get; set; }
		public PartnerUI Partner
		{
			get { return _partner; }
			set
			{
				_partner = value;
				OnPropertyChanged(nameof(Partner));
			}
		}
		public ProjectUI Project
		{
			get { return _project; }
			set
			{
				_project = value;
				OnPropertyChanged(nameof(Project));
			}
		}
		public string Rol
		{
			get { return _rol; }
			set
			{
				_rol = value;
				OnPropertyChanged(nameof(Rol));
			}
		}
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
