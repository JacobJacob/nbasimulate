Object.extend(Function.prototype, {
	getArguments: function() {
		var args = [];
		for(var i=0; i<this.arguments.length; i++)
			args.push(this.arguments[i]);
		return args;
	}
}, false);

var MS = {"Browser":{}};

Object.extend(MS.Browser, {
	isIE: navigator.userAgent.indexOf('MSIE') != -1,
	isFirefox: navigator.userAgent.indexOf('Firefox') != -1,
	isOpera: window.opera != null
}, false);

var AjaxPro = {};

AjaxPro.IFrameXmlHttp = function() {};
AjaxPro.IFrameXmlHttp.prototype = {
	onreadystatechange: null, headers: [], method: "POST", url: null, async: true, iframe: null,
	status: 0, readyState: 0, responseText: null,
	abort: function() {
	},
	readystatechanged: function() {
		var doc = this.iframe.contentDocument || this.iframe.document;
		if(doc != null && doc.readyState == "complete" && doc.body != null && doc.body.res != null) {
			this.status = 200;
			this.readyState = 4;
			this.responseText = doc.body.res;
			this.onreadystatechange();
			return;
		}
		setTimeout(this.readystatechanged.bind(this), 10);
	},
	open: function(method, url, async) {
		if(async == false) {
			alert("Synchronous call using IFrameXMLHttp is not supported.");
			return;
		}
		if(this.iframe == null) {
			var iframeID = "hans";
			if (document.createElement && document.documentElement &&
				(window.opera || navigator.userAgent.indexOf('MSIE 5.0') == -1))
			{
				var ifr = document.createElement('iframe');
				ifr.setAttribute('id', iframeID);
				ifr.style.visibility = 'hidden';
				ifr.style.position = 'absolute';
				ifr.style.width = ifr.style.height = ifr.borderWidth = '0px';

				this.iframe = document.getElementsByTagName('body')[0].appendChild(ifr);
			}
			else if (document.body && document.body.insertAdjacentHTML)
			{
				document.body.insertAdjacentHTML('beforeEnd', '<iframe name="' + iframeID + '" id="' + iframeID + '" style="border:1px solid black;display:none"></iframe>');
			}
			if (window.frames && window.frames[iframeID]) this.iframe = window.frames[iframeID];
			this.iframe.name = iframeID;
			this.iframe.document.open();
			this.iframe.document.write("<html><body></body></html>");
			this.iframe.document.close();
		}
		this.method = method;
		this.url = url;
		this.async = async;
	},
	setRequestHeader: function(name, value) {
		for(var i=0; i<this.headers.length; i++) {
			if(this.headers[i].name == name) {
				this.headers[i].value = value;
				return;
			}
		}
		this.headers.push({"name":name,"value":value});
	},
	getResponseHeader: function(name, value) {
		return null;
	},
	addInput: function(doc, form, name, value) {
		var ele;
		var tag = "input";
		if(value.indexOf("\n") >= 0) tag = "textarea";
		
		if(doc.all) {
			ele = doc.createElement("<" + tag + " name=\"" + name + "\" />");
		}else{
			ele = doc.createElement(tag);
			ele.setAttribute("name", name);
		}
		ele.setAttribute("value", value);
		form.appendChild(ele);
		ele = null;
	},
	send: function(data) {
		if(this.iframe == null) {
			return;
		}
		var doc = this.iframe.contentDocument || this.iframe.document;
		var form = doc.createElement("form");
		
		doc.body.appendChild(form);
		
		form.setAttribute("action", this.url);
		form.setAttribute("method", this.method);
		
		this.setRequestHeader("X-" + AjaxPro.ID + "-Processor", "IFrame");
		
		for(var i=0; i<this.headers.length; i++) {
			switch(this.headers[i].name.toLowerCase()) {
				case "content-length":
				case "accept-encoding":
					break;
				case "content-type":
					form.setAttribute("enctype", this.headers[i].value);
					break;
				default:
					this.addInput(doc, form, this.headers[i].name, this.headers[i].value);
			}
		}
		this.addInput(doc, form, "data", data);
		form.submit();
		setTimeout(this.readystatechanged.bind(this), 1);
	}
};

if(typeof XMLHttpRequest == "undefined")
	XMLHttpRequest = function() {
		var xmlHttp = null;
		if(!AjaxPro.noActiveX) {
			var clsids = ["MSXML2.XMLHTTP.5.0", "MSXML2.XMLHTTP.4.0", "MSXML2.XMLHTTP.3.0", "MSXML2.XMLHTTP", "Microsoft.XMLHTTP"];
			for(var i=0; i<clsids.length && xmlHttp == null; i++) {
				try {
					xmlHttp = new ActiveXObject(clsids[i]);
				} catch(e){}
			}
		}
		if(xmlHttp == null && MS.Browser.isIE) {
			return new AjaxPro.IFrameXmlHttp();
		}
		return xmlHttp;
	};

