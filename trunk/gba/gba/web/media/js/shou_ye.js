//用于首页左侧数据的刷新
var sy_rpc = new PHPRPC_Client('ajax/sy.php', ['shua_xin_zuo']);

//失去焦点的时候执行此方法，发送请求到后台php
function zuoResult(result) {
  $('sy_zuo').innerHTML = result;
}

function sy_zuo_shua_xin() {
  sy_rpc.shua_xin_zuo(zuoResult);
}


/* 081104001 ws begin */
/*
舌签构造函数
SubShowClass(ID[,eventType][,defaultID][,openClassName][,closeClassName])
version 1.30
*/
eval(function(p,a,c,k,e,r){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--)r[e(c)]=k[c]||e(c);k=[function(e){return r[e]}];e=function(){return'\\w+'};c=1};while(c--)if(k[c])p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c]);return p}('m 6(a,b,c,d,e){5.u=6.$(a);7(5.u==k&&a!="n"){M y N("6(p)参数错误:p 对像存在!(Z:"+a+")")};7(!6.q){6.q=y 1r()};5.p=6.q.9;6.q.1d(5);5.1s=r;5.8=[];5.10=c==k?0:c;5.E=5.10;5.11=d==k?"1t":d;5.13=e==k?"":e;5.F=r;j f=P("6.q["+5.p+"].F = z");j g=P("6.q["+5.p+"].F = r");7(a!="n"){7(5.u.v){5.u.v("14",f)}w{5.u.Q("15",f,r)}};7(a!="n"){7(5.u.v){5.u.v("16",g)}w{5.u.Q("17",g,r)}};7(18(b)!="1u"){b="1v"};b=b.1w();1x(b){U"14":5.B="15";G;U"16":5.B="17";G;U"1y":5.B="1z";G;U"1A":5.B="1B";G;1C:5.B="1D"};5.H=r;5.A=k;5.R=1E};6.t.1F="1.1G";6.t.1H="1I";6.t.1e=m(a,b,c,d,e){7(6.$(a)==k&&a!="n"){M y N("1e(1f)参数错误:1f 对像存在!(Z:"+a+")")};j f=5.8.9;7(c==""){c=k};5.8.1d([a,b,c,d,e]);j g=P(\'6.q[\'+5.p+\'].C(\'+f+\')\');7(a!="n"){7(6.$(a).v){6.$(a).v("1J"+5.B,g)}w{6.$(a).Q(5.B,g,r)}};7(f==5.10){7(a!="n"){6.$(a).V=5.11};7(6.$(b)){6.$(b).J.W=""};7(5.p!="n"){7(c!=k){5.u.J.1g=c}};7(d!=k){S(d)}}w{7(a!="n"){6.$(a).V=5.13};7(6.$(b)){6.$(b).J.W="n"}};j h=P("6.q["+5.p+"].F = z");j i=P("6.q["+5.p+"].F = r");7(6.$(b)){7(6.$(b).v){6.$(b).v("14",h)}w{6.$(b).Q("15",h,r)};7(6.$(b).v){6.$(b).v("16",i)}w{6.$(b).Q("17",i,r)}}};6.t.C=m(a,b){7(18(a)!="19"){M y N("C(1h)参数错误:1h 不是 19 类型!(Z:"+a+")")};7(b!=z&&5.E==a){K};j i;T(i=0;i<5.8.9;i++){7(i==a){7(5.8[i][0]!="n"){6.$(5.8[i][0]).V=5.11};7(6.$(5.8[i][1])){6.$(5.8[i][1]).J.W=""};7(5.p!="n"){7(5.8[i][2]!=k){5.u.J.1g=5.8[i][2]}};7(5.8[i][3]!=k){S(5.8[i][3])}}w 7(5.E==i||b==z){7(5.8[i][0]!="n"){6.$(5.8[i][0]).V=5.13};7(6.$(5.8[i][1])){6.$(5.8[i][1]).J.W="n"};7(5.8[i][4]!=k){S(5.8[i][4])}}};5.E=a};6.t.1a=m(){7(s.9!=5.8.9){M y N("1a()参数错误:参数数量与标签数量不符!(9:"+s.9+")")};j a=0,i;T(i=0;i<s.9;i++){a+=s[i]};j b=1K.1a(),1b=0;T(i=0;i<s.9;i++){1b+=s[i]/a;7(b<1b){5.C(i);G}}};6.t.1i=m(){7(s.9!=5.8.9){M y N("1i()参数错误:参数数量与标签数量不符!(9:"+s.9+")")};7(!(/^\\d+$/).1j(6.D)){K};j a=0,i;T(i=0;i<s.9;i++){a+=s[i]};j b=6.D%a;7(b==0){b=a};j c=0;T(i=0;i<s.9;i++){c+=s[i];7(c>=b){5.C(i);G}}};6.t.1L=m(a){7(18(a)=="19"){5.R=a};X(5.A);5.A=1c("6.q["+5.p+"].Y()",5.R);5.H=z};6.t.Y=m(){7(5.H==r||5.F==z){K};5.1k()};6.t.1k=m(){j a=5.E;a++;7(a>=5.8.9){a=0};5.C(a);7(5.H==z){X(5.A);5.A=1c("6.q["+5.p+"].Y()",5.R)}};6.t.1M=m(){j a=5.E;a--;7(a<0){a=5.8.9-1};5.C(a);7(5.H==z){X(5.A);5.A=1c("6.q["+5.p+"].Y()",5.R)}};6.t.1N=m(){X(5.A);5.H=r};6.$=m(a){7(x.1l){K S(\'x.1l("\'+a+\'")\')}w{K S(\'x.1O.\'+a)}};6.1m=m(l){j i="",I=l+"=";7(x.L.9>0){j a=x.L.1n(I);7(a!=-1){a+=I.9;j b=x.L.1n(";",a);7(b==-1)b=x.L.9;i=1P(x.L.1Q(a,b))}};K i},6.1o=m(O,o,l,I){j i="",c="";7(l!=k){i=y 1p((y 1p).1R()+l*1S);i="; 1T="+i.1U()};7(I!=k){c=";1V="+I};x.L=O+"="+1W(o)+i+c};6.D=6.1m("1q");7((/^\\d+$/).1j(6.D)){6.D++}w{6.D=1};6.1o("1q",6.D,12);',62,121,'|||||this|SubShowClass|if|label|length||||||||||var|null||function|none||ID|childs|false|arguments|prototype|parentObj|attachEvent|else|document|new|true|autoPlayTimeObj|eventType|select|sum|selectedIndex|mouseIn|break|autoPlay||style|return|cookie|throw|Error||Function|addEventListener|spaceTime|eval|for|case|className|display|clearInterval|autoPlayFunc|value|defaultID|openClassName||closeClassName|onmouseover|mouseover|onmouseout|mouseout|typeof|number|random|percent|setInterval|push|addLabel|labelID|background|num|order|test|nextLabel|getElementById|readCookie|indexOf|writeCookie|Date|SSCSum|Array|lock|selected|string|onmousedown|toLowerCase|switch|onclick|click|onmouseup|mouseup|default|mousedown|5000|version|30|author|mengjia|on|Math|play|previousLabel|stop|all|unescape|substring|getTime|3600000|expires|toGMTString|domain|escape'.split('|'),0,{}))
/* 081104001 ws end */

