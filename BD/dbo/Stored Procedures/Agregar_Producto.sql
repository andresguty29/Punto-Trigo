
CREATE PROCEDURE Agregar_Producto
	@Id_Producto     UNIQUEIDENTIFIER,
	@Id_Proveedor    UNIQUEIDENTIFIER = NULL,
	@Nombre_Producto VARCHAR(MAX),
	@Precio_Venta    DECIMAL(18,2),
	@Stock_Actual    INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Producto_TB]
		(
			[Id_Producto],
			[Id_Estado],
			[Id_Proveedor],
			[Nombre_Producto],
			[Precio_Venta],
			[Stock_Actual]
		)
		VALUES
		(
			@Id_Producto,
			1,
			@Id_Proveedor,
			@Nombre_Producto,
			@Precio_Venta,
			@Stock_Actual
		)

		SELECT @Id_Producto

	COMMIT TRANSACTION
END