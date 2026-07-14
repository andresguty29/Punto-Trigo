CREATE PROCEDURE Obtener_Compras
	@Fecha_Inicio DATE          = NULL,
	@Fecha_Fin    DATE          = NULL,
	@Monto_Minimo DECIMAL(18,2) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		c.[Id_Compra],
		c.[Id_Proveedor],
		p.[Nombre_Proveedor],
		c.[Numero_Factura],
		c.[Fecha_Compra],
		c.[Categoria],
		c.[Descripcion_Adicional],
		c.[Monto_Total],
		c.[Id_Estado]
	FROM [dbo].[Compra_TB] c
	LEFT JOIN [dbo].[Proveedor_TB] p ON c.[Id_Proveedor] = p.[Id_Proveedor]
	WHERE (@Fecha_Inicio IS NULL OR CAST(c.[Fecha_Compra] AS DATE) >= @Fecha_Inicio)
	  AND (@Fecha_Fin    IS NULL OR CAST(c.[Fecha_Compra] AS DATE) <= @Fecha_Fin)
	  AND (@Monto_Minimo IS NULL OR c.[Monto_Total] >= @Monto_Minimo)
	ORDER BY c.[Fecha_Compra] DESC
END
