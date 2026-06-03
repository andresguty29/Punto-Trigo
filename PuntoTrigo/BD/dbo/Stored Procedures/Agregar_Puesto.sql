CREATE PROCEDURE Agregar_Pueso 
	@Id_Puesto UNIQUEIDENTIFIER,
	@Nombre_Puesto VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Producto_TB]
		(
			[Id_Puesto],
            [Nombre_Puesto],
            [Id_Estado]
		)
		VALUES
		(
			@Id_Puesto,
            @Nombre_Puesto,
			1
		)

		SELECT @Id_Puesto

	COMMIT TRANSACTION
END