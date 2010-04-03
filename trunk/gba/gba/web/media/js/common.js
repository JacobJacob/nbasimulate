/**
*	特别说明：本js目前只能支持的浏览器为：IE6.0-7.0  FireFox
*/

document.write('<script type="text/javascript" src="/site_media/js/rpc.js"></script>');
document.write('<script type="text/javascript" src="/site_media/js/prototype.js"></script>');
document.write('<script type="text/javascript" src="/site_media/js/hide_show.js"></script>');

//	预载入图片
var loadingImg = new Image();
loadingImg.src = "/site_media/images/loading.gif" ;

/**		更新主div的内容。页面内容的主div的id应该是main_content
*
*/
function update_content_div(func_id)
{
	document.getElementById('main_content').innerHTML='<div class="loading"><img src="'+loadingImg.src+'"></div>';
	var url = 'n.php?a='+func_id;
	var myAjax = new Ajax.Updater('main_content',url, {evalScripts:true});
}

/**		链接不直接刷新整个页面，而是刷新特定的div
*	用法：<a href="http://www.sina.com.cn" id="href_sina" onclick="return href_to_div('href_sina','sina_content');">SINA</a><div id="sina_content"></div>
*
*/
function href_to_div(myhref,div_id)
{
	myhref+="";		//不管传递进来的是href的this，还是一个字符串，都可以正常使用这个功能了。
	if(div_id=='main_content')
		main_url = myhref;
	document.getElementById(div_id).innerHTML='<div class="loading"><img src="'+loadingImg.src+'"></div>';
	var myAjax = new Ajax.Updater(div_id,myhref, {evalScripts:true, method:'get'});
	return false;
}
/**		链接不直接刷新整个页面，而是刷新特定的div
*		当需要关闭弹出的div并且刷新main_contentdiv的时候调用此方法
*
*/
function href_to_div_new(myhref,div_id)
{
	myhref+="";		//不管传递进来的是href的this，还是一个字符串，都可以正常使用这个功能了。
	document.getElementById(div_id).innerHTML='<div class="loading"><img src="'+loadingImg.src+'"></div>';
	var myAjax = new Ajax.Updater(div_id,myhref, {evalScripts:true});
	show_popup('floatBox');
	return false;
}

/**		表格不直接提交刷新整个页面，而是刷新特定的div。现在凑合用get吧，有时间了改成标准的写法
*		<form onsubmit="return submit_to_div(this);">
*/
function submit_to_div(target,content_div)
{
	//alert(target.action);
	if(content_div=='main_content')
		main_url = target.action;
	var len = target.elements.length;
	var tmpstr = target.action;
	if (tmpstr.length > 5)		// means have action
	{
		if(tmpstr.indexOf("?")!=-1){
			tmpstr+="&";
		}else{
			tmpstr+="?";
	    }
	}
	else
	{
		tmpstr="n.php?";
	}
	
	for (var i=0; i<len; i++)
	{
		var elem = target.elements[i];
		if (elem.id)
		{
			if(elem.type=="radio")
			{
				if(elem.checked)
				{
					if (i>0) tmpstr+="&";
						tmpstr += elem.name+"="+ encodeURIComponent($F(elem.id));				
				}
			}else if (elem.type=="checkbox")
			{
				if(elem.checked)
				{
					if (i>0) tmpstr+="&";
						tmpstr += elem.name+"="+ encodeURIComponent($F(elem.id));				
				}
			}else if (elem.type=="select-one")
			{
				if (i>0) tmpstr+="&";
					tmpstr += elem.name+"="+ encodeURIComponent(elem.options[elem.selectedIndex].value);	

			}else
			{
			//alert("name="+elem.name);
			//alert("value="+$F(elem.id));
				if (i>0) tmpstr+="&";
					tmpstr += elem.name+"="+ encodeURIComponent($F(elem.id));
			}
		}
	}
	//alert(tmpstr);
	if(content_div=='popup_inner')
	{
		document.getElementById('popup_inner').innerHTML = "Loading..";
		display_popup();
	}
	var myAjax = new Ajax.Updater(content_div,tmpstr,{evalScripts:true});
	return false;
}


