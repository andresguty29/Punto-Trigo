CREATE PROCEDURE Obtener_Registro_Acceso
	@Id_Registro UNIQUEIDENTIFIER
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
	WHERE Id_Registro = @Id_Registro
END
