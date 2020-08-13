var multer = require('multer'); //  middleware for handling multipart/form-data,
var fs = require('fs');
// Constructor 
module.exports = function (name) {
    try {
        // Configuring appropriate storage 
        var storage = multer.diskStorage({
            // Absolute path
            destination: function (req, file, callback) {
                var path = `./upload/${name}`;
                if (!fs.existsSync(path)) {
                    fs.mkdirSync(path);
                }
                //callback(null, './upload/'+name);
                callback(null, path);
            },
            // Match the field name in the request body
            filename: function (req, file, callback) {
                //callback(null, file.fieldname + '-' + Date.now());
                callback(null, file.originalname);
            }
        });
        return storage;
    } catch (ex) {
        console.log("Error :\n"+ex);
    }
};