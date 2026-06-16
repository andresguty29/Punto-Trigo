CREATE PROCEDURE Obtener_Puestos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [Id_Puesto],
        [Nombre_Puesto],
        [Id_Estado]
    FROM [dbo].[Puesto_TB]
    
END