/**		处理主菜单的滑动,加亮效果
*			主菜单div名字应该叫做menu_main_n，对应下级菜单的名字应该叫做menu_tab_n。
*			子菜单名称menu_sub_n_m
*		入口参数：
*			n：主菜单项目
*			m: 子菜单项目，当m>0的时候，令当前子菜单加亮，一般用于页面初始化。当m=0的时候，不对子菜单加亮，一般用于主菜单切换
*/
function menu_highlight(n,m)
{
	for (i=1;i<=9;i++)
	{
		var cname = (i==n) ? "active": "";
		var disp = (i==n) ? "" : "none";
		var main = document.getElementById("menu_main_"+i);
		if (main)
			main.className=cname;
		var sub = document.getElementById("menu_tab_"+i);
		if (sub)
			sub.style.display=disp;
	}
	
	if (m>0)
	{
		for (i=1;i<=9;i++)
		{
			var cname = (i==m) ? "active":"";
			var ele = document.getElementById("menu_sub_"+n+"_"+i);
			if (ele)
				ele.className=cname;
		}
	}
}

/**		三级菜单加亮
*
*/
function third_menu_highlight(n)
{
	for (i=1;i<9;i++)
	{
		var cname = (i==n) ? "active":"";
		var ele=document.getElementById("menu_third_"+i);
		if (ele)
			ele.className= cname;
	}
}

//	因为有可能四处都用到，所以放在这里，而不是放在js里面了。
//	todo:这个东西一旦放到html里面，就会跟prototype.js冲突。
function $(id) {
  return document.getElementById(id);
}



/**
*函数名:show_sub
*功能：级联菜单
*参数：sub_name 下级菜单的id
*	   selectValue 当前选中数值
*/
function show_sub(sub_name,selectValue)
{
	var array_name =  eval(sub_name + "_sub");		//数组名称
	var sel = document.getElementById(sub_name);		//取得二级菜单对象
	sel.length = 0;		//清空二级菜单
	for (i=0;i<array_name.length ;i++ )
		if (array_name[i][2] == selectValue)
			sel.options[sel.length] = new Option(array_name[i][1], array_name[i][0]);
}

/**
*函数名 : isDel 
*功能   : 询问是否删除，用于链接或者按钮的删除单条操作
*参数   : 无
*返回值 : true/false
*/
function isDel(){ 
	if(!confirm("确定要执行此操作吗？")){	
		return false; 
	}
	return true;
}

/**
*函数名 : isCheckDel  
*功能   : 选中并询问是否删除，当要删除的信息前有复选框时用此函数  
*参数   : name : 表单项check的name属性    
*返回值 : true/false
*/
function isCheckDel(name){
	var o =document.getElementsByName(name);
	deleteCount=0;
   	for(var i=0;i<o.length;i++){
		if(o[i].checked==true){
			deleteCount=1;
		}
	}
	if(deleteCount==0){
		window.alert("请选择要删除的信息！");
		return false;
	}
	else{
		if(!confirm("确定要删除这条信息吗？")){	
			return false; 
		}
		return true;
	}
}

/**
*函数名 : allSelect
*功能   : 复选框的全选,用于按钮或链接进行对复选框的全选操作 
*参数   : name        : 表单项check的name属性
*		: checkstatus : check的状态值
*返回值 : 无
*/
function allSelect(name,checkstatus){
	var o =document.getElementsByName(name);
	if(!o){ 
		return;
	}
	if(!o.length){
		o.checked=checkstatus;
	}else{
		for(var i=0;i<o.length;i++){
			o[i].checked=checkstatus;
		}
	}
}

/**
*函数名 : isNum
*功能   : 校验表单项是否为数字
*参数   : id : 表单项的id属性
*返回值 : true/false
*/
function isNum(id){ 
	var o =document.getElementById(id);
	if(isNaN(o.value.trim())){		
//		window.alert("请输入数字！");
		o.value = o.value.trim();
		o.focus();
		return false;
	}
	return true;
} 

/**
*函数名 : isNull  
*功能   : 校验表单项是否为空
*参数   : id : 表单项的id属性
*返回值 : true/false
*/
function isNull(id){ 
	var o =document.getElementById(id);
	if(o.value.trim() =="" || o.value.trim() == null ){
//		window.alert("不能为空！");
		o.value = o.value.trim();
		o.focus(); 
		return false;
	}
	return true;
}

/**
*函数名 : checkStrLen             
*功能   : 校验表单项的长度是否符合条件(包括中文 非中文)
*参数   : id  : 表单项的id属性    
*	      num : 用户设定的长度值
*返回值 : sum
*/
function checkStrLen(id,num){
	var o =document.getElementById(id);
	if(Len(o.value.trim()) > num){
//		alert("输入的字符太长！");
		o.value = o.value.trim();
		return false;
	}
	return true;
}
function Len(str){
    var i,sum;
    sum=0;
	for(i=0;i<str.length;i++){
		if ((str.charCodeAt(i)>=0) && (str.charCodeAt(i)<=255)){
			sum=sum+1;
		}else{
            sum=sum+2;
		}
    }
    return sum;
}

