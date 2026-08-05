/*
    Script de datos de prueba - Punto Trigo (version idempotente)
    ------------------------------------------------------------
    Seguro de correr varias veces: si un dato ya existe (por cedula,
    nombre de usuario, identificacion, etc.) reutiliza su Id en vez
    de duplicarlo. Los registros transaccionales (compra, produccion,
    asistencia, prestamo, horas extra, tiquete) solo se insertan si
    la factura de ejemplo "F-001-2026" no existe todavia.

    Usuarios para iniciar sesion:
        admin     / Admin123!      (Rol: Admin)
        cajero    / Cajero123!     (Rol: Cajas)
        panadero  / Panadero123!   (Rol: Panadero)
*/

SET NOCOUNT ON;

-- ── Estados base ─────────────────────────────────────────
IF NOT EXISTS (SELECT 1 FROM dbo.Estados_TB WHERE Id = 1)
    INSERT INTO dbo.Estados_TB (Id, Estado) VALUES (1, 'Activo');
IF NOT EXISTS (SELECT 1 FROM dbo.Estados_TB WHERE Id = 2)
    INSERT INTO dbo.Estados_TB (Id, Estado) VALUES (2, 'Inactivo');

DECLARE @Id_Puesto_Admin    UNIQUEIDENTIFIER;
DECLARE @Id_Puesto_Cajero   UNIQUEIDENTIFIER;
DECLARE @Id_Puesto_Panadero UNIQUEIDENTIFIER;

DECLARE @Id_Trabajador_Admin    UNIQUEIDENTIFIER;
DECLARE @Id_Trabajador_Cajero   UNIQUEIDENTIFIER;
DECLARE @Id_Trabajador_Panadero UNIQUEIDENTIFIER;

DECLARE @Id_Proveedor_Harinas UNIQUEIDENTIFIER;
DECLARE @Id_Proveedor_Espiga  UNIQUEIDENTIFIER;

DECLARE @Id_Cliente_Ana  UNIQUEIDENTIFIER;
DECLARE @Id_Cliente_Luis UNIQUEIDENTIFIER;

DECLARE @Id_Producto_Baguette  UNIQUEIDENTIFIER;
DECLARE @Id_Producto_Croissant UNIQUEIDENTIFIER;
DECLARE @Id_Producto_Torta     UNIQUEIDENTIFIER;
DECLARE @Id_Producto_Galletas  UNIQUEIDENTIFIER;

DECLARE @Id_Inv_Harina      UNIQUEIDENTIFIER;
DECLARE @Id_Inv_Azucar      UNIQUEIDENTIFIER;
DECLARE @Id_Inv_Mantequilla UNIQUEIDENTIFIER;
DECLARE @Id_Inv_Levadura    UNIQUEIDENTIFIER;
DECLARE @Id_Inv_Chocolate   UNIQUEIDENTIFIER;

-- ═══════════════════════════════════════════════════════
-- PUESTOS
-- ═══════════════════════════════════════════════════════
IF EXISTS (SELECT 1 FROM dbo.Puesto_TB WHERE Nombre_Puesto = 'Administrador')
    SELECT @Id_Puesto_Admin = Id_Puesto FROM dbo.Puesto_TB WHERE Nombre_Puesto = 'Administrador';
ELSE
BEGIN
    SET @Id_Puesto_Admin = NEWID();
    INSERT INTO dbo.Puesto_TB (Id_Puesto, Nombre_Puesto, Id_Estado) VALUES (@Id_Puesto_Admin, 'Administrador', 1);
END

IF EXISTS (SELECT 1 FROM dbo.Puesto_TB WHERE Nombre_Puesto = 'Cajero')
    SELECT @Id_Puesto_Cajero = Id_Puesto FROM dbo.Puesto_TB WHERE Nombre_Puesto = 'Cajero';
ELSE
BEGIN
    SET @Id_Puesto_Cajero = NEWID();
    INSERT INTO dbo.Puesto_TB (Id_Puesto, Nombre_Puesto, Id_Estado) VALUES (@Id_Puesto_Cajero, 'Cajero', 1);
END

IF EXISTS (SELECT 1 FROM dbo.Puesto_TB WHERE Nombre_Puesto = 'Panadero')
    SELECT @Id_Puesto_Panadero = Id_Puesto FROM dbo.Puesto_TB WHERE Nombre_Puesto = 'Panadero';
