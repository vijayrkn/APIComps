const { response } = require('express');
const express = require('express')
const app = express()
const port = 5002

app.get('/compute', (req, res) => {
    var n = +req.query.n;
    let a = 0, b = 1, c = 0, result = 0, i = 0;
    if (n == 0)
        result = a;
    else {
        for (i = 2; i <= n; i++) {
            c = a + b;
            a = b;
            b = c;
        }
        result = b;
    }

    res.setHeader('stack', 'node')
    res.json({ Result: result });
})

app.listen(port, () => {
    console.log(`App listening on port ${port}`)
})