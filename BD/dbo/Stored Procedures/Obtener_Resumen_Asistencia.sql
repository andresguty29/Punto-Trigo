CREATE PROCEDURE Obtener_Resumen_Asistencia
	@Id_Trabajador UNIQUEIDENTIFIER,
	@Fecha_Inicio  DATE,
	@Fecha_Fin     DATE
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Salario_Base DECIMAL(18,2), @Tipo_Pago VARCHAR(20)
	SELECT @Salario_Base = Salario_Base, @Tipo_Pago = Tipo_Pago
	FROM [dbo].[Trabajador_TB]
	WHERE Id_Trabajador = @Id_Trabajador

	DECLARE @Faltas INT, @Retardos INT, @DiasTrabajados INT

	SELECT
		@Faltas = SUM(CASE WHEN Tipo_Evento = 'Falta' THEN 1 ELSE 0 END),
		@Retardos = SUM(CASE WHEN Tipo_Evento = 'Retardo' THEN 1 ELSE 0 END),
		@DiasTrabajados = SUM(CASE WHEN Tipo_Evento = 'DiaTrabajado' THEN 1 ELSE 0 END)
	FROM [dbo].[Asistencia_TB]
	WHERE Id_Trabajador = @Id_Trabajador
	  AND Fecha BETWEEN @Fecha_Inicio AND @Fecha_Fin

	DECLARE @Salario_Diario DECIMAL(18,2) = NULL
	DECLARE @Descuento_Estimado DECIMAL(18,2) = NULL

	IF @Tipo_Pago = 'Mensual' AND @Salario_Base IS NOT NULL
	BEGIN
		SET @Salario_Diario = @Salario_Base / 30.0
		SET @Descuento_Estimado = @Salario_Diario * (ISNULL(@Faltas, 0) + (ISNULL(@Retardos, 0) * 0.5))
	END

	SELECT
		ISNULL(@Faltas, 0)          AS Faltas,
		ISNULL(@Retardos, 0)        AS Retardos,
		ISNULL(@DiasTrabajados, 0)  AS Dias_Trabajados,
		@Salario_Diario             AS Salario_Diario,
		@Descuento_Estimado         AS Descuento_Estimado
END
