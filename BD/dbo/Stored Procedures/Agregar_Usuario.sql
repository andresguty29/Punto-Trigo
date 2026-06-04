CREATE OR ALTER PROCEDURE Agregar_Usuario
    @Id_Usuario UNIQUEIDENTIFIER,
    @Nombre_Usuario VARCHAR(MAX),
    @Contrasena VARCHAR(MAX),
    @Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        INSERT INTO [dbo].[USUARIO_TB]
        (
            [NOMBRE_USUARIO],
            [CORREO],
            [CONTRASENA],
            [ID_ESTADO]
        )
        VALUES
        (
            @Nombre_Usuario,
            CONVERT(VARCHAR(36), NEWID()) + '@local',
            @Contrasena,
            1
        );

        DECLARE @IdInt INT = CAST(SCOPE_IDENTITY() AS INT)
        DECLARE @IdGuid UNIQUEIDENTIFIER = CONVERT(UNIQUEIDENTIFIER, '00000000-0000-0000-0000-' + RIGHT('000000000000' + CONVERT(VARCHAR(8), CONVERT(VARBINARY(4), @IdInt), 2), 12))

        SELECT @IdGuid AS Id_Usuario;

    COMMIT TRANSACTION;
END