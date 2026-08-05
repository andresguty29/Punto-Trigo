CREATE PROCEDURE Obtener_Trabajadores
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t.[Id_Trabajador],
        t.[Cedula],
        t.[Nombre_Completo],
        t.[Id_Estado],
        t.[Id_Puesto],
        p.[Nombre_Puesto],
        t.[Fecha_Ingreso],
        t.[Tipo_Pago],
        t.[Salario_Base],
        t.[Tarifa_Hora]
    FROM [dbo].[Trabajador_TB] t
    LEFT JOIN [dbo].[Puesto_TB] p ON t.[Id_Puesto] = p.[Id_Puesto]

END
