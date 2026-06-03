CREATE PROCEDURE Obtener_Trabajador
    @Id_Usuario UNIQUEIDENTIFIER,
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Nombre_Usuario,
		Contrasena,
        Id_Trabajdor,
	FROM dbo.Usuario_TB
    WHERE Id_Usuario = @Id_Usuario
END