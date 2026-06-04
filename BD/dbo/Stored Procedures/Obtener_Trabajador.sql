CREATE PROCEDURE Obtener_Trabajador
    @Id_Trabajador UNIQUEIDENTIFIER
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
    WHERE [Id_Trabajador] = @Id_Trabajador;
END