ELSE
BEGIN
    SET @Id_Puesto_Panadero = NEWID();
    INSERT INTO dbo.Puesto_TB (Id_Puesto, Nombre_Puesto, Id_Estado) VALUES (@Id_Puesto_Panadero, 'Panadero', 1);
END

-- ═══════════════════════════════════════════════════════
-- TRABAJADORES
-- ═══════════════════════════════════════════════════════
IF EXISTS (SELECT 1 FROM dbo.Trabajador_TB WHERE Cedula = '101110111')
    SELECT @Id_Trabajador_Admin = Id_Trabajador FROM dbo.Trabajador_TB WHERE Cedula = '101110111';
ELSE
BEGIN
    SET @Id_Trabajador_Admin = NEWID();
    INSERT INTO dbo.Trabajador_TB (Id_Trabajador, Cedula, Nombre_Completo, Id_Estado, Id_Puesto, Fecha_Ingreso, Tipo_Pago, Salario_Base, Tarifa_Hora)
    VALUES (@Id_Trabajador_Admin, '101110111', 'Juan Perez Solano', 1, @Id_Puesto_Admin, DATEADD(YEAR, -2, GETDATE()), 'Mensual', 650000.00, NULL);
END

IF EXISTS (SELECT 1 FROM dbo.Trabajador_TB WHERE Cedula = '202220222')
    SELECT @Id_Trabajador_Cajero = Id_Trabajador FROM dbo.Trabajador_TB WHERE Cedula = '202220222';
ELSE
BEGIN
    SET @Id_Trabajador_Cajero = NEWID();
    INSERT INTO dbo.Trabajador_TB (Id_Trabajador, Cedula, Nombre_Completo, Id_Estado, Id_Puesto, Fecha_Ingreso, Tipo_Pago, Salario_Base, Tarifa_Hora)
    VALUES (@Id_Trabajador_Cajero, '202220222', 'Maria Rojas Vindas', 1, @Id_Puesto_Cajero, DATEADD(YEAR, -1, GETDATE()), 'Quincenal', 500000.00, 3500.00);
END

IF EXISTS (SELECT 1 FROM dbo.Trabajador_TB WHERE Cedula = '303330333')
    SELECT @Id_Trabajador_Panadero = Id_Trabajador FROM dbo.Trabajador_TB WHERE Cedula = '303330333';
ELSE
BEGIN
    SET @Id_Trabajador_Panadero = NEWID();
    INSERT INTO dbo.Trabajador_TB (Id_Trabajador, Cedula, Nombre_Completo, Id_Estado, Id_Puesto, Fecha_Ingreso, Tipo_Pago, Salario_Base, Tarifa_Hora)
    VALUES (@Id_Trabajador_Panadero, '303330333', 'Carlos Mora Jimenez', 1, @Id_Puesto_Panadero, DATEADD(YEAR, -3, GETDATE()), 'Mensual', 550000.00, 3200.00);
END

-- ═══════════════════════════════════════════════════════
-- USUARIOS
-- ═══════════════════════════════════════════════════════
IF NOT EXISTS (SELECT 1 FROM dbo.Usuario_TB WHERE Nombre_Usuario = 'admin')
    INSERT INTO dbo.Usuario_TB (Id_Usuario, Nombre_Usuario, Contrasena, Id_Trabajador, Id_Estado, Rol)
    VALUES (NEWID(), 'admin', '3cXtppVm2tI/BpJHIHGiPg==:QrRlHZRr09/0QTUvweGwri4Rxg4DQkfaV4E+r/zlQy8=', @Id_Trabajador_Admin, 1, 'Admin');

IF NOT EXISTS (SELECT 1 FROM dbo.Usuario_TB WHERE Nombre_Usuario = 'cajero')
    INSERT INTO dbo.Usuario_TB (Id_Usuario, Nombre_Usuario, Contrasena, Id_Trabajador, Id_Estado, Rol)
    VALUES (NEWID(), 'cajero', 'KrIhg//QfWO/IURCSPDdVg==:0xjCMh92UzklU0UodfF/oTf3LQ7TeJ5MHD6gl/uOpf0=', @Id_Trabajador_Cajero, 1, 'Cajas');

