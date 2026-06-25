
CREATE PROCEDURE [dbo].[Obtener_Incapacidades_Calendario]
    @Fecha_Inicio   DATE = NULL,
    @Fecha_Fin      DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Fecha_Inicio IS NULL SET @Fecha_Inicio = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
    IF @Fecha_Fin   IS NULL SET @Fecha_Fin   = EOMONTH(GETDATE());

    SELECT
        i.[Id_Incapacidad],
        i.[Id_Trabajador],
        t.[Nombre_Completo],
        t.[Cedula],
        p.[Nombre_Puesto],
        i.[Fecha_Inicio],
        i.[Fecha_Fin],
        i.[Dias_Incapacidad],
        i.[Tipo_Incapacidad],
        i.[Numero_CCSS],
        i.[Id_Estado]
    FROM [dbo].[Incapacidad_TB] i
    INNER JOIN [dbo].[Trabajador_TB] t ON t.[Id_Trabajador] = i.[Id_Trabajador]
    INNER JOIN [dbo].[Puesto_TB] p ON p.[Id_Puesto] = t.[Id_Puesto]
    WHERE i.[Id_Estado] = 1
      AND (
            i.[Fecha_Inicio] <= @Fecha_Fin AND
            i.[Fecha_Fin]   >= @Fecha_Inicio
          )
    ORDER BY i.[Fecha_Inicio] ASC;
END