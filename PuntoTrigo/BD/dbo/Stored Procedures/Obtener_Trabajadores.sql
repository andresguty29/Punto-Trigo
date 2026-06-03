CREATE PROCEDURE Obtener_Trabajadores
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [Id_Trabajador],
        [Cedula],
        [Nombre_Completo],
        [Id_Estado],
        [Id_Puesto]
    FROM [dbo].[Trabajador_TB]
    WHERE [Id_Estado] = 1;
END