CREATE PROCEDURE Login_Usuario
    @Nombre_Usuario NVARCHAR(100)
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
    WHERE u.[Nombre_Usuario] = @Nombre_Usuario
      AND u.[Id_Estado] = 1;
END
