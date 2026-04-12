import cors from 'cors';
import express from 'express';
import helmet from 'helmet';

export function createApp() {
  const app = express();

  app.use(helmet());
  app.use(cors());
  app.use(express.json());

  app.get('/health', (_request, response) => {
    response.json({ status: 'ok', service: 'api-gateway' });
  });

  app.get('/services', (_request, response) => {
    response.json({
      services: [
        { name: 'users-service', port: 4010 },
        { name: 'payroll-service', port: 4020 },
        { name: 'inventory-service', port: 4030 },
        { name: 'products-service', port: 4040 },
        { name: 'suppliers-service', port: 4050 },
        { name: 'finance-service', port: 4060 }
      ]
    });
  });

  return app;
}
