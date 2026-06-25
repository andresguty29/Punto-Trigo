
CREATE PROCEDURE [dbo].[Agregar_Vacaciones]
    @Id_Vacaciones      UNIQUEIDENTIFIER,
    @Id_Trabajador      UNIQUEIDENTIFIER,
    @Fecha_Inicio       DATE,
    @Fecha_Fin          DATE,
    @Dias_Solicitados   INT,
    @Observaciones      VARCHAR(500) = NULL,
    @Anio               INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar que el trabajador existe y está activo
    IF NOT EXISTS (
        SELECT 1 FROM [dbo].[Trabajador_TB] 
        WHERE [Id_Trabajador] = @Id_Trabajador AND [Id_Estado] = 1
    )
    BEGIN
        RAISERROR('El trabajador no existe o está inactivo.', 16, 1);
        RETURN;
    END

    -- Verificar que no existe traslape con otro periodo de vacaciones del mismo trabajador
    IF EXISTS (
        SELECT 1 FROM [dbo].[Vacaciones_TB]
        WHERE [Id_Trabajador] = @Id_Trabajador
          AND [Id_Estado] = 1
          AND (
                (@Fecha_Inicio BETWEEN [Fecha_Inicio] AND [Fecha_Fin]) OR
                (@Fecha_Fin   BETWEEN [Fecha_Inicio] AND [Fecha_Fin]) OR
                ([Fecha_Inicio] BETWEEN @Fecha_Inicio AND @Fecha_Fin)
              )
    )
    BEGIN
        RAISERROR('El trabajador ya tiene vacaciones registradas en ese rango de fechas.', 16, 1);
        RETURN;
    END

    -- Verificar saldo disponible
    DECLARE @DiasDisponibles DECIMAL(6,2) = 0;

    SELECT @DiasDisponibles = ISNULL([Dias_Pendientes], 0)
    FROM [dbo].[SaldoVacaciones_TB]
    WHERE [Id_Trabajador] = @Id_Trabajador AND [Anio] = @Anio;

    IF @DiasDisponibles < @Dias_Solicitados
    BEGIN
        RAISERROR('El trabajador no tiene suficientes días de vacaciones disponibles para este periodo.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;

        -- Insertar el periodo de vacaciones
        INSERT INTO [dbo].[Vacaciones_TB]
        (
            [Id_Vacaciones],
            [Id_Trabajador],
            [Fecha_Inicio],
            [Fecha_Fin],
            [Dias_Solicitados],
            [Observaciones],
            [Id_Estado],
            [Fecha_Registro]
        )
        VALUES
        (
            @Id_Vacaciones,
            @Id_Trabajador,
            @Fecha_Inicio,
            @Fecha_Fin,
            @Dias_Solicitados,
            @Observaciones,
            1,
            GETDATE()
        );

        -- Actualizar saldo: incrementar días gozados
        UPDATE [dbo].[SaldoVacaciones_TB]
        SET [Dias_Gozados] = [Dias_Gozados] + @Dias_Solicitados,
            [Fecha_Actualizacion] = GETDATE()
        WHERE [Id_Trabajador] = @Id_Trabajador AND [Anio] = @Anio;

        SELECT @Id_Vacaciones AS Id_Vacaciones;

    COMMIT TRANSACTION;
END