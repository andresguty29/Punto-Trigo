CREATE PROCEDURE Agregar_Trabajador
    @Id_Trabajador UNIQUEIDENTIFIER,
    @Cedula VARCHAR(MAX),
    @Nombre_Completo VARCHAR(MAX),
    @Id_Puesto UNIQUEIDENTIFIER,
    @Fecha_Ingreso DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

        INSERT INTO [dbo].[Trabajador_TB]
        (
            [Id_Trabajador],
            [Cedula],
            [Nombre_Completo],
            [Id_Estado],
            [Id_Puesto],
            [Fecha_Ingreso]
        )
        VALUES
        (
            @Id_Trabajador,
            @Cedula,
            @Nombre_Completo,
            1,
            @Id_Puesto,
            @Fecha_Ingreso
        );

        SELECT @Id_Trabajador AS Id_Trabajador;

    COMMIT TRANSACTION;
END