CREATE PROCEDURE Activar_Asignacion_Produccion
    @Id_Asignacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION

        UPDATE dbo.ProductoTrabajador_TB
        SET Id_Estado = 1
        WHERE Id_Asignacion = @Id_Asignacion;

        SELECT @Id_Asignacion;

    COMMIT TRANSACTION
END
