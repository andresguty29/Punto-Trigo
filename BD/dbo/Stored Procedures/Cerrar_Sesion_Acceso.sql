CREATE PROCEDURE Cerrar_Sesion_Acceso
	@Id_Registro UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

		UPDATE [dbo].[RegistroAcceso_TB]
		SET Fecha_Logout = SYSDATETIME()
		WHERE Id_Registro = @Id_Registro
		  AND Fecha_Logout IS NULL

	COMMIT TRANSACTION
END
