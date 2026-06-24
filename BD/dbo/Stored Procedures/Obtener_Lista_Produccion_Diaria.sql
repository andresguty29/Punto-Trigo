CREATE PROCEDURE Obtener_Lista_Produccion_Diaria
    @Id_Trabajador UNIQUEIDENTIFIER = NULL
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
        CAST(GETDATE() AS DATE) AS Fecha_Lista
    FROM dbo.ProductoTrabajador_TB a
    INNER JOIN dbo.Trabajador_TB t ON a.Id_Trabajador = t.Id_Trabajador
    INNER JOIN dbo.Producto_TB p ON a.Id_Producto = p.Id_Producto
    WHERE a.Id_Estado = 1
      AND t.Id_Estado = 1
      AND p.Id_Estado = 1
      AND (@Id_Trabajador IS NULL OR a.Id_Trabajador = @Id_Trabajador)
    ORDER BY t.Nombre_Completo, p.Nombre_Producto;
END
