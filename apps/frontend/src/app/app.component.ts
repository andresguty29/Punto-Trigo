import { DatePipe, CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

interface SubView { key: string; label: string; icon: string; }
interface ModuleCard {
  name: string; key: string; tag: string; description: string; status: string;
  points: string[]; accent: string; subViews: SubView[];
  metrics: Array<{ label: string; value: string; trend: string }>;
  sections: Array<{ title: string; description: string; columns: string[]; rows: string[][]; }>;
  activity: string[];
}

@Component({
  standalone: true,
  imports: [DatePipe, CommonModule, FormsModule],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  readonly today = new Date();
  selectedModuleKey = 'usuarios';
  activeSubView: Record<string, string> = {
    usuarios: 'directorio', planilla: 'empleados', inventario: 'existencias',
    productos: 'catalogo', proveedores: 'directorio', financiero: 'caja',
  };
  toast: { msg: string; type: 'success' | 'info' } | null = null;
  newUsuario = { nombre: '', rol: 'Cajero', estado: 'Activo' };
  newEmpleado = { nombre: '', puesto: '', salario: '' };
  newProducto = { nombre: '', categoria: 'Tradicional', precio: '', estado: 'Activo' };
  newProveedor = { nombre: '', contacto: '', telefono: '' };
  busquedaProveedor = ''; busquedaProducto = '';
  showNewUsuarioForm = false; showNewEmpleadoForm = false;
  showNewProductoForm = false; showNewProveedorForm = false;

  usuariosRows = [
    ['Ana Campos','Administrador','Central','Activo'],
    ['Luis Rivas','Cajero','Centro','Activo'],
    ['Marta Solís','Producción','Norte','Vacaciones'],
    ['Pedro Gómez','Cajero','Central','Activo'],
    ['Carmen López','Administrador','Sur','Activo'],
  ];
  sesionesRows = [
    ['Ana Campos','Administrador','06:02','14:00','Cerrado'],
    ['Luis Rivas','Cajero','06:10','—','En sesión'],
    ['Pedro Gómez','Cajero','07:00','13:00','Cerrado'],
    ['Carmen López','Administrador','08:00','—','En sesión'],
  ];
  empleadosRows = [
    ['Carlos Pinto','Panadero','01/03/2021','L 18,000','Quincenal','Activo'],
    ['Rosa Flores','Cajera','15/06/2022','L 14,500','Quincenal','Activo'],
    ['Jose Carias','Panadero','20/01/2020','L 19,500','Mensual','Activo'],
    ['Lucía Ramos','Limpieza','10/09/2023','L 10,000','Quincenal','Activo'],
  ];
  asistenciaRows = [
    ['Carlos Pinto','06:02','14:00','Puntual','0','—'],
    ['Rosa Flores','06:18','14:05','Tardía','1','—'],
    ['Jose Carias','05:59','14:02','Puntual','0','2h'],
    ['Lucía Ramos','—','—','Falta','1','—'],
  ];
  valesRows = [
    ['Carlos Pinto','L 2,000','10/04/2026','Descontado Q2'],
    ['Rosa Flores','L 1,500','14/04/2026','Pendiente'],
    ['Jose Carias','L 3,000','01/04/2026','Descontado Q1'],
  ];
  pagosRows = [
    ['Carlos Pinto','L 18,000','L 1,000','L 2,000','L 17,000','Q2 Abril','Pendiente'],
    ['Rosa Flores','L 7,250','L 500','L 1,500','L 6,250','Q2 Abril','Pendiente'],
    ['Jose Carias','L 9,750','L 1,500','L 3,000','L 8,250','Q2 Abril','Pendiente'],
  ];
  vacacionesRows = [
    ['Carlos Pinto','15','5','10','—','—'],
    ['Rosa Flores','10','0','10','01/05/2026','15/05/2026'],
    ['Jose Carias','20','10','10','—','—'],
    ['Lucía Ramos','8','0','8','—','—'],
  ];
  incapacidadesRows = [
    ['Rosa Flores','08/04/2026','12/04/2026','5 días','IHSS','Registrado'],
    ['Jose Carias','01/03/2026','03/03/2026','3 días','Médico privado','Registrado'],
  ];
  insumoRows = [
    ['Harina premium','kg','120','150','Crítico','Molinos del Valle'],
    ['Mantequilla','kg','84','60','Normal','Lácteos Sierra'],
    ['Levadura seca','kg','12','20','Crítico','DistAlimentos'],
    ['Azúcar blanca','kg','210','100','Normal','Molinos del Valle'],
    ['Sal','kg','45','30','Normal','DistAlimentos'],
  ];
  movimientosRows = [
    ['11:45','Salida','Producción','35 kg harina','Turno A','—'],
    ['09:30','Entrada','Compra','50 kg azúcar','Bodega','OC-2419'],
    ['07:10','Ajuste','Merma','4 kg harina','Supervisor','—'],
    ['06:00','Entrada','Compra','30 kg mantequilla','Bodega','OC-2418'],
  ];
  produccionRows = [
    ['Juan Pérez','Pan mantequilla','200 unidades','Mañana','21/04/2026','☐'],
    ['Juan Pérez','Semita grande','80 unidades','Mañana','21/04/2026','☐'],
    ['María Torres','Rosca integral','60 unidades','Tarde','21/04/2026','☐'],
    ['María Torres','Pan dulce','150 unidades','Tarde','21/04/2026','☐'],
  ];
  productosRows = [
    ['Pan mantequilla','Tradicional','L 18.00','L 8.50','Activo'],
    ['Semita grande','Dulce','L 35.00','L 14.00','Activo'],
    ['Rosca integral','Saludable','L 42.00','L 18.00','Bajo rotación'],
    ['Pan dulce','Dulce','L 12.00','L 5.00','Activo'],
    ['Croissant','Premium','L 28.00','L 12.00','Temporada'],
  ];
  recetasRows = [
    ['Masa blanca PT-01','Pan mantequilla','90 u.','L 710','35 kg harina, 5 kg mantequilla','v1.4'],
    ['Masa dulce PT-04','Semita / Pan dulce','65 u.','L 845','30 kg harina, 8 kg azúcar','v1.2'],
    ['Integral PT-09','Rosca integral','52 u.','L 920','28 kg integral, 4 kg semilla','v1.1'],
  ];
  proveedoresRows = [
    ['Molinos del Valle','Jorge Estrada','9876-5432','jorge@molinos.hn','Harina, Azúcar','Activo'],
    ['Lácteos Sierra','Ana Mora','8765-4321','ana@lacteossierra.hn','Mantequilla, Leche','Activo'],
    ['DistAlimentos','Raúl Soto','7654-3210','rsoto@distalim.hn','Levadura, Sal','Activo'],
    ['Empaques Centro','Luis Funes','6543-2109','lfunes@empaques.hn','Cajas, Bolsas','Activo'],
  ];
  ordenesRows = [
    ['OC-2418','20/04/2026','Molinos del Valle','30 kg mantequilla','L 54,000','En tránsito'],
    ['OC-2419','20/04/2026','Lácteos Sierra','50 kg azúcar','L 18,600','Confirmada'],
    ['OC-2420','21/04/2026','Empaques Centro','Cajas x500','L 9,840','Pendiente'],
    ['OC-2421','18/04/2026','DistAlimentos','40 kg levadura','L 22,000','Entregada'],
  ];
  historialComprasRows = [
    ['Molinos del Valle','15/04/2026','L 54,000','Harina x300 kg','OC-2415','Pagada'],
    ['Lácteos Sierra','10/04/2026','L 18,600','Mantequilla x50 kg','OC-2410','Pagada'],
    ['DistAlimentos','05/04/2026','L 22,000','Levadura x40 kg','OC-2408','Pagada'],
    ['Molinos del Valle','01/04/2026','L 48,000','Harina x260 kg','OC-2402','Pagada'],
  ];
  tiquetesRows = [
    ['TK-1045','21/04/2026','10:32','Luis Rivas','L 185.00','Pan mantequilla x5, Semita x3','Válido'],
    ['TK-1044','21/04/2026','09:18','Pedro Gómez','L 90.00','Pan dulce x10','Válido'],
    ['TK-1043','21/04/2026','08:55','Luis Rivas','L 42.00','Rosca integral x1','Válido'],
    ['TK-1042','20/04/2026','17:10','Rosa Flores','L 320.00','Mayorista - Semita x8','Anulado'],
  ];
  facturasRows = [
    ['FAC-0091','20/04/2026','Molinos del Valle','L 54,000','Harina x300 kg','Materia Prima','Pagada'],
    ['FAC-0090','18/04/2026','Lácteos Sierra','L 18,600','Mantequilla','Materia Prima','Pendiente'],
    ['FAC-0089','15/04/2026','Serv. Limpieza','L 4,200','Productos limpieza','Limpieza','Pagada'],
    ['FAC-0088','10/04/2026','TecMaq S.A.','L 12,500','Mant. horno','Mantenimiento','Pagada'],
  ];
  cajaDiariaRows = [
    ['Ventas mostrador','L 98,700','Ingreso','Conciliado'],
    ['Ventas mayoristas','L 84,720','Ingreso','Pendiente'],
    ['Pago Molinos del Valle','L 54,000','Egreso','Aprobado'],
    ['Pago planilla Q2','L 20,900','Egreso','Aprobado'],
    ['Compra directa limpieza','L 4,200','Egreso','Conciliado'],
  ];
  clientesRows = [
    ['Supermercado La Colonia','9900-1234','contacto@lacolonia.hn','Mayorista','Activo'],
    ['Cafetería El Sol','8800-5678','elsol@cafe.hn','Frecuente','Activo'],
    ['Juan Pérez','7700-9012','—','Regular','Activo'],
  ];
  kpiRows = [
    ['Margen bruto','42 %','44.6 %','Normal','↑ Verde'],
    ['Gasto operativo','< 28 %','27.1 %','Normal','↑ Verde'],
    ['Rotación cartera','15 días','18 días','Alto','↓ Amarillo'],
    ['Costo por unidad','L 22','L 22.40','Normal','→ Verde'],
  ];

  readonly modules: ModuleCard[] = [
    {
      name: 'Usuarios', key: 'usuarios', tag: 'Acceso y seguridad',
      description: 'Gestión de cuentas, roles, permisos y bitácora de accesos al sistema.',
      status: 'Online', points: ['Cajero / Administrador','Permisos por módulo','Auditoría de sesiones'],
      accent: '#C41E1E',
      subViews: [
        { key: 'directorio', label: 'Directorio', icon: 'fa-solid fa-users' },
        { key: 'roles', label: 'Roles y Permisos', icon: 'fa-solid fa-shield-halved' },
        { key: 'sesiones', label: 'Auditoría', icon: 'fa-solid fa-clock-rotate-left' },
      ],
      metrics: [
        { label: 'Usuarios activos', value: '48', trend: '+3 esta semana' },
        { label: 'Sesiones abiertas', value: '12', trend: '2 fuera de horario' },
        { label: 'Roles', value: '2', trend: 'Cajero / Administrador' },
      ],
      sections: [],
      activity: ['11:20 — Nuevo usuario: sucursal Norte','10:45 — Cambio de rol: Luis Rivas → Supervisor','09:10 — Restablecimiento contraseña solicitado'],
    },
    {
      name: 'Planilla', key: 'planilla', tag: 'Recursos humanos',
      description: 'Empleados, asistencia, cálculo de pagos, vacaciones e incapacidades.',
      status: 'Corte Q2 en curso', points: ['Pago mensual / quincenal / horas','Adelantos y descuentos','Vacaciones e incapacidades'],
      accent: '#0B3D6E',
      subViews: [
        { key: 'empleados', label: 'Empleados', icon: 'fa-solid fa-id-card' },
        { key: 'asistencia', label: 'Asistencia', icon: 'fa-solid fa-calendar-check' },
        { key: 'vales', label: 'Adelantos', icon: 'fa-solid fa-money-bill-transfer' },
        { key: 'pagos', label: 'Recibos de Pago', icon: 'fa-solid fa-file-invoice-dollar' },
        { key: 'vacaciones', label: 'Vacaciones', icon: 'fa-solid fa-umbrella-beach' },
        { key: 'incapacidades', label: 'Incapacidades', icon: 'fa-solid fa-briefcase-medical' },
      ],
      metrics: [
        { label: 'Colaboradores activos', value: '26', trend: 'Turno mañana completo' },
        { label: 'Horas extras hoy', value: '18 h', trend: '+2 h vs ayer' },
        { label: 'Pendiente de pago', value: 'L 23,500', trend: 'Corte Q2 Abril' },
      ],
      sections: [],
      activity: ['12:05 — Incapacidad cargada: Rosa Flores','10:32 — Aprobadas 6 h extra: Jose Carias','08:50 — Inicio corte quincenal Q2'],
    },
    {
      name: 'Inventario', key: 'inventario', tag: 'Materia prima',
      description: 'Existencias, movimientos, alertas de stock y listas de producción.',
      status: '2 alertas críticas', points: ['Entradas y salidas','Stock mínimo configurable','Listas por panadero'],
      accent: '#D4920A',
      subViews: [
        { key: 'existencias', label: 'Existencias', icon: 'fa-solid fa-boxes-stacked' },
        { key: 'movimientos', label: 'Movimientos', icon: 'fa-solid fa-arrows-up-down' },
        { key: 'produccion', label: 'Listas de Producción', icon: 'fa-solid fa-list-check' },
      ],
      metrics: [
        { label: 'Insumos en catálogo', value: '126', trend: '4 nuevos esta semana' },
        { label: 'Stock crítico', value: '2', trend: 'Harina y levadura' },
        { label: 'Mermas del día', value: '3.6 %', trend: '−0.4 % vs ayer' },
      ],
      sections: [],
      activity: ['11:50 — Alerta crítica: harina premium','09:35 — Orden sugerida para levadura','07:12 — Ajuste de merma registrado'],
    },
    {
      name: 'Productos', key: 'productos', tag: 'Catálogo',
      description: 'Catálogo de productos, categorías, precios, costo y recetas base.',
      status: 'Lotes en ejecución', points: ['Tradicional, Dulce, Saludable','Costeo por producto','Recetas vinculadas'],
      accent: '#0B3D6E',
      subViews: [
        { key: 'catalogo', label: 'Catálogo', icon: 'fa-solid fa-tag' },
        { key: 'recetas',  label: 'Recetas',  icon: 'fa-solid fa-scroll' },
      ],
      metrics: [
        { label: 'Productos activos', value: '34', trend: '2 en temporada' },
        { label: 'Categorías', value: '5', trend: 'Tradicional, Dulce…' },
        { label: 'Costo promedio', value: 'L 22.40', trend: '+L 0.80 vs sem. pasada' },
      ],
      sections: [],
      activity: ['12:00 — Lote PT-01 en horneado final','10:20 — Actualización de costo: masa dulce','08:05 — Producto estacional habilitado'],
    },
    {
      name: 'Proveedores', key: 'proveedores', tag: 'Compras y abastecimiento',
      description: 'Directorio de proveedores, órdenes de compra e historial de transacciones.',
      status: 'Entrega en camino', points: ['Directorio con datos bancarios','Órdenes de compra','Historial por proveedor'],
      accent: '#0B3D6E',
      subViews: [
        { key: 'directorio', label: 'Directorio', icon: 'fa-solid fa-building' },
        { key: 'ordenes', label: 'Órdenes de Compra', icon: 'fa-solid fa-cart-flatbed' },
        { key: 'historial', label: 'Historial', icon: 'fa-solid fa-chart-bar' },
      ],
      metrics: [
        { label: 'Proveedores activos', value: '19', trend: '3 estratégicos' },
        { label: 'Órdenes abiertas', value: '7', trend: '2 con entrega hoy' },
        { label: 'Cumplimiento', value: '94 %', trend: '+1.8 % mensual' },
      ],
      sections: [],
      activity: ['11:10 — OC-2418 salió de bodega proveedor','09:00 — Nueva cotización: empaques','08:15 — Recordatorio: factura Lácteos Sierra'],
    },
    {
      name: 'Financiero', key: 'financiero', tag: 'Control financiero',
      description: 'Caja, tiquetes, facturas de compra, reportes y directorio de clientes.',
      status: 'Cierre diario en curso', points: ['Emisión de tiquetes','Facturas de compra','Reportes de ingresos/egresos'],
      accent: '#C41E1E',
      subViews: [
        { key: 'caja', label: 'Caja del Día', icon: 'fa-solid fa-cash-register' },
        { key: 'tiquetes', label: 'Tiquetes', icon: 'fa-solid fa-receipt' },
        { key: 'facturas', label: 'Facturas de Compra', icon: 'fa-solid fa-file-invoice' },
        { key: 'reportes', label: 'Reportes', icon: 'fa-solid fa-chart-line' },
        { key: 'clientes', label: 'Clientes', icon: 'fa-solid fa-address-book' },
      ],
      metrics: [
        { label: 'Ventas del día', value: 'L 183,420', trend: '+6.2 % vs ayer' },
        { label: 'Egresos del día', value: 'L 74,900', trend: 'Dentro del presupuesto' },
        { label: 'Caja neta', value: 'L 108,520', trend: 'Meta superada +12 %' },
      ],
      sections: [],
      activity: ['11:58 — Conciliación parcial completada','10:14 — Ajuste de gasto operativo aprobado','08:00 — Inicio del cierre financiero diario'],
    },
  ];

  get selectedModule(): ModuleCard {
    return this.modules.find(m => m.key === this.selectedModuleKey) ?? this.modules[0];
  }
  get currentSubView(): string {
    return this.activeSubView[this.selectedModuleKey] ?? this.selectedModule.subViews[0].key;
  }
  selectModule(key: string) { this.selectedModuleKey = key; }
  selectSubView(key: string) { this.activeSubView[this.selectedModuleKey] = key; }

  filteredProveedores() {
    const q = this.busquedaProveedor.toLowerCase();
    return q ? this.proveedoresRows.filter(r => r.some(c => c.toLowerCase().includes(q))) : this.proveedoresRows;
  }
  filteredProductos() {
    const q = this.busquedaProducto.toLowerCase();
    return q ? this.productosRows.filter(r => r.some(c => c.toLowerCase().includes(q))) : this.productosRows;
  }
  addUsuario() {
    if (!this.newUsuario.nombre.trim()) return;
    this.usuariosRows = [[this.newUsuario.nombre, this.newUsuario.rol, 'Central', this.newUsuario.estado], ...this.usuariosRows];
    this.newUsuario = { nombre: '', rol: 'Cajero', estado: 'Activo' };
    this.showNewUsuarioForm = false; this.showToast('Usuario creado exitosamente');
  }
  addEmpleado() {
    if (!this.newEmpleado.nombre.trim()) return;
    this.empleadosRows = [[this.newEmpleado.nombre, this.newEmpleado.puesto, '—', `L ${this.newEmpleado.salario}`, 'Quincenal', 'Activo'], ...this.empleadosRows];
    this.newEmpleado = { nombre: '', puesto: '', salario: '' };
    this.showNewEmpleadoForm = false; this.showToast('Empleado registrado exitosamente');
  }
  addProducto() {
    if (!this.newProducto.nombre.trim()) return;
    this.productosRows = [[this.newProducto.nombre, this.newProducto.categoria, `L ${this.newProducto.precio}`, '—', this.newProducto.estado], ...this.productosRows];
    this.newProducto = { nombre: '', categoria: 'Tradicional', precio: '', estado: 'Activo' };
    this.showNewProductoForm = false; this.showToast('Producto registrado exitosamente');
  }
  addProveedor() {
    if (!this.newProveedor.nombre.trim()) return;
    this.proveedoresRows = [[this.newProveedor.nombre, this.newProveedor.contacto, this.newProveedor.telefono, '—', '—', 'Activo'], ...this.proveedoresRows];
    this.newProveedor = { nombre: '', contacto: '', telefono: '' };
    this.showNewProveedorForm = false; this.showToast('Proveedor registrado exitosamente');
  }
  toggleUsuarioEstado(i: number) {
    const row = [...this.usuariosRows[i]];
    row[3] = row[3] === 'Activo' ? 'Inactivo' : 'Activo';
    this.usuariosRows = this.usuariosRows.map((r, idx) => idx === i ? row : r);
    this.showToast(`Usuario ${row[3]}`);
  }
  anularTiquete(i: number) {
    const row = [...this.tiquetesRows[i]];
    if (row[6] === 'Anulado') return;
    row[6] = 'Anulado';
    this.tiquetesRows = this.tiquetesRows.map((r, idx) => idx === i ? row : r);
    this.showToast('Tiquete anulado');
  }
  emitirTiquete() {
    const num = 1045 + this.tiquetesRows.length;
    const now = new Date();
    const t = `${now.getHours().toString().padStart(2,'0')}:${now.getMinutes().toString().padStart(2,'0')}`;
    this.tiquetesRows = [[`TK-${num}`,'21/04/2026', t,'Luis Rivas','L 0.00','Nueva venta','Válido'], ...this.tiquetesRows];
    this.showToast(`Tiquete TK-${num} generado`);
  }
  showToast(msg: string, type: 'success' | 'info' = 'success') {
    this.toast = { msg, type };
    setTimeout(() => this.toast = null, 2800);
  }
  rowBadge(cell: string): string {
    const v = cell.toLowerCase();
    if (['crítico','inactivo','anulado','falta','tardía'].some(k => v.includes(k))) return 'badge-danger';
    if (['pendiente','en sesión','en tránsito','vacaciones','amarillo','bajo rot'].some(k => v.includes(k))) return 'badge-warn';
    if (['activo','puntual','normal','válido','pagad','conciliado','verde','entregada','confirmada'].some(k => v.includes(k))) return 'badge-ok';
    return '';
  }
}
