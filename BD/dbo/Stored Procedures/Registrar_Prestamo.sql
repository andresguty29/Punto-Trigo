CREATE PROCEDURE Registrar_Prestamo
	@Id_Prestamo   UNIQUEIDENTIFIER,
	@Id_Trabajador UNIQUEIDENTIFIER,
	@Monto         DECIMAL(18,2),
	@Fecha         DATE,
	@Descripcion   VARCHAR(200) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM [dbo].[Trabajador_TB] WHERE Id_Trabajador = @Id_Trabajador)
	BEGIN
		RAISERROR('El empleado no existe.', 16, 1)
		RETURN
	END

	IF @Monto IS NULL OR @Monto <= 0
	BEGIN
		RAISERROR('El monto ingresado no es valido.', 16, 1)
		RETURN
	END

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Prestamo_TB]
		(
			[Id_Prestamo],
			[Id_Trabajador],
			[Monto],
			[Fecha],
			[Descripcion],
			[Saldo_Pendiente]
		)
		VALUES
		(
			@Id_Prestamo,
			@Id_Trabajador,
			@Monto,
			@Fecha,
			@Descripcion,
			@Monto
		)

		SELECT @Id_Prestamo

	COMMIT TRANSACTION
END