IF NOT EXISTS (SELECT 1 FROM dbo.Usuario_TB WHERE Nombre_Usuario = 'panadero')
    INSERT INTO dbo.Usuario_TB (Id_Usuario, Nombre_Usuario, Contrasena, Id_Trabajador, Id_Estado, Rol)
    VALUES (NEWID(), 'panadero', '49xgkyzk/0it3w+zl+r4lQ==:R48e3knI/8pRJyUulTt5YyjOnSBaQIGqXgMwoYhRoAw=', @Id_Trabajador_Panadero, 1, 'Panadero');

-- ═══════════════════════════════════════════════════════
-- PROVEEDORES
-- ═══════════════════════════════════════════════════════
IF EXISTS (SELECT 1 FROM dbo.Proveedor_TB WHERE Identificacion_Proveedor = '3101123456')
    SELECT @Id_Proveedor_Harinas = Id_Proveedor FROM dbo.Proveedor_TB WHERE Identificacion_Proveedor = '3101123456';
ELSE
BEGIN
    SET @Id_Proveedor_Harinas = NEWID();
    INSERT INTO dbo.Proveedor_TB (Id_Proveedor, Id_Estado, Identificacion_Proveedor, Nombre_Proveedor, Telefono_Proveedor, Correo_Proveedor)
    VALUES (@Id_Proveedor_Harinas, 1, '3101123456', 'Harinas del Valle S.A.', '22778899', 'contacto@harinasdelvalle.com');
END

IF EXISTS (SELECT 1 FROM dbo.Proveedor_TB WHERE Identificacion_Proveedor = '3101987654')
    SELECT @Id_Proveedor_Espiga = Id_Proveedor FROM dbo.Proveedor_TB WHERE Identificacion_Proveedor = '3101987654';
ELSE
BEGIN
    SET @Id_Proveedor_Espiga = NEWID();
    INSERT INTO dbo.Proveedor_TB (Id_Proveedor, Id_Estado, Identificacion_Proveedor, Nombre_Proveedor, Telefono_Proveedor, Correo_Proveedor)
    VALUES (@Id_Proveedor_Espiga, 1, '3101987654', 'Distribuidora La Espiga', '22334455', 'ventas@laespiga.com');
END

-- ═══════════════════════════════════════════════════════
-- CLIENTES
-- ═══════════════════════════════════════════════════════
IF EXISTS (SELECT 1 FROM dbo.Cliente_TB WHERE Cedula = '111220333')
    SELECT @Id_Cliente_Ana = Id_Cliente FROM dbo.Cliente_TB WHERE Cedula = '111220333';
ELSE
BEGIN
    SET @Id_Cliente_Ana = NEWID();
    INSERT INTO dbo.Cliente_TB (Id_Cliente, Cedula, Nombre_Completo, Id_Estado, Correo_Cliente, Telefono_Cliente)
    VALUES (@Id_Cliente_Ana, '111220333', 'Ana Jimenez Castro', 1, 'ana.jimenez@example.com', '88881234');
END

IF EXISTS (SELECT 1 FROM dbo.Cliente_TB WHERE Cedula = '400400400')
    SELECT @Id_Cliente_Luis = Id_Cliente FROM dbo.Cliente_TB WHERE Cedula = '400400400';
ELSE
BEGIN
    SET @Id_Cliente_Luis = NEWID();
    INSERT INTO dbo.Cliente_TB (Id_Cliente, Cedula, Nombre_Completo, Id_Estado, Correo_Cliente, Telefono_Cliente)
    VALUES (@Id_Cliente_Luis, '400400400', 'Luis Vargas Solis', 1, 'luis.vargas@example.com', '89992345');
END

-- ═══════════════════════════════════════════════════════
-- PRODUCTOS
-- ═══════════════════════════════════════════════════════
IF EXISTS (SELECT 1 FROM dbo.Producto_TB WHERE Nombre_Producto = 'Pan Baguette')
    SELECT @Id_Producto_Baguette = Id_Producto FROM dbo.Producto_TB WHERE Nombre_Producto = 'Pan Baguette';
ELSE
BEGIN
    SET @Id_Producto_Baguette = NEWID();
    INSERT INTO dbo.Producto_TB (Id_Producto, Id_Estado, Id_Proveedor, Nombre_Producto, Precio_Venta, Stock_Actual)
    VALUES (@Id_Producto_Baguette, 1, NULL, 'Pan Baguette', 1200.00, 50);
