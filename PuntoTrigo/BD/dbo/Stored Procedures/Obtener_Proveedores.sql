
CREATE PROCEDURE Obtener_Proveedores
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id_Proveedor,
		Estado,
		Nombre_Proveedor,
		Telefono_Proveedor,
		Correo_Proveedor
	FROM dbo.Proveedor_TB
	WHERE Estado = 1
END