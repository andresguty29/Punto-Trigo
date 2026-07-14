CREATE PROCEDURE Obtener_Productos
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
        p.[Stock_Actual],
        p.[Imagen_Path]
    FROM [dbo].[Producto_TB] p
    LEFT JOIN [dbo].[Proveedor_TB] pv ON p.[Id_Proveedor] = pv.[Id_Proveedor]
END