/**
*函数名 : checkIsEmail   
*功能   : 校验电子邮箱的书写格式       
*参数   : id : 表单项的id属性
*返回值 : true/false
*/
function checkIsEmail(id){ 
	var o =document.getElementById(id);
	if(! isEmail(o.value)){ 
//		alert("邮箱格式不正确！");
		o.focus();
		return false;
	}
	return true;
}
function isEmail(strEmail) {
	if (strEmail.search(/^([\.a-zA-Z0-9_-]){1,}@[A-Za-z0-9]+((.|-)[A-Za-z0-9]+)*.[A-Za-z0-9]+$/) != -1){
		return true;
	}
}

/**
*函数名 : checkValueSame  
*功能   : 判断两个表单项的值是否相同    
*参数   : id1 : 表单项的id属性  
*         id2 : 表单项的id属性
*返回值 : true/false
*/
function checkValueSame(id1,id2){
	var o =document.getElementById(id1);
	var j =document.getElementById(id2);
	if ((o.value=o.value.trim())!=(j.value=j.value.trim())){ 
		//alert("您两次输入的密码不一样！请重新输入。");
		return false;
	}
	return true;
}

/**
*函数名 : checkIsNumber      
*功能   : 表单项只能为数字和"-",用于电话/银行帐号验证上,可扩展到域名注册等,注意：不能以"-"作为开头或者结尾
*参数   : id : 表单项的id属性
*返回值 : true/false
*/
function checkIsNumber(id){ 
	var o =document.getElementById(id);
	if(!isNumber(o.value)) { 
//		alert("您的电话号码不合法！");
		o.focus();
		return false;
	}
	return true;
}
function isNumber(str){
	var Letters = "1234567890-"; //可以自己增加可输入值
	var i;
	var c;
	if(str.charAt( 0 )=='-')
		return false;
	if( str.charAt( str.length - 1 ) == '-' )
		return false;
	for( i = 0; i < str.length; i ++ ){ 
		c = str.charAt( i );
		if (Letters.indexOf( c ) < 0)
			return false;
	}
	return true;
}

/**
*函数名 : checkContain           
*功能   : 限定表单项不能输入的字符
*参数   : id : 表单项的id属性
*返回值 : true/false
*/
function checkContain(id){ 
	var o =document.getElementById(id);
	if((contain(o.value, "@#$%^&*()<>?|}-+{."))){  // 可以自己增加不可输入值
//		alert("输入了非法字符");
		o.focus();
		return false;
	}
	return true;
}
function contain(str,charset){
	for(i=0;i<charset.length;i++){
		if(str.indexOf(charset.charAt(i))>=0){
			return true;
		}
	}
}

/**
*函数名 : delSpaceTrim/delSpaceTrimLeft/delSpaceTrimRight/delSpaceTrimAll
*功能   : 删除字符串的空格
*参数   : id : 表单项的id属性
*返回值 : value
*/
//删除字符串俩端的空格
function delSpaceTrim(id){ 
	var o =document.getElementById(id);
	if(o.value=o.value.trim()){ 
		return o.value; 
	}
}
String.prototype.trim =function(){
    return this.replace(/(^\s*)|(\s*$)/g,""); 
}

//删除字符串左端的空格
function delSpaceTrimLeft(id){
	var o =document.getElementById(id);
	if(o.value=o.value.trimleft()){ 
		return o.value; 
	}
}
String.prototype.trimleft=function(){ 
    return this.replace(/(^\s*)/g,""); 
} 

//删除字符串右端的空格
function delSpaceTrimRight(id){ 	
	var o =document.getElementById(id);
	if(o.value=o.value.trimright()){ 
		return o.value; 
	}
}
String.prototype.trimright=function(){ 
    return this.replace(/(\s*$)/g,""); 
}

//删除所有空格
function delSpaceTrimAll(id){
	var o =document.getElementById(id);
	if(o.value=o.value.trimall()){ 
		return o.value; 
	}
}
String.prototype.trimall=function(){
    return this.replace(/\s+/g,"");
}

