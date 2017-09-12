IF OBJECT_ID('dbo.P_TOTAL_IMPOSTO_CFOP') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.P_TOTAL_IMPOSTO_CFOP
    IF OBJECT_ID('dbo.P_TOTAL_IMPOSTO_CFOP') IS NOT NULL
        PRINT '<<< FALHA APAGANDO A PROCEDURE dbo.P_TOTAL_IMPOSTO_CFOP >>>'
    ELSE
        PRINT '<<< PROCEDURE dbo.P_TOTAL_IMPOSTO_CFOP APAGADA >>>'
END
go
SET QUOTED_IDENTIFIER ON
GO
SET NOCOUNT ON 
GO 
CREATE PROCEDURE P_TOTAL_IMPOSTO_CFOP
(
	@pCfop varchar(5)
)
AS
BEGIN
	SELECT
		ItensNotaFiscal.Cfop,
		SUM(ItensNotaFiscal.BaseIcms) AS TotalBaseIcms,
		SUM(ItensNotaFiscal.ValorIcms) AS TotalValorIcms,
		SUM(ItensNotaFiscal.BaseIpi) AS TotalBaseIpi,
		SUM(ItensNotaFiscal.ValorIpi) AS TotalValorIpi
	FROM
		dbo.NotaFiscalItem ItensNotaFiscal
	WHERE
		((@pCfop = '') OR (ItensNotaFiscal.Cfop = @pCfop))
	GROUP BY
		ItensNotaFiscal.Cfop
END
GO
GRANT EXECUTE ON dbo.P_TOTAL_IMPOSTO_CFOP TO [public]
go
IF OBJECT_ID('dbo.P_TOTAL_IMPOSTO_CFOP') IS NOT NULL
    PRINT '<<< PROCEDURE dbo.P_TOTAL_IMPOSTO_CFOP CRIADA >>>'
ELSE
    PRINT '<<< FALHA NA CRIACAO DA PROCEDURE dbo.P_TOTAL_IMPOSTO_CFOP >>>'
go
