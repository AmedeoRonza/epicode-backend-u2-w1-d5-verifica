using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Anagrafica
    {
        public int ID_Anagrafica { get; set; }


        public string Cognome { get; set; }


        public string Nome { get; set; }


        public string Indirizzo { get; set; }

        
        public string Citta { get; set; }

        public int CAP { get; set; }

        [Display(Name = "Codice Fiscale")]
        public string CodiceFiscale { get; set; }

        
    }
}