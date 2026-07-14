CREATE PROCEDURE Agregar_Detalle_Tiquete
	@Id_DetalleTiquete UNIQUEIDENTIFIER,
	@Id_Tiquete        UNIQUEIDENTIFIER,
	@Id_Producto       UNIQUEIDENTIFIER,
	@Cantidad          INT,
	@Precio_Unitario   DECIMAL(18,2)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @StockActual INT
	SELECT @StockActual = Stock_Actual FROM [dbo].[Producto_TB] WHERE Id_Producto = @Id_Producto

	IF @StockActual IS NULL
	BEGIN
		RAISERROR('El producto no existe en el catalogo.', 16, 1)
		RETURN
	END

	IF @StockActual < @Cantidad
	BEGIN
		RAISERROR('Stock insuficiente para completar la venta de este producto.', 16, 1)
		RETURN
	END

	BEGIN TRANSACTION

		INSERT INTO [dbo].[DetalleTiquete_TB]
		(
			[Id_DetalleTiquete],
			[Id_Tiquete],
			[Id_Producto],
			[Cantidad],
			[Precio_Unitario],
			[Subtotal]
		)
		VALUES
		(
			@Id_DetalleTiquete,
			@Id_Tiquete,
			@Id_Producto,
			@Cantidad,
			@Precio_Unitario,
			@Cantidad * @Precio_Unitario
		)

		UPDATE [dbo].[Producto_TB]
		SET Stock_Actual = Stock_Actual - @Cantidad
		WHERE Id_Producto = @Id_Producto

	COMMIT TRANSACTION
END
