const express        = require('express');
const bodyParser     = require('body-parser');
const app = express();
var multer = require('multer');
const port = 8000;
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

app.listen(port, ()=>{
console.log('We are live on ' + port);
});

var upload = multer({dest:'./upload/'});

app.post('/post', upload.single('file'), function(req, res) {
 console.log(req.file);
res.send("file saved on server");
});

///////////////////////////////////////

const express = require('express');
const path = require('path');
const bodyParser = require('body-parser');
const fs = require('fs');
const app = express();
const multer = require('multer');
const port = 8000;
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

app.listen(port, ()=>{
console.log('We are live on ' + port);
});


// SET STORAGE
var storage = multer.diskStorage({
  destination: function (req, file, cb) {
    cb(null, './upload/')
  },
  filename: function (req, file, cb) {
    //cb(null, file.fieldname + '-' + Date.now() + //fieldname + date
    cb(null, file.originalname) //originalfilename with extension
    //path.extname(file.originalname)) // path name 
  }
})
 
var upload = multer({ storage: storage })

app.post('/post', upload.single('file'), function(req, res, next) {
    console.log('landing here', req.file)
    if (req.file == null) {
    // If Submit was accidentally clicked with no file selected...
      const error = new Error('Please upload a file')
      error.httpStatusCode = 400
      return next(error)
    } 
    var newImg = fs.readFileSync(req.file.path);
    // encode the file as a base64 string.
    var encImg = newImg.toString('base64');
    // define your new document
    var newItem = {
        contentType: req.file.mimetype,
        size: req.file.size,
        name: req.file.originalname,
        path: req.file.path
    };
    //res.send("file saved on server");
    res.send(req.file);
});