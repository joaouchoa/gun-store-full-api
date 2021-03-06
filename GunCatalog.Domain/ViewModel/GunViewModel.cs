

using GunCatalog.Domain.Enumerable;
using System;

namespace GunCatalog.Domain.ViewModel
{
    public class GunViewModel
    {
        public Guid id { get; set; }
        public string? Fabricante { get; set; }
        public int Capacidade { get; set; }
        public string? Modelo { get; set; }
        public ECalibre Calibre { get; set; }
        public string? NumeroDeSerie { get; set; }
        public double Preco { get; set; }
    }
}
