CREATE PROCEDURE Obtener_Inventarios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        i.[Id_Inventario],
        i.[Nombre],
        i.[Unidad],
        i.[Stock_Actual],
        i.[Stock_Minimo],
        i.[Id_Proveedor],
        p.[Nombre_Proveedor],
        i.[Id_Estado]
    FROM [dbo].[Inventario_TB] i
    LEFT JOIN [dbo].[Proveedor_TB] p ON i.[Id_Proveedor] = p.[Id_Proveedor]
END