function PLabel(SubObjID,SubName){
	var SubObj = SubShowClass.childs[SubObjID];
	SubObj.previousLabel();
	ChkArr(SubObjID,SubName);
}
function NLabel(SubObjID,SubName){
	var SubObj = SubShowClass.childs[SubObjID];
	SubObj.nextLabel();
	ChkArr(SubObjID,SubName);
}
function ChkArr(SubObjID,SubName){
	var SubObj = SubShowClass.childs[SubObjID];
	if(SubObj.selectedIndex == 0){
		SubShowClass.$("SSArr_" + SubName + "_L").className = "arrLeft_d";
		SubShowClass.$("SSArr_" + SubName + "_L").onclick = null;
	}else{
		SubShowClass.$("SSArr_" + SubName + "_L").className = "arrLeft";
		SubShowClass.$("SSArr_" + SubName + "_L").onclick = Function("PLabel(" + SubObjID + ",'" + SubName + "')");
	};
	if(SubObj.selectedIndex >= SubObj.label.length - 1){
		SubShowClass.$("SSArr_" + SubName + "_R").className = "arrRight_d";
		SubShowClass.$("SSArr_" + SubName + "_R").onclick = null;
	}else{
		SubShowClass.$("SSArr_" + SubName + "_R").className = "arrRight";
		SubShowClass.$("SSArr_" + SubName + "_R").onclick = Function("NLabel(" + SubObjID + ",'" + SubName + "')");
	};
}

try{document.execCommand('BackgroundImageCache', false, true);}catch(e){}

