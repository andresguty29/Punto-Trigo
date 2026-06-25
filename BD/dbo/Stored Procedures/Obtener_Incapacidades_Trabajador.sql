
CREATE PROCEDURE [dbo].[Obtener_Incapacidades_Trabajador]
    @Id_Trabajador  UNIQUEIDENTIFIER,
    @Anio           INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        i.[Id_Incapacidad],
        i.[Id_Trabajador],
        t.[Nombre_Completo],
        t.[Cedula],
        i.[Fecha_Inicio],
        i.[Fecha_Fin],
        i.[Dias_Incapacidad],
        i.[Tipo_Incapacidad],
        i.[Numero_CCSS],
        i.[Diagnostico],
        i.[Observaciones],
        i.[Id_Estado],
        i.[Fecha_Registro]
    FROM [dbo].[Incapacidad_TB] i
    INNER JOIN [dbo].[Trabajador_TB] t ON t.[Id_Trabajador] = i.[Id_Trabajador]
    WHERE i.[Id_Trabajador] = @Id_Trabajador
      AND (@Anio IS NULL OR YEAR(i.[Fecha_Inicio]) = @Anio)
    ORDER BY i.[Fecha_Inicio] DESC;
END