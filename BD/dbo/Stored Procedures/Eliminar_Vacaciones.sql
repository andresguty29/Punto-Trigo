
CREATE PROCEDURE [dbo].[Eliminar_Vacaciones]
    @Id_Vacaciones UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id_Trabajador  UNIQUEIDENTIFIER;
    DECLARE @Dias           INT;
    DECLARE @Anio           INT;

    -- Obtener datos del registro
    SELECT 
        @Id_Trabajador  = [Id_Trabajador],
        @Dias           = [Dias_Solicitados],
        @Anio           = YEAR([Fecha_Inicio])
    FROM [dbo].[Vacaciones_TB]
    WHERE [Id_Vacaciones] = @Id_Vacaciones AND [Id_Estado] = 1;

    IF @Id_Trabajador IS NULL
    BEGIN
        RAISERROR('El periodo de vacaciones no existe o ya está inactivo.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;

        -- Desactivar el periodo
        UPDATE [dbo].[Vacaciones_TB]
        SET [Id_Estado] = 2
        WHERE [Id_Vacaciones] = @Id_Vacaciones;

        -- Revertir los días gozados en el saldo
        UPDATE [dbo].[SaldoVacaciones_TB]
        SET [Dias_Gozados] = CASE 
                                WHEN [Dias_Gozados] - @Dias < 0 THEN 0 
                                ELSE [Dias_Gozados] - @Dias 
                             END,
            [Fecha_Actualizacion] = GETDATE()
        WHERE [Id_Trabajador] = @Id_Trabajador AND [Anio] = @Anio;

        SELECT @Id_Vacaciones AS Id_Vacaciones;

    COMMIT TRANSACTION;
END