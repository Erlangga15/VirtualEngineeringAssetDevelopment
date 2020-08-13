//Main Module
const express = require('express');
const path = require('path');
const bodyParser = require('body-parser');
const fs = require('fs');
const app = express();
const mongodb = require('mongodb');
const multer = require('multer');
const _ = require('lodash');
const sharp = require('sharp');
const obj2gltf = require('obj2gltf');
const http = require('http').Server(app);
var io = require('socket.io')(http);
//const https = require('https');
var request = require('request');

//--**Authentication**--
const expressLayouts = require('express-ejs-layouts');
const mongoose = require('mongoose');
const passport = require('passport');
const flash = require('connect-flash');
const session = require('express-session');
// Passport Config
require('./config/passport')(passport);
//Mongoose Connect
mongoose.connect("mongodb://erlangga:labduafa@x1.hcm-lab.id:27070/3DObjectManagement?authSource=admin", {
  useNewUrlParser: "true",
});
mongoose.connection.on("error", err => {
  console.log("err", err)
});
mongoose.connection.on("connected", (err, res) => {
  console.log("mongoose is connected")
});
// EJS
//app.use(expressLayouts);
// Express session
app.use(
  session({
    secret: 'secret',
    resave: true,
    saveUninitialized: true
  })
);
// Passport middleware
app.use(passport.initialize());
app.use(passport.session());
// Express body parser
app.use(express.urlencoded({ extended: true }));
// Connect flash
app.use(flash());
// Global variables
app.use(function(req, res, next) {
  res.locals.success_msg = req.flash('success_msg');
  res.locals.error_msg = req.flash('error_msg');
  res.locals.error = req.flash('error');
  next();
});
// Routes
const { ensureAuthenticated, forwardAuthenticated } = require('./config/auth');

// Welcome Page
app.get('/', forwardAuthenticated, (req, res) => res.render('welcome'));

// Dashboard
app.get('/home', ensureAuthenticated, (req, res) =>{
  res.render('home', {
    user: req.user
  }),
  userku = req.user.name;
  collArr = 'array';
  tridArr = '';
  arrayName = '';
  idname = 0;
  console.log(userku);
});
app.use('/users', require('./routes/users.js'));
//--**Autentichation**-- 


//Global Variable
const port = 3000;
var idname = 0;
var arrayName;
var arrayName2;
var collArr = 'array';
var tridArr = '';
var collArrView;
var userku= 'name';

//Mqtt Conenction
var mqtt = require('mqtt');
var options = {
  port:18883,
  host:'mqtt://x1.hcm-lab.id'
};
var client  = mqtt.connect('mqtt://x1.hcm-lab.id', options);

//Bodyparser Express
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

//Set view engine to ejs
app.set("view engine", "ejs"); 

//Tell Express where we keep our index.ejs
app.use(express.static(__dirname));

//Http listen port
http.listen(port, '0.0.0.0', ()=>{
console.log('We are live on ' + port);
});

//Socket connection
io.sockets.on('connection', function (socket) {
  // socket connection indicates what mqtt topic to subscribe to in data.topic
  socket.on('subscribe', function (data) {
      socket.join(data.topic);
      client.subscribe(data.topic);
  });
  // when socket connection publishes a message, forward that message
  // the the mqtt broker
  socket.on('publish', function (data) {
      client.publish(data.topic,data.payload);
  });
  socket.on('status',function (msg) {
    console.log('Message from pusher send is :' + msg);
    io.sockets.emit('status2', msg);
  });
});
var nameProject; 
var msg1;
client.on('message', function(topic, message) {
  io.sockets.in(topic).emit('mqtt',{'topic': String(topic), 'payload':String(message)});
  if(topic === 'vecom/project'){
    msg1 = message.toString();
    nameProject = msg1;
    console.log('Name Project : '+ msg1);
  }
  if(topic === 'vecom/view'){
    msg1 = message.toString();
    const directory = 'files';
    fs.readdir(directory, (err, files) => {
      if (err) throw err;
      for (const file of files) {
        fs.unlink(path.join(directory, file), err => {
          if (err) throw err;
        });
      }
    });
    console.log('Unlink : '+ msg1);
  }
  message = message.toString();
  console.log(message);
});
client.on('connect', ()=>{
  client.subscribe('vecom/project')
  client.subscribe('vecom/view')
})

// SET STORAGE
//Multer Setting
var storage = multer.diskStorage({
  // Absolute path
  destination: function (req, file, callback) {
      var path = `./upload/${nameProject}`;
      if (!fs.existsSync(path)) {
          fs.mkdirSync(path);
      }
      callback(null, path);
  },
  // Match the field name in the request body
  filename: function (req, file, callback) {
      callback(null, file.originalname);
  }
});

