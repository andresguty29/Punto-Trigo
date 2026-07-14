CREATE PROCEDURE Anular_Compra
	@Id_Compra UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM [dbo].[Compra_TB] WHERE Id_Compra = @Id_Compra AND Id_Estado = 2)
	BEGIN
		RAISERROR('La compra ya se encuentra anulada.', 16, 1)
		RETURN
	END

	BEGIN TRANSACTION

		-- Revierte el stock que esta compra habia incrementado
		UPDATE inv
		SET inv.Stock_Actual = CASE
			WHEN inv.Stock_Actual - dc.Cantidad < 0 THEN 0
			ELSE inv.Stock_Actual - dc.Cantidad
		END
		FROM [dbo].[Inventario_TB] inv
		INNER JOIN [dbo].[DetalleCompra_TB] dc ON dc.Id_Inventario = inv.Id_Inventario
		WHERE dc.Id_Compra = @Id_Compra

		UPDATE [dbo].[Compra_TB]
		SET Id_Estado = 2
		WHERE Id_Compra = @Id_Compra

	COMMIT TRANSACTION
END
