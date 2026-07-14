CREATE PROCEDURE Obtener_Tiquete
	@Id_Tiquete UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		t.[Id_Tiquete],
		t.[Consecutivo],
		t.[Clave],
		t.[Id_Cliente],
		ISNULL(c.[Nombre_Completo], 'Receptor Generico') AS Nombre_Cliente,
		t.[Id_Trabajador],
		tr.[Nombre_Completo] AS Nombre_Trabajador,
		t.[Fecha_Emision],
		t.[Estado],
		t.[Monto_Total]
	FROM [dbo].[Tiquete_TB] t
	LEFT JOIN [dbo].[Cliente_TB] c ON t.[Id_Cliente] = c.[Id_Cliente]
	LEFT JOIN [dbo].[Trabajador_TB] tr ON t.[Id_Trabajador] = tr.[Id_Trabajador]
	WHERE t.[Id_Tiquete] = @Id_Tiquete
END
