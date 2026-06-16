CREATE PROCEDURE Obtener_Producto
    @Id_Producto UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.[Id_Producto],
        p.[Id_Estado],
        p.[Id_Proveedor],
        pv.[Nombre_Proveedor],
        p.[Nombre_Producto],
        p.[Precio_Venta],
        p.[Stock_Actual]
    FROM [dbo].[Producto_TB] p
    LEFT JOIN [dbo].[Proveedor_TB] pv ON p.[Id_Proveedor] = pv.[Id_Proveedor]
    WHERE p.[Id_Producto] = @Id_Producto
END
