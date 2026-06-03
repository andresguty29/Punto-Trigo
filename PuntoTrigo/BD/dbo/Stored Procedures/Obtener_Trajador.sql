CREATE PROCEDURE Obtener_Trabajador
    @Cedula VARCHAR (MAX),
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Nombre_Completo,
		Id_Puesto,
	FROM dbo.Trabajador_TB
    WHERE Cedula = @Cedula
END