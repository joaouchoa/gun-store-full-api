using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunCatalog.Service.Exceptions
{
    public class GunHasNotSavedException : Exception
    {
        public GunHasNotSavedException() : base("Este jogo não esta cadastrado") { }
    }
}
