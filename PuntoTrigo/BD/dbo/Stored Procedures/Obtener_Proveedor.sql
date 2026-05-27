
CREATE PROCEDURE Obtener_Proveedor
	@Id_Proveedor UNIQUEIDENTIFIER
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
	WHERE Id_Proveedor = @Id_Proveedor
END