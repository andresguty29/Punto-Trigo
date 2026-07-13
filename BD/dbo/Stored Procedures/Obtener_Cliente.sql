CREATE PROCEDURE Obtener_Cliente
	@Id_Cliente UNIQUEIDENTIFIER
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
	WHERE Id_Cliente = @Id_Cliente
END
