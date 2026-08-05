CREATE PROCEDURE Configurar_Pago_Trabajador
	@Id_Trabajador UNIQUEIDENTIFIER,
	@Tipo_Pago     VARCHAR(20),
	@Salario_Base  DECIMAL(18,2) = NULL,
	@Tarifa_Hora   DECIMAL(18,2) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM [dbo].[Trabajador_TB] WHERE Id_Trabajador = @Id_Trabajador)
	BEGIN
		RAISERROR('El empleado no existe.', 16, 1)
		RETURN
	END

	IF @Tipo_Pago NOT IN ('Mensual', 'Quincenal')
	BEGIN
		RAISERROR('El tipo de pago indicado no es valido.', 16, 1)
		RETURN
	END

	BEGIN TRANSACTION

		UPDATE [dbo].[Trabajador_TB]
		SET
			Tipo_Pago    = @Tipo_Pago,
			Salario_Base = @Salario_Base,
			Tarifa_Hora  = @Tarifa_Hora
		WHERE Id_Trabajador = @Id_Trabajador

		SELECT @Id_Trabajador

	COMMIT TRANSACTION
END