/**
*函数名 : checkDate     
*功能   : 校验时间格式,已精确到日
*参数   : id : 表单项的id属性
*返回值 : true/false
*/
function checkDate(id){
	var o =document.getElementById(id);
    var strLength;   
    if (o.value != ""){
		strLength= o.value.length;
	}else{
		strLength=0;   
    }   
    var tmpy="";   
    var tmpm="";   
    var tmpd="";   
    var status=0;   
  
    if ( strLength== 0){   
      return false;   
    }   
  
    for (i=0;i<strLength;i++){   
		if(o.value.charAt(i)== '-'){   
　　　		status++;   
　　	}   
		if (status>2){   
			return false;   
		}   
		if ((status==0) && (o.value.charAt(i)!='-')){   
			tmpy=tmpy+o.value.charAt(i);   
		}    
		if ((status==1) && (o.value.charAt(i)!='-')){   
			tmpm=tmpm+o.value.charAt(i);   
		}   
		if ((status==2) && (o.value.charAt(i)!='-')){   
			tmpd=tmpd+o.value.charAt(i);   
		}   
	}   

	year=new String (tmpy);   
	month=new String (tmpm);   
	day=new String (tmpd)   

	if((tmpy.length!=4 ) || (tmpm.length>2) || (tmpd.length>2)){   
		//alert("无效的日期!");
		return false;   
	}   
	if(!((1<=month) && (12>=month) && (31>=day) && (1<=day)) ){   
		//alert ("无效的月份或者日子!");   
		return false;   
    }    
	if(!((year % 4)==0) && (month==2) && (day==29)){   
		//alert ("这一年不是闰年!");   
		return false;   
	}   
	if((month<=7) && ((month % 2)==0) && (day>=31)){   
		//alert ("这月天数只有30天!");   
		return false;   
	}    
	if((month>=8) && ((month % 2)==1) && (day>=31)){   
		//alert ("这月天数只有30天!");   
		return false;   
	}     
	if((month==2) && (day==30)){   
		//alert("二月不存在30日");   
		return false;   
	}    
  return true;   
}  

/**    
*函数名 : chkWebsites
*功能   : 校验url格式 
*参数   : id : 表单项的id属性
*返回值 : true/false
*/
function chkWebsites(id){
	var o =document.getElementById(id);
	var myReg = /^(http:\/\/[a-z0-9]{1,5}\.)+([-\/a-z0-9]+\.)+[a-z0-9]{2,4}$/;   
	if(!myReg.test(o.value)) {
//		alert("URL不合法！例如:\n http://www.baidu.com");
		o.focus();
		return false;   
	}
	return true;   
}

/**
*函数名 : checkIDCard     
*功能   : 校验15位 或者18位 或者17位+X 的身份证号码
*参数   : value : 表单项的value属性
*返回值 : true/false
*/
function checkIDCard(id){
	var o =document.getElementById(id);
	var isIDcard=/^(\d{15}|\d{17}[x0-9])$/i; 
	if(!isIDcard.test(o.value)){ 
		//alert("请重新输入正确的身份证码！"); 
		o.focus();
		return false; 
	} 
	return true;
}

/**
*函数名 : checkIDCard     
*功能   : 公用为空判断
*参数   : id  : 需要检测不能为空的表单中元素的id数组
		  msg : 检测未通过时的提示信息
*返回值 : true/false
*/
function checkSubmitIsNull(id,msg){
	len = id.length;
	for(var i=0;i<len;i++){
		ret = isNull(id[i]);
		if(!ret){
			alert(msg[i]);
			break;
		}
	}
	return ret;
}
function checkSubmitIsNumber(id,msg){
	len = id.length;
	for(var i=0;i<len;i++){
		ret = isNum(id[i]);
		if(!ret){
			alert(msg[i]);
			break;
		}
	}
	return ret;
}

/**
*函数名 : 检测页面上字段     
*功能   : 检测数组中的字段是否有效
*参数   : id  : 需要检测不能为空的表单中元素的id数组
		  msg : 检测未通过时的提示信息
		  types:被检测数据的类型
*返回值 : true/false
*/
function checkSubmit(ids,msgs,types){
	len = ids.length;
	for(var i=0 ;i<len;i++){
		switch(types[i]){
			case 'NUM':   //数字型
				ret = isNum(ids[i]);
				break;
			case 'STRING'://字符串
				ret = isNull(ids[i]);
				break;
			case 'EMAIL': //邮件
				ret = checkIsEmail(ids[i]);
				break;
			case 'NUMBER'://电话号码
				ret = checkIsNumber(ids[i]);
				break;
			case 'DATE':  //日期检测
				ret = checkDate(ids[i]);
				break;
			case 'URL':  //网址
				ret = chkWebsites(ids[i]);
				break;
		}
		if (!ret)
		{
			alert(msgs[i]);
			return ret;
			break;
		}
	}
	return ret;
}
/**
*函数名 : 检测页面上的非法字段值     
*功能   : 检测页面上的非法字段值     
*参数   : type: 检测类型  LENGTH 检测长度（包含中文、非中文）
                          SUBMIT 检测提交时需要检测的元素
          id  : 需要检测不能为空的表单中元素的id数组
		  msg : 检测未通过时的提示信息
*返回值 : true/false
*/
function checkPublic(type,ids,msgs,other){
	len = ids.length;
	if (type=='LENGTH')
	{
		for(var i=0;i<len;i++){
			ret = checkCharLength(ids[i],other[i]);
			if(!ret){
				alert(msgs[i]);
				break;
			}
		}
		return ret;
	}
	if(type=="SUBMIT"){
		return checkSubmit(ids,msgs,other);
	}
	return true;
}