// SET DATABASE
//Configuring Mongodb
const MongoClient = mongodb.MongoClient;
const ObjectId = mongodb.ObjectId;
const url = 'mongodb://erlangga:labduafa@x1.hcm-lab.id:27070/admin';
MongoClient.connect(url,{
    useUnifiedTopology:true},(err,client) =>{
        if(err) return console.log(err);
        db = client.db('3DObjectManagement')
        app.listen(6000,() => {
        console.log("Mongodb server Listening at 6000")
  })
})

//Multer Upload File
var upload = multer({ storage: storage })
app.post('/post', upload.single('file'), (req, res, next) =>{
    console.log('landing here', req.file)
    var tipe2 = String(req.file.originalname);
    var tipe = tipe2.split(".");
    console.log("Tipe : "+tipe);
    var newItem = {
        _id: req.file._id,
        contentType: tipe[1],
        size: req.file.size,
        name: req.file.originalname,
        path: req.file.path,
        linkpath: "http://x1.hcm-lab.id:3000/"+req.file.path
    };
    db.listCollections().toArray(function(err, collections) {
      var collectionExists = false;
      if(!err && collections && collections.length>0){
          _.each(collections,function (co) {
              if(co.name == nameProject){
                  collectionExists = true;
                  console.log("Collection Exist!");
              }
          })
      }
      // insert upload file to collection
      if(!collectionExists){
        db.createCollection(userku + '-' + nameProject, function(err, res) {
          if (err) throw err;
          console.log("Collection created!");
        });
      }
    });
    db.collection(userku + '-' + nameProject)
      .insertOne(newItem,(err, result) => {
      if (err) { console.log(err); };
      console.log("Saved to database");
    });
    res.send(req.file);
});

app.post('/postmultiple', upload.array('files',10),(req, res, next) => {
  console.log('landing here', req.user)
    var newItem = [];
    req.files.forEach(function(value, index){
      var tipe2 = String(value.originalname);
      var tipe = tipe2.split(".");
      newItem.push({
        _id: value._id,
        contentType: tipe[1],
        size: value.size,
        name: value.originalname,
        path: value.path,
        linkpath: "http://x1.hcm-lab.id:3000/"+value.path
      });
    });
    db.listCollections().toArray(function(err, collections) {
      var collectionExists = false;
      if(!err && collections && collections.length>0){
          _.each(collections,function (co) {
              if(co.name == nameProject){
                  collectionExists = true;
                  console.log("Collection Exist!");
              }
          })
      }
      // insert upload file to collection
      if(!collectionExists){
        db.createCollection(userku + '-' + nameProject, function(err, res) {
          if (err) throw err;
          console.log("Collection created!");
        });
      }
    });
    db.collection(userku + '-' + nameProject)
      .insertMany(newItem,(err, result) => {
      if (err) { console.log(err); };
      console.log("Saved to database");
    });
    res.send(req.files);
});

//Routes Page
app.get('/scan',  ensureAuthenticated, (req, res)=>{
  res.render('scan', {
    user:req.user
  });
  collArr = 'array';
  tridArr = '';
  arrayName = '';
  idname = 0;
});
app.get('/form', ensureAuthenticated, (req, res)=>{
res.render('form', {
  user: req.user
});
  collArr = 'array';
  tridArr = '';
  arrayName = '';
  idname = 0;
});
app.get('/login',  ensureAuthenticated, (req, res)=>{
  res.render('login', {
    user:req.user
  });
  collArr = 'array';
  tridArr = '';
  arrayName = '';
  idname = 0;
});
app.get('/home',  ensureAuthenticated, (req, res)=>{
  res.render('home', {
    user:req.user
  });
  collArr = 'array';
  tridArr = '';
  arrayName = '';
  idname = 0;
});

app.get('/userfile',  ensureAuthenticated, (req, res)=>{
  collArr = 'array';
  tridArr = '';
  arrayName = '';
  //idname = 0;

  var resultArray = [];
  var cursor = db.collection(collArr).find();
  cursor.forEach(function(doc,err) {
    resultArray.push(doc);
  }, function(){
  });
  let allCollections = [];
  var sizeColl = [];
  var countColl = [];
  var promises = [];
//create client by providing database name
  db.listCollections().toArray(function(err, collections) {  
    if(!err && collections && collections.length>0){
      _.each(collections,function (co) {
        promises.push(promisified());
        function promisified(){
          return new Promise(function(resolve, reject){
            db.collection(co.name).stats(function(err,stats){
              if(co.name != 'users'){
                console.log(co.name.substring(0, co.name.indexOf('-')));
                if((co.name.substring(0, co.name.indexOf('-'))) == userku){
                  allCollections.push(co.name);
                  sizeColl.push(stats.size);
                  countColl.push(stats.count);
                }
              }
              resolve();
            }); 
          });
        }
      });
      Promise.all(promises).then(function(){
      res.render('userfile', {
          user: req.user,
          items: resultArray,
          files: allCollections,
          sizes: sizeColl,
          counts: countColl,
          images: arrayName,
          filetrid: tridArr});
      });
    }
  });
});

