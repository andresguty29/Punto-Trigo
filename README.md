# Punto Trigo | Sistema de Gestión Interno

Sistema de gestión integrado para empleados de Panadería La Florida. Centro de control operativo con módulos para usuarios, planilla, inventario, productos, proveedores y finanzas.

## 🎯 Stack Definido

- **Frontend**: Angular 18+ con diseño responsivo
- **Backend**: Node.js + Express + TypeScript
- **Arquitectura**: Microservicios por dominio
- **Base de datos**: MySQL 8.4
- **Cache**: Redis
- **Infraestructura Local**: Docker Compose

## 📁 Estructura del Proyecto

```
Punto-Trigo/
├── apps/
│   └── frontend/              # Interfaz Angular (Centro de Control)
├── services/
│   ├── api-gateway/           # Punto de entrada API
│   ├── users-service/         # Usuarios, roles y permisos
│   ├── payroll-service/       # Planilla y jornadas
│   ├── inventory-service/     # Inventario e insumos
│   ├── products-service/      # Productos, recetas y producción
│   ├── suppliers-service/     # Compras y proveedores
│   └── finance-service/       # Control financiero
├── packages/
│   ├── shared-types/          # Contratos TypeScript compartidos
│   └── shared-config/         # Configuración compartida
└── docs/
    └── ARCHITECTURE.md        # Documentación técnica
```

## 🚀 Primeros Pasos

### Requisitos Previos
- **Node.js** >= 20.0.0
- **npm** >= 10.0.0
- **Docker** y **Docker Compose** (opcional, para base de datos)

### Instalación

1. **Clonar el repositorio**
```bash
git clone <url-del-repositorio>
cd Punto-Trigo
```

2. **Instalar dependencias**
```bash
npm install
```

3. **Iniciar infraestructura (Opcional - si necesitas base de datos)**
```bash
docker compose up -d
```

### ▶️ Ejecutar en Local

#### Opción 1: Solo Frontend (Recomendado para Ver la Interfaz)

```bash
npm run dev:frontend
```

El sistema estará disponible en: **http://localhost:4200/**

Si el puerto 4200 está ocupado, Angular automáticamente usará otro puerto (verás el número en la terminal).

#### Opción 2: Frontend + API Gateway

```bash
# En una terminal
npm run dev:frontend

# En otra terminal
npm run dev:gateway
```

## 📊 Módulos Disponibles

El centro de control incluye 6 módulos operativos:

| Módulo | Descripción | Función |
|--------|-------------|---------|
| **Usuarios** | Gestión de acceso y permisos | Roles, bitácora de sesiones, perfiles por sucursal |
| **Planilla** | Control de recursos humanos | Asistencia, horas extras, incidencias, cierre quincenal |
| **Inventario** | Seguimiento de existencias | Alertas de stock, mermas, entradas y salidas |
| **Productos** | Gestión de catálogo | Recetas, lotes, costeo de producción |
| **Proveedores** | Compras y logística | Órdenes de compra, evaluación de desempeño |
| **Financiero** | Visión global económica | Flujo de caja, cierres diarios, reportes ejecutivos |

## 🎨 Diseño y Características

- **Paleta de Colores Corporativa**: Azul marino, rojo, dorado y blanco
- **Interfaz Amigable**: Espaciado generoso, mucho aire blanco
- **Datos de Ejemplo**: Todos los módulos incluyen información de demostración
- **Responsive**: Optimizado para desktop, tablet y móvil
- **Animaciones Suaves**: Transiciones elegantes entre módulos
- **KPI Cards**: Métricas en tiempo real con tendencias
- **Tablas Interactivas**: Presentación clara de datos operativos
- **Actividad Reciente**: Log de eventos y cambios del sistema

## 📝 Scripts Disponibles

```bash
# Desarrollo
npm run dev:frontend           # Inicia Angular en modo desarrollo
npm run dev:gateway            # Inicia API Gateway en modo desarrollo

# Build
npm run build:frontend         # Compila el frontend para producción
npm run build:gateway          # Compila el gateway para producción
```

## 🔧 Configuración

### Cambiar Puerto del Frontend

Por defecto intenta usar el puerto 4200. Para especificar otro:

```bash
ng serve --port 3000
```

### Variables de Entorno

Create un archivo `.env` en la raíz para configuración personalizada:

```env
NODE_ENV=development
API_URL=http://localhost:3000
DATABASE_URL=mysql://user:password@localhost:3306/puntotrigo
REDIS_URL=redis://localhost:6379
```

## 📖 Estructura de Carpetas

### Frontend
```
apps/frontend/src/
├── app/
│   ├── app.component.ts/html/scss  # Componente principal
│   ├── app.config.ts               # Configuración de la app
│   ├── app.routes.ts               # Rutas de la aplicación
│   ├── core/                       # Servicios core
│   └── pages/                      # Páginas por módulo
├── styles.scss                     # Estilos globales
└── index.html                      # HTML base
```

## 🚀 Siguiente Fase Sugerida

1. ✅ **Implementar autenticación** y control de permisos
2. ✅ **Diseñar dashboards** internos por módulo
3. ✅ **Definir contratos API** y persistencia por servicio
4. ✅ **Agregar observabilidad** y pipeline CI/CD
5. ✅ **Conectar base de datos** MySQL
6. ✅ **Implementar cache** Redis para optimización

## 📞 Soporte

Para preguntas o problemas, revisa la documentación en `docs/ARCHITECTURE.md` o contacta al equipo de desarrollo.

## 📄 Licencia

Proyecto interno de Panadería La Florida © 2026
