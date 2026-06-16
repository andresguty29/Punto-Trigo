CREATE PROCEDURE Obtener_Movimientos
    @Id_Inventario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        m.[Id_Movimiento],
        m.[Id_Inventario],
        m.[Tipo],
        m.[Cantidad],
        m.[Fecha],
        m.[Motivo],
        m.[Id_Proveedor],
        p.[Nombre_Proveedor],
        m.[Id_Estado]
    FROM [dbo].[MovimientoInventario_TB] m
    LEFT JOIN [dbo].[Proveedor_TB] p ON m.[Id_Proveedor] = p.[Id_Proveedor]
    WHERE m.[Id_Inventario] = @Id_Inventario
    ORDER BY m.[Fecha] DESC
END
