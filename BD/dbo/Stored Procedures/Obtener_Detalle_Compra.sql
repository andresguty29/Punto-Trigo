CREATE PROCEDURE Obtener_Detalle_Compra
	@Id_Compra UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		dc.[Id_DetalleCompra],
		dc.[Id_Compra],
		dc.[Id_Inventario],
		i.[Nombre] AS Nombre_Inventario,
		i.[Unidad],
		dc.[Cantidad],
		dc.[Unidad_Ingresada],
		dc.[Costo_Unitario],
		dc.[Fecha_Vencimiento]
	FROM [dbo].[DetalleCompra_TB] dc
	INNER JOIN [dbo].[Inventario_TB] i ON dc.[Id_Inventario] = i.[Id_Inventario]
	WHERE dc.[Id_Compra] = @Id_Compra
END
