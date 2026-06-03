CREATE PROCEDURE Agregar_Trabajador
	@Id_Usuario UNIQUEIDENTIFIER,
	@Cedula VARCHAR (MAX),
	@Nombre_Completo   VARCHAR (MAX),
    @Id_Puesto    INT,
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Trabajador_TB]
		(
			[Id_Usuario]
			[Cedula],
            [Nombre_Completo],
            [Id_Puesto]
            [Id_Estado]
		)
		VALUES
		(
			@Id_Usuario
			@Cedula,
            @Nombre_Completo,
            @Id_Puesto,
			1
		)

		SELECT @Id_Usuario

	COMMIT TRANSACTION
END