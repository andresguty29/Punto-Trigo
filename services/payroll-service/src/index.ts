import dotenv from 'dotenv';

import { createApp } from './app.js';

dotenv.config();

const port = Number(process.env.PORT ?? 4020);
const app = createApp();

app.listen(port, () => {
  console.log(`payroll-service listening on http://localhost:${port}`);
});
