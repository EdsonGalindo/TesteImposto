using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        private SqlConnection conexaoBancoDeDados { get; set; }

        private void criarConexaoBancoDeDados()
        {
            var stringConexaoBanco = string.Empty;
            ConnectionStringSettings configuracoesConexao = ConfigurationManager.ConnectionStrings["TesteImposto.Properties.Settings.stringBancoImposto"];

            if (configuracoesConexao != null)
                stringConexaoBanco = configuracoesConexao.ConnectionString;
            
            conexaoBancoDeDados = new SqlConnection(stringConexaoBanco);
        }

        public bool salvarNotaFiscal(NotaFiscal notaFiscal)
        {
            try
            {
                criarConexaoBancoDeDados();
                using (var comandoBancoDeDados = new SqlCommand())
                {
                    comandoBancoDeDados.CommandText = "dbo.P_NOTA_FISCAL";
                    comandoBancoDeDados.Connection = conexaoBancoDeDados;
                    comandoBancoDeDados.CommandType = CommandType.StoredProcedure;

                    comandoBancoDeDados.Parameters.Add("@pId", SqlDbType.Int).Direction = ParameterDirection.InputOutput;
                    comandoBancoDeDados.Parameters["@pId"].Value = notaFiscal.Id;
                    comandoBancoDeDados.Parameters.Add("@pNumeroNotaFiscal", SqlDbType.Int);
                    comandoBancoDeDados.Parameters["@pNumeroNotaFiscal"].Value = notaFiscal.NumeroNotaFiscal;
                    comandoBancoDeDados.Parameters.Add("@pSerie", SqlDbType.Int);
                    comandoBancoDeDados.Parameters["@pSerie"].Value = notaFiscal.Serie;
                    comandoBancoDeDados.Parameters.Add("@pNomeCliente", SqlDbType.VarChar, 50);
                    comandoBancoDeDados.Parameters["@pNomeCliente"].Value = notaFiscal.NomeCliente;
                    comandoBancoDeDados.Parameters.Add("@pEstadoDestino", SqlDbType.VarChar, 50);
                    comandoBancoDeDados.Parameters["@pEstadoDestino"].Value = notaFiscal.EstadoDestino;
                    comandoBancoDeDados.Parameters.Add("@pEstadoOrigem", SqlDbType.VarChar, 50);
                    comandoBancoDeDados.Parameters["@pEstadoOrigem"].Value = notaFiscal.EstadoOrigem;
                    
                    conexaoBancoDeDados.Open();
                    comandoBancoDeDados.ExecuteNonQuery();

                    if (notaFiscal.Id == 0)
                        notaFiscal.Id = Convert.ToInt32(comandoBancoDeDados.Parameters["@pId"].Value);
                    conexaoBancoDeDados.Close();
                }
            }
            catch (Exception e) { return false; }

            return true;
        }

        public bool salvarItemNotaFiscal(NotaFiscal notaFiscal)
        {
            try
            {
                criarConexaoBancoDeDados();
                foreach (var itemNofiscal in notaFiscal.ItensDaNotaFiscal)
                {
                    using (var comandoBancoDeDados = new SqlCommand())
                    {
                        comandoBancoDeDados.CommandText = "dbo.P_NOTA_FISCAL_ITEM";
                        comandoBancoDeDados.Connection = conexaoBancoDeDados;
                        comandoBancoDeDados.CommandType = CommandType.StoredProcedure;
                        
                        comandoBancoDeDados.Parameters.Add("@pId", SqlDbType.Int);
                        comandoBancoDeDados.Parameters["@pId"].Value = itemNofiscal.Id;
                        comandoBancoDeDados.Parameters.Add("@pIdNotaFiscal", SqlDbType.Int);
                        comandoBancoDeDados.Parameters["@pIdNotaFiscal"].Value = notaFiscal.Id;
                        comandoBancoDeDados.Parameters.Add("@pCfop", SqlDbType.VarChar, 5);
                        comandoBancoDeDados.Parameters["@pCfop"].Value = (itemNofiscal.Cfop == null ? String.Empty : itemNofiscal.Cfop);
                        comandoBancoDeDados.Parameters.Add("@pTipoIcms", SqlDbType.VarChar, 20);
                        comandoBancoDeDados.Parameters["@pTipoIcms"].Value = itemNofiscal.TipoIcms;
                        comandoBancoDeDados.Parameters.Add("@pBaseIcms", SqlDbType.Decimal);
                        comandoBancoDeDados.Parameters["@pBaseIcms"].Value = itemNofiscal.BaseIcms;
                        comandoBancoDeDados.Parameters.Add("@pAliquotaIcms", SqlDbType.Decimal);
                        comandoBancoDeDados.Parameters["@pAliquotaIcms"].Value = itemNofiscal.AliquotaIcms;
                        comandoBancoDeDados.Parameters.Add("@pValorIcms", SqlDbType.Decimal);
                        comandoBancoDeDados.Parameters["@pValorIcms"].Value = itemNofiscal.ValorIcms;
                        comandoBancoDeDados.Parameters.Add("@pNomeProduto", SqlDbType.VarChar, 50);
                        comandoBancoDeDados.Parameters["@pNomeProduto"].Value = itemNofiscal.NomeProduto;
                        comandoBancoDeDados.Parameters.Add("@pCodigoProduto", SqlDbType.VarChar, 20);
                        comandoBancoDeDados.Parameters["@pCodigoProduto"].Value = itemNofiscal.CodigoProduto;
                        conexaoBancoDeDados.Open();
                        comandoBancoDeDados.ExecuteNonQuery();
                        conexaoBancoDeDados.Close();
                    }
                }
            }
            catch (Exception e) { return false; }

            return true;
        }

        public NotaFiscalRepository()
        {
            //criarConexaoBancoDeDados();
        }
    }
}
