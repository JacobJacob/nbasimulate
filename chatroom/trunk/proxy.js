
var ENCODING = "utf-8";

doError = function(res, code){
	var message = "";
	switch(code){
	   case 404:
	      message = "<p>页面不存在</p>";
	      break;
	   default:
	      message = "<p>未知错误</p>";
		  break;
	}
	res.writeHead(code, {'Content-Type': 'text/plain'});
    res.end(message, ENCODING);
}

/**
处理客户端请求
@param: path
@param: req
@param: res
*/

doGet = function(path, req, res){
    
	if(path==null){
	    return getDefaultController();
	}
	
	var index = path.indexOf("?");
	if(index > 0){
	    path = path.substring(0, index);
	}
	var paths = path.split("/");
	var controllerName = paths[1];
	var methodName = "index"; 
	
	if(paths.length>=2){
	    methodName = paths[2];
	}
	console.log("controllerName[" + controllerName + "], methodName[" + methodName + "]");
	try{
	    var controller = require("./controller/" + controllerName);
	    console.log(controller);
        var content = controller[methodName].apply();
		console.log(content);
		res.end(content);
		res.writeHead(200, {'Content-Type': 'text/plain'});
 	}catch(error){
	    console.log(error);
	    var message = error.message;
	    console.log("error type[" + error.name + "], message[" + message + "]");   	
	    var code = 503;
		if(message.indexOf("Cannot find module" > 0)){
		    code = 404;
		}
		
		doError(res, code);
 	}
}

exports.get = function(req, res){
    try{
        var path = req.url;
	    console.log("request url:" + path);
		doGet(path, req, res);

    }catch(error){
	    var message = error.message;
	    console.log("error:" + error.message);
	    res.writeHead(503, {'Content-Type': 'text/plain'});
 	    res.end(message);
	}
}