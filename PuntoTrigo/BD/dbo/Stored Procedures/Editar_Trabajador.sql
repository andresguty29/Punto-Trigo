CREATE PROCEDURE Editar_Trabajador
    @Id_Trabajador UNIQUEIDENTIFIER,
    @Cedula VARCHAR(MAX),
    @Nombre_Completo VARCHAR(MAX),
    @Id_Puesto UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        UPDATE [dbo].[Trabajador_TB]
        SET
            [Cedula] = @Cedula,
            [Nombre_Completo] = @Nombre_Completo,
            [Id_Puesto] = @Id_Puesto
        WHERE [Id_Trabajador] = @Id_Trabajador;

        SELECT @Id_Trabajador AS Id_Trabajador;

    COMMIT TRANSACTION;
END
