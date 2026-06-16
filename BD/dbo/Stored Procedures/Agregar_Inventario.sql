CREATE PROCEDURE Agregar_Inventario
    @Id_Inventario UNIQUEIDENTIFIER,
    @Nombre        NVARCHAR(100),
    @Unidad        NVARCHAR(20),
    @Stock_Minimo  DECIMAL(10,2),
    @Id_Proveedor  UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Inventario_TB] (Id_Inventario, Nombre, Unidad, Stock_Minimo, Id_Proveedor)
    VALUES (@Id_Inventario, @Nombre, @Unidad, @Stock_Minimo, @Id_Proveedor)

    SELECT @Id_Inventario
END
