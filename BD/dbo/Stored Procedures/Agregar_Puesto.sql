CREATE OR ALTER PROCEDURE Agregar_Puesto
    @Id_Puesto UNIQUEIDENTIFIER,
    @Nombre_Puesto VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        INSERT INTO [dbo].[PUESTO_TB]
        (
            [NOMBRE_PUESTO],
            [SALARIO_BASE]
        )
        VALUES
        (
            @Nombre_Puesto,
            0
        );

        DECLARE @IdInt INT = CAST(SCOPE_IDENTITY() AS INT)
        DECLARE @IdGuid UNIQUEIDENTIFIER = CONVERT(UNIQUEIDENTIFIER, '00000000-0000-0000-0000-' + RIGHT('000000000000' + CONVERT(VARCHAR(8), CONVERT(VARBINARY(4), @IdInt), 2), 12))

        SELECT @IdGuid AS Id_Puesto;

    COMMIT TRANSACTION;
END