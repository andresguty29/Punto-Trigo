CREATE PROCEDURE Reintentar_Envio_Tiquete
	@Id_Tiquete UNIQUEIDENTIFIER,
	@Estado     VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM [dbo].[Tiquete_TB] WHERE Id_Tiquete = @Id_Tiquete AND Estado = 'PendienteEnvio')
	BEGIN
		RAISERROR('El tiquete no esta pendiente de envio.', 16, 1)
		RETURN
	END

	BEGIN TRANSACTION

		UPDATE [dbo].[Tiquete_TB]
		SET Estado = @Estado
		WHERE Id_Tiquete = @Id_Tiquete

	COMMIT TRANSACTION
END
