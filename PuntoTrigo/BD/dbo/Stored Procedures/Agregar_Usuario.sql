CREATE PROCEDURE Agregar_Usuario
	@Nombre_Usuario VARCHAR (MAX),
	@Contrasena   VARCHAR (MAX),
    @Id_Trabajdor    INT,
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Uusuario_TB]
		(
			[Nombre_Usuario],
            [Contrasena],
            [Id_Trabajdor]
            [Id_Estado]
		)
		VALUES
		(
			@Nombre_Usuario,
            @Contrasena,
            @Id_Estado,
			1
		)

		SELECT @Nombre_Usuario

	COMMIT TRANSACTION
END