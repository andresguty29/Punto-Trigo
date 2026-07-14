CREATE PROCEDURE Agregar_Detalle_Compra
	@Id_DetalleCompra UNIQUEIDENTIFIER,
	@Id_Compra        UNIQUEIDENTIFIER,
	@Id_Inventario    UNIQUEIDENTIFIER,
	@Cantidad         DECIMAL(10,2),
	@Unidad_Ingresada VARCHAR(20),
	@Costo_Unitario   DECIMAL(18,2) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Unidad_Catalogo VARCHAR(20)
	SELECT @Unidad_Catalogo = Unidad FROM [dbo].[Inventario_TB] WHERE Id_Inventario = @Id_Inventario

	IF @Unidad_Catalogo IS NULL
	BEGIN
		RAISERROR('El insumo no existe en el catalogo de inventario.', 16, 1)
		RETURN
	END

	IF LOWER(LTRIM(RTRIM(@Unidad_Catalogo))) <> LOWER(LTRIM(RTRIM(@Unidad_Ingresada)))
	BEGIN
		RAISERROR('La unidad ingresada (%s) no coincide con la unidad registrada para este insumo (%s).', 16, 1, @Unidad_Ingresada, @Unidad_Catalogo)
		RETURN
	END

	BEGIN TRANSACTION

		INSERT INTO [dbo].[DetalleCompra_TB]
		(
			[Id_DetalleCompra],
			[Id_Compra],
			[Id_Inventario],
			[Cantidad],
			[Unidad_Ingresada],
			[Costo_Unitario]
		)
		VALUES
		(
			@Id_DetalleCompra,
			@Id_Compra,
			@Id_Inventario,
			@Cantidad,
			@Unidad_Ingresada,
			@Costo_Unitario
		)

		UPDATE [dbo].[Inventario_TB]
		SET Stock_Actual = Stock_Actual + @Cantidad
		WHERE Id_Inventario = @Id_Inventario

		SELECT @Id_DetalleCompra

	COMMIT TRANSACTION
END
