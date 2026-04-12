import { Component } from '@angular/core';

interface ModuleCard {
  name: string;
  key: string;
  tag: string;
  description: string;
  points: string[];
  accent: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  readonly modules: ModuleCard[] = [
    {
      name: 'Usuarios',
      key: 'usuarios',
      tag: 'Acceso seguro',
      description: 'Base para roles, permisos, perfiles de colaborador y trazabilidad de accesos.',
      points: ['Roles por area', 'Bitacora de sesion', 'Perfiles por sucursal'],
      accent: '#d4a574'
    },
    {
      name: 'Planilla',
      key: 'planilla',
      tag: 'Operacion diaria',
      description: 'Turnos, horas trabajadas, incidencias y preparacion de pagos sin friccion.',
      points: ['Control de jornada', 'Bonos e incidencias', 'Cierre quincenal'],
      accent: '#a67c52'
    },
    {
      name: 'Inventario',
      key: 'inventario',
      tag: 'Materia prima',
      description: 'Seguimiento de existencias, mermas y alertas para insumos criticos.',
      points: ['Alertas de stock', 'Entradas y salidas', 'Mermas y ajustes'],
      accent: '#c9956b'
    },
    {
      name: 'Productos',
      key: 'productos',
      tag: 'Produccion',
      description: 'Catalogo, recetas base y costeo para el dia a dia del horno.',
      points: ['Recetas estandar', 'Lotes del dia', 'Costeo por producto'],
      accent: '#d4a574'
    },
    {
      name: 'Proveedores',
      key: 'proveedores',
      tag: 'Compras',
      description: 'Control de ordenes, entregas y contactos clave para abastecimiento continuo.',
      points: ['Ordenes de compra', 'Historial de entrega', 'Evaluacion proveedor'],
      accent: '#a67c52'
    },
    {
      name: 'Financiero',
      key: 'financiero',
      tag: 'Vision global',
      description: 'Resumen de ingresos, egresos y cierres para una operacion ordenada.',
      points: ['Flujo de caja', 'Cierres diarios', 'Reportes ejecutivos'],
      accent: '#c9956b'
    }
  ];
}
