CREATE PROCEDURE CambiarContrasena_Usuario
    @Id_Usuario UNIQUEIDENTIFIER,
    @Contrasena NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Usuario_TB]
    SET [Contrasena] = @Contrasena
    WHERE [Id_Usuario] = @Id_Usuario;

    SELECT @Id_Usuario;
END
