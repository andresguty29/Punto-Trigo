CREATE PROCEDURE Obtener_Detalle_Pago
	@Id_Planilla UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		p.[Id_Planilla],
		p.[Id_Trabajador],
		t.[Nombre_Completo] AS Nombre_Trabajador,
		p.[Periodo],
		p.[Fecha_Inicio],
		p.[Fecha_Fin],
		p.[Salario_Base_Aplicado],
		p.[Ingreso_Horas_Extra],
		p.[Deduccion_Asistencia],
		p.[Deduccion_Prestamos],
		p.[Deduccion_CCSS],
		p.[Total_Ingresos],
		p.[Total_Deducciones],
		p.[Monto_Neto],
		p.[Fecha_Generacion]
	FROM [dbo].[PlanillaPago_TB] p
	INNER JOIN [dbo].[Trabajador_TB] t ON p.[Id_Trabajador] = t.[Id_Trabajador]
	WHERE p.[Id_Planilla] = @Id_Planilla
END
