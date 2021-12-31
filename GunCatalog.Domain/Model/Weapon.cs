using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunCatalog.Domain.Model
{
    public abstract class Weapon
    {
        public Guid id { get; set; }
        public bool Active { get; set; }
        public double Preco { get; set; }
        public void Delete()
        {
            this.Active = false;
        }
    }
}
