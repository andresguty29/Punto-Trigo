CREATE PROCEDURE Editar_Inventario
    @Id_Inventario UNIQUEIDENTIFIER,
    @Nombre        NVARCHAR(100),
    @Unidad        NVARCHAR(20),
    @Stock_Minimo  DECIMAL(10,2),
    @Id_Proveedor  UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Inventario_TB]
    SET
        Nombre       = @Nombre,
        Unidad       = @Unidad,
        Stock_Minimo = @Stock_Minimo,
        Id_Proveedor = @Id_Proveedor
    WHERE Id_Inventario = @Id_Inventario

    SELECT @Id_Inventario
END
