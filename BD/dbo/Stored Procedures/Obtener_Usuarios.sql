CREATE PROCEDURE Obtener_Usuarios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.[Id_Usuario],
        u.[Nombre_Usuario],
        u.[Contrasena],
        u.[Id_Trabajador],
        t.[Nombre_Completo] AS Nombre_Trabajador,
        u.[Rol],
        u.[Id_Estado]
    FROM [dbo].[Usuario_TB] u
    LEFT JOIN [dbo].[Trabajador_TB] t ON u.[Id_Trabajador] = t.[Id_Trabajador]
END
