using StadOntwikkeling_BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
	public abstract class Project
	{
		private int _id;
		private string _title;
		private DateTime _startDatum;
		private Status _status;
		private string _beschrijving;
		private Locatie _locatie;

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public string Titel
		{
			get { return _title; }
			set { _title = value; }
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
			set { _beschrijving = value; }
		}
		public Locatie Locatie
		{
			get { return _locatie; }
			set { _locatie = value; }
		}
		public List<ProjectPartner> Projecten { get; set; } = new();
	}
}
