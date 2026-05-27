
CREATE PROCEDURE Obtener_Producto
	@Id_Producto UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id_Producto,
		Estado,
		Nombre_Producto,
		Precio_Venta,
		Stock_Actual
	FROM dbo.Producto_TB
	WHERE Id_Producto = @Id_Producto
END