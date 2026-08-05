CREATE PROCEDURE Calcular_Pago_Trabajador
	@Id_Trabajador    UNIQUEIDENTIFIER,
	@Horas_Trabajadas DECIMAL(10,2) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Tipo_Pago VARCHAR(20), @Salario_Base DECIMAL(18,2), @Tarifa_Hora DECIMAL(18,2)

	SELECT
		@Tipo_Pago = Tipo_Pago,
		@Salario_Base = Salario_Base,
		@Tarifa_Hora = Tarifa_Hora
	FROM [dbo].[Trabajador_TB]
	WHERE Id_Trabajador = @Id_Trabajador

	IF @Tipo_Pago IS NULL
	BEGIN
		RAISERROR('El empleado no tiene un tipo de pago configurado.', 16, 1)
		RETURN
	END

	IF @Salario_Base IS NULL
	BEGIN
		RAISERROR('Falta el salario base para calcular el pago.', 16, 1)
		RETURN
	END

	-- @Salario_Base siempre representa el salario MENSUAL completo del empleado.
	IF @Tipo_Pago = 'Quincenal'
		SELECT (@Salario_Base / 2.0) AS Monto_Calculado, @Tipo_Pago AS Tipo_Pago
	ELSE
		SELECT @Salario_Base AS Monto_Calculado, @Tipo_Pago AS Tipo_Pago
END
