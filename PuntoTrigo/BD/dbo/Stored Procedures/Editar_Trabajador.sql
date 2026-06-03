CREATE PROCEDURE Editar_Trabajador
	@Cedula VARCHAR (MAX),
	@Nombre_Completo   VARCHAR (MAX),
    @Id_Puesto    INT,
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		UPDATE dbo.Trabajador_TB
		SET
			Nombre_Completo = @Nombre_Completo,
            Id_Puesto = @Id_Puesto
		WHERE Cedula = @Cedula

	COMMIT TRANSACTION
END