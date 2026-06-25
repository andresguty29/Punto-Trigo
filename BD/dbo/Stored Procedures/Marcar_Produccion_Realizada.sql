CREATE PROCEDURE Marcar_Produccion_Realizada
    @Id_Asignacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.ProductoTrabajador_TB
    SET Realizado = 1
    WHERE Id_Asignacion = @Id_Asignacion
      AND Id_Estado = 1;

    SELECT @Id_Asignacion;
END
