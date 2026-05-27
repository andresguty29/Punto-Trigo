
CREATE PROCEDURE Editar_Producto
	@Id_Producto UNIQUEIDENTIFIER,
	@Nombre_Producto VARCHAR(MAX),
	@Precio_Venta DECIMAL(18,0),
	@Stock_Actual INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		UPDATE dbo.Producto_TB
		SET
			Nombre_Producto = @Nombre_Producto,
			Precio_Venta = @Precio_Venta,
			Stock_Actual = @Stock_Actual
		WHERE Id_Producto = @Id_Producto

	COMMIT TRANSACTION
END