CREATE PROCEDURE Obtener_Compra
	@Id_Compra UNIQUEIDENTIFIER
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
	WHERE c.[Id_Compra] = @Id_Compra
END
