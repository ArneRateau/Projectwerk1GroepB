using StadOntwikkeling_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_BL.Models
{
    public class Gebruiker
    {
        private string _email;
        private int _id;

        public string? Naam { get; set; }
        public Gebruiker(string email, bool isAdmin, bool isPartner)
		{
			Email = email;
			IsAdmin = isAdmin;
			IsPartner = isPartner;
		}

		public Gebruiker(int id, string email, bool isAdmin, bool isPartner)
		{
			Id = id;
			Email = email;
			IsAdmin = isAdmin;
			IsPartner = isPartner;
		}

        public Gebruiker(int id, string email, bool isAdmin, bool isPartner, string? naam)
        {
            Id = id;
            Email = email;
            IsAdmin = isAdmin;
            IsPartner = isPartner;
            Naam = naam;
        }


        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0)
                    throw new PartnerException("Id mag niet nul of negatief zijn");
                _id = value;
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new PartnerException("Email mag niet leeg of null zijn");
                _email = value;
            }
        }

        public bool IsAdmin { get; set; }
        public bool IsPartner { get; set; }
    }
}
