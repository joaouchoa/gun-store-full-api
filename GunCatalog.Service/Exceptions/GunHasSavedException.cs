using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunCatalog.Service.Exceptions
{
    public class GunHasSavedException : Exception
    {
        public GunHasSavedException() : base("Esta arma ja foi cadastrada no Inventario."){ }
        
    }
}
