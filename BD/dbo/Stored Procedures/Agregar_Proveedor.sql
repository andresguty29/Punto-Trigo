
CREATE PROCEDURE Agregar_Proveedor
	@Id_Proveedor UNIQUEIDENTIFIER,
	@Identificacion_Proveedor VARCHAR(20),
	@Nombre_Proveedor VARCHAR(150),
	@Telefono_Proveedor VARCHAR(20),
	@Correo_Proveedor VARCHAR(200)

AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Proveedor_TB]
		(
			[Id_Proveedor],
			[Id_Estado],
			[Identificacion_Proveedor],
			[Nombre_Proveedor],
			[Telefono_Proveedor],
			[Correo_Proveedor]
		)
		VALUES
		(
			@Id_Proveedor,
			1,
			@Identificacion_Proveedor,
			@Nombre_Proveedor,
			@Telefono_Proveedor,
			@Correo_Proveedor
		)

		SELECT @Id_Proveedor

	COMMIT TRANSACTION
END