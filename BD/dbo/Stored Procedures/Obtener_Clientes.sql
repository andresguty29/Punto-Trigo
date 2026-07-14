CREATE PROCEDURE Obtener_Clientes
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id_Cliente,
		Id_Estado,
		Cedula,
		Nombre_Completo,
		Correo_Cliente,
		Telefono_Cliente
	FROM dbo.Cliente_TB
END
