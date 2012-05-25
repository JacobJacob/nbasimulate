
var path = require("path");

var dir = path.dirname(__dirname);

var HTML_PATH = path.join(dir, "/web/html/");

render_html = function(filename, context){
    filename += ".html";
    var file = path.join(HTML_PATH, filename);
	var output = Shotenjin.renderView(file, context);
	return output;
}

render_json = function(context){

}

global.render_html = render_html;
global.render_json = render_json;