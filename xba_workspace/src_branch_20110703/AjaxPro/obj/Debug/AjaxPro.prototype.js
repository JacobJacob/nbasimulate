���   �   )System.Resources.ResourceReader, mscorlibsSystem.Resources.RuntimeResourceSet, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089         mSystem.CodeDom.MemberAttributes, System, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^System.Boolean, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089PADPA�c��&���t`o`       1   /  ,$ t h i s . D e f a u l t M o d i f i e r s     *$ t h i s . T r a y A u t o A r r a n g e �   &$ t h i s . T r a y L a r g e I c o n �        ����          LSystem, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   System.CodeDom.MemberAttributes   value__     P      ����          System.Boolean   m_value     ����          System.Boolean   m_value                                                                                                                                                                                                       bjs[objId].__closures[funcId] = function() {
			return Function.__funcs[funcId].apply(Function.__objs[objId], arguments);
		};
	}
}, false);

Object.extend(Array.prototype, {
	push: function(o) {
		this[this.length] = o;
	},
	addRange: function(items) {
		if(items.length > 0)
			for(var i=0; i<items.length; i++)
				this.push(items[i]);
	},
	clear: function() {
		this.length = 0;
		return this;
	},
	shift: function() {
		if(this.length == 0) return null;
		var o = this[0];
		for(var i=0; i<this.length-1; i++)
			this[i] = this[i + 1];
		this.length--;
		return o;
	}
}, false);

Object.extend(String.prototype, {
	trimLeft: function() {
		return this.replace(/^\s*/,"");
	},
	trimRight: function() {
		return this.replace(/\s*$/,"");
	},
	trim: function() {
		return this.trimRight().trimLeft();
	},
	endsWith: function(s) {
		if(this.length == 0 || this.length < s.length) return false;
		return (this.substr(this.length - s.length) == s);
	},
	startsWith: function(s) {
		if(this.length == 0 || this.length < s.length) return false;
		return (this.substr(0, s.length) == s);
	},
	split: function(c) {
		var a = [];
		if(this.length == 0) return a;
		var p = 0;
		for(var i=0; i<this.length; i++) {
			if(this.charAt(i) == c) {
				a.push(this.substring(p, i));
				p = ++i;
			}
		}
		a.push(s.substr(p));
		return a;
	}
}, false);

Object.extend(String, {
	format: function(s) {
		for(var i=1; i<arguments.length; i++)
			s = s.replace("{" + (i -1) + "}", arguments[i]);
		return s;
	},
	isNullOrEmpty: function(s) {
		if(s == null || s.length == 0)
			return true;
		return false;
	}
}, false);

if(typeof addEvent == "undefined")
	addEvent = function(o, evType, f, capture) {
		if(o == null) return false;
		if(o.addEventListener) {
			o.addEventListener(evType, f, capture);
			return true;
		} else if (o.attachEvent) {
			var r = o.attachEvent("on" + evType, f);
			return r;
		} else {
			try{ o["on" + evType] = f; }catch(e){}
		}
	};
	
if(typeof removeEvent == "undefined")
	removeEvent = function(o, evType, f, capture) {
		if(o == null) return false;
		if(o.removeEventListener) {
			o.removeEventListener(evType, f, capture);
			return true;
		} else if (o.detachEvent) {
			o.detachEvent("on" + evType, f);
		} else {
			try{ o["on" + evType] = function(){}; }catch(e){}
		}
	};