CREATE PROCEDURE Registrar_Bitacora
	@Id_Bitacora    UNIQUEIDENTIFIER,
	@Id_Usuario     UNIQUEIDENTIFIER = NULL,
	@Nombre_Usuario VARCHAR(100),
	@Accion         VARCHAR(100),
	@Detalle        VARCHAR(500) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Bitacora_TB]
	(
		[Id_Bitacora],
		[Id_Usuario],
		[Nombre_Usuario],
		[Accion],
		[Detalle]
	)
	VALUES
	(
		@Id_Bitacora,
		@Id_Usuario,
		@Nombre_Usuario,
		@Accion,
		@Detalle
	)
END
