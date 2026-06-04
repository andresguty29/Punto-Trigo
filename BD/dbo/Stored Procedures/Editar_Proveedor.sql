
CREATE PROCEDURE Editar_Proveedor
	@Id_Proveedor UNIQUEIDENTIFIER,
	@Nombre_Proveedor VARCHAR(MAX),
	@Telefono_Proveedor VARCHAR(MAX),
	@Correo_Proveedor VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		UPDATE dbo.Proveedor_TB
		SET
			Nombre_Proveedor = @Nombre_Proveedor,
			Telefono_Proveedor = @Telefono_Proveedor,
			Correo_Proveedor = @Correo_Proveedor
		WHERE Id_Proveedor = @Id_Proveedor

	COMMIT TRANSACTION
END