/* 收缩扩展多级列表
obj对应要控制的行
*/
function rowClicked(obj)
{
	
	var Browser = new Object();
	Browser.isMozilla = (typeof document.implementation != 'undefined') && (typeof document.implementation.createDocument != 'undefined') && (typeof HTMLDocument != 'undefined');
	Browser.isIE = window.ActiveXObject ? true : false;
	Browser.isFirefox = (navigator.userAgent.toLowerCase().indexOf("firefox") != - 1);
	Browser.isSafari = (navigator.userAgent.toLowerCase().indexOf("safari") != - 1);
	
	var imgPlus = new Image();
	imgPlus.src = "../images/menu_plus.gif";

  obj = obj.parentNode.parentNode;

  var tbl = document.getElementById("morelevel");
  var lvl = parseInt(obj.className);
  var fnd = false;

  for (i = 0; i < tbl.rows.length; i++)
  {
      var row = tbl.rows[i];

      if (tbl.rows[i] == obj)
      {
          fnd = true;
      }
      else
      {
          if (fnd == true)
          {
              var cur = parseInt(row.className);
              if (cur > lvl)
              {
                  row.style.display = (row.style.display != 'none') ? 'none' : (Browser.isIE) ? 'block' : 'table-row';
              }
              else
              {
                  fnd = false;
                  break;
              }
          }
      }
  }

  for (i = 0; i < obj.cells[0].childNodes.length; i++)
  {
      var imgObj = obj.cells[0].childNodes[i];
      if (imgObj.tagName == "IMG" && imgObj.src != '../images/menu_arrow.gif')
      {
          imgObj.src = (imgObj.src == imgPlus.src) ? '../images/menu_minus.gif' : imgPlus.src;
      }
  }
}

/**
*函数名 : 检测输入的字符长度
*功能   : 检测输入的字符长度（包括中文。非中文）
*参数   : id  : 要检测的控件
		  num : 最大长度
*返回值 : true/false
*/
function checkCharLength(id,num){  
	var o =document.getElementById(id);
	var a=0; 
    for(var i=0;i<o.value.trim().length;i++)   
    {
		if(o.value.charCodeAt(i)>255)  //大于255的才是中文                  
		{
			a+=2;
		}  
		else
		{
			a+=1;
		}  
	} 
    if(a>num){
//		alert("超过指定的字符长度！");
		o.value=o.value.trim();
		o.focus();
		return false;   
      }   
	return true; 
	
}  

var menu_n;
var menu_m;
function sub_menu_highlight(n,m)
{
	if (m>0)
	{
		for (i=1;i<=9;i++)
		{
			var cname = (i==m) ? "active":"";
			var ele = document.getElementById("menu_sub_"+n+"_"+i);
			if (ele)
				ele.className=cname;
		}
	}
	menu_n = n; //为了点击一级栏目自动显示二级栏目的第一顺位的目录内容
	menu_m = m; //
}
//导航

var navClick = null;
var showClick = null;
var showOver = null;
var tid;
var dration =100;
function nav_show(nav, show)
{
	clearTimeout(tid);
	if(showClick)showClick.style.display = 'none';
	if(navClick)navClick.className = '';
	var navObj = document.getElementById(nav);
	var showObj = document.getElementById(show);
	navObj.className = 'active';
	showObj.style.display = '';
	navClick = navObj
	showClick = showObj;
	var menu_zong = nav.substr(3,1); //得到一级栏目数
	if(menu_n != menu_zong)
	{
		//window.location.href = document.getElementById("menu_sub_href_"+menu_zong+"_1").href;
	}
}
function nav_over(nav, show)
{
	clearTimeout(tid);
	if(showOver)showOver.style.display = 'none';
	var navObj = document.getElementById(nav);
	var showObj = document.getElementById(show);
	navObj.className = 'over';
	showObj.style.display = '';
	showOver = showObj;
	if(showClick != showObj)
		if(showClick)showClick.style.display = 'none';
}
function nav_out(nav, show)
{
	var navObj = document.getElementById(nav);
	var showObj = document.getElementById(show);
	if(navObj == navClick)
		navObj.className = 'active';
	else
		navObj.className = 'blur';
	clearTimeout(tid);
	tid = setTimeout(show_out_dration, dration);
}
function show_over()
{
	clearTimeout(tid);
	/*
	if(showOver)
		if(showClick)showClick.style.display = 'none';
	if(showOver)showOver.style.display = '';
	*/
}
function show_out()
{
	clearTimeout(tid);
	tid = setTimeout(show_out_dration, dration);
}
function show_out_dration()
{
	if(showClick)showClick.style.display = '';
	if(showOver != showClick)
		if(showOver)showOver.style.display = 'none';
}
/**
*  显示经理详细信息（职业、业余、荣誉、短信、约战）
*/
function show_s_table(n,m,count) {

	for (var i=1;i<=count;i++) {
		if (i==m) {
			document.getElementById("tab"+n+"_"+m).style.display="";
			document.getElementById("wz"+n+"_"+m).className="active";
		}else {
			document.getElementById("tab"+n+"_"+i).style.display="none";
			document.getElementById("wz"+n+"_"+i).className="";
		}
	}
}


