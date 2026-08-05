CREATE PROCEDURE Obtener_Bitacora
	@Fecha_Inicio DATE = NULL,
	@Fecha_Fin    DATE = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id_Bitacora,
		Id_Usuario,
		Nombre_Usuario,
		Accion,
		Detalle,
		Fecha_Hora
	FROM [dbo].[Bitacora_TB]
	WHERE (@Fecha_Inicio IS NULL OR CAST(Fecha_Hora AS DATE) >= @Fecha_Inicio)
	  AND (@Fecha_Fin    IS NULL OR CAST(Fecha_Hora AS DATE) <= @Fecha_Fin)
	ORDER BY Fecha_Hora DESC
END