//Table Page
app.get('/table',  ensureAuthenticated, (req, res)=>{
  console.log("Will get data");
  var resultArray = [];
  var cursor = db.collection(collArr).find();
  cursor.forEach(function(doc,err) {
    resultArray.push(doc);
  }, function(){
  });
  let allCollections = [];
  var sizeColl = [];
  var countColl = [];
  var promises = [];
//create client by providing database name
  db.listCollections().toArray(function(err, collections) {  
    if(!err && collections && collections.length>0){
      _.each(collections,function (co) {
        promises.push(promisified());
        function promisified(){
          return new Promise(function(resolve, reject){
            db.collection(co.name).stats(function(err,stats){
              if(co.name != 'users'){
                allCollections.push(co.name);
                sizeColl.push(stats.size);
                countColl.push(stats.count);
              }
              resolve();
            }); 
          });
        }
      });
      Promise.all(promises).then(function(){
      res.render('table', {
          user: req.user,
          items: resultArray,
          files: allCollections,
          sizes: sizeColl,
          counts: countColl,
          images: arrayName,
          filetrid: tridArr});
      });
    }
  });
});

//Table Collection Page
app.get('/table/:collection', function(req, res, next) {
  var collec = req.params.collection;
  collArr = collec;
  
  /*var testStr = collec;
  var splitStr = testStr.substring(testStr.indexOf('-') + 1);
  var inputFolder = './upload/'+ splitStr + '/';
  console.log("SUBSTRING IS " + splitStr);
  fs.readdir(inputFolder, function (err, files) {
    console.log(files.length + " files read...");
    
    fs.exists(inputFolder + splitStr +'.gltf', function(exists) {
      if(exists){
        console.log('File exists');
      }
      if(!exists) {
        // file does not exist
        /* db.collection(collec).findOne({}, {sort:{$natural:-1}}, function(err, results){
          db.collection(collec).remove({results});
        });  
        
        obj2gltf('upload/'+ splitStr + '/'+ splitStr +'.obj')
        .then(function(gltf) {
            const data = Buffer.from(JSON.stringify(gltf));
            fs.writeFileSync('upload//'+ splitStr + '//'+ splitStr +'.gltf', data);
            request.post({
              //url: 'http://x1.hcm-lab.id:3000/post',
              url: 'http://192.168.8.101:3000/post',
              headers: {
              "Content-Type": "multipart/form-data"
              },
              formData: {
                  file: fs.createReadStream(inputFolder + splitStr +'.gltf'),
                  filename: 'myFile',
              },                    
            },
            function(error, response, body) {
              console.log(body);
                request.post({
                  //url: 'http://x1.hcm-lab.id:3000/post',
                  url: 'http://192.168.8.101:3000/post',
                  headers: {
                  "Content-Type": "multipart/form-data"
                  },
                  formData: {
                      file: fs.createReadStream(inputFolder + splitStr +'.jpg'),
                      filename: 'myFile',
                  },                    
                },
                function(error, response, body) {
                  console.log(body);
                });
            });
        });     
      }  
    });
  }); */

  var promises = [];
  console.log("HELLO");
  
  var length = 0;
  
  //db.collection(collec).findOne({}, {sort:{$natural:-1}}, function(err, results){
  db.collection(collec).findOne({contentType:'jpg'}, {'contentType':1}, function(err, results){  
    let inputFile  = (results.path);
    let outputFile = (results.path);
    sharp(inputFile).resize({ width: 512, height:512 }).toBuffer(function(err, buffer) {
      if (err) throw err
      fs.writeFile(results.path, buffer, function(e) { 
      });
    });
    arrayName = results.path;
  });
  
  promises.push(promisified());
  Promise.all(promises).then(function(){
    res.redirect('/table'); 
  })
  function promisified(){
    return new Promise(function(resolve, reject){
      var collectionArr = [];
      db.collection(collec).findOne({contentType:'obj'}, {'contentType':1}, function(err, results){
        if(results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
          tridArr = ("/"+collectionArr[0]+',/'+collectionArr[1]+',/'+collectionArr[2]);
          resolve();
        }
      });
      db.collection(collec).findOne({contentType:'mtl'}, {'contentType':1}, function(err, results){
        if(results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
          tridArr = ("/"+collectionArr[0]+',/'+collectionArr[1]+',/'+collectionArr[2]);
          resolve();
        }
      });
      db.collection(collec).findOne({contentType:'png'}, {'contentType':1}, function(err, results){
        if(results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
          console.log(collectionArr);
          tridArr = ("/"+collectionArr[0]+',/'+collectionArr[1]+',/'+collectionArr[2]);
          console.log(tridArr);
          resolve();
        }
      });
    })
  }
});

