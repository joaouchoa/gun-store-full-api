using GunCatalog.Domain.Enumerable;
using System;

namespace GunCatalog.Domain.Model
{
    public class Gun : Weapon
    {
        public string Fabricante { get; set; }
        public int Capacidade { get; set; }
        public string Modelo { get; set; }
        public ECalibre Calibre { get; set; }
        public string NumeroDeSerie { get; set; }

        public override string ToString()
        {
            string retorno = "";
            retorno += "Modelo: " + this.Modelo + Environment.NewLine;
            retorno += "Calibre: " + this.Calibre + Environment.NewLine;
            retorno += "Capacidade: " + this.Capacidade + Environment.NewLine;
            retorno += "Fabricante: " + this.Fabricante + Environment.NewLine;
            retorno += "Numero de Série: " + this.NumeroDeSerie + Environment.NewLine;
            retorno += "Ativo: " + this.Active + Environment.NewLine;

            return retorno;
        }

        public string retornaModelo()
        {
            return this.Modelo;
        }

        public Guid retornaId()
        {
            return this.id;
        }
    }
}
