using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	 public class Project
	{
		private int _id;
		private string _titel;
		private DateTime _startDatum;
		private string _beschrijving;
		private Status _status;
		private Locatie _locatie;

		public Project(string titel, DateTime startDatum, Status status, string beschrijving, Locatie locatie, List<ProjectPartner> projecten, List<ProjectOnderdeel> projectOnderdelen)
		{
			Titel = titel;
			StartDatum = startDatum;
			Status = status;
			Beschrijving = beschrijving;
			Locatie = locatie;
			Projecten = projecten;
			ProjectOnderdelen = projectOnderdelen;
		}

		public Project(int id, string titel, DateTime startDatum, Status status, string beschrijving, Locatie locatie, List<ProjectPartner> projecten, List<ProjectOnderdeel> projectOnderdelen)
		{
			Id = id;
			Titel = titel;
			StartDatum = startDatum;
			Status = status;
			Beschrijving = beschrijving;
			Locatie = locatie;
			Projecten = projecten;
			ProjectOnderdelen = projectOnderdelen;
		}

		public int Id
		{
			get { return _id; }
			set
			{
				if (value <= 0)
					throw new ProjectException("Id mag niet nul of negatief zijn");
				_id = value;
			}
		}
		public string Titel
		{
			get { return _titel; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ProjectException("Titel mag niet leeg of null zijn");
				_titel = value;
			}
		}
		public DateTime StartDatum
		{
			get { return _startDatum; }
			set { _startDatum = value; }
		}
		public Status Status
		{
			get { return _status; }
			set { _status = value; }
		}
		public string Beschrijving
		{
			get { return _beschrijving; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ProjectException("Beschrijving mag niet leeg of null zijn");
				_beschrijving = value;
			}
		}
		public Locatie Locatie
		{
			get { return _locatie; }
			set
			{
				if (value is null)
					throw new ProjectException("Locatie mag niet null zijn");
				_locatie = value;
			}
		}
		public List<ProjectPartner> Projecten { get; set; } = new();
		public List<ProjectOnderdeel> ProjectOnderdelen { get; set; } = new();
	}
}
