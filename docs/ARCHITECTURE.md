# Punto Trigo Architecture

## Base Structure

- `apps/frontend`: Angular employee-facing interface.
- `services/api-gateway`: single entry point for the frontend.
- `services/users-service`: user and access domain.
- `services/payroll-service`: payroll and staff payment domain.
- `services/inventory-service`: stock movement and raw materials.
- `services/products-service`: catalog and bakery output.
- `services/suppliers-service`: suppliers and purchasing.
- `services/finance-service`: financial summaries and reconciliation.
- `packages/shared-types`: shared TypeScript contracts.
- `packages/shared-config`: shared ports and service metadata.

## Infra

- MySQL 8.4 for persistent storage.
- Redis 7 for cache, sessions, and future queues.
- Docker Compose for local infra bootstrap.

## Recommended Next Steps

- Add an API gateway reverse proxy layer for auth and rate limiting.
- Introduce JWT/OIDC authentication and role-based permissions.
- Add OpenTelemetry, Prometheus, and centralized logging.
- Define per-service schemas or database ownership boundaries.
- Add CI pipelines for lint, tests, security scanning, and image build.
