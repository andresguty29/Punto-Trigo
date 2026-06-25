CREATE PROCEDURE Eliminar_Materiales_Por_Asignacion
    @Id_Asignacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.AsignacionMaterial_TB
    WHERE Id_Asignacion = @Id_Asignacion;
END