END

IF EXISTS (SELECT 1 FROM dbo.Producto_TB WHERE Nombre_Producto = 'Croissant')
    SELECT @Id_Producto_Croissant = Id_Producto FROM dbo.Producto_TB WHERE Nombre_Producto = 'Croissant';
ELSE
BEGIN
    SET @Id_Producto_Croissant = NEWID();
    INSERT INTO dbo.Producto_TB (Id_Producto, Id_Estado, Id_Proveedor, Nombre_Producto, Precio_Venta, Stock_Actual)
    VALUES (@Id_Producto_Croissant, 1, NULL, 'Croissant', 900.00, 30);
END

IF EXISTS (SELECT 1 FROM dbo.Producto_TB WHERE Nombre_Producto = 'Torta de Chocolate')
    SELECT @Id_Producto_Torta = Id_Producto FROM dbo.Producto_TB WHERE Nombre_Producto = 'Torta de Chocolate';
ELSE
BEGIN
    SET @Id_Producto_Torta = NEWID();
    INSERT INTO dbo.Producto_TB (Id_Producto, Id_Estado, Id_Proveedor, Nombre_Producto, Precio_Venta, Stock_Actual)
    VALUES (@Id_Producto_Torta, 1, NULL, 'Torta de Chocolate', 8500.00, 5);
END

IF EXISTS (SELECT 1 FROM dbo.Producto_TB WHERE Nombre_Producto = 'Galletas de Avena')
    SELECT @Id_Producto_Galletas = Id_Producto FROM dbo.Producto_TB WHERE Nombre_Producto = 'Galletas de Avena';
ELSE
BEGIN
    SET @Id_Producto_Galletas = NEWID();
    INSERT INTO dbo.Producto_TB (Id_Producto, Id_Estado, Id_Proveedor, Nombre_Producto, Precio_Venta, Stock_Actual)
    VALUES (@Id_Producto_Galletas, 1, NULL, 'Galletas de Avena', 600.00, 100);
END

-- ═══════════════════════════════════════════════════════
-- INVENTARIO
-- ═══════════════════════════════════════════════════════
IF EXISTS (SELECT 1 FROM dbo.Inventario_TB WHERE Nombre = 'Harina de Trigo')
    SELECT @Id_Inv_Harina = Id_Inventario FROM dbo.Inventario_TB WHERE Nombre = 'Harina de Trigo';
ELSE
BEGIN
    SET @Id_Inv_Harina = NEWID();
    INSERT INTO dbo.Inventario_TB (Id_Inventario, Nombre, Unidad, Stock_Actual, Stock_Minimo, Id_Proveedor, Id_Estado)
    VALUES (@Id_Inv_Harina, 'Harina de Trigo', 'kg', 100.00, 20.00, @Id_Proveedor_Harinas, 1);
END

IF EXISTS (SELECT 1 FROM dbo.Inventario_TB WHERE Nombre = 'Azucar')
    SELECT @Id_Inv_Azucar = Id_Inventario FROM dbo.Inventario_TB WHERE Nombre = 'Azucar';
ELSE
BEGIN
    SET @Id_Inv_Azucar = NEWID();
    INSERT INTO dbo.Inventario_TB (Id_Inventario, Nombre, Unidad, Stock_Actual, Stock_Minimo, Id_Proveedor, Id_Estado)
    VALUES (@Id_Inv_Azucar, 'Azucar', 'kg', 50.00, 10.00, @Id_Proveedor_Espiga, 1);
END

IF EXISTS (SELECT 1 FROM dbo.Inventario_TB WHERE Nombre = 'Mantequilla')
    SELECT @Id_Inv_Mantequilla = Id_Inventario FROM dbo.Inventario_TB WHERE Nombre = 'Mantequilla';
ELSE
BEGIN
    SET @Id_Inv_Mantequilla = NEWID();
    INSERT INTO dbo.Inventario_TB (Id_Inventario, Nombre, Unidad, Stock_Actual, Stock_Minimo, Id_Proveedor, Id_Estado)
    VALUES (@Id_Inv_Mantequilla, 'Mantequilla', 'kg', 20.00, 5.00, @Id_Proveedor_Espiga, 1);
