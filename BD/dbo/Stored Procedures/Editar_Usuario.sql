CREATE PROCEDURE Editar_Usuario
    @Id_Usuario     UNIQUEIDENTIFIER,
    @Nombre_Usuario NVARCHAR(100),
    @Contrasena     NVARCHAR(MAX),
    @Id_Trabajador  UNIQUEIDENTIFIER,
    @Rol            NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        UPDATE [dbo].[Usuario_TB]
        SET
            [Nombre_Usuario] = @Nombre_Usuario,
            [Contrasena]     = @Contrasena,
            [Id_Trabajador]  = @Id_Trabajador,
            [Rol]            = @Rol
        WHERE [Id_Usuario] = @Id_Usuario;

        SELECT @Id_Usuario AS Id_Usuario;

    COMMIT TRANSACTION;
END
