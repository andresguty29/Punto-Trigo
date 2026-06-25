CREATE PROCEDURE Obtener_Materiales_Asignacion
    @Id_Asignacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        am.[Id_AsignacionMaterial],
        am.[Id_Asignacion],
        am.[Id_Inventario],
        i.[Nombre]  AS Nombre_Inventario,
        i.[Unidad],
        am.[Cantidad]
    FROM dbo.AsignacionMaterial_TB am
    INNER JOIN dbo.Inventario_TB i ON am.[Id_Inventario] = i.[Id_Inventario]
    WHERE am.[Id_Asignacion] = @Id_Asignacion
    ORDER BY i.[Nombre];
END
