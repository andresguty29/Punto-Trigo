import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';

interface ModuleCard {
  name: string;
  key: string;
  tag: string;
  description: string;
  status: string;
  points: string[];
  accent: string;
  metrics: Array<{
    label: string;
    value: string;
    trend: string;
  }>;
  sections: Array<{
    title: string;
    description: string;
    columns: string[];
    rows: string[][];
  }>;
  activity: string[];
}

@Component({
  standalone: true,
  imports: [DatePipe],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  readonly today = new Date();

  selectedModuleKey = 'usuarios';

  readonly modules: ModuleCard[] = [
    {
      name: 'Usuarios',
      key: 'usuarios',
      tag: 'Acceso seguro',
      description: 'Base para roles, permisos, perfiles de colaborador y trazabilidad de accesos.',
      status: 'Online',
      points: ['Roles por area', 'Bitacora de sesion', 'Perfiles por sucursal'],
      accent: '#D32F2F',
      metrics: [
        { label: 'Usuarios activos', value: '48', trend: '+3 esta semana' },
        { label: 'Sesiones abiertas', value: '12', trend: '2 fuera de horario' },
        { label: 'Roles definidos', value: '7', trend: '1 pendiente de aprobar' }
      ],
      sections: [
        {
          title: 'Directorio de colaboradores',
          description: 'Listado base de personal para acceso al sistema.',
          columns: ['Nombre', 'Rol', 'Sucursal', 'Estado'],
          rows: [
            ['Ana Campos', 'Administrador', 'Central', 'Activo'],
            ['Luis Rivas', 'Cajero', 'Centro', 'Activo'],
            ['Marta Solis', 'Produccion', 'Norte', 'Vacaciones']
          ]
        },
        {
          title: 'Permisos por area',
          description: 'Matriz de permisos para controles operativos.',
          columns: ['Area', 'Crear', 'Editar', 'Aprobar'],
          rows: [
            ['Inventario', 'Si', 'Si', 'No'],
            ['Finanzas', 'No', 'No', 'Si'],
            ['Proveedores', 'Si', 'Si', 'Si']
          ]
        }
      ],
      activity: [
        '11:20 - Nuevo usuario para sucursal Norte',
        '10:45 - Cambio de rol a supervisor de caja',
        '09:10 - Restablecimiento de contrasena solicitado'
      ]
    },
    {
      name: 'Planilla',
      key: 'planilla',
      tag: 'Operacion diaria',
      description: 'Turnos, horas trabajadas, incidencias y preparacion de pagos sin friccion.',
      status: 'Revisando corte',
      points: ['Control de jornada', 'Bonos e incidencias', 'Cierre quincenal'],
      accent: '#1976D2',
      metrics: [
        { label: 'Colaboradores en turno', value: '26', trend: 'Turno manana completo' },
        { label: 'Horas extras', value: '18h', trend: '+2h vs ayer' },
        { label: 'Incidencias abiertas', value: '4', trend: '1 requiere aprobacion' }
      ],
      sections: [
        {
          title: 'Control de asistencia',
          description: 'Marcaciones y cumplimiento de horarios.',
          columns: ['Empleado', 'Entrada', 'Salida', 'Estado'],
          rows: [
            ['Carlos Pinto', '06:02', '14:00', 'Puntual'],
            ['Rosa Flores', '06:18', '14:05', 'Tardia'],
            ['Jose Carias', '05:59', '14:02', 'Puntual']
          ]
        },
        {
          title: 'Resumen de pago quincenal',
          description: 'Previsualizacion del cierre de planilla.',
          columns: ['Concepto', 'Monto', 'Estado', 'Corte'],
          rows: [
            ['Salario base', 'L 268,000', 'Calculado', 'Q2 Abril'],
            ['Bonos', 'L 21,500', 'Pendiente', 'Q2 Abril'],
            ['Deducciones', 'L 8,700', 'Calculado', 'Q2 Abril']
          ]
        }
      ],
      activity: [
        '12:05 - Se cargo incidencia por incapacidad',
        '10:32 - Supervisor aprobo 6 horas extra',
        '08:50 - Inicio de corte quincenal'
      ]
    },
    {
      name: 'Inventario',
      key: 'inventario',
      tag: 'Materia prima',
      description: 'Seguimiento de existencias, mermas y alertas para insumos criticos.',
      status: '2 alertas criticas',
      points: ['Alertas de stock', 'Entradas y salidas', 'Mermas y ajustes'],
      accent: '#FFC107',
      metrics: [
        { label: 'Items en catalogo', value: '126', trend: '4 nuevos esta semana' },
        { label: 'Stock critico', value: '2', trend: 'Harina y levadura' },
        { label: 'Mermas del dia', value: '3.6%', trend: '-0.4% vs ayer' }
      ],
      sections: [
        {
          title: 'Stock de insumos',
          description: 'Existencias actuales con semaforo de alerta.',
          columns: ['Insumo', 'Disponible', 'Minimo', 'Estado'],
          rows: [
            ['Harina premium', '120 kg', '150 kg', 'Critico'],
            ['Mantequilla', '84 kg', '60 kg', 'Normal'],
            ['Levadura seca', '12 kg', '20 kg', 'Critico']
          ]
        },
        {
          title: 'Movimientos recientes',
          description: 'Entradas y salidas del dia por lote.',
          columns: ['Hora', 'Movimiento', 'Cantidad', 'Responsable'],
          rows: [
            ['11:45', 'Salida produccion', '35 kg harina', 'Turno A'],
            ['09:30', 'Entrada compra', '50 kg azucar', 'Bodega Central'],
            ['07:10', 'Ajuste merma', '4 kg harina', 'Supervisor']
          ]
        }
      ],
      activity: [
        '11:50 - Alerta critica: harina premium',
        '09:35 - Orden sugerida para levadura',
        '07:12 - Ajuste de merma registrado'
      ]
    },
    {
      name: 'Productos',
      key: 'productos',
      tag: 'Produccion',
      description: 'Catalogo, recetas base y costeo para el dia a dia del horno.',
      status: 'Lotes en ejecucion',
      points: ['Recetas estandar', 'Lotes del dia', 'Costeo por producto'],
      accent: '#0B3D6E',
      metrics: [
        { label: 'Productos activos', value: '34', trend: '2 en temporada' },
        { label: 'Lotes de hoy', value: '17', trend: '63% completado' },
        { label: 'Costo promedio', value: 'L 22.40', trend: '+L 0.80 vs semana pasada' }
      ],
      sections: [
        {
          title: 'Catalogo principal',
          description: 'Portafolio con prioridad de venta diaria.',
          columns: ['Producto', 'Categoria', 'Precio', 'Estado'],
          rows: [
            ['Pan mantequilla', 'Tradicional', 'L 18.00', 'Activo'],
            ['Semita grande', 'Dulce', 'L 35.00', 'Activo'],
            ['Rosca integral', 'Saludable', 'L 42.00', 'Bajo rotacion']
          ]
        },
        {
          title: 'Recetas base',
          description: 'Componentes para control de produccion y costeo.',
          columns: ['Receta', 'Rendimiento', 'Costo lote', 'Version'],
          rows: [
            ['Masa blanca PT-01', '90 unidades', 'L 710', 'v1.4'],
            ['Masa dulce PT-04', '65 unidades', 'L 845', 'v1.2'],
            ['Integral PT-09', '52 unidades', 'L 920', 'v1.1']
          ]
        }
      ],
      activity: [
        '12:00 - Lote PT-01 en horneado final',
        '10:20 - Actualizacion de costo en masa dulce',
        '08:05 - Producto estacional habilitado'
      ]
    },
    {
      name: 'Proveedores',
      key: 'proveedores',
      tag: 'Compras',
      description: 'Control de ordenes, entregas y contactos clave para abastecimiento continuo.',
      status: 'Entrega en camino',
      points: ['Ordenes de compra', 'Historial de entrega', 'Evaluacion proveedor'],
      accent: '#D32F2F',
      metrics: [
        { label: 'Proveedores activos', value: '19', trend: '3 estrategicos' },
        { label: 'Ordenes abiertas', value: '7', trend: '2 con entrega hoy' },
        { label: 'Cumplimiento', value: '94%', trend: '+1.8% mensual' }
      ],
      sections: [
        {
          title: 'Ordenes de compra',
          description: 'Seguimiento de compras por fecha de entrega.',
          columns: ['OC', 'Proveedor', 'Monto', 'Estado'],
          rows: [
            ['OC-2418', 'Molinos del Valle', 'L 54,000', 'En transito'],
            ['OC-2419', 'Lacteos Sierra', 'L 18,600', 'Confirmada'],
            ['OC-2420', 'Empaques Centro', 'L 9,840', 'Pendiente']
          ]
        },
        {
          title: 'Evaluacion de servicio',
          description: 'Puntajes para negociar precio y prioridad.',
          columns: ['Proveedor', 'Puntualidad', 'Calidad', 'Score'],
          rows: [
            ['Molinos del Valle', '95%', '93%', 'A'],
            ['Lacteos Sierra', '92%', '97%', 'A'],
            ['Empaques Centro', '88%', '90%', 'B+']
          ]
        }
      ],
      activity: [
        '11:10 - OC-2418 salio de bodega del proveedor',
        '09:00 - Nueva cotizacion recibida para empaques',
        '08:15 - Recordatorio de factura pendiente'
      ]
    },
    {
      name: 'Financiero',
      key: 'financiero',
      tag: 'Vision global',
      description: 'Resumen de ingresos, egresos y cierres para una operacion ordenada.',
      status: 'Cierre diario en curso',
      points: ['Flujo de caja', 'Cierres diarios', 'Reportes ejecutivos'],
      accent: '#1976D2',
      metrics: [
        { label: 'Ventas del dia', value: 'L 183,420', trend: '+6.2% vs ayer' },
        { label: 'Egresos del dia', value: 'L 74,900', trend: 'Dentro del presupuesto' },
        { label: 'Caja neta', value: 'L 108,520', trend: 'Meta superada +12%' }
      ],
      sections: [
        {
          title: 'Flujo de caja diario',
          description: 'Consolidado operativo para toma de decisiones.',
          columns: ['Concepto', 'Monto', 'Tipo', 'Estado'],
          rows: [
            ['Ventas mostrador', 'L 98,700', 'Ingreso', 'Conciliado'],
            ['Ventas mayoristas', 'L 84,720', 'Ingreso', 'Pendiente'],
            ['Pago proveedores', 'L 54,900', 'Egreso', 'Aprobado']
          ]
        },
        {
          title: 'Panel de indicadores',
          description: 'Indicadores de control semanal para gerencia.',
          columns: ['KPI', 'Meta', 'Actual', 'Semaforo'],
          rows: [
            ['Margen bruto', '42%', '44.6%', 'Verde'],
            ['Gasto operativo', '< 28%', '27.1%', 'Verde'],
            ['Rotacion cartera', '15 dias', '18 dias', 'Amarillo']
          ]
        }
      ],
      activity: [
        '11:58 - Conciliacion parcial completada',
        '10:14 - Ajuste de gasto operativo aprobado',
        '08:00 - Inicio del cierre financiero diario'
      ]
    }
  ];

  get selectedModule(): ModuleCard {
    return this.modules.find((module) => module.key === this.selectedModuleKey) ?? this.modules[0];
  }

  selectModule(key: string): void {
    this.selectedModuleKey = key;
  }
}
