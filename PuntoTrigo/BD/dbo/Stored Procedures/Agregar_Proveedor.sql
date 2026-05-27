
CREATE PROCEDURE Agregar_Proveedor
	@Id_Proveedor UNIQUEIDENTIFIER,
	@Nombre_Proveedor VARCHAR(MAX),
	@Telefono_Proveedor VARCHAR(MAX),
	@Correo_Proveedor VARCHAR(MAX)
	
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Proveedor_TB]
		(
			[Id_Proveedor],
			[Estado],
			[Nombre_Proveedor],
			[Telefono_Proveedor],
			[Correo_Proveedor]
		)
		VALUES
		(
			@Id_Proveedor,
			1, 
			@Nombre_Proveedor,
			@Telefono_Proveedor,
			@Correo_Proveedor
		)

		SELECT @Id_Proveedor

	COMMIT TRANSACTION
END