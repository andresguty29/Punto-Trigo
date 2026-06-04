CREATE OR ALTER PROCEDURE Editar_Puesto
    @Id_Puesto UNIQUEIDENTIFIER,
    @Nombre_Puesto VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IdInt INT = CONVERT(INT, CONVERT(VARBINARY(4), RIGHT(CONVERT(VARCHAR(36), @Id_Puesto), 8), 2));

    BEGIN TRANSACTION;

        UPDATE [dbo].[PUESTO_TB]
        SET
            [NOMBRE_PUESTO] = @Nombre_Puesto
        WHERE [ID_PUESTO] = @IdInt;

        SELECT @Id_Puesto AS Id_Puesto;

    COMMIT TRANSACTION;
END