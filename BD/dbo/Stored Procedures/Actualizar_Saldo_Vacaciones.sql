
CREATE PROCEDURE [dbo].[Actualizar_Saldo_Vacaciones]
    @Id_Trabajador      UNIQUEIDENTIFIER,
    @Dias_Acumulados    DECIMAL(6,2),
    @Anio               INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar que el trabajador existe
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Trabajador_TB] WHERE [Id_Trabajador] = @Id_Trabajador)
    BEGIN
        RAISERROR('El trabajador no existe.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;

        -- Si ya existe el saldo para ese año, actualizar; si no, insertar
        IF EXISTS (
            SELECT 1 FROM [dbo].[SaldoVacaciones_TB]
            WHERE [Id_Trabajador] = @Id_Trabajador AND [Anio] = @Anio
        )
        BEGIN
            UPDATE [dbo].[SaldoVacaciones_TB]
            SET [Dias_Acumulados] = @Dias_Acumulados,
                [Fecha_Actualizacion] = GETDATE()
            WHERE [Id_Trabajador] = @Id_Trabajador AND [Anio] = @Anio;
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[SaldoVacaciones_TB]
            ([Id_Saldo], [Id_Trabajador], [Dias_Acumulados], [Dias_Gozados], [Anio], [Fecha_Actualizacion])
            VALUES
            (NEWID(), @Id_Trabajador, @Dias_Acumulados, 0, @Anio, GETDATE());
        END

        SELECT @Id_Trabajador AS Id_Trabajador;

    COMMIT TRANSACTION;
END