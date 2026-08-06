--comentario prueba trigger.
CREATE PROCEDURE Activar_Trabajador
	@Id_Trabajador UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		UPDATE dbo.Trabajador_TB
		SET Id_Estado = 1
		WHERE Id_Trabajador = @Id_Trabajador

		SELECT @Id_Trabajador AS Id_Trabajador;

	COMMIT TRANSACTION
END