END

IF EXISTS (SELECT 1 FROM dbo.Inventario_TB WHERE Nombre = 'Levadura')
    SELECT @Id_Inv_Levadura = Id_Inventario FROM dbo.Inventario_TB WHERE Nombre = 'Levadura';
ELSE
BEGIN
    SET @Id_Inv_Levadura = NEWID();
    INSERT INTO dbo.Inventario_TB (Id_Inventario, Nombre, Unidad, Stock_Actual, Stock_Minimo, Id_Proveedor, Id_Estado)
    VALUES (@Id_Inv_Levadura, 'Levadura', 'kg', 5.00, 2.00, @Id_Proveedor_Harinas, 1);
END

IF EXISTS (SELECT 1 FROM dbo.Inventario_TB WHERE Nombre = 'Chocolate en polvo')
    SELECT @Id_Inv_Chocolate = Id_Inventario FROM dbo.Inventario_TB WHERE Nombre = 'Chocolate en polvo';
ELSE
BEGIN
    SET @Id_Inv_Chocolate = NEWID();
    INSERT INTO dbo.Inventario_TB (Id_Inventario, Nombre, Unidad, Stock_Actual, Stock_Minimo, Id_Proveedor, Id_Estado)
    VALUES (@Id_Inv_Chocolate, 'Chocolate en polvo', 'kg', 8.00, 3.00, @Id_Proveedor_Espiga, 1);
END

