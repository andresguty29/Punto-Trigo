# Punto-Trigo

Base inicial del sistema para empleados de Panaderia La Florida.

## Stack definido

- Frontend: Angular.
- Backend: Node.js + Express + TypeScript.
- Arquitectura: microservicios por dominio.
- Datos: MySQL 8.4.
- Cache y soporte futuro para colas: Redis.
- Infra local: Docker Compose.

## Estructura

- `apps/frontend`: interfaz inicial con branding y landing operativa.
- `services/api-gateway`: punto de entrada API.
- `services/users-service`: usuarios, roles y permisos.
- `services/payroll-service`: planilla y jornadas.
- `services/inventory-service`: inventario e insumos.
- `services/products-service`: productos, recetas y produccion.
- `services/suppliers-service`: compras y proveedores.
- `services/finance-service`: control financiero.
- `packages/shared-types`: contratos TypeScript compartidos.
- `packages/shared-config`: configuracion compartida.
- `docs/ARCHITECTURE.md`: resumen tecnico del sistema.

## Primeros comandos

```bash
npm install
docker compose up -d
npm run dev:frontend
```

## Siguiente fase sugerida

1. Implementar autenticacion y permisos.
2. Diseñar dashboard interno por modulo.
3. Definir contratos API y persistencia por servicio.
4. Agregar observabilidad y pipeline CI/CD.
