CREATE PROCEDURE Agregar_Cliente
	@Id_Cliente        UNIQUEIDENTIFIER,
	@Cedula            VARCHAR(20),
	@Nombre_Completo   VARCHAR(150),
	@Correo_Cliente    VARCHAR(200) = NULL,
	@Telefono_Cliente  VARCHAR(20)  = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Cliente_TB]
		(
			[Id_Cliente],
			[Id_Estado],
			[Cedula],
			[Nombre_Completo],
			[Correo_Cliente],
			[Telefono_Cliente]
		)
		VALUES
		(
			@Id_Cliente,
			1,
			@Cedula,
			@Nombre_Completo,
			@Correo_Cliente,
			@Telefono_Cliente
		)

		SELECT @Id_Cliente

	COMMIT TRANSACTION
END