/* 是污言秽语吗 */
function is_dirty_words(msg) {
  var re = new Array();
  var i = 0;

  re[i++] = /[操屄婊娼妓肛屌肏痿淫奸嫖鸨姘毬卵干日贱靠]/i;
  re[i++] = /[垃拉啦](\s|　){0,}[圾及几级鸡基击]/i;
  re[i++] = /[你他妳](\s|　){0,}[妈姐妹奶姥娘母婆姑婶姨]/i;
  re[i++] = /[骚臭草狗娘妈吗傻煞杀沙mＭｍsＳｓ](\s|　){0,}(逼|比|笔|[Ｂｂb]|[XＸｘ])/i;
  //re[i++] = /(\s|　|，|。|！|,|.|!|~|～){0,}[日](\s|\w|　|，|。|！|,|.|!|~|～){0,}$/i;
  //re[i++] = /(\s|　|，|。|！|,|.|!|~|～){0,}[靠](\s|\w|　|，|。|！|,|.|!|~|～){0,}$/i;
  re[i++] = /[<>]/i;
  re[i++] = /[曰日草干插叉弄玩](\s|　){0,}(逼|[Ｂｂb]|[XＸｘ])/i;
  re[i++] = /[奶妈娘吗](\s|　){0,}[的得德]/i;
  re[i++] = /[鸡谿机基几Ｊｊj](\s|　){0,}[谿鸡巴吧把八８8]/i;
  re[i++] = /[曰日草干插叉弄玩睡XＸｘ](\s|　){0,}[你妳他她它死]/i;
  re[i++] = /[阴因音荫殷](\s|　){0,}[毛茎蒂道囊门户]/i;
  re[i++] = /[你妳他](\s|　){0,}[老媳](\s|　){0,}[婆妇娘]/i;
  re[i++] = /[狗我](\s|　){0,}[曰日]/i;
  re[i++] = /[做作造](\s|　){0,}[爱]/i;
  re[i++] = /[贱](\s|　){0,}[人货]/i;
  re[i++] = /[咪](\s|　){0,}[咪]/i;
  re[i++] = /[乌](\s|　){0,}[龟]/i;
  re[i++] = /[阳](\s|　){0,}[萎]/i;
  re[i++] = /[王](\s|　){0,}[八]/i;
  re[i++] = /[乳](\s|　){0,}[房]/i;
  re[i++] = /[包](\s|　){0,}[皮]/i;
  re[i++] = /[高](\s|　){0,}[潮]/i;
  re[i++] = /[搞](\s|　){0,}[你妳]/i;
  re[i++] = /[奶](\s|　){0,}[子]/i;
  re[i++] = /[屁PＰｐ](\s|　){0,}[眼]/i;
  re[i++] = /[龟](\s|　){0,}[头]/i;
  re[i++] = /[小](\s|　){0,}[穴]/i;
  re[i++] = /[精](\s|　){0,}[液]/i;
  re[i++] = /[射](\s|　){0,}[精]/i;
  re[i++] = /[经](\s|　){0,}[血]/i;
  re[i++] = /[月](\s|　){0,}[经]/i;
  re[i++] = /[尿](\s|　){0,}[道]/i;
  re[i++] = /[早](\s|　){0,}[泄]/i;
  re[i++] = /[白](\s|　){0,}[痴]/i;
  re[i++] = /[鸟](\s|　){0,}[人]/i;
  re[i++] = /[老](\s|　){0,}[母]/i;
  re[i++] = /[女](\s|　){0,}[干]/i;
  re[i++] = /[土](\s|　){0,}[立]/i;
  re[i++] = /[土](\s|　){0,}[及]/i;
  re[i++] = /[女](\s|　){0,}[马]/i;
  re[i++] = /[女](\s|　){0,}[良]/i;
  re[i++] = /[女](\s|　){0,}[乃]/i;
  re[i++] = /[女](\s|　){0,}[且]/i;
  re[i++] = /[女](\s|　){0,}[未]/i;
  re[i++] = /[女](\s|　){0,}[表]/i;
  re[i++] = /[女](\s|　){0,}[昌]/i;
  re[i++] = /[女](\s|　){0,}[支]/i;
  re[i++] = /[女](\s|　){0,}[票]/i;
  re[i++] = /[女](\s|　){0,}[老]/i;
  re[i++] = /[女](\s|　){0,}[息]/i;
  re[i++] = /[女](\s|　){0,}[末]/i;
  re[i++] = /[女](\s|　){0,}[古]/i;
  re[i++] = /[女](\s|　){0,}[审]/i;
  re[i++] = /[女](\s|　){0,}[夷]/i;
  re[i++] = /[女](\s|　){0,}[并]/i;
  re[i++] = /[月](\s|　){0,}[工]/i;
  re[i++] = /[又](\s|　){0,}[鸟]/i;
  re[i++] = /[口](\s|　){0,}[米]/i;
  re[i++] = /[身](\s|　){0,}[寸]/i;
  re[i++] = /[米](\s|　){0,}[青]/i;
  re[i++] = /[马](\s|　){0,}[蚤]/i;
  re[i++] = /[贱处荡](\s|　){0,}[女妇]/i;
  re[i++] = /[口性](\s|　){0,}[交]/i;
  re[i++] = /(土立|土及|女马|女良|女乃|女且|女未|女干|女表|女昌|女支|月工|女票|女老|又鸟|口米|身寸|米青|女息|女末|马蚤|女古|女审|女夷|女并)/i;
  re[i++] = /[sＳｓ](\s|　){0,}[eＥｅ](\s|　){0,}[xＸｘ]/i;
  re[i++] = /[TＴｔ](\s|　){0,}[MＭｍ](\s|　){0,}[DＤｄ]/i;
  re[i++] = /[NＮｎ](\s|　){0,}[NＮｎ](\s|　){0,}[DＤｄ]/i;
  re[i++] = /[aＡａ](\s|　){0,}[sＳｓ](\s|　){0,}[sＳｓ]/i;
  re[i++] = /[SＳｓ](\s|　){0,}[HＨｈ](\s|　){0,}[IＩｉ](\s|　){0,}[TＴｔ]/i;
  re[i++] = /[fＦｆ](\s|　){0,}[uＵｕ](\s|　){0,}[cＣｃ](\s|　){0,}[kＫｋ]/i;

  for (i=0; i<re.length; i++) {
    if (re[i].test(msg)) return true;
  }

  return false;
}

