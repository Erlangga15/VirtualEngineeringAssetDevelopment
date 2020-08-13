var fs = require('fs');
var request = require('request');
var mqtt = require('mqtt');
var path = require('path');
var io = require('socket.io-client');
//var socket = io.connect('http://x1.hcm-lab.id:3000', {reconnect: true});
var socket = io.connect('http://192.168.8.101:3000', {reconnect: true});
var inputFolder = "./files/";

var options = {
    port:18883,
    host:'mqtt://x1.hcm-lab.id'
};
var client  = mqtt.connect('mqtt://x1.hcm-lab.id', options);

var msg1;
var msg2;
var msg3;
var msg4;
var msg5;
var fileStream;
var counter = 0;
client.on('message', (topic, message)=>{
    if(topic === 'vecom/scan/init'){
        msg1 = message;
    }
    if(topic === 'vecom/scan/start'){
        msg2 = message;
    }
    if(topic === 'vecom/save'){
        msg3 = message;
    }
    if(topic === 'vecom/upload'){
        msg4 = message.toString();
        if(msg4 == 'upload'){
            
            fs.readdir(inputFolder, function (err, files) {
                if (err) throw err;
                console.log(files.length + " files read...");
                /* for(var i = 0; i < files.length; i++){
                    fileStream = fs.createReadStream(inputFolder + files[i]);
                    request.post({
                            //url: 'http://x1.hcm-lab.id:3000/post',
                            url: 'http://192.168.8.101:3000/post',
                            headers: {
                            "Content-Type": "multipart/form-data"
                        },
                        formData: {
                            file: fileStream,
                            //file: fs.createReadStream(inputFolder + files[i]),
                            filename: 'myFile',
                        },                    
                    },
                    function(error, response, body) {
                        console.log(body);  
                        socket.emit('status',body);       
                    });                   
                    //client.publish("vecom/upload/progres", "/"+(i+1) +"/"+files.length);
                    /* fs.unlink(path.join(inputFolder, files[i]), err => {                      
                        if (err) throw err;
                    }); 
                } */
                fileStream = fs.createReadStream(inputFolder + files[0]);
                request.post({
                    //url: 'http://x1.hcm-lab.id:3000/post',
                    url: 'http://192.168.8.101:3000/post',
                    headers: {
                    "Content-Type": "multipart/form-data"
                    },
                    formData: {
                        file: fileStream,
                        //file: fs.createReadStream(inputFolder + files[i]),
                        filename: 'myFile',
                    },                    
                },
                function(error, response, body) {
                    console.log(body);
                    socket.emit('status',body);   
                        fileStream = fs.createReadStream(inputFolder + files[1]);
                        request.post({
                            //url: 'http://x1.hcm-lab.id:3000/post',
                            url: 'http://192.168.8.101:3000/post',
                            headers: {
                            "Content-Type": "multipart/form-data"
                            },
                            formData: {
                                file: fileStream,
                                //file: fs.createReadStream(inputFolder + files[i]),
                                filename: 'myFile',
                            },                    
                        },
                        function(error, response, body) {
                            console.log(body);
                            socket.emit('status',body);
                                fileStream = fs.createReadStream(inputFolder + files[2]);
                                request.post({
                                    //url: 'http://x1.hcm-lab.id:3000/post',
                                    url: 'http://192.168.8.101:3000/post',
                                    headers: {
                                    "Content-Type": "multipart/form-data"
                                    },
                                    formData: {
                                        file: fileStream,
                                        //file: fs.createReadStream(inputFolder + files[i]),
                                        filename: 'myFile',
                                    },                    
                                },
                                function(error, response, body) {
                                    console.log(body);
                                    socket.emit('status',body);
                                        fileStream = fs.createReadStream(inputFolder + files[3]);
                                        request.post({
                                            //url: 'http://x1.hcm-lab.id:3000/post',
                                            url: 'http://192.168.8.101:3000/post',
                                            headers: {
                                            "Content-Type": "multipart/form-data"
                                            },
                                            formData: {
                                                file: fileStream,
                                                //file: fs.createReadStream(inputFolder + files[i]),
                                                filename: 'myFile',
                                            },                    
                                        },
                                        function(error, response, body) {
                                            console.log(body);
                                            socket.emit('status',body);       
                                        });       
                                });
                        });     
                }); 
                
                /* fileStream.on('data', function(data) {
                    var blockLength = data.length
                    console.log(blockLength);
                });  */             
            });
        }
    }
    if(topic === 'vecom/project'){
        msg5 = message;
        msg5 = message.toString();
        console.log('Name Project : '+ msg5);
    }
    message = message.toString();
    console.log(message);
})

client.on('connect', ()=>{
    console.log('Connected to server');
    client.subscribe('vecom/project')
    client.subscribe('vecom/scan/init')
    client.subscribe('vecom/scan/start')
    client.subscribe('vecom/save')
    client.subscribe('vecom/upload')
  });