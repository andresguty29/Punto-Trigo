CREATE PROCEDURE Procesar_Perdida_Vencimiento
	@Id_Movimiento UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM [dbo].[PerdidaInventario_TB] WHERE Id_Movimiento = @Id_Movimiento)
	BEGIN
		RAISERROR('Este vencimiento ya fue procesado.', 16, 1)
		RETURN
	END

	DECLARE @Id_Inventario UNIQUEIDENTIFIER, @Cantidad DECIMAL(10,2), @Fecha_Vencimiento DATE, @Costo_Unitario DECIMAL(18,2)
	SELECT
		@Id_Inventario = Id_Inventario,
		@Cantidad = Cantidad,
		@Fecha_Vencimiento = Fecha_Vencimiento,
		@Costo_Unitario = Costo_Unitario
	FROM [dbo].[MovimientoInventario_TB]
	WHERE Id_Movimiento = @Id_Movimiento AND Tipo = 'Entrada'

	IF @Id_Inventario IS NULL
	BEGIN
		RAISERROR('El movimiento indicado no existe o no es una entrada.', 16, 1)
		RETURN
	END

	DECLARE @StockActual DECIMAL(10,2)
	SELECT @StockActual = Stock_Actual FROM [dbo].[Inventario_TB] WHERE Id_Inventario = @Id_Inventario

	-- Solo se puede dar de baja lo que realmente queda en existencia
	DECLARE @CantidadPerdida DECIMAL(10,2) = CASE WHEN @Cantidad > @StockActual THEN @StockActual ELSE @Cantidad END

	IF @CantidadPerdida <= 0
	BEGIN
		RAISERROR('No hay existencia disponible de este lote para procesar como perdida.', 16, 1)
		RETURN
	END

	DECLARE @Costo_Total DECIMAL(18,2) = @CantidadPerdida * ISNULL(@Costo_Unitario, 0)
	DECLARE @Id_Perdida UNIQUEIDENTIFIER = NEWID()

	BEGIN TRANSACTION

		INSERT INTO [dbo].[MovimientoInventario_TB] (Id_Movimiento, Id_Inventario, Tipo, Cantidad, Motivo)
		VALUES (NEWID(), @Id_Inventario, 'Salida', @CantidadPerdida, 'Vencimiento de producto')

		UPDATE [dbo].[Inventario_TB]
		SET Stock_Actual = Stock_Actual - @CantidadPerdida
		WHERE Id_Inventario = @Id_Inventario

		INSERT INTO [dbo].[PerdidaInventario_TB]
		(
			[Id_Perdida], [Id_Movimiento], [Id_Inventario], [Cantidad], [Costo_Total], [Fecha_Vencimiento]
		)
		VALUES
		(
			@Id_Perdida, @Id_Movimiento, @Id_Inventario, @CantidadPerdida, @Costo_Total, @Fecha_Vencimiento
		)

		SELECT @Id_Perdida AS Id_Perdida, @Costo_Total AS Costo_Total

	COMMIT TRANSACTION
END
