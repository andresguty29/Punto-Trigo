CREATE PROCEDURE Registrar_Horas_Extra
	@Id_HorasExtra UNIQUEIDENTIFIER,
	@Id_Trabajador UNIQUEIDENTIFIER,
	@Fecha         DATE,
	@Horas         DECIMAL(5,2)
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM [dbo].[Trabajador_TB] WHERE Id_Trabajador = @Id_Trabajador)
	BEGIN
		RAISERROR('El empleado no existe.', 16, 1)
		RETURN
	END

	IF @Horas IS NULL OR @Horas <= 0
	BEGIN
		RAISERROR('La cantidad de horas ingresada no es valida.', 16, 1)
		RETURN
	END

	IF EXISTS (SELECT 1 FROM [dbo].[HorasExtra_TB] WHERE Id_Trabajador = @Id_Trabajador AND Fecha = @Fecha)
	BEGIN
		RAISERROR('Ya existe un registro de horas adicionales para este empleado en esta fecha.', 16, 1)
		RETURN
	END

	DECLARE @Tarifa_Hora DECIMAL(18,2)
	SELECT @Tarifa_Hora = Tarifa_Hora FROM [dbo].[Trabajador_TB] WHERE Id_Trabajador = @Id_Trabajador

	DECLARE @Monto_Calculado DECIMAL(18,2) = NULL
	IF @Tarifa_Hora IS NOT NULL
		SET @Monto_Calculado = @Horas * @Tarifa_Hora * 1.5

	BEGIN TRANSACTION

		INSERT INTO [dbo].[HorasExtra_TB]
		(
			[Id_HorasExtra],
			[Id_Trabajador],
			[Fecha],
			[Horas],
			[Tarifa_Aplicada],
			[Monto_Calculado]
		)
		VALUES
		(
			@Id_HorasExtra,
			@Id_Trabajador,
			@Fecha,
			@Horas,
			@Tarifa_Hora,
			@Monto_Calculado
		)

		SELECT @Id_HorasExtra

	COMMIT TRANSACTION
END