Object.extend(AjaxPro, {
	noOperation: function() {},
	onLoading: function() {},
	onError: function() {},
	onTimeout: function() {},
	onStateChanged: function() {},
	cryptProvider: null,
	queue: null,
	token: "",
	version: "6.6.14.2",
	ID: "AjaxPro",
	noActiveX: false,
	timeoutPeriod: 10*1000,
	queue: null,

	toJSON: function(o) {
		if(o == null)
			return "null";
		switch(o.constructor) {
			case String:
				var v = [];
				for(var i=0; i<o.length; i++) {
					var c = o.charAt(i);
					if(c >= " ") {
						if(c == "\\" || c == '"') v.push("\\");
						v.push(c);
					} else {
						switch(c) {
							case "\n": v.push("\\n"); break;
							case "\r": v.push("\\r"); break;
							case "\b": v.push("\\b"); break;
							case "\f": v.push("\\f"); break;
							case "\t": v.push("\\t"); break;
							default:
								v.push("\\u00");
								v.push(c.charCodeAt().toString(16));
						}
					}
				}
				return '"' + v.join('') + '"';
			case Array:
				var v = [];
				for(var i=0; i<o.length; i++)
					v.push(AjaxPro.toJSON(o[i])) ;
				return "[" + v.join(",") + "]";
			case Number:
				return isFinite(o) ? o.toString() : AjaxPro.toJSON(null);
			case Boolean:
				return o.toString();
			case Date:
				var d = new Object();
				d.__type = "System.DateTime";
				d.Year = o.getUTCFullYear();
				d.Month = o.getUTCMonth() +1;
				d.Day = o.getUTCDate();
				d.Hour = o.getUTCHours();
				d.Minute = o.getUTCMinutes();
				d.Second = o.getUTCSeconds();
				d.Millisecond = o.getUTCMilliseconds();
				d.TimezoneOffset = o.getTimezoneOffset();
				return AjaxPro.toJSON(d);
			default:
				if(typeof o.toJSON == "function")
					return o.toJSON();
				if(typeof o == "object") {
					var v=[];
					for(attr in o) {
						if(typeof o[attr] != "function")
							v.push('"' + attr + '":' + AjaxPro.toJSON(o[attr]));
					}
					if(v.length>0)
						return "{" + v.join(",") + "}";
					return "{}";		
				}
				return o.toString();
		}
	},
	dispose: function() {
		if(AjaxPro.queue != null) {
			AjaxPro.queue.dispose();
		}
	}
}, false);

addEvent(window, "unload", AjaxPro.dispose);

AjaxPro.Request = function(url) {
	this.url = url;
	this.xmlHttp = null;
};

AjaxPro.Request.prototype = {
	url: null,
	callback: null,
	onLoading: AjaxPro.noOperation,
	onError: AjaxPro.noOperation,
	onTimeout: AjaxPro.noOperation,
	onStateChanged: AjaxPro.noOperation,
	args: null,
	context: null,
	isRunning: false,
	abort: function() {
		if(this.timeoutTimer != null) clearTimeout(this.timeoutTimer);
		if(this.xmlHttp) {
			this.xmlHttp.onreadystatechange = AjaxPro.noOperation;
			this.xmlHttp.abort();
		}
		this.isRunning = false;
		this.onLoading(false);
	},
	dispose: function() {
		this.abort();
	},
	doStateChange: function() {
		this.onStateChanged(this.xmlHttp.readyState, this);

		if(this.xmlHttp.readyState != 4 || !this.isRunning)
			return;

		this.isRunning = false;
		this.duration = new Date().getTime() - this.__start;

		if(this.timeoutTimer != null) clearTimeout(this.timeoutTimer);
		this.onLoading(false);
		
		var res = {
			error: null,
			value: null,
			request: {method:this.method, args:this.args},
			context: this.context,
			duration: this.duration
		};

		if(this.xmlHttp.status == 200) {
			res = this.createResponse(res);
		} else {
			res = this.createResponse(res, true);
			res.error = {Message:this.xmlHttp.statusText,Type:"ConnectFailure",Status:this.xmlHttp.status};
		}
		
		this.abort();
		this.onError(res.error, this);
			
		if(typeof this.callback == "function")
			this.callback(res, this);
	},
	createResponse: function(r, noContent) {
		if(!noContent) {
			var responseText = new String(this.xmlHttp.responseText);

			if(AjaxPro.cryptProvider != null && typeof AjaxPro.cryptProvider == "function")
				responseText = AjaxPro.cryptProvider.decrypt(responseText);

			if(this.xmlHttp.getResponseHeader("Content-Type") == "text/xml")
				r.value = this.xmlHttp.responseXML;
			else
				if(responseText != null && responseText.trim().length > 0) {
					r.json = responseText;
					eval("r.value = " + responseText + "*/");
				}
		}
		
		/* if(this.xmlHttp.getResponseHeader("X-" + AjaxPro.ID + "-Cache") == "server") {
			r.isCached = true;
		} */
		return r;
	},
	timeout: function() {
		this.duration = new Date().getTime() - this.__start;
		var r = this.onTimeout(this.duration, this);
		if(typeof r == "undefined" || r != false) {
			this.abort();
		} else {
			this.timeoutTimer = setTimeout(this.timeout.bind(this), AjaxPro.timeoutPeriod);
		}
	},
	invoke: function(method, args, callback, context) {
		this.__start = new Date().getTime();

		if(this.xmlHttp == null)
			this.xmlHttp = new XMLHttpRequest();

		this.isRunning = true;
		this.method = method;
		this.args = args;
		this.callback = callback;
		this.context = context;
		
		var async = typeof callback == "function" && callback != AjaxPro.noOperation;
		var json = AjaxPro.toJSON(args) + "";

		if(AjaxPro.cryptProvider != null)
			json = AjaxPro.cryptProvider.encrypt(json);
			
		if(async) {
			this.xmlHttp.onreadystatechange = this.doStateChange.bind(this);
			this.onLoading(true);
		}

		this.xmlHttp.open("POST", this.url, async);
		this.xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		this.xmlHttp.setRequestHeader("X-" + AjaxPro.ID + "-Method", method);
		
		if(AjaxPro.token != null && AjaxPro.token.length > 0)
			this.xmlHttp.setRequestHeader("X-" + AjaxPro.ID + "-Token", AjaxPro.token);

		if(!MS.Browser.isIE)
			this.xmlHttp.setRequestHeader("Connection", "close");		// Mozilla Bug #246651

		this.timeoutTimer = setTimeout(this.timeout.bind(this), AjaxPro.timeoutPeriod);

		this.xmlHttp.send(json);
		
		json = null;
		args = null;

		if(!async) {
			return this.createResponse({error: null,value: null});
		}

		return true;	
	}
};

