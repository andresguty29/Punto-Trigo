
CREATE PROCEDURE Agregar_Producto 
	@Id_Producto UNIQUEIDENTIFIER,
	@Nombre_Producto VARCHAR(MAX),
	@Precio_Venta DECIMAL(18,0),
	@Stock_Actual INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Producto_TB]
		(
			[Id_Producto],
			[Estado],
			[Nombre_Producto],
			[Precio_Venta],
			[Stock_Actual]
		)
		VALUES
		(
			@Id_Producto,
			1, 
			@Nombre_Producto,
			@Precio_Venta,
			@Stock_Actual
		)

		SELECT @Id_Producto

	COMMIT TRANSACTION
END