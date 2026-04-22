# Análisis de Requerimientos vs Estructura Actual

**Proyecto:** Punto Trigo - Sistema de Gestión para Panadería La Florida  
**Fecha:** Abril 2026  
**Estado:** Estructura Base Implementada | Backend Esquelético | Frontend Inicial

---

## 📋 Resumen Ejecutivo

El proyecto tiene **infraestructura y arquitectura bien definida** pero está en fase temprana:

- ✅ **7 Servicios Backend** creados (usuarios, planilla, inventario, productos, proveedores, finanzas)
- ✅ **Frontend Angular** con datos de ejemplo para 6 módulos principales
- ⚠️ **APIs sin implementar** (endpoints básicos de health check solamente)
- ⚠️ **Base de datos no configurada** (MySQL + Redis listos en Docker)
- 📊 **Simulación visual** completa en la UI con datos mockup

---

## 🎯 Cobertura por Módulo

### 1. **USUARIOS [USR]** - 7 Requerimientos
**Servicio:** `users-service`  
**Estado Backend:** ⚠️ Esquelético (sin implementar)  
**Estado Frontend:** ✅ Datos de simulación disponibles

| Código | Requerimiento | Implementado | Simulado | Notas |
|--------|---------------|--------------|----------|-------|
| USR-001 | Creación de Cuentas | ❌ | ✅ | Vista de directorio con usuarios activos |
| USR-002 | Asignación de Roles | ❌ | ✅ | 7 roles definidos en vista |
| USR-003 | Gestión de Permisos | ❌ | ✅ | Matriz de permisos por área visible |
| USR-004 | Activación/Desactivación | ❌ | ✅ | Estados "Activo" / "Vacaciones" en directorio |
| USR-005 | Auditoría de Sesiones | ❌ | ✅ | 12 sesiones abiertas con timestamp en métricas |
| USR-006 | Autenticación Segura | ❌ | ⚠️ | No hay login implementado |
| USR-007 | Cambio de Contraseña | ❌ | ⚠️ | No hay formulario |

**Próximos Pasos:**
- Implementar endpoints CRUD para usuarios en `users-service`
- Crear pantalla de login/autenticación
- Implementar JWT/OIDC
- Crear gestión de permisos en backend

---

### 2. **PLANILLA [PLA]** - 14 Requerimientos
**Servicio:** `payroll-service`  
**Estado Backend:** ⚠️ Esquelético  
**Estado Frontend:** ✅ Datos de simulación disponibles

| Código | Requerimiento | Implementado | Simulado | Notas |
|--------|---------------|--------------|----------|-------|
| PLA-001 | Registro de Empleados | ❌ | ✅ | 26 colaboradores en turno |
| PLA-002 | Definición de Esquema de Pago | ❌ | ✅ | Cierre quincenal visible |
| PLA-003 | Registro de Asistencia | ❌ | ✅ | Entrada/salida con estado en tabla |
| PLA-004 | Gestión de Adelantos | ❌ | ⚠️ | No hay desglose en simulación |
| PLA-005 | Cálculo de Horas Extra | ❌ | ✅ | 18h extras registradas, +2h vs ayer |
| PLA-006 | Generación de Recibos | ❌ | ✅ | Resumen con salario base, bonos, deducciones |
| PLA-007 | Historial de Pagos | ❌ | ⚠️ | No hay histórico implementado |
| PLA-008 | Vacaciones Anuales | ❌ | ⚠️ | No hay módulo específico |
| PLA-009 | Registro Vacacional | ❌ | ✅ | Estado "Vacaciones" en directorio |
| PLA-010 | Calendario de Ausencias | ❌ | ❌ | No hay componente |
| PLA-011 | Consulta de Saldos | ❌ | ❌ | No hay cálculo |
| PLA-012 | Validación de Disponibilidad | ❌ | ❌ | No hay alertas |
| PLA-013 | Impacto en Nómina | ❌ | ⚠️ | Cálculo manual en ejemplo |
| PLA-014 | Registro Incapacidades | ❌ | ⚠️ | Mencionado en actividad |

**Próximos Pasos:**
- Implementar cálculos de nómina en `payroll-service`
- Crear calendario para vacaciones e incapacidades
- Generar PDFs de recibos de pago
- Implementar audit trail

---

