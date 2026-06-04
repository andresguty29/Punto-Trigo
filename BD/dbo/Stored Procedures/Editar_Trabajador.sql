CREATE OR ALTER PROCEDURE Editar_Trabajador
    @Id_Trabajador UNIQUEIDENTIFIER,
    @Cedula VARCHAR(MAX),
    @Nombre_Completo VARCHAR(MAX),
    @Id_Puesto UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IdInt INT = CONVERT(INT, CONVERT(VARBINARY(4), RIGHT(CONVERT(VARCHAR(36), @Id_Trabajador), 8), 2));
    DECLARE @IdPuestoInt INT = CONVERT(INT, CONVERT(VARBINARY(4), RIGHT(CONVERT(VARCHAR(36), @Id_Puesto), 8), 2));
    DECLARE @Nombre VARCHAR(MAX) = LTRIM(RTRIM(CASE WHEN CHARINDEX(' ', ISNULL(@Nombre_Completo, '')) > 0 THEN LEFT(@Nombre_Completo, CHARINDEX(' ', @Nombre_Completo) - 1) ELSE ISNULL(@Nombre_Completo, '') END));
    DECLARE @Apellido VARCHAR(MAX) = LTRIM(RTRIM(CASE WHEN CHARINDEX(' ', ISNULL(@Nombre_Completo, '')) > 0 THEN SUBSTRING(@Nombre_Completo, CHARINDEX(' ', @Nombre_Completo) + 1, LEN(@Nombre_Completo)) ELSE '' END));

    BEGIN TRANSACTION;

        UPDATE [dbo].[TRABAJADOR_TB]
        SET
            [CEDULA] = @Cedula,
            [NOMBRE] = @Nombre,
            [APELLIDO] = @Apellido,
            [ID_PUESTO] = @IdPuestoInt
        WHERE [ID_TRABAJADOR] = @IdInt;

        SELECT @Id_Trabajador AS Id_Trabajador;

    COMMIT TRANSACTION;
END
