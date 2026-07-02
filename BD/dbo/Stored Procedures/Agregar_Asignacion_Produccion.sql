CREATE PROCEDURE Agregar_Asignacion_Produccion
    @Id_Asignacion   UNIQUEIDENTIFIER,
    @Id_Trabajador   UNIQUEIDENTIFIER,
    @Id_Producto     UNIQUEIDENTIFIER,
    @Cantidad_Diaria INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM dbo.ProductoTrabajador_TB
        WHERE Id_Trabajador = @Id_Trabajador
          AND Id_Producto = @Id_Producto
          AND CAST(Fecha_Asignacion AS DATE) = CAST(GETDATE() AS DATE)
    )
    BEGIN
        RAISERROR('El producto ya esta vinculado a este empleado el dia de hoy.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION

        INSERT INTO dbo.ProductoTrabajador_TB
        (
            Id_Asignacion,
            Id_Trabajador,
            Id_Producto,
            Cantidad_Diaria,
            Id_Estado
        )
        VALUES
        (
            @Id_Asignacion,
            @Id_Trabajador,
            @Id_Producto,
            @Cantidad_Diaria,
            1
        )

        SELECT @Id_Asignacion;

    COMMIT TRANSACTION
END