### 3. **INVENTARIO [INV]** - 13 Requerimientos
**Servicio:** `inventory-service`  
**Estado Backend:** ⚠️ Esquelético  
**Estado Frontend:** ✅ Datos de simulación disponibles

| Código | Requerimiento | Implementado | Simulado | Notas |
|--------|---------------|--------------|----------|-------|
| INV-001 | Registro de Entradas | ❌ | ✅ | Entrada de 50 kg azúcar en movimientos |
| INV-002 | Registro de Salidas | ❌ | ✅ | Salida por producción registrada |
| INV-003 | Consulta de Existencias | ❌ | ✅ | Stock actual con semáforo de alerta |
| INV-004 | Ajuste Manual | ❌ | ✅ | Ajuste de merma visible (4 kg) |
| INV-005 | Definición Stock Mínimo | ❌ | ✅ | Columna "Mínimo" en tabla |
| INV-006 | Alertas Bajo Inventario | ❌ | ✅ | 2 alertas críticas, harina y levadura |
| INV-007 | Historial de Movimientos | ❌ | ✅ | Últimos 3 movimientos con timestamp |
| INV-008 | Asignación por Panadero | ❌ | ❌ | No implementado |
| INV-009 | Listas de Producción | ❌ | ❌ | No hay generador |
| INV-010 | Hojas Limpias | ❌ | ❌ | Concepto no implementado |
| INV-011 | Formato PDF | ❌ | ❌ | No hay exportación |
| INV-012 | Checklist Impreso | ❌ | ❌ | No hay componente |
| INV-013 | Registro Fecha/Turno | ❌ | ❌ | No hay captura |

**Próximos Pasos:**
- Implementar CRUD completo en `inventory-service`
- Crear módulo de asignación por trabajador
- Generar PDFs de listas de producción
- Implementar alertas en tiempo real

---

### 4. **PRODUCTOS [PROD]** - 5 Requerimientos
**Servicio:** `products-service`  
**Estado Backend:** ⚠️ Esquelético  
**Estado Frontend:** ✅ Datos de simulación disponibles

| Código | Requerimiento | Implementado | Simulado | Notas |
|--------|---------------|--------------|----------|-------|
| PROD-001 | Registro de Productos | ❌ | ✅ | 34 productos activos con categoría y precio |
| PROD-002 | Consulta de Productos | ❌ | ✅ | Catálogo principal visible |
| PROD-003 | Edición de Productos | ❌ | ⚠️ | No hay formulario |
| PROD-004 | Eliminar/Desactivar | ❌ | ✅ | Estado "Bajo rotación" visible |
| PROD-005 | Clasificación por Categoría | ❌ | ✅ | Categorías: Tradicional, Dulce, Saludable |

**Próximos Pasos:**
- CRUD completo en `products-service`
- Crear gestor de recetas
- Implementar costeo por producto
- Filtros avanzados por categoría

---

### 5. **PROVEEDORES [PROV]** - 7 Requerimientos
**Servicio:** `suppliers-service`  
**Estado Backend:** ⚠️ Esquelético  
**Estado Frontend:** ❌ Sin datos de simulación

| Código | Requerimiento | Implementado | Simulado | Notas |
|--------|---------------|--------------|----------|-------|
| PROV-001 | Registro de Proveedores | ❌ | ❌ | No hay módulo visible |
| PROV-002 | Consulta de Proveedores | ❌ | ❌ | |
| PROV-003 | Edición de Proveedores | ❌ | ❌ | |
| PROV-004 | Desactivación | ❌ | ❌ | |
| PROV-005 | Asociación con Productos | ❌ | ❌ | |
| PROV-006 | Búsqueda de Proveedores | ❌ | ❌ | |
| PROV-007 | Historial de Compras | ❌ | ❌ | |

**Próximos Pasos:**
- Crear módulo de UI para proveedores
- Implementar CRUD en `suppliers-service`
- Agregar datos de simulación
- Crear vistas de historial

---

### 6. **FINANCIERO [FIN]** - 15 Requerimientos
**Servicio:** `finance-service`  
**Estado Backend:** ⚠️ Esquelético  
**Estado Frontend:** ❌ Sin módulo implementado

