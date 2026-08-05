CREATE PROCEDURE Obtener_Vacaciones_Trabajador
	@Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id_Vacacion,
		Id_Trabajador,
		Anio_Antiguedad,
		Dias_Asignados,
		Fecha_Asignacion
	FROM [dbo].[VacacionAsignada_TB]
	WHERE Id_Trabajador = @Id_Trabajador
	ORDER BY Anio_Antiguedad
END
