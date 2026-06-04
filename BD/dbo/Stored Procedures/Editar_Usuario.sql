CREATE OR ALTER PROCEDURE Editar_Usuario
    @Id_Usuario UNIQUEIDENTIFIER,
    @Nombre_Usuario VARCHAR(MAX),
    @Contrasena VARCHAR(MAX),
    @Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IdInt INT = CONVERT(INT, CONVERT(VARBINARY(4), RIGHT(CONVERT(VARCHAR(36), @Id_Usuario), 8), 2));

    BEGIN TRANSACTION;

        UPDATE [dbo].[USUARIO_TB]
        SET
            [NOMBRE_USUARIO] = @Nombre_Usuario,
            [CONTRASENA] = @Contrasena
        WHERE [ID_USUARIO] = @IdInt;

        SELECT @Id_Usuario AS Id_Usuario;

    COMMIT TRANSACTION;
END