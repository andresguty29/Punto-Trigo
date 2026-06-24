CREATE PROCEDURE Obtener_Productos_Asignados_Trabajador
    @Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.Id_Producto,
        p.Nombre_Producto
    FROM dbo.ProductoTrabajador_TB a
    INNER JOIN dbo.Producto_TB p ON a.Id_Producto = p.Id_Producto
    WHERE a.Id_Trabajador = @Id_Trabajador
      AND a.Id_Estado = 1
      AND p.Id_Estado = 1
    ORDER BY p.Nombre_Producto;
END
