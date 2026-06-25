CREATE PROCEDURE Editar_Asignacion_Produccion
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
          AND Id_Asignacion <> @Id_Asignacion
    )
    BEGIN
        RAISERROR('Ya existe una asignacion con este trabajador y producto.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION

        UPDATE dbo.ProductoTrabajador_TB
        SET
            Id_Trabajador = @Id_Trabajador,
            Id_Producto = @Id_Producto,
            Cantidad_Diaria = @Cantidad_Diaria
        WHERE Id_Asignacion = @Id_Asignacion;

        SELECT @Id_Asignacion;

    COMMIT TRANSACTION
END
