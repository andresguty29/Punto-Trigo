CREATE PROCEDURE Obtener_Asignacion_Produccion
    @Id_Asignacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        a.Id_Asignacion,
        a.Id_Trabajador,
        t.Nombre_Completo AS Nombre_Trabajador,
        a.Id_Producto,
        p.Nombre_Producto,
        a.Cantidad_Diaria,
        a.Fecha_Asignacion,
        a.Id_Estado
    FROM dbo.ProductoTrabajador_TB a
    INNER JOIN dbo.Trabajador_TB t ON a.Id_Trabajador = t.Id_Trabajador
    INNER JOIN dbo.Producto_TB p ON a.Id_Producto = p.Id_Producto
    WHERE a.Id_Asignacion = @Id_Asignacion;
END
