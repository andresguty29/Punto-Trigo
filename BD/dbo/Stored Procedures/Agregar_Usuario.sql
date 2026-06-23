CREATE PROCEDURE Agregar_Usuario
    @Id_Usuario     UNIQUEIDENTIFIER,
    @Nombre_Usuario NVARCHAR(100),
    @Contrasena     NVARCHAR(MAX),
    @Id_Trabajador  UNIQUEIDENTIFIER,
    @Rol            NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        INSERT INTO [dbo].[Usuario_TB]
            ([Id_Usuario], [Nombre_Usuario], [Contrasena], [Id_Trabajador], [Rol], [Id_Estado])
        VALUES
            (@Id_Usuario, @Nombre_Usuario, @Contrasena, @Id_Trabajador, @Rol, 1);

        SELECT @Id_Usuario AS Id_Usuario;

    COMMIT TRANSACTION;
END
