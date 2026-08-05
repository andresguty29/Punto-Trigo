CREATE PROCEDURE Obtener_Vencimientos_Pendientes
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		m.[Id_Movimiento],
		m.[Id_Inventario],
		i.[Nombre] AS Nombre_Inventario,
		i.[Unidad],
		m.[Cantidad],
		m.[Fecha_Vencimiento],
		m.[Costo_Unitario],
		i.[Stock_Actual]
	FROM [dbo].[MovimientoInventario_TB] m
	INNER JOIN [dbo].[Inventario_TB] i ON m.[Id_Inventario] = i.[Id_Inventario]
	LEFT JOIN [dbo].[PerdidaInventario_TB] p ON p.[Id_Movimiento] = m.[Id_Movimiento]
	WHERE m.[Tipo] = 'Entrada'
	  AND m.[Fecha_Vencimiento] IS NOT NULL
	  AND m.[Fecha_Vencimiento] <= CAST(GETDATE() AS DATE)
	  AND p.[Id_Movimiento] IS NULL
	ORDER BY m.[Fecha_Vencimiento] ASC
END
