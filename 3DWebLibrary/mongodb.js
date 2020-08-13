const express = require('express');
const path = require('path');
const bodyParser = require('body-parser');
const fs = require('fs');
const app = express();
const mongodb = require('mongodb');
const multer = require('multer');
const _ = require('lodash');
//var storage = require('./storage')('AngularJS'); // storage folder.
const port = 8000;


app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

//app.use(express.static(__dirname + '/public'));
app.use(express.static(__dirname));

app.listen(port, ()=>{
console.log('We are live on ' + port);
});

// SET DATABASE
//Configuring Mongodb
const MongoClient = mongodb.MongoClient;
const ObjectId = mongodb.ObjectId;
const url = 'mongodb://192.168.1.11:27017';

MongoClient.connect(url,{
    useUnifiedTopology:true},(err,client) =>{
        if(err) return console.log(err);
        db = client.db('VirtualEngineering')
        app.listen(6000,() => {
        console.log("Mongodb server Listening at 6000")
  })
})