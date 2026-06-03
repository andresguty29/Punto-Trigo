CREATE PROCEDURE Editar_Puesto
    @Id_Puesto UNIQUEIDENTIFIER,
    @Nombre_Puesto VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        UPDATE [dbo].[Puesto_TB]
        SET
            [Nombre_Puesto] = @Nombre_Puesto
        WHERE [Id_Puesto] = @Id_Puesto;

        SELECT @Id_Puesto AS Id_Puesto;

    COMMIT TRANSACTION;
END