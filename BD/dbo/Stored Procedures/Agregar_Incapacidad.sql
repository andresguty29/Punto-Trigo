
-- =============================================
-- INCAPACIDADES
-- =============================================

CREATE PROCEDURE [dbo].[Agregar_Incapacidad]
    @Id_Incapacidad     UNIQUEIDENTIFIER,
    @Id_Trabajador      UNIQUEIDENTIFIER,
    @Fecha_Inicio       DATE,
    @Fecha_Fin          DATE,
    @Dias_Incapacidad   INT,
    @Tipo_Incapacidad   VARCHAR(100),
    @Numero_CCSS        VARCHAR(50) = NULL,
    @Diagnostico        VARCHAR(500) = NULL,
    @Observaciones      VARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Trabajador_TB] WHERE [Id_Trabajador] = @Id_Trabajador AND [Id_Estado] = 1)
    BEGIN
        RAISERROR('El trabajador no existe o está inactivo.', 16, 1);
        RETURN;
    END

    -- Verificar traslape con otras incapacidades
    IF EXISTS (
        SELECT 1 FROM [dbo].[Incapacidad_TB]
        WHERE [Id_Trabajador] = @Id_Trabajador
          AND [Id_Estado] = 1
          AND (
                (@Fecha_Inicio BETWEEN [Fecha_Inicio] AND [Fecha_Fin]) OR
                (@Fecha_Fin   BETWEEN [Fecha_Inicio] AND [Fecha_Fin]) OR
                ([Fecha_Inicio] BETWEEN @Fecha_Inicio AND @Fecha_Fin)
              )
    )
    BEGIN
        RAISERROR('El trabajador ya tiene una incapacidad registrada en ese rango de fechas.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;

        INSERT INTO [dbo].[Incapacidad_TB]
        (
            [Id_Incapacidad],
            [Id_Trabajador],
            [Fecha_Inicio],
            [Fecha_Fin],
            [Dias_Incapacidad],
            [Tipo_Incapacidad],
            [Numero_CCSS],
            [Diagnostico],
            [Observaciones],
            [Id_Estado],
            [Fecha_Registro]
        )
        VALUES
        (
            @Id_Incapacidad,
            @Id_Trabajador,
            @Fecha_Inicio,
            @Fecha_Fin,
            @Dias_Incapacidad,
            @Tipo_Incapacidad,
            @Numero_CCSS,
            @Diagnostico,
            @Observaciones,
            1,
            GETDATE()
        );

        SELECT @Id_Incapacidad AS Id_Incapacidad;

    COMMIT TRANSACTION;
END