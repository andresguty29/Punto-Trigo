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
	EgresosCompras AS (
		SELECT
			CAST(Fecha_Compra AS DATE) AS Fecha,
			Monto_Total AS Monto
		FROM [dbo].[Compra_TB]
		WHERE Id_Estado = 1
		  AND CAST(Fecha_Compra AS DATE) BETWEEN @Fecha_Inicio AND @Fecha_Fin
	),
	EgresosPerdidas AS (
		SELECT
			CAST(Fecha_Procesado AS DATE) AS Fecha,
			Costo_Total AS Monto
		FROM [dbo].[PerdidaInventario_TB]
		WHERE CAST(Fecha_Procesado AS DATE) BETWEEN @Fecha_Inicio AND @Fecha_Fin
	),
	Egresos AS (
		SELECT Fecha, SUM(Monto) AS Monto
		FROM (
			SELECT Fecha, Monto FROM EgresosCompras
			UNION ALL
			SELECT Fecha, Monto FROM EgresosPerdidas
		) todo
		GROUP BY Fecha
	)
	SELECT
		COALESCE(i.Fecha, e.Fecha) AS Fecha,
		ISNULL(i.Monto, 0) AS Ingresos,
		ISNULL(e.Monto, 0) AS Egresos
	FROM Ingresos i
	FULL OUTER JOIN Egresos e ON i.Fecha = e.Fecha
	ORDER BY Fecha
END
