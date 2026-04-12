import dotenv from 'dotenv';

import { createApp } from './app.js';

dotenv.config();

const port = Number(process.env.PORT ?? 4060);
const app = createApp();

app.listen(port, () => {
  console.log(`finance-service listening on http://localhost:${port}`);
});
