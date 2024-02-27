using System;

namespace MVC1.Models
{
    public class Pagamenti
    {
        public int IdDipendente { get; set; }

        public DateTime DataPagamento { get; set; }

        public decimal Ammontare { get; set; }

        public int TipoPagamento { get; set; }
    }
}