CREATE PROCEDURE Obtener_Cliente_Por_Cedula
	@Cedula VARCHAR(20)
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
	WHERE Cedula = @Cedula
END
