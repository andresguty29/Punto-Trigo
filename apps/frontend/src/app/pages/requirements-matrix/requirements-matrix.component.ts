import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

interface Requirement {
  code: string;
  title: string;
  status: 'implemented' | 'simulated' | 'planned';
}

interface Module {
  name: string;
  key: string;
  icon: string;
  color: string;
  description: string;
  requirements: Requirement[];
}

@Component({
  selector: 'app-requirements-matrix',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="requirements-container">
      <header class="requirements-header">
        <h1>🎯 Matriz de Requerimientos - Punto Trigo</h1>
        <p>Sistema de Gestión Interno para Panadería La Florida</p>
        <div class="stats-grid">
          <div class="stat">
            <span class="stat-number">61</span>
            <span class="stat-label">Requerimientos Totales</span>
          </div>
          <div class="stat">
            <span class="stat-number">7</span>
            <span class="stat-label">Módulos</span>
          </div>
          <div class="stat">
            <span class="stat-number">22</span>
            <span class="stat-label">Simulados (36%)</span>
          </div>
          <div class="stat">
            <span class="stat-number">39</span>
            <span class="stat-label">Planeados (64%)</span>
          </div>
        </div>
      </header>

      <div class="modules-container">
        <div *ngFor="let module of modules" 
             [class]="'module-section ' + module.key"
             (click)="selectedModule = selectedModule === module.key ? null : module.key">
          
          <div class="module-title" [style.backgroundColor]="module.color">
            <span class="module-icon">{{ module.icon }}</span>
            <span class="module-name">{{ module.name }}</span>
            <span class="req-count">({{ module.requirements.length }})</span>
          </div>

          <div class="module-description">
            {{ module.description }}
          </div>

          <div class="requirements-content" *ngIf="selectedModule === module.key">
            <table class="requirements-table">
              <thead>
                <tr>
                  <th>Código</th>
                  <th>Requerimiento</th>
                  <th class="status-col">Estado</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let req of module.requirements" [class]="'status-' + req.status">
                  <td class="code">{{ req.code }}</td>
                  <td>{{ req.title }}</td>
                  <td class="status-badge">
                    <span [class]="'badge badge-' + req.status">
                      {{ getStatusLabel(req.status) }}
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>

            <div class="module-stats">
              <div class="stat-item">
                <span class="stat-label">Implementados:</span>
                <span class="stat-value">{{ countByStatus(module.requirements, 'implemented') }}</span>
              </div>
              <div class="stat-item">
                <span class="stat-label">Simulados:</span>
                <span class="stat-value">{{ countByStatus(module.requirements, 'simulated') }}</span>
              </div>
              <div class="stat-item">
                <span class="stat-label">Planeados:</span>
                <span class="stat-value">{{ countByStatus(module.requirements, 'planned') }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="summary-section">
        <h2>📊 Resumen por Módulo</h2>
        <table class="summary-table">
          <thead>
            <tr>
              <th>Módulo</th>
              <th>Total</th>
              <th>Impl.</th>
              <th>Sim.</th>
              <th>Plan.</th>
              <th>% Cob.</th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let module of modules">
              <td>{{ module.icon }} {{ module.name }}</td>
              <td class="center">{{ module.requirements.length }}</td>
              <td class="center">{{ countByStatus(module.requirements, 'implemented') }}</td>
              <td class="center">{{ countByStatus(module.requirements, 'simulated') }}</td>
              <td class="center">{{ countByStatus(module.requirements, 'planned') }}</td>
              <td class="center">
                <strong>{{ getModuleCoverage(module) }}%</strong>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <div class="recommendations">
        <h3>✅ Recomendaciones de Implementación</h3>
        <ol>
          <li><strong>Fase 1 (Usuarios):</strong> Implementar autenticación y roles - 2-3 semanas</li>
          <li><strong>Fase 2 (Operación):</strong> Planilla e inventario - 3-4 semanas</li>
          <li><strong>Fase 3 (Financiero):</strong> POS y reportes - 2-3 semanas</li>
          <li><strong>Fase 4 (Optimización):</strong> Producción y integraciones - 2 semanas</li>
        </ol>
      </div>
    </div>
  `,
  styles: [`
    .requirements-container {
      padding: 20px;
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      min-height: 100vh;
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }

    .requirements-header {
      background: white;
      padding: 30px;
      border-radius: 10px;
      margin-bottom: 30px;
      text-align: center;
      box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    }

    .requirements-header h1 {
      margin: 0 0 10px 0;
      color: #0B3D6E;
      font-size: 2em;
    }

    .requirements-header p {
      margin: 0 0 20px 0;
      color: #666;
    }

    .stats-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
      gap: 15px;
    }

    .stat {
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      color: white;
      padding: 15px;
      border-radius: 8px;
      text-align: center;
    }

    .stat-number {
      display: block;
      font-size: 1.8em;
      font-weight: bold;
      margin-bottom: 5px;
    }

    .stat-label {
      font-size: 0.9em;
      opacity: 0.9;
    }

    .modules-container {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
      gap: 20px;
      margin-bottom: 30px;
    }

    .module-section {
      background: white;
      border-radius: 10px;
      overflow: hidden;
      box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
      cursor: pointer;
      transition: transform 0.3s, box-shadow 0.3s;
    }

    .module-section:hover {
      transform: translateY(-5px);
      box-shadow: 0 8px 25px rgba(0, 0, 0, 0.15);
    }

    .module-title {
      padding: 15px;
      color: white;
      font-weight: bold;
      display: flex;
      align-items: center;
      gap: 10px;
      font-size: 1.1em;
    }

    .module-icon {
      font-size: 1.3em;
    }

    .module-name {
      flex: 1;
    }

    .req-count {
      font-size: 0.9em;
      opacity: 0.9;
    }

    .module-description {
      padding: 15px;
      border-bottom: 1px solid #eee;
      color: #666;
      font-size: 0.95em;
    }

    .requirements-content {
      padding: 15px;
      animation: slideDown 0.3s ease-out;
    }

    @keyframes slideDown {
      from {
        opacity: 0;
        max-height: 0;
      }
      to {
        opacity: 1;
        max-height: 1000px;
      }
    }

    .requirements-table {
      width: 100%;
      border-collapse: collapse;
      margin-bottom: 15px;
    }

    .requirements-table thead {
      background: #f5f5f5;
    }

    .requirements-table th {
      padding: 10px;
      text-align: left;
      font-weight: bold;
      color: #333;
      border-bottom: 2px solid #ddd;
    }

    .requirements-table td {
      padding: 10px;
      border-bottom: 1px solid #eee;
    }

    .requirements-table .code {
      font-weight: bold;
      color: #0B3D6E;
    }

    .status-col {
      text-align: center;
      width: 100px;
    }

    .status-badge {
      text-align: center;
    }

    .badge {
      padding: 4px 8px;
      border-radius: 4px;
      font-size: 0.85em;
      font-weight: bold;
    }

    .badge-implemented {
      background: #4CAF50;
      color: white;
    }

    .badge-simulated {
      background: #FFC107;
      color: #333;
    }

    .badge-planned {
      background: #9E9E9E;
      color: white;
    }

    .module-stats {
      display: grid;
      grid-template-columns: repeat(3, 1fr);
      gap: 10px;
      margin-top: 15px;
      padding-top: 15px;
      border-top: 1px solid #eee;
    }

    .stat-item {
      text-align: center;
      padding: 10px;
      background: #f9f9f9;
      border-radius: 4px;
    }

    .stat-item .stat-label {
      display: block;
      font-size: 0.9em;
      color: #666;
      margin-bottom: 5px;
    }

    .stat-item .stat-value {
      display: block;
      font-size: 1.4em;
      font-weight: bold;
      color: #0B3D6E;
    }

    .summary-section {
      background: white;
      padding: 25px;
      border-radius: 10px;
      margin-bottom: 30px;
      box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
    }

    .summary-section h2 {
      color: #0B3D6E;
      margin-top: 0;
    }

    .summary-table {
      width: 100%;
      border-collapse: collapse;
    }

    .summary-table thead {
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      color: white;
    }

    .summary-table th {
      padding: 12px;
      text-align: left;
      font-weight: bold;
    }

    .summary-table td {
      padding: 12px;
      border-bottom: 1px solid #ddd;
    }

    .summary-table .center {
      text-align: center;
    }

    .recommendations {
      background: white;
      padding: 25px;
      border-radius: 10px;
      box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
      margin-bottom: 20px;
    }

    .recommendations h3 {
      color: #0B3D6E;
      margin-top: 0;
    }

    .recommendations ol {
      margin: 0;
      padding-left: 20px;
    }

    .recommendations li {
      margin: 10px 0;
      line-height: 1.6;
    }

    @media (max-width: 768px) {
      .modules-container {
        grid-template-columns: 1fr;
      }

      .stats-grid {
        grid-template-columns: repeat(2, 1fr);
      }

      .requirements-header h1 {
        font-size: 1.5em;
      }
    }
  `]
})
export class RequirementsMatrixComponent {
  selectedModule: string | null = null;

  modules: Module[] = [
    {
      name: 'Usuarios',
      key: 'usuarios',
      icon: '🔐',
      color: '#D32F2F',
      description: 'Gestión de acceso, roles, permisos y auditoría de sesiones',
      requirements: [
        { code: 'USR-001', title: 'Creación de Cuentas', status: 'simulated' },
        { code: 'USR-002', title: 'Asignación de Roles', status: 'simulated' },
        { code: 'USR-003', title: 'Gestión de Permisos', status: 'simulated' },
        { code: 'USR-004', title: 'Activación/Desactivación', status: 'simulated' },
        { code: 'USR-005', title: 'Auditoría de Sesiones', status: 'simulated' },
        { code: 'USR-006', title: 'Autenticación Segura', status: 'planned' },
        { code: 'USR-007', title: 'Cambio de Contraseña', status: 'planned' },
      ]
    },
    {
      name: 'Planilla',
      key: 'planilla',
      icon: '👥',
      color: '#1976D2',
      description: 'Control de jornada, horas extras, incidencias y pagos',
      requirements: [
        { code: 'PLA-001', title: 'Registro de Empleados', status: 'simulated' },
        { code: 'PLA-002', title: 'Definición de Esquema de Pago', status: 'simulated' },
        { code: 'PLA-003', title: 'Registro de Asistencia', status: 'simulated' },
        { code: 'PLA-004', title: 'Gestión de Adelantos', status: 'planned' },
        { code: 'PLA-005', title: 'Cálculo de Horas Extra', status: 'simulated' },
        { code: 'PLA-006', title: 'Generación de Recibos de Pago', status: 'simulated' },
        { code: 'PLA-007', title: 'Historial de Pagos', status: 'planned' },
        { code: 'PLA-008', title: 'Configuración de Vacaciones', status: 'planned' },
        { code: 'PLA-009', title: 'Registro de Periodos Vacacionales', status: 'simulated' },
        { code: 'PLA-010', title: 'Calendario de Ausencias', status: 'planned' },
        { code: 'PLA-011', title: 'Consulta de Saldos de Vacaciones', status: 'planned' },
        { code: 'PLA-012', title: 'Validación de Disponibilidad', status: 'planned' },
        { code: 'PLA-013', title: 'Impacto en Nómina', status: 'planned' },
        { code: 'PLA-014', title: 'Registro de Incapacidades', status: 'planned' },
      ]
    },
    {
      name: 'Inventario',
      key: 'inventario',
      icon: '📦',
      color: '#FFC107',
      description: 'Seguimiento de existencias, mermas y alertas de stock',
      requirements: [
        { code: 'INV-001', title: 'Registro de Entradas', status: 'simulated' },
        { code: 'INV-002', title: 'Registro de Salidas', status: 'simulated' },
        { code: 'INV-003', title: 'Consulta de Existencias', status: 'simulated' },
        { code: 'INV-004', title: 'Ajuste Manual', status: 'simulated' },
        { code: 'INV-005', title: 'Definición de Stock Mínimo', status: 'simulated' },
        { code: 'INV-006', title: 'Alertas de Bajo Inventario', status: 'simulated' },
        { code: 'INV-007', title: 'Historial de Movimientos', status: 'simulated' },
        { code: 'INV-008', title: 'Asignación de Productos por Panadero', status: 'planned' },
        { code: 'INV-009', title: 'Generación de Listas de Producción', status: 'planned' },
        { code: 'INV-010', title: 'Configuración de Hojas de Trabajo', status: 'planned' },
        { code: 'INV-011', title: 'Formato de Impresión Personalizado', status: 'planned' },
        { code: 'INV-012', title: 'Diseño de Hoja de Trabajo (Checklist)', status: 'planned' },
        { code: 'INV-013', title: 'Registro de Fecha y Turno en Impresión', status: 'planned' },
      ]
    },
    {
      name: 'Productos',
      key: 'productos',
      icon: '🍞',
      color: '#0B3D6E',
      description: 'Catálogo de productos, recetas y costeo',
      requirements: [
        { code: 'PROD-001', title: 'Registro de Productos', status: 'simulated' },
        { code: 'PROD-002', title: 'Consulta de Productos', status: 'simulated' },
        { code: 'PROD-003', title: 'Edición de Productos', status: 'planned' },
        { code: 'PROD-004', title: 'Desactivar Productos', status: 'simulated' },
        { code: 'PROD-005', title: 'Clasificación por Categoría', status: 'simulated' },
      ]
    },
    {
      name: 'Proveedores',
      key: 'proveedores',
      icon: '🚚',
      color: '#00796B',
      description: 'Gestión de proveedores, compras e historial',
      requirements: [
        { code: 'PROV-001', title: 'Registro de Proveedores', status: 'planned' },
        { code: 'PROV-002', title: 'Consulta de Proveedores', status: 'planned' },
        { code: 'PROV-003', title: 'Edición de Proveedores', status: 'planned' },
        { code: 'PROV-004', title: 'Desactivación de Proveedores', status: 'planned' },
        { code: 'PROV-005', title: 'Asociación con Productos', status: 'planned' },
        { code: 'PROV-006', title: 'Búsqueda de Proveedores', status: 'planned' },
        { code: 'PROV-007', title: 'Historial de Compras', status: 'planned' },
      ]
    },
    {
      name: 'Financiero',
      key: 'financiero',
      icon: '💰',
      color: '#4CAF50',
      description: 'Tiquetes, facturas, reportes y dashboards financieros',
      requirements: [
        { code: 'FIN-001', title: 'Creación de Tiquetes Electrónicos', status: 'planned' },
        { code: 'FIN-002', title: 'Anulación de Tiquetes', status: 'planned' },
        { code: 'FIN-003', title: 'Consulta de Tiquetes', status: 'planned' },
        { code: 'FIN-004', title: 'Registro de Facturas de Compra', status: 'planned' },
        { code: 'FIN-005', title: 'Eliminación de Facturas', status: 'planned' },
        { code: 'FIN-006', title: 'Consulta de Facturas de Compra', status: 'planned' },
        { code: 'FIN-007', title: 'Reportes Financieros', status: 'planned' },
        { code: 'FIN-008', title: 'Descarga de Reportes', status: 'planned' },
        { code: 'FIN-009', title: 'Dashboards Financieros', status: 'planned' },
        { code: 'FIN-010', title: 'Registro de Compras Directas', status: 'planned' },
        { code: 'FIN-011', title: 'Actualización Automática de Inventario', status: 'planned' },
        { code: 'FIN-012', title: 'Categorización de Gastos', status: 'planned' },
        { code: 'FIN-013', title: 'Historial de Egresos', status: 'planned' },
        { code: 'FIN-014', title: 'Directorio de Proveedores', status: 'planned' },
        { code: 'FIN-015', title: 'Directorio de Clientes', status: 'planned' },
      ]
    },
  ];

  getStatusLabel(status: string): string {
    const labels: { [key: string]: string } = {
      'implemented': '✅ Implementado',
      'simulated': '📊 Simulado',
      'planned': '📋 Planeado'
    };
    return labels[status] || status;
  }

  countByStatus(requirements: Requirement[], status: string): number {
    return requirements.filter(r => r.status === status).length;
  }

  getModuleCoverage(module: Module): number {
    const simulated = this.countByStatus(module.requirements, 'simulated');
    const implemented = this.countByStatus(module.requirements, 'implemented');
    return Math.round(((simulated + implemented) / module.requirements.length) * 100);
  }
}
