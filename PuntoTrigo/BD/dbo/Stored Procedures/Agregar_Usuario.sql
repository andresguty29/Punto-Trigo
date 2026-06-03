CREATE PROCEDURE Agregar_Usuario
    @Id_Usuario UNIQUEIDENTIFIER,
    @Nombre_Usuario VARCHAR(MAX),
    @Contrasena VARCHAR(MAX),
    @Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        INSERT INTO [dbo].[Usuario_TB]
        (
            [Id_Usuario],
            [Nombre_Usuario],
            [Contrasena],
            [Id_Trabajador],
            [Id_Estado]
        )
        VALUES
        (
            @Id_Usuario,
            @Nombre_Usuario,
            @Contrasena,
            @Id_Trabajador,
            1
        );

        SELECT @Id_Usuario AS Id_Usuario;

    COMMIT TRANSACTION;
END