using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Imposto.Core.Data;

namespace Imposto.Core.Domain
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }

        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }
        
        [XmlIgnore]
        public IEnumerable<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        [XmlElement("ItensDaNotaFiscal")]
        public List<NotaFiscalItem> ItensDaNotaFiscalSerializavel
        {
            get { return ItensDaNotaFiscal.ToList<NotaFiscalItem>(); }
            set { ItensDaNotaFiscal = value as IEnumerable<NotaFiscalItem>;  }
        }

        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }

        public string validarOrigemDestino(Pedido pedido)
        {
            StringBuilder mensagemValidacao = new StringBuilder();

            if (!Enum.IsDefined(typeof(PedidoOrigem.Origens), pedido.EstadoOrigem))
            {
                mensagemValidacao.Append("Estado de origem inválido.");
            }
            if (!Enum.IsDefined(typeof(PedidoDestino.Destinos), pedido.EstadoDestino))
            {
                mensagemValidacao.Append((mensagemValidacao != null ? "\n" : "") + 
                                         "Estado de destino inválido.");
            }
            return mensagemValidacao.ToString();
        }

        public void EmitirNotaFiscal(Pedido pedido)
        {
            this.NumeroNotaFiscal = 99999;
            this.Serie = new Random().Next(Int32.MaxValue);
            this.NomeCliente = pedido.NomeCliente;

            this.EstadoDestino = pedido.EstadoDestino;
            this.EstadoOrigem = pedido.EstadoOrigem;

            var ItensDaNotaFiscalAdicionar = new List<NotaFiscalItem>();

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();
                #region Define CFOP
                if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RJ"))
                {
                    notaFiscalItem.Cfop = "6.000";                    
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PE"))
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "MG"))
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PB"))
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PR"))
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PI"))
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RO"))
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "TO"))
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PA"))
                {
                    notaFiscalItem.Cfop = "6.010";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "RJ"))
                {
                    notaFiscalItem.Cfop = "6.000";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PE"))
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "MG"))
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PB"))
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PR"))
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PI"))
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "RO"))
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "TO"))
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PA"))
                {
                    notaFiscalItem.Cfop = "6.010";
                }

                if (this.EstadoDestino == this.EstadoOrigem)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }
                if (notaFiscalItem.Cfop == "6.009")
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido*0.90; //redução de base
                }
                else
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
                }
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms*notaFiscalItem.AliquotaIcms;
                #endregion

                #region Define o ICMS
                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                }
                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;
                #endregion

                #region Cálculo IPI
                var valorAliquotaIpi = 0.1;
                notaFiscalItem.BaseIpi = itemPedido.ValorItemPedido;

                if (itemPedido.Brinde)
                    valorAliquotaIpi = 0.0;

                notaFiscalItem.AliquotaIpi = valorAliquotaIpi;
                notaFiscalItem.ValorIpi = (notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi);
                #endregion

                ItensDaNotaFiscalAdicionar.Add(notaFiscalItem);
            }

            this.ItensDaNotaFiscal = ItensDaNotaFiscalAdicionar;

            if (!gerarNotaXML(this))
                return;

            NotaFiscalRepository repositorioNotaFiscal = new NotaFiscalRepository();
            repositorioNotaFiscal.salvarNotaFiscal(this);

            if (this.Id > 0)
                repositorioNotaFiscal.salvarItemNotaFiscal(this);
            else
                return;
        }

        private bool gerarNotaXML(NotaFiscal notaFiscal)
        {
            XmlSerializer serializardorXML;
            StreamWriter escritorTexto;
            string diretorioArquivoNota;

            if (notaFiscal == null)
                return false;

            diretorioArquivoNota = ConfigurationManager.AppSettings["PastaXMLsNotasFicais"];

            if (string.IsNullOrWhiteSpace(diretorioArquivoNota))
                return false;

            diretorioArquivoNota += "\\nf_" + notaFiscal.NumeroNotaFiscal + ".xml";

            using (escritorTexto = new StreamWriter(diretorioArquivoNota))
            {
                serializardorXML = new XmlSerializer(notaFiscal.GetType());
                serializardorXML.Serialize(escritorTexto, notaFiscal);
                escritorTexto.Close();
            }

            return true;
        }

    }
}
