CREATE PROCEDURE Agregar_Asignacion_Produccion
    @Id_Asignacion   UNIQUEIDENTIFIER,
    @Id_Trabajador   UNIQUEIDENTIFIER,
    @Id_Producto     UNIQUEIDENTIFIER,
    @Cantidad_Diaria DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM dbo.ProductoTrabajador_TB
        WHERE Id_Trabajador = @Id_Trabajador
          AND Id_Producto = @Id_Producto
    )
    BEGIN
        RAISERROR('El producto ya esta vinculado a este empleado.', 16, 1);
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
