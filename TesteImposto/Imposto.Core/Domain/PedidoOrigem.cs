using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class PedidoOrigem
    {
        public enum Origens
        {
            [Description("SP")]
            SP = 1,
            [Description("MG")]
            MG = 2
        }
    }
}
