CREATE PROCEDURE Registrar_Movimiento
    @Id_Movimiento      UNIQUEIDENTIFIER,
    @Id_Inventario      UNIQUEIDENTIFIER,
    @Tipo               NVARCHAR(10),
    @Cantidad           DECIMAL(10,2),
    @Motivo             NVARCHAR(200) = NULL,
    @Id_Proveedor       UNIQUEIDENTIFIER = NULL,
    @Fecha_Vencimiento  DATE = NULL,
    @Costo_Unitario     DECIMAL(18,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar stock suficiente para salidas
    IF @Tipo = 'Salida'
    BEGIN
        DECLARE @StockActual DECIMAL(10,2)
        SELECT @StockActual = Stock_Actual FROM [dbo].[Inventario_TB] WHERE Id_Inventario = @Id_Inventario

        IF @StockActual < @Cantidad
        BEGIN
            RAISERROR('Stock insuficiente para realizar la salida.', 16, 1)
            RETURN
        END
    END

    INSERT INTO [dbo].[MovimientoInventario_TB] (Id_Movimiento, Id_Inventario, Tipo, Cantidad, Motivo, Id_Proveedor, Fecha_Vencimiento, Costo_Unitario)
    VALUES (@Id_Movimiento, @Id_Inventario, @Tipo, @Cantidad, @Motivo, @Id_Proveedor, @Fecha_Vencimiento, @Costo_Unitario)

    UPDATE [dbo].[Inventario_TB]
    SET Stock_Actual = CASE
        WHEN @Tipo = 'Entrada' THEN Stock_Actual + @Cantidad
        WHEN @Tipo = 'Salida'  THEN Stock_Actual - @Cantidad
        WHEN @Tipo = 'Ajuste'  THEN @Cantidad
    END
    WHERE Id_Inventario = @Id_Inventario

    SELECT @Id_Movimiento
END
