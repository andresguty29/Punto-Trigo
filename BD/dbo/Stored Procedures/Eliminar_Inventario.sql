CREATE PROCEDURE Eliminar_Inventario
    @Id_Inventario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Inventario_TB]
    SET Id_Estado = 2
    WHERE Id_Inventario = @Id_Inventario

    SELECT @Id_Inventario
END
