namespace MVC1.Models
{
    public class Dipendenti
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Cognome { get; set; }
        public string Indirizzo { get; set; }
        public string CodFiscale { get; set; }

        public bool Coniugato { get; set; }

        public int FigliACarico { get; set; }

        public string Mansione { get; set; }
    }
}