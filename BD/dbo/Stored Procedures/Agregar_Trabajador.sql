CREATE OR ALTER PROCEDURE Agregar_Trabajador
    @Id_Trabajador UNIQUEIDENTIFIER,
    @Cedula VARCHAR(MAX),
    @Nombre_Completo VARCHAR(MAX),
    @Id_Puesto UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IdPuestoInt INT = CONVERT(INT, CONVERT(VARBINARY(4), RIGHT(CONVERT(VARCHAR(36), @Id_Puesto), 8), 2));
    DECLARE @Nombre VARCHAR(MAX) = LTRIM(RTRIM(CASE WHEN CHARINDEX(' ', ISNULL(@Nombre_Completo, '')) > 0 THEN LEFT(@Nombre_Completo, CHARINDEX(' ', @Nombre_Completo) - 1) ELSE ISNULL(@Nombre_Completo, '') END));
    DECLARE @Apellido VARCHAR(MAX) = LTRIM(RTRIM(CASE WHEN CHARINDEX(' ', ISNULL(@Nombre_Completo, '')) > 0 THEN SUBSTRING(@Nombre_Completo, CHARINDEX(' ', @Nombre_Completo) + 1, LEN(@Nombre_Completo)) ELSE '' END));

    BEGIN TRANSACTION;

        INSERT INTO [dbo].[TRABAJADOR_TB]
        (
            [NOMBRE],
            [APELLIDO],
            [CEDULA],
            [FECHA_INGRESO],
            [ID_PUESTO],
            [ID_DIRECCION],
            [ID_ESTADO]
        )
        VALUES
        (
            @Nombre,
            @Apellido,
            @Cedula,
            CAST(GETDATE() AS DATE),
            @IdPuestoInt,
            NULL,
            1
        );

        DECLARE @IdInt INT = CAST(SCOPE_IDENTITY() AS INT)
        DECLARE @IdGuid UNIQUEIDENTIFIER = CONVERT(UNIQUEIDENTIFIER, '00000000-0000-0000-0000-' + RIGHT('000000000000' + CONVERT(VARCHAR(8), CONVERT(VARBINARY(4), @IdInt), 2), 12))

        SELECT @IdGuid AS Id_Trabajador;

    COMMIT TRANSACTION;
END