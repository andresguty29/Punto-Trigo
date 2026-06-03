CREATE PROCEDURE Editar_Usuario
    @Id_Usuario UNIQUEIDENTIFIER,
    @Nombre_Usuario VARCHAR(MAX),
    @Contrasena VARCHAR(MAX),
    @Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        UPDATE [dbo].[Usuario_TB]
        SET
            [Nombre_Usuario] = @Nombre_Usuario,
            [Contrasena] = @Contrasena,
            [Id_Trabajador] = @Id_Trabajador
        WHERE [Id_Usuario] = @Id_Usuario;

        SELECT @Id_Usuario AS Id_Usuario;

    COMMIT TRANSACTION;
END