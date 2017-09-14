using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class EstadoSudeste
    {
        public enum Estados
        {
            [Description("ES")]
            ES = 1,
            [Description("MG")]
            MG = 2,
            [Description("RJ")]
            RJ = 3,
            [Description("SP")]
            SP = 4
        }

        public bool verificarEstadoSudeste(string estadoDestino)
        {
            if (Enum.IsDefined(typeof(EstadoSudeste.Estados), estadoDestino))
                return true;

            return false;
        }
    }
}
