CREATE PROCEDURE Generar_Detalle_Pago
	@Id_Trabajador UNIQUEIDENTIFIER,
	@Periodo       VARCHAR(30),
	@Fecha_Inicio  DATE,
	@Fecha_Fin     DATE
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM [dbo].[Trabajador_TB] WHERE Id_Trabajador = @Id_Trabajador)
	BEGIN
		RAISERROR('El empleado no existe.', 16, 1)
		RETURN
	END

	IF EXISTS (SELECT 1 FROM [dbo].[PlanillaPago_TB] WHERE Id_Trabajador = @Id_Trabajador AND Periodo = @Periodo)
	BEGIN
		RAISERROR('Ya existe un detalle de pago generado para este periodo.', 16, 1)
		RETURN
	END

	DECLARE @Tipo_Pago VARCHAR(20), @Salario_Base DECIMAL(18,2)
	SELECT @Tipo_Pago = Tipo_Pago, @Salario_Base = Salario_Base
	FROM [dbo].[Trabajador_TB]
	WHERE Id_Trabajador = @Id_Trabajador

	IF @Tipo_Pago IS NULL
	BEGIN
		RAISERROR('El empleado no tiene un tipo de pago configurado; complete la configuracion antes de generar el detalle.', 16, 1)
		RETURN
	END

	IF @Salario_Base IS NULL
	BEGIN
		RAISERROR('Falta el salario base del empleado; complete la configuracion antes de generar el detalle.', 16, 1)
		RETURN
	END

	DECLARE @Faltas INT = 0, @Retardos INT = 0, @DiasTrabajados INT = 0

	SELECT
		@Faltas = ISNULL(SUM(CASE WHEN Tipo_Evento = 'Falta' THEN 1 ELSE 0 END), 0),
		@Retardos = ISNULL(SUM(CASE WHEN Tipo_Evento = 'Retardo' THEN 1 ELSE 0 END), 0),
		@DiasTrabajados = ISNULL(SUM(CASE WHEN Tipo_Evento = 'DiaTrabajado' THEN 1 ELSE 0 END), 0)
	FROM [dbo].[Asistencia_TB]
	WHERE Id_Trabajador = @Id_Trabajador
	  AND Fecha BETWEEN @Fecha_Inicio AND @Fecha_Fin

	-- @Salario_Base siempre representa el salario MENSUAL completo del empleado,
	-- sin importar si el tipo de pago es Mensual o Quincenal.
	DECLARE @Salario_Base_Aplicado DECIMAL(18,2)
	IF @Tipo_Pago = 'Quincenal'
		SET @Salario_Base_Aplicado = @Salario_Base / 2.0
	ELSE
		SET @Salario_Base_Aplicado = @Salario_Base

	-- La cuota diaria (para descuentos por falta/retardo) siempre se basa en el salario
	-- mensual completo, para que el descuento sea consistente sin importar la frecuencia de pago.
	DECLARE @Deduccion_Asistencia DECIMAL(18,2) = (@Salario_Base / 30.0) * (@Faltas + (@Retardos * 0.5))

	DECLARE @Ingreso_Horas_Extra DECIMAL(18,2)
	SELECT @Ingreso_Horas_Extra = ISNULL(SUM(Monto_Calculado), 0)
	FROM [dbo].[HorasExtra_TB]
	WHERE Id_Trabajador = @Id_Trabajador
	  AND Fecha BETWEEN @Fecha_Inicio AND @Fecha_Fin

	DECLARE @Deduccion_Prestamos DECIMAL(18,2)
	SELECT @Deduccion_Prestamos = ISNULL(SUM(Saldo_Pendiente), 0)
	FROM [dbo].[Prestamo_TB]
	WHERE Id_Trabajador = @Id_Trabajador AND Saldo_Pendiente > 0

	DECLARE @Total_Ingresos DECIMAL(18,2) = @Salario_Base_Aplicado + @Ingreso_Horas_Extra

	-- Deduccion de la Caja Costarricense de Seguro Social (CCSS): 10.83% del salario bruto
	DECLARE @Deduccion_CCSS DECIMAL(18,2) = @Total_Ingresos * 0.1083

	DECLARE @Total_Deducciones DECIMAL(18,2) = @Deduccion_Asistencia + @Deduccion_Prestamos + @Deduccion_CCSS
	DECLARE @Monto_Neto DECIMAL(18,2) = @Total_Ingresos - @Total_Deducciones
	DECLARE @Id_Planilla UNIQUEIDENTIFIER = NEWID()

	BEGIN TRANSACTION

		INSERT INTO [dbo].[PlanillaPago_TB]
		(
			[Id_Planilla], [Id_Trabajador], [Periodo], [Fecha_Inicio], [Fecha_Fin],
			[Salario_Base_Aplicado], [Ingreso_Horas_Extra], [Deduccion_Asistencia], [Deduccion_Prestamos], [Deduccion_CCSS],
			[Total_Ingresos], [Total_Deducciones], [Monto_Neto]
		)
		VALUES
		(
			@Id_Planilla, @Id_Trabajador, @Periodo, @Fecha_Inicio, @Fecha_Fin,
			@Salario_Base_Aplicado, @Ingreso_Horas_Extra, @Deduccion_Asistencia, @Deduccion_Prestamos, @Deduccion_CCSS,
			@Total_Ingresos, @Total_Deducciones, @Monto_Neto
		)

		-- Los prestamos pendientes se consideran saldados al deducirse en esta planilla
		UPDATE [dbo].[Prestamo_TB]
		SET Saldo_Pendiente = 0
		WHERE Id_Trabajador = @Id_Trabajador AND Saldo_Pendiente > 0

		SELECT @Id_Planilla AS Id_Planilla

	COMMIT TRANSACTION
END
