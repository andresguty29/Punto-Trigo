
CREATE PROCEDURE Editar_Producto
	@Id_Producto     UNIQUEIDENTIFIER,
	@Id_Proveedor    UNIQUEIDENTIFIER = NULL,
	@Nombre_Producto VARCHAR(MAX),
	@Precio_Venta    DECIMAL(18,2),
	@Stock_Actual    INT,
	@Imagen_Path     VARCHAR(500) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		UPDATE dbo.Producto_TB
		SET
			Id_Proveedor    = @Id_Proveedor,
			Nombre_Producto = @Nombre_Producto,
			Precio_Venta    = @Precio_Venta,
			Stock_Actual    = @Stock_Actual,
			Imagen_Path     = @Imagen_Path
		WHERE Id_Producto = @Id_Producto

	COMMIT TRANSACTION
END