app.get('/users/table', (req, res)=>{
  console.log("Will get data");
  var resultArray = [];
  var cursor = db.collection(collArr).find();
  cursor.forEach(function(doc,err) {
    resultArray.push(doc);
  }, function(){
  });
  let allCollections = [];
  var sizeColl = [];
  var countColl = [];
  var promises = [];
//create client by providing database name
  db.listCollections().toArray(function(err, collections) {  
    if(!err && collections && collections.length>0){
      _.each(collections,function (co) {
        promises.push(promisified());
        function promisified(){
          return new Promise(function(resolve, reject){
            db.collection(co.name).stats(function(err,stats){
              if(co.name != 'users'){
                allCollections.push(co.name);
                sizeColl.push(stats.size);
                countColl.push(stats.count);
              }
              resolve();
            }); 
          });
        }
      });
      Promise.all(promises).then(function(){
      res.render('table2', {
          items: resultArray,
          files: allCollections,
          sizes: sizeColl,
          counts: countColl,
          images: arrayName2,
          filetrid: tridArr});
      });
    }
  });
});

app.get('/table2/:collection', function(req, res, next) {
  collArr = 'array';
  tridArr = '';
  arrayName = '';
  idname = 0;

  var collec = req.params.collection;
  collArr = collec;
  var promises = [];
  console.log("HELLO");
  //var collectionArr = [];
  //var length = 0;
  /*db.collection(collec).find({}).toArray(function(err, result) {
    if (err) throw err
    length = result.length;
    console.log(length);
    switch(true){
      case (length < 3) :
        db.collection(collec).findOne({'_id':0}, function(err, results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
          console.log(collectionArr);
          tridArr = ("/"+collectionArr);
          console.log(tridArr);
        });
      break;
      case (length == 3) :
        db.collection(collec).findOne({'_id':0}, function(err, results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
        });
        db.collection(collec).findOne({'_id':1}, function(err, results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
          console.log(collectionArr);
          tridArr = ("/"+collectionArr[0]+',/'+collectionArr[1]);
          console.log(tridArr);
        });
      break;
      case (length > 3) :
        db.collection(collec).findOne({'_id':0}, function(err, results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
        });
        db.collection(collec).findOne({'_id':1}, function(err, results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
        });
        db.collection(collec).findOne({'_id':2}, function(err, results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
          console.log(collectionArr);
          tridArr = ("/"+collectionArr[0]+',/'+collectionArr[1]+',/'+collectionArr[2]);
          console.log(tridArr);
        });
      break;
    }
  });*/
  //Sort data pada database upload paling terbaru
  /* db.collection(collec).findOne({}, {sort:{$natural:-1}}, function(err, results){
    let inputFile  = (results.path);
    let outputFile = (results.path);
    sharp(inputFile).resize({ width: 512, height:512 }).toBuffer(function(err, buffer) {
      if (err) throw err
      fs.writeFile(results.path, buffer, function(e) { 
      });
    });
    arrayName2 = "..\\" + results.path;
    console.log("PATH ="+arrayName2);
  }); */

  db.collection(collec).findOne({contentType:'jpg'}, {'contentType':1}, function(err, results){  
    let inputFile  = (results.path);
    let outputFile = (results.path);
    sharp(inputFile).resize({ width: 512, height:512 }).toBuffer(function(err, buffer) {
      if (err) throw err
      fs.writeFile(results.path, buffer, function(e) { 
      });
    });
    arrayName2 = "..\\" +results.path;
  });

  promises.push(promisified());
  Promise.all(promises).then(function(){
    res.redirect('/users/table'); 
  })
  function promisified(){
    return new Promise(function(resolve, reject){
      var collectionArr = [];
      db.collection(collec).findOne({contentType:'obj'}, {'contentType':1}, function(err, results){
        if(results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
          tridArr = ("/"+collectionArr[0]+',/'+collectionArr[1]+',/'+collectionArr[2]);
          resolve();
        } 
      });
      db.collection(collec).findOne({contentType:'mtl'}, {'contentType':1}, function(err, results){
        if(results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
          tridArr = ("/"+collectionArr[0]+',/'+collectionArr[1]+',/'+collectionArr[2]);
          resolve();
        }
      });
      db.collection(collec).findOne({contentType:'png'}, {'contentType':1}, function(err, results){
        if(results){
          collectionArr.push(results.path.replace(/\\/g,'/'));
          console.log(collectionArr);
          tridArr = ("/"+collectionArr[0]+',/'+collectionArr[1]+',/'+collectionArr[2]);
          console.log(tridArr);
          resolve();
        }
      });
    })
  }
});

