CREATE PROCEDURE Agregar_Compra
	@Id_Compra              UNIQUEIDENTIFIER,
	@Id_Proveedor           UNIQUEIDENTIFIER,
	@Numero_Factura         VARCHAR(50),
	@Categoria              VARCHAR(50),
	@Descripcion_Adicional  VARCHAR(200) = NULL,
	@Monto_Total            DECIMAL(18,2)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		INSERT INTO [dbo].[Compra_TB]
		(
			[Id_Compra],
			[Id_Proveedor],
			[Numero_Factura],
			[Categoria],
			[Descripcion_Adicional],
			[Monto_Total],
			[Id_Estado]
		)
		VALUES
		(
			@Id_Compra,
			@Id_Proveedor,
			@Numero_Factura,
			@Categoria,
			@Descripcion_Adicional,
			@Monto_Total,
			1
		)

		SELECT @Id_Compra

	COMMIT TRANSACTION
END
