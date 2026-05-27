
CREATE PROCEDURE Obtener_Productos
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
END