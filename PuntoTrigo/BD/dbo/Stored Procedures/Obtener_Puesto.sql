CREATE PROCEDURE Obtener_Puesto
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id_Puesto,
		Nombre_Puesto,
	FROM dbo.Puesto_TB
END