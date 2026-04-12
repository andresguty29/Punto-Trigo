import cors from 'cors';
import express from 'express';
import helmet from 'helmet';

export function createApp() {
  const app = express();

  app.use(helmet());
  app.use(cors());
  app.use(express.json());

  app.get('/health', (_request, response) => {
    response.json({ status: 'ok', service: 'products-service' });
  });

  app.get('/summary', (_request, response) => {
    response.json({
      module: 'Productos',
      focus: ['recetas', 'produccion', 'costeo', 'catalogo']
    });
  });

  return app;
}
