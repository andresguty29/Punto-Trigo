CREATE PROCEDURE Eliminar_Usuario
    @Id_Usuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        UPDATE [dbo].[Usuario_TB]
        SET [Id_Estado] = 2
        WHERE [Id_Usuario] = @Id_Usuario;

        SELECT @Id_Usuario AS Id_Usuario;

    COMMIT TRANSACTION;
END