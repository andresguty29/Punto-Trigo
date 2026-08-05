CREATE PROCEDURE Registrar_Acceso
	@Id_Registro    UNIQUEIDENTIFIER,
	@Id_Usuario     UNIQUEIDENTIFIER = NULL,
	@Nombre_Usuario VARCHAR(100),
	@Exitoso        BIT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		INSERT INTO [dbo].[RegistroAcceso_TB]
		(
			[Id_Registro],
			[Id_Usuario],
			[Nombre_Usuario],
			[Exitoso]
		)
		VALUES
		(
			@Id_Registro,
			@Id_Usuario,
			@Nombre_Usuario,
			@Exitoso
		)

	COMMIT TRANSACTION
END