-- ═══════════════════════════════════════════════════════
-- DATOS TRANSACCIONALES (solo se insertan una vez)
-- ═══════════════════════════════════════════════════════
IF NOT EXISTS (SELECT 1 FROM dbo.Compra_TB WHERE Numero_Factura = 'F-001-2026')
BEGIN
    DECLARE @Id_Compra UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.Compra_TB (Id_Compra, Id_Proveedor, Numero_Factura, Fecha_Compra, Categoria, Monto_Total, Id_Estado)
    VALUES (@Id_Compra, @Id_Proveedor_Harinas, 'F-001-2026', DATEADD(DAY, -10, GETDATE()), 'Materia Prima', 37000.00, 1);

    INSERT INTO dbo.DetalleCompra_TB (Id_DetalleCompra, Id_Compra, Id_Inventario, Cantidad, Unidad_Ingresada, Costo_Unitario, Fecha_Vencimiento) VALUES
        (NEWID(), @Id_Compra, @Id_Inv_Harina,   50.00, 'kg', 500.00,  DATEADD(MONTH, 6, GETDATE())),
        (NEWID(), @Id_Compra, @Id_Inv_Levadura,  3.00, 'kg', 4000.00, DATEADD(DAY, -2, GETDATE()));

    INSERT INTO dbo.MovimientoInventario_TB (Id_Movimiento, Id_Inventario, Tipo, Cantidad, Fecha, Motivo, Id_Proveedor, Fecha_Vencimiento, Costo_Unitario) VALUES
        (NEWID(), @Id_Inv_Harina, 'Entrada', 50.00, DATEADD(DAY, -10, GETDATE()), 'Compra a proveedor', @Id_Proveedor_Harinas, DATEADD(MONTH, 6, GETDATE()), 500.00);

    -- Este movimiento queda pendiente de procesar como perdida (para probar "Productos Vencidos")
    INSERT INTO dbo.MovimientoInventario_TB (Id_Movimiento, Id_Inventario, Tipo, Cantidad, Fecha, Motivo, Id_Proveedor, Fecha_Vencimiento, Costo_Unitario) VALUES
        (NEWID(), @Id_Inv_Levadura, 'Entrada', 3.00, DATEADD(DAY, -10, GETDATE()), 'Compra a proveedor', @Id_Proveedor_Harinas, DATEADD(DAY, -2, GETDATE()), 4000.00);

    -- Produccion asignada al panadero
    DECLARE @Id_Asignacion UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.ProductoTrabajador_TB (Id_Asignacion, Id_Trabajador, Id_Producto, Cantidad_Diaria, Id_Estado, Realizado)
    VALUES (@Id_Asignacion, @Id_Trabajador_Panadero, @Id_Producto_Baguette, 30, 1, 0);

    INSERT INTO dbo.AsignacionMaterial_TB (Id_AsignacionMaterial, Id_Asignacion, Id_Inventario, Cantidad) VALUES
        (NEWID(), @Id_Asignacion, @Id_Inv_Harina,   5.00),
        (NEWID(), @Id_Asignacion, @Id_Inv_Levadura, 0.50);

    -- Asistencia, prestamos y horas extra del panadero
    INSERT INTO dbo.Asistencia_TB (Id_Asistencia, Id_Trabajador, Fecha, Tipo_Evento, Observaciones) VALUES
        (NEWID(), @Id_Trabajador_Panadero, DATEADD(DAY, -5, CAST(GETDATE() AS DATE)), 'Falta',       'No se presento sin justificacion'),
        (NEWID(), @Id_Trabajador_Panadero, DATEADD(DAY, -3, CAST(GETDATE() AS DATE)), 'Retardo',      'Llego 20 minutos tarde'),
        (NEWID(), @Id_Trabajador_Panadero, DATEADD(DAY, -2, CAST(GETDATE() AS DATE)), 'DiaTrabajado', NULL),
        (NEWID(), @Id_Trabajador_Panadero, DATEADD(DAY, -1, CAST(GETDATE() AS DATE)), 'DiaTrabajado', NULL);

    INSERT INTO dbo.Prestamo_TB (Id_Prestamo, Id_Trabajador, Monto, Fecha, Descripcion, Saldo_Pendiente)
    VALUES (NEWID(), @Id_Trabajador_Panadero, 50000.00, DATEADD(DAY, -7, CAST(GETDATE() AS DATE)), 'Adelanto de salario', 50000.00);

    INSERT INTO dbo.HorasExtra_TB (Id_HorasExtra, Id_Trabajador, Fecha, Horas, Tarifa_Aplicada, Monto_Calculado)
    VALUES (NEWID(), @Id_Trabajador_Panadero, DATEADD(DAY, -1, CAST(GETDATE() AS DATE)), 3.00, 3200.00, 3.00 * 3200.00 * 1.5);

    -- Tiquete de venta simulado
    DECLARE @Id_Tiquete UNIQUEIDENTIFIER = NEWID();
    DECLARE @Monto_Tiquete DECIMAL(18,2) = (2 * 1200.00) + (3 * 900.00);
    DECLARE @Clave_Tiquete VARCHAR(50) = LEFT(REPLACE(CONVERT(VARCHAR(36), NEWID()), '-', '') + REPLACE(CONVERT(VARCHAR(36), NEWID()), '-', ''), 50);

    INSERT INTO dbo.Tiquete_TB (Id_Tiquete, Consecutivo, Clave, Id_Cliente, Id_Trabajador, Fecha_Emision, Estado, Monto_Total)
    VALUES (@Id_Tiquete, '0000000001', @Clave_Tiquete, @Id_Cliente_Ana, @Id_Trabajador_Cajero, DATEADD(HOUR, -3, GETDATE()), 'Emitido', @Monto_Tiquete);

    INSERT INTO dbo.DetalleTiquete_TB (Id_DetalleTiquete, Id_Tiquete, Id_Producto, Cantidad, Precio_Unitario, Subtotal) VALUES
        (NEWID(), @Id_Tiquete, @Id_Producto_Baguette,  2, 1200.00, 2 * 1200.00),
        (NEWID(), @Id_Tiquete, @Id_Producto_Croissant, 3,  900.00, 3 *  900.00);

    UPDATE dbo.Producto_TB SET Stock_Actual = Stock_Actual - 2 WHERE Id_Producto = @Id_Producto_Baguette;
    UPDATE dbo.Producto_TB SET Stock_Actual = Stock_Actual - 3 WHERE Id_Producto = @Id_Producto_Croissant;

    PRINT 'Datos transaccionales de ejemplo insertados (compra, produccion, asistencia, prestamo, horas extra, tiquete).';
END
ELSE
BEGIN
    PRINT 'Los datos transaccionales de ejemplo ya existian (factura F-001-2026 encontrada); no se volvieron a insertar.';
END

PRINT '';
PRINT 'Listo. Usuarios para iniciar sesion:';
PRINT '  admin     / Admin123!';
PRINT '  cajero    / Cajero123!';
PRINT '  panadero  / Panadero123!';
