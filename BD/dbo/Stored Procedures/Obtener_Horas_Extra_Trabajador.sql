CREATE PROCEDURE Obtener_Horas_Extra_Trabajador
	@Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id_HorasExtra,
		Id_Trabajador,
		Fecha,
		Horas,
		Tarifa_Aplicada,
		Monto_Calculado,
		Fecha_Registro
	FROM [dbo].[HorasExtra_TB]
	WHERE Id_Trabajador = @Id_Trabajador
	ORDER BY Fecha DESC
END
