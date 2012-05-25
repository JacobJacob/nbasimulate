var http = require('http');
require('./util/shotenjin');
require('./util/render');

http.createServer(function (request, response) {

  require("./proxy").get(request, response);

}).listen(8124);

console.log('Server running at http://127.0.0.1:8124/');