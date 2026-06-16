CREATE PROCEDURE Obtener_Trabajador
    @Id_Trabajador UNIQUEIDENTIFIER
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
    FROM [dbo].[Trabajador_TB] t
    LEFT JOIN [dbo].[Puesto_TB] p ON t.[Id_Puesto] = p.[Id_Puesto]
    WHERE t.[Id_Trabajador] = @Id_Trabajador;
END
