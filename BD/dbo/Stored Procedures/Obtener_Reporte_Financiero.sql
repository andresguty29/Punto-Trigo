CREATE PROCEDURE Obtener_Reporte_Financiero
	@Fecha_Inicio DATE,
	@Fecha_Fin    DATE
AS
BEGIN
	SET NOCOUNT ON;

	;WITH Ingresos AS (
		SELECT
			CAST(Fecha_Emision AS DATE) AS Fecha,
			SUM(Monto_Total) AS Monto
		FROM [dbo].[Tiquete_TB]
		WHERE Estado = 'Emitido'
		  AND CAST(Fecha_Emision AS DATE) BETWEEN @Fecha_Inicio AND @Fecha_Fin
		GROUP BY CAST(Fecha_Emision AS DATE)
	),
	Egresos AS (
		SELECT
			CAST(Fecha_Compra AS DATE) AS Fecha,
			SUM(Monto_Total) AS Monto
		FROM [dbo].[Compra_TB]
		WHERE Id_Estado = 1
		  AND CAST(Fecha_Compra AS DATE) BETWEEN @Fecha_Inicio AND @Fecha_Fin
		GROUP BY CAST(Fecha_Compra AS DATE)
	)
	SELECT
		COALESCE(i.Fecha, e.Fecha) AS Fecha,
		ISNULL(i.Monto, 0) AS Ingresos,
		ISNULL(e.Monto, 0) AS Egresos
	FROM Ingresos i
	FULL OUTER JOIN Egresos e ON i.Fecha = e.Fecha
	ORDER BY Fecha
END
