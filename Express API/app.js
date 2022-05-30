import express from 'express';
import cors from 'cors';
import mssqlRouter from './routes/mssql.routes.js';

const app = express();
app.use(cors());

app.listen(4000, () => {
    console.log('Server is running on port 4000');
});

app.use('/pizzas', mssqlRouter)