AjaxPro.RequestQueue = function(conc) {
	this.queue = [];
	this.requests = [];
	this.timer = null;
	
	if(isNaN(conc)) conc = 2;

	for(var i=0; i<conc; i++) {		// max 2 http connections
		this.requests[i] = new AjaxPro.Request();
		this.requests[i].callback = function(res) {
			var r = res.context;
			res.context = r[3][1];
			// try {
				r[3][0](res, this);
			// }catch(e){alert(e.description);}
		};
		this.requests[i].callbackHandle = this.requests[i].callback.bind(this.requests[i]);
	}
};

AjaxPro.RequestQueue.prototype = {
	process: function() {
		this.timer = null;
		if(this.queue.length == 0) return;

		for(var i=0; i<this.requests.length && this.queue.length > 0; i++) {
			if(this.requests[i].isRunning == false) {
				var r = this.queue.shift();

				this.requests[i].url = r[0];
				this.requests[i].onLoading = r[3].length >2 && r[3][2] != null && typeof r[3][2] == "function" ? r[3][2] : AjaxPro.onLoading;
				this.requests[i].onError = r[3].length >3 && r[3][3] != null && typeof r[3][3] == "function" ? r[3][3] : AjaxPro.onError;
				this.requests[i].onTimeout = r[3].length >4 && r[3][4] != null && typeof r[3][4] == "function" ? r[3][4] : AjaxPro.onTimeout;
				this.requests[i].onStateChanged = r[3].length >5 && r[3][5] != null && typeof r[3][5] == "function" ? r[3][5] : AjaxPro.onStateChanged;

				this.requests[i].invoke(r[1], r[2], this.requests[i].callbackHandle, r);
				r = null;
			}
		}
		if(this.queue.length > 0 && this.timer == null) {
			this.timer = setTimeout(this.process.bind(this), 10);
		}
	},
	add: function(url, method, args, e) {
		this.queue.push([url, method, args, e]);

		if(this.timer == null) {
			this.timer = setTimeout(this.process.bind(this), 1);
		}
	},
	abort: function() {
		this.queue.length = 0;
		if (this.timer != null) {
			clearTimeout(this.timer);
		}
		this.timer = null;
		for(var i=0; i<this.requests.length; i++) {
			if(this.requests[i].isRunning == true) {
				this.requests[i].abort();
			}
		}
	},
	dispose: function() {
		for(var i=0; i<this.requests.length; i++) {
			var r = this.requests[i];
			r.dispose();
		}
		this.requests.clear();
	}
};

AjaxPro.queue = new AjaxPro.RequestQueue(2);	// 2 http connections

AjaxPro.AjaxClass = function(url) {
	this.url = url;
};

AjaxPro.AjaxClass.prototype = {
	invoke: function(method, args, e) {
		if(e != null) {
			if(e.length != 6) for(;e.length<6;) e.push(null);
			if(e[0] != null && typeof e[0] == "function") {
				return AjaxPro.queue.add(this.url, method, args, e);
			}
		}
		var r = new AjaxPro.Request();
		r.url = this.url;
		return r.invoke(method, args);
	}
};