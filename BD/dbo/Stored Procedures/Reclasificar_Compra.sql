CREATE PROCEDURE Reclasificar_Compra
	@Id_Compra              UNIQUEIDENTIFIER,
	@Categoria              VARCHAR(50),
	@Descripcion_Adicional  VARCHAR(200) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		UPDATE [dbo].[Compra_TB]
		SET
			Categoria = @Categoria,
			Descripcion_Adicional = @Descripcion_Adicional
		WHERE Id_Compra = @Id_Compra

		SELECT @Id_Compra

	COMMIT TRANSACTION
END
