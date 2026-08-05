CREATE PROCEDURE Registrar_Asistencia
	@Id_Asistencia  UNIQUEIDENTIFIER,
	@Id_Trabajador  UNIQUEIDENTIFIER,
	@Fecha          DATE,
	@Tipo_Evento    VARCHAR(20),
	@Observaciones  VARCHAR(200) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM [dbo].[Trabajador_TB] WHERE Id_Trabajador = @Id_Trabajador)
	BEGIN
		RAISERROR('El empleado no existe.', 16, 1)
		RETURN
	END

	IF @Tipo_Evento NOT IN ('Falta', 'Retardo', 'DiaTrabajado')
	BEGIN
		RAISERROR('El tipo de registro indicado no es valido.', 16, 1)
		RETURN
	END

	IF EXISTS (SELECT 1 FROM [dbo].[Asistencia_TB] WHERE Id_Trabajador = @Id_Trabajador AND Fecha = @Fecha)
	BEGIN
		RAISERROR('Ya existe un registro de asistencia para este empleado en esta fecha.', 16, 1)
		RETURN
	END

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Asistencia_TB]
		(
			[Id_Asistencia],
			[Id_Trabajador],
			[Fecha],
			[Tipo_Evento],
			[Observaciones]
		)
		VALUES
		(
			@Id_Asistencia,
			@Id_Trabajador,
			@Fecha,
			@Tipo_Evento,
			@Observaciones
		)

		SELECT @Id_Asistencia

	COMMIT TRANSACTION
END
