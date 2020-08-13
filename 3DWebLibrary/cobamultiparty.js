var fs = require('fs');
var inputFolder = "./folder/";
var outputFolder = "./output/";
var http = require("http");

var options = require("url").parse("http://localhost:8000/post");
options.method = "POST";
options.headers = "multipart/form-data";

console.log("Reading files...");

fs.readdir("./folder/", function (err, files) {
  if (err) throw err;
  console.log(files.length + " files read...");
  var input, request;
  for(var i = 0; i < files.length; i++){
    input = fs.createReadStream(inputFolder + files[i]);
    request = closureRequest(fs.createWriteStream(outputFolder + files[i]));
    input.pipe(request);
  }
});

function closureRequest(output){
    return new http.request(options, function(response) {
        if (response.statusCode === 201) {
          /* Compression was successful, retrieve output from Location header. */
          http.get(response.headers.location, function(response) {
            response.pipe(output);
          });
        } else {
          /* Something went wrong! You can parse the JSON body for details. */
          console.log("Compression failed");
        }
    });
} 


