CREATE PROCEDURE Editar_Usuario
    @Id_Usuario UNIQUEIDENTIFIER,
	@Nombre_Usuario VARCHAR (MAX),
	@Contrasena   VARCHAR (MAX),
    @Id_Trabajdor    INT,
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		UPDATE dbo.Usuario_TB
		SET
			Nombre_Usuario = @Nombre_Usuario,
            Contrasena = @Contrasena,
            Id_Trabajdor = @Id_Trabajdor
		WHERE Id_Usuario = @Id_Usuario

	COMMIT TRANSACTION
END