/* 是政治词语吗 */
function is_political_words(msg) {
  var re = new Array();
  var i = 0;
  re[i++] = /[四](\s|　){0,}[月](\s|　){0,}[十](\s|　){0,}[六七](\s|　){0,}[日]/i;
  re[i++] = /[天](\s|　){0,}[安](\s|　){0,}[门](\s|　){0,}[广](\s|　){0,}[场]/i;
  re[i++] = /[爱](\s|　){0,}[国](\s|　){0,}[者](\s|　){0,}[同](\s|　){0,}[盟]/i;
  re[i++] = /[五](\s|　){0,}[月](\s|　){0,}[四](\s|　){0,}[日]/i;
  re[i++] = /[江](\s|　){0,}[泽](\s|　){0,}[民]/i;
  re[i++] = /[李](\s|　){0,}[洪](\s|　){0,}[志]/i;
  re[i++] = /[朱](\s|　){0,}[镕](\s|　){0,}[基]/i;
  re[i++] = /[真](\s|　){0,}[善](\s|　){0,}[忍]/i;
  re[i++] = /[天](\s|　){0,}[安](\s|　){0,}[门]/i;
  re[i++] = /[周](\s|　){0,}[恩](\s|　){0,}[来]/i;
  re[i++] = /[胡](\s|　){0,}[锦](\s|　){0,}[涛]/i;
  re[i++] = /[温](\s|　){0,}[家](\s|　){0,}[宝]/i;
  re[i++] = /[赵](\s|　){0,}[紫](\s|　){0,}[阳]/i;
  re[i++] = /[四](\s|　){0,}[一](\s|　){0,}[六七]/i;
  re[i++] = /[毛](\s|　){0,}[泽](\s|　){0,}[东]/i;
  re[i++] = /[法](\s|　){0,}[轮]/i;
  re[i++] = /[台](\s|　){0,}[独]/i;
  re[i++] = /[李](\s|　){0,}[鹏]/i;
  re[i++] = /[大](\s|　){0,}[法]/i;
  re[i++] = /[六](\s|　){0,}[四]/i;
  re[i++] = /[抗](\s|　){0,}[议]/i;
  re[i++] = /[集](\s|　){0,}[会]/i;
  re[i++] = /[堵](\s|　){0,}[截]/i;
  re[i++] = /[堵](\s|　){0,}[路]/i;
  re[i++] = /[抵](\s|　){0,}[制]/i;
  re[i++] = /[日](\s|　){0,}[货]/i;
  re[i++] = /[游](\s|　){0,}[行]/i;
  re[i++] = /[日曰](\s|　){0,}[本]/i;
  re[i++] = /[聚](\s|　){0,}[集]/i;
  re[i++] = /[保](\s|　){0,}[钓]/i;
  re[i++] = /[示](\s|　){0,}[威]/i;
  re[i++] = /[维](\s|　){0,}[权]/i;
  re[i++] = /[老](\s|　){0,}[兵]/i;
  re[i++] = /[复](\s|　){0,}[员]/i;
  re[i++] = /[军](\s|　){0,}[转]/i;
  re[i++] = /[退](\s|　){0,}[役]/i;
  re[i++] = /[上](\s|　){0,}[访]/i;
	
  /*防止在游戏中使用这些关键字*/
  re[i++] = /[大](\s|　){0,}[联](\s|　){0,}[盟](\s|　){0,}[管](\s|　){0,}[理](\s|　){0,}[员]/i;	
  re[i++] = /[管](\s|　){0,}[理](\s|　){0,}[员]/i;
  re[i++] = /[验](\s|　){0,}[证](\s|　){0,}[码]/i;
  re[i++] = /[校](\s|　){0,}[验](\s|　){0,}[码]/i;
  re[i++] = /[系](\s|　){0,}[统]/i;
  re[i++] = /[公](\s|　){0,}[告]/i;
  re[i++] = /[活](\s|　){0,}[动](\s|　){0,}[奖](\s|　){0,}[励]/i;
  re[i++] = /[活](\s|　){0,}[动](\s|　){0,}[发](\s|　){0,}[奖]/i;
  re[i++] = /[获](\s|　){0,}[奖](\s|　){0,}[信](\s|　){0,}[息]/i;
  re[i++] = /[活](\s|　){0,}[动](\s|　){0,}[奖](\s|　){0,}[品]/i;
  re[i++] = /[奖](\s|　){0,}[品](\s|　){0,}[发](\s|　){0,}[放]/i;
  re[i++] = /[系](\s|　){0,}[统](\s|　){0,}[消](\s|　){0,}[息]/i;
  for (i=0; i<re.length; i++) {
    if (re[i].test(msg)) return true;
  }

  return false;
}

