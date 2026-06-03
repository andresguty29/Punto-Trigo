CREATE PROCEDURE Eliminar_Puesto
    @Id_Puesto UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        UPDATE [dbo].[Puesto_TB]
        SET [Id_Estado] = 2
        WHERE [Id_Puesto] = @Id_Puesto;

        SELECT @Id_Puesto AS Id_Puesto;

    COMMIT TRANSACTION;
END