//Table Drop Collection
app.get('/userfile/:collection/delete', function(req, res, next) {
  var collec = req.params.collection;
  var promises = [];
  console.log("DELETE");
  promises.push(promisified());
  Promise.all(promises).then(function(){
    res.redirect('/userfile');
  })
  function promisified(){
    return new Promise(function(resolve, reject){
      db.collection(collec).drop(function(err, delOK){
        if (err) throw err
        if (delOK) console.log("Collection deleted");
        resolve();
      });
    })
  }
});

//Table Drop Collection
app.get('/userfile/:collection/convert', function(req, res, next) {
  var collec = req.params.collection;
  
  var testStr = collec;
  var splitStr = testStr.substring(testStr.indexOf('-') + 1);
  var inputFolder = './upload/'+ splitStr + '/';
  console.log("SUBSTRING IS " + splitStr);
  fs.readdir(inputFolder, function (err, files) {
    console.log(files.length + " files read...");
    fs.exists(inputFolder + splitStr +'.gltf', function(exists) {
      if(exists){
        console.log('File exists');
      }
      if(!exists) {
        // file does not exist
        /* db.collection(collec).findOne({}, {sort:{$natural:-1}}, function(err, results){
          db.collection(collec).remove({results});
        }); */
        
        obj2gltf('upload/'+ splitStr + '/'+ splitStr +'.obj')
        .then(function(gltf) {
            const data = Buffer.from(JSON.stringify(gltf));
            fs.writeFileSync('upload//'+ splitStr + '//'+ splitStr +'.gltf', data);
            request.post({
              url: 'http://x1.hcm-lab.id:3000/post',
              //url: 'http://192.168.8.102:3000/post',
              headers: {
              "Content-Type": "multipart/form-data"
              },
              formData: {
                  file: fs.createReadStream(inputFolder + splitStr +'.gltf'),
                  filename: 'myFile',
              },                    
            },
            function(error, response, body) {
              console.log(body);
            });
        });     
      }  
    });
  });

  /* var testStr = collec;
  var splitStr = testStr.substring(testStr.indexOf('-') + 1);
  var inputFolder = 'upload/'+ splitStr + '/';
  console.log("SUBSTRING IS " + splitStr);
  fs.readdir(inputFolder, function (err, files) {
    console.log(files.length + " files read...");
    fs.exists(inputFolder + splitStr +'.gltf', function(exists) {
      if(exists){
        console.log('File exists');
      }
      if(!exists) {
        obj2gltf('upload/'+ splitStr + '/'+ splitStr +'.obj')
        .then(function(gltf) {
            const data = Buffer.from(JSON.stringify(gltf));
            fs.writeFile('upload//'+ splitStr + '//'+ splitStr +'.gltf', data, function(err) {
              if (err) throw err; 
              console.log("Convert Success");
              client.publish('vecom/project',splitStr);
              var stats = fs.statSync('upload/'+ splitStr + '/'+ splitStr +'.gltf')
              var fileSizeInBytes = stats["size"]

              var newItem = {
                contentType: 'gltf',
                size: fileSizeInBytes,
                name: splitStr +'.gltf',
                path: inputFolder + splitStr +'.gltf',
                linkpath: "https://x1.hcm-lab.id:3001/"+inputFolder + splitStr +'.gltf'
              };
              db.collection(userku + '-' + splitStr)
                .insertOne(newItem,(err, result) => {
                if (err) { console.log(err); };
                res.redirect('/userfile');
                console.log("Saved to database");
              });
            });    
        })
        .catch(function (err) {
          if (err) throw err;
        });    
      }  
    });
  }); */

  var promises = [];
  Promise.all(promises).then(function(){
    client.publish('vecom/project',splitStr);
    res.redirect('/userfile');
  })
  function promisified(){
    return new Promise(function(resolve, reject){
      resolve();
    })
  }
  console.log("CONVERT");
});


