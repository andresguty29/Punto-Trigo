
CREATE PROCEDURE [dbo].[Eliminar_Incapacidad]
    @Id_Incapacidad UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Incapacidad_TB] WHERE [Id_Incapacidad] = @Id_Incapacidad AND [Id_Estado] = 1)
    BEGIN
        RAISERROR('La incapacidad no existe o ya está inactiva.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[Incapacidad_TB]
    SET [Id_Estado] = 2
    WHERE [Id_Incapacidad] = @Id_Incapacidad;

    SELECT @Id_Incapacidad AS Id_Incapacidad;
END