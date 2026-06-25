CREATE PROCEDURE Obtener_Trabajadores_Panaderos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t.[Id_Trabajador],
        t.[Cedula],
        t.[Nombre_Completo],
        t.[Id_Estado],
        t.[Id_Puesto],
        p.[Nombre_Puesto]
    FROM dbo.Trabajador_TB t
    INNER JOIN dbo.Usuario_TB u ON t.[Id_Trabajador] = u.[Id_Trabajador]
    LEFT JOIN dbo.Puesto_TB p ON t.[Id_Puesto] = p.[Id_Puesto]
    WHERE u.[Rol] = 'Panadero'
      AND t.[Id_Estado] = 1
      AND u.[Id_Estado] = 1
    ORDER BY t.[Nombre_Completo];
END
