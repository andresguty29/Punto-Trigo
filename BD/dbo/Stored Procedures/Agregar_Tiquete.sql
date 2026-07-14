CREATE PROCEDURE Agregar_Tiquete
	@Id_Tiquete    UNIQUEIDENTIFIER,
	@Id_Cliente    UNIQUEIDENTIFIER = NULL,
	@Id_Trabajador UNIQUEIDENTIFIER = NULL,
	@Estado        VARCHAR(20),
	@Monto_Total   DECIMAL(18,2),
	@Consecutivo   VARCHAR(20) OUTPUT,
	@Clave         VARCHAR(50) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		DECLARE @Siguiente INT
		SELECT @Siguiente = COUNT(*) + 1 FROM [dbo].[Tiquete_TB]

		SET @Consecutivo = RIGHT('0000000000' + CAST(@Siguiente AS VARCHAR(10)), 10)
		-- Clave simulada (formato real de Hacienda no aplica sin certificado digital)
		SET @Clave = LEFT(REPLACE(CONVERT(VARCHAR(36), NEWID()), '-', '') + REPLACE(CONVERT(VARCHAR(36), NEWID()), '-', ''), 50)

		INSERT INTO [dbo].[Tiquete_TB]
		(
			[Id_Tiquete],
			[Consecutivo],
			[Clave],
			[Id_Cliente],
			[Id_Trabajador],
			[Estado],
			[Monto_Total]
		)
		VALUES
		(
			@Id_Tiquete,
			@Consecutivo,
			@Clave,
			@Id_Cliente,
			@Id_Trabajador,
			@Estado,
			@Monto_Total
		)

	COMMIT TRANSACTION
END