| Código | Requerimiento | Implementado | Simulado | Notas |
|--------|---------------|--------------|----------|-------|
| FIN-001 | Tiquetes Electrónicos | ❌ | ❌ | No hay POS |
| FIN-002 | Anulación de Tiquetes | ❌ | ❌ | |
| FIN-003 | Consulta de Tiquetes | ❌ | ❌ | |
| FIN-004 | Registro de Facturas | ❌ | ❌ | |
| FIN-005 | Eliminación de Facturas | ❌ | ❌ | |
| FIN-006 | Consulta de Facturas | ❌ | ❌ | |
| FIN-007 | Reportes Financieros | ❌ | ❌ | |
| FIN-008 | Descarga de Reportes | ❌ | ❌ | |
| FIN-009 | Dashboards Financieros | ❌ | ❌ | |
| FIN-010 | Compras Directas | ❌ | ❌ | |
| FIN-011 | Actualización Automática | ❌ | ❌ | |
| FIN-012 | Categorización de Gastos | ❌ | ❌ | |
| FIN-013 | Historial de Egresos | ❌ | ❌ | |
| FIN-014 | Directorio de Proveedores | ❌ | ❌ | |
| FIN-015 | Directorio de Clientes | ❌ | ❌ | |

**Próximos Pasos:**
- Crear módulo de POS/caja
- Implementar financiero con `finance-service`
- Crear generador de reportes
- Integrar con inventario y proveedores

---

## 📊 Estadísticas Generales

| Métrica | Valor |
|---------|-------|
| **Requerimientos Totales** | 61 |
| **Completamente Implementados** | 0 (0%) |
| **Simulados en UI** | 31 (51%) |
| **En Estructura Backend** | 7 servicios listos |
| **Faltantes o No Iniciados** | 30 (49%) |

---

## 🏗️ Estructura Disponible

### Backend (Listos para Implementar)
```
services/
├── api-gateway/          ← Punto de entrada centralizado
├── users-service/        ← Autenticación, roles, permisos
├── payroll-service/      ← Nómina, asistencia, pagos
├── inventory-service/    ← Stock, movimientos, alertas
├── products-service/     ← Catálogo, recetas, costeo
├── suppliers-service/    ← Proveedores, compras
└── finance-service/      ← Ingresos, egresos, reportes
```

### Frontend (Base Definida)
```
apps/frontend/src/app/
├── app.component.ts      ← Dashboard principal (6 módulos)
├── pages/
│   ├── dashboard/        ← Disponible para expandir
│   ├── module-detail/    ← Para vistas detalladas
│   └── welcome/          ← Página de bienvenida
└── core/                 ← Servicios compartidos
```

### Infraestructura
```
✅ Docker Compose (MySQL 8.4, Redis 7)
✅ TypeScript monorepo
✅ Configuración centralizada
❌ Base de datos sin esquemas
❌ APIs sin lógica
```

---

## 🎬 Propuesta de Implementación (Fases)

### **Fase 1: MVP Usuarios (2-3 semanas)**
- Implementar autenticación básica
- CRUD de usuarios y roles
- Bitácora de sesiones

### **Fase 2: Operación Diaria (3-4 semanas)**
- Planilla: asistencia y cálculos
- Inventario: entradas y salidas
- Alertas de stock

### **Fase 3: Financiero (2-3 semanas)**
- Módulo de caja/POS
- Facturas y tiquetes
- Reportes básicos

### **Fase 4: Optimización (2 semanas)**
- Listas de producción
- Calendarios avanzados
- Integración con suministros

---

## 📝 Observaciones

1. **Datos Mockup Realistas:** El proyecto incluye datos de ejemplo muy bien estructurados que simulan realidad operativa
2. **Arquitectura Escalable:** Microservicios permiten crecimiento modular
3. **Frontend Responsivo:** UI está lista para agregar funcionalidad
4. **Base de Datos:** Infraestructura lista pero sin esquemas SQL definidos
5. **Seguridad Pendiente:** No hay JWT, CORS limitado, sin rate limiting

---

## ✅ Recomendación Final

**El proyecto tiene excelente base y estructura.** Es recomendable:

1. ✅ Empezar por **Fase 1 (Usuarios)** - es la base crítica
2. ✅ Implementar **esquemas SQL** antes de desarrollar lógica
3. ✅ Crear **tests unitarios** desde el inicio
4. ✅ Usar los **datos mockup** para UI hasta tener BD real
5. ✅ Establecer **CI/CD** temprano (GitHub Actions)

