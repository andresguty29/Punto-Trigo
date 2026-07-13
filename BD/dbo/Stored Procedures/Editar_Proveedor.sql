
CREATE PROCEDURE Editar_Proveedor
	@Id_Proveedor UNIQUEIDENTIFIER,
	@Identificacion_Proveedor VARCHAR(20),
	@Nombre_Proveedor VARCHAR(150),
	@Telefono_Proveedor VARCHAR(20),
	@Correo_Proveedor VARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		UPDATE dbo.Proveedor_TB
		SET
			Identificacion_Proveedor = @Identificacion_Proveedor,
			Nombre_Proveedor = @Nombre_Proveedor,
			Telefono_Proveedor = @Telefono_Proveedor,
			Correo_Proveedor = @Correo_Proveedor
		WHERE Id_Proveedor = @Id_Proveedor

	COMMIT TRANSACTION
END