/* 首页时间显示 */
function sys_clock() 
{ 
	sec = SEC+a;                   //(GMT+8:00)时区:中国标准时间 
	S=sec%60;                                               //秒 
	I=Math.floor(sec/60)%60;                                //分 
	H=Math.floor(sec/3600)%24;                              //时 
	W='四五六日一二三'.charAt(Math.floor(sec/86400)%7);     //星期几 
	if(S<10) S='0'+S; 
	if(I<10) I='0'+I; 
	if(H<10) H='0'+H; 
	if (H=='00' & I=='00' & S=='00') D=D+1;                 //日进位
	//------------------------------------------------------判断是否为二月份****** 
	if (M==2) 
	{
		//--------------------------------------------------是闰年(二月有29天) 
		if (Y%4==0 & Y%100!=0 || Y%400==0)
		{ 
			if (D==30){M=M+1;D=1;}                          //月份进位 
		}
		//--------------------------------------------------非闰年(二月有28天) 
		else 
		{ 
			if (D==29){M=M+1;D=1;}                          //月份进位 
		} 
	}
	//------------------------------------------------------不是二月份的月份****** 
	else 
	{ 
		//--------------------------------------------------小月(30天)
		if (M==4 || M==6 || M==9 || M==11) 
		{ 
			if (D==31) {M=M+1;D=1;}                         //月份进位 
		}
		//--------------------------------------------------大月(31天) 
		else
		{
			if (D==32){M=M+1;D=1;}                          //月份进位 
		} 
	} 
	if (M==13) {Y=Y+1;M=1;}                                 //年份进位 
	timeStr=Y+'/'+M+'/'+D+'/'+'&nbsp;&nbsp;&nbsp;'+H+':'+I+':'+S;
	document.getElementById('sj_notice').innerHTML = timeStr; 
	a++; 
} 
