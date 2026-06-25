CREATE PROCEDURE Agregar_Material_Asignacion
    @Id_AsignacionMaterial UNIQUEIDENTIFIER,
    @Id_Asignacion         UNIQUEIDENTIFIER,
    @Id_Inventario         UNIQUEIDENTIFIER,
    @Cantidad              DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.AsignacionMaterial_TB
    (
        Id_AsignacionMaterial,
        Id_Asignacion,
        Id_Inventario,
        Cantidad
    )
    VALUES
    (
        @Id_AsignacionMaterial,
        @Id_Asignacion,
        @Id_Inventario,
        @Cantidad
    )

    SELECT @Id_AsignacionMaterial;
END
