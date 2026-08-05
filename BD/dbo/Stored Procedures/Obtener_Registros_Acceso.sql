CREATE PROCEDURE Obtener_Registros_Acceso
	@Fecha_Inicio   DATE        = NULL,
	@Fecha_Fin      DATE        = NULL,
	@Nombre_Usuario VARCHAR(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id_Registro,
		Id_Usuario,
		Nombre_Usuario,
		Fecha_Login,
		Fecha_Logout,
		Exitoso
	FROM [dbo].[RegistroAcceso_TB]
	WHERE (@Fecha_Inicio   IS NULL OR CAST(Fecha_Login AS DATE) >= @Fecha_Inicio)
	  AND (@Fecha_Fin      IS NULL OR CAST(Fecha_Login AS DATE) <= @Fecha_Fin)
	  AND (@Nombre_Usuario IS NULL OR Nombre_Usuario = @Nombre_Usuario)
	ORDER BY Fecha_Login DESC
END
