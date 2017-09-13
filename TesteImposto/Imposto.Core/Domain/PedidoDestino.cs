using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class PedidoDestino
    {
        public enum Destinos
        {
            [Description("RJ")]
            RJ = 1,
            [Description("PE")]
            PE = 2,
            [Description("MG")]
            MG = 3,
            [Description("PB")]
            PB = 4,
            [Description("PR")]
            PR = 5,
            [Description("PI")]
            PI = 6,
            [Description("RO")]
            RO = 7,
            [Description("SE")]
            SE = 8,
            [Description("TO")]
            TO = 9,
            [Description("PA")]
            PA = 10
        }
    }
}
