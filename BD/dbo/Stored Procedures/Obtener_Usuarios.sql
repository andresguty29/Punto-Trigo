CREATE PROCEDURE Obtener_Usuarios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [Id_Usuario],
        [Nombre_Usuario],
        [Contrasena],
        [Id_Trabajador],
        [Id_Estado]
    FROM [dbo].[Usuario_TB]
    WHERE [Id_Estado] = 1;
END