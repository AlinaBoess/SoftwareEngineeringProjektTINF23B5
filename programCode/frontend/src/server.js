const https = require('https');
const fs = require('fs');
const path = require('path');
const next = require('next');

const dev = process.env.NODE_ENV !== 'production';
const app = next({ dev });
const handle = app.getRequestHandler();

const httpsOptions = {
    key: fs.readFileSync(path.join(__dirname, 'localhost-key.pem')),
    cert: fs.readFileSync(path.join(__dirname, 'localhost.pem')),
};

app.prepare().then(() => {
    https.createServer(httpsOptions, (req, res) => {
        // Log requests (optional)
        console.log('Request for:', req.url);

        // Use Next.js request handler to serve everything (pages, assets)
        handle(req, res);
    }).listen(3000, (err) => {
        if (err) throw err;
        console.log('> Ready on https://localhost:3000');
    });
});
