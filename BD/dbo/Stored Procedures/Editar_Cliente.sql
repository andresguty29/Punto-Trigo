CREATE PROCEDURE Editar_Cliente
	@Id_Cliente        UNIQUEIDENTIFIER,
	@Cedula            VARCHAR(20),
	@Nombre_Completo   VARCHAR(150),
	@Correo_Cliente    VARCHAR(200) = NULL,
	@Telefono_Cliente  VARCHAR(20)  = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		UPDATE dbo.Cliente_TB
		SET
			Cedula           = @Cedula,
			Nombre_Completo  = @Nombre_Completo,
			Correo_Cliente   = @Correo_Cliente,
			Telefono_Cliente = @Telefono_Cliente
		WHERE Id_Cliente = @Id_Cliente

	COMMIT TRANSACTION
END
