CREATE PROCEDURE Asignar_Vacaciones
	@Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Fecha_Ingreso DATE
	SELECT @Fecha_Ingreso = Fecha_Ingreso FROM [dbo].[Trabajador_TB] WHERE Id_Trabajador = @Id_Trabajador

	IF @Fecha_Ingreso IS NULL
	BEGIN
		RAISERROR('El empleado no tiene una fecha de ingreso registrada; no se puede calcular la antiguedad.', 16, 1)
		RETURN
	END

	IF @Fecha_Ingreso > CAST(GETDATE() AS DATE)
	BEGIN
		RAISERROR('La fecha de ingreso registrada no es valida.', 16, 1)
		RETURN
	END

	DECLARE @Meses INT = DATEDIFF(MONTH, @Fecha_Ingreso, GETDATE())
	DECLARE @Anio_Antiguedad_Actual INT = @Meses / 12

	IF @Anio_Antiguedad_Actual < 1
	BEGIN
		RAISERROR('El empleado aun no cumple con la antiguedad minima de un anio para recibir vacaciones.', 16, 1)
		RETURN
	END

	-- Se asignan todos los periodos de antiguedad cumplidos que aun no se hayan asignado
	-- (no solo el ultimo), para que un empleado con varios anios sin procesar reciba todo lo pendiente.
	DECLARE @Resultado TABLE (Id_Vacacion UNIQUEIDENTIFIER, Anio_Antiguedad INT, Dias_Asignados INT)
	DECLARE @Anio INT = 1

	BEGIN TRANSACTION

		WHILE @Anio <= @Anio_Antiguedad_Actual
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM [dbo].[VacacionAsignada_TB] WHERE Id_Trabajador = @Id_Trabajador AND Anio_Antiguedad = @Anio)
			BEGIN
				DECLARE @Id_Vacacion UNIQUEIDENTIFIER = NEWID()

				INSERT INTO [dbo].[VacacionAsignada_TB] (Id_Vacacion, Id_Trabajador, Anio_Antiguedad, Dias_Asignados)
				VALUES (@Id_Vacacion, @Id_Trabajador, @Anio, 12)

				INSERT INTO @Resultado VALUES (@Id_Vacacion, @Anio, 12)
			END

			SET @Anio = @Anio + 1
		END

		IF NOT EXISTS (SELECT 1 FROM @Resultado)
		BEGIN
			ROLLBACK TRANSACTION
			RAISERROR('Ya se asignaron vacaciones para todos los periodos de antiguedad cumplidos.', 16, 1)
			RETURN
		END

		SELECT Id_Vacacion, Anio_Antiguedad, Dias_Asignados FROM @Resultado ORDER BY Anio_Antiguedad

	COMMIT TRANSACTION
END
