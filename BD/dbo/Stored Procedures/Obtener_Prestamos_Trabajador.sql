CREATE PROCEDURE Obtener_Prestamos_Trabajador
	@Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id_Prestamo,
		Id_Trabajador,
		Monto,
		Fecha,
		Descripcion,
		Saldo_Pendiente,
		Fecha_Registro
	FROM [dbo].[Prestamo_TB]
	WHERE Id_Trabajador = @Id_Trabajador
	ORDER BY Fecha DESC
END
