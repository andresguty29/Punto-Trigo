CREATE PROCEDURE Obtener_Usuario
    @Id_Usuario UNIQUEIDENTIFIER
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
    WHERE [Id_Usuario] = @Id_Usuario;
END