CREATE PROCEDURE Obtener_Detalle_Tiquete
	@Id_Tiquete UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		dt.[Id_DetalleTiquete],
		dt.[Id_Tiquete],
		dt.[Id_Producto],
		p.[Nombre_Producto],
		dt.[Cantidad],
		dt.[Precio_Unitario],
		dt.[Subtotal]
	FROM [dbo].[DetalleTiquete_TB] dt
	INNER JOIN [dbo].[Producto_TB] p ON dt.[Id_Producto] = p.[Id_Producto]
	WHERE dt.[Id_Tiquete] = @Id_Tiquete
END
