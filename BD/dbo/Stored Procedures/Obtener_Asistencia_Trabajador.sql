CREATE PROCEDURE Obtener_Asistencia_Trabajador
	@Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id_Asistencia,
		Id_Trabajador,
		Fecha,
		Tipo_Evento,
		Observaciones,
		Fecha_Registro
	FROM [dbo].[Asistencia_TB]
	WHERE Id_Trabajador = @Id_Trabajador
	ORDER BY Fecha DESC
END
