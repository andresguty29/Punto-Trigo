CREATE PROCEDURE Obtener_Puesto
    @Id_Puesto UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [Id_Puesto],
        [Nombre_Puesto],
        [Id_Estado]
    FROM [dbo].[Puesto_TB]
    WHERE [Id_Puesto] = @Id_Puesto;
END