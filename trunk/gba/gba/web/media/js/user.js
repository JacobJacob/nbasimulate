var service = new rpc.ServiceProxy("/services/user/", {asynchronous:false, methods: ['check_user_exist']});

//失去焦点的时候执行此方法，发送请求到后台php
function usernameResult(result) {
  $('notice').innerHTML = result;
}

function jiao_se_ming_chengCheck() {
  var input = $('jiao_se_ming_cheng').value;
  //rpc.jiao_se_ming_cheng_check(input, usernameResult);
  result = service.check_user_exist('jacky');
  alert(result);
}


//失去焦点的时候执行此方法，发送请求到后台php
function usernameResult2(result) {
  var mes="";
  if(result==1)
  {
	  mes = "该经理存在！请点击添加";
	  $('can_shu').value=1;
  }
  else if(result==-1)
  {
	  mes = "该经理不存在！";
	  $('can_shu').value=-1;
  }
  else if(result==-1)
  {
	  $('can_shu').value=-2;
  }
 
  $('notice').innerHTML = mes;
}

function hao_you_ming_chengCheck() {
  var input = $('jiao_se_ming_cheng').value;
  //rpc.hao_you_ming_cheng_check(input, usernameResult2);
  result = service.check_user_exist('jacky');
  usernameResult2(result);
}
//--------------------------------填充页面-------------------------------------

function htmlResult(result) {
  $('left_logo_html').innerHTML = result['left_logo_html'];
  $('left_info_html').innerHTML = result['left_info_html'];
  $('left_chat_html').innerHTML = result['left_chat_html'];
  $('right_html').innerHTML = result['right_html'];
}

/**
*	填充页面
*/
function tian_chong(str)
{	
	rpc.tian_chong(htmlResult);
}

/**
*   导航
*/
function show_s_table(n,m,count) {
	for (var i=1;i<=count;i++) {
		if (i==m) {
			document.getElementById("tab"+n+"_"+m).style.display=""
			document.getElementById("wz"+n+"_"+m).className="active"
		}else {
			document.getElementById("tab"+n+"_"+i).style.display="none"
			document.getElementById("wz"+n+"_"+i).className=""
		}
	}
}
//------------------------------------end---------------------------------------

/**
*函数名:jiao_se_check_form
*功能：角色表单提交验证
*参数：isNullName  id名称
*	   isNullMsg   弹出的消息
*/
function jiao_se_check_form()
{
	//经理名称为空判断
	if(!isNull("jiao_se_ming_cheng"))
	{
		$("notice").innerHTML = "经理名称不能为空！";
		return false;
	}
	//过滤内容
	var jiao_se_ming = $("jiao_se_ming_cheng").value;
	if(is_political_words(jiao_se_ming)||is_dirty_words(jiao_se_ming))
	{
		$("notice").innerHTML = "经理名称不合法！";
		return false;
	}
	//判断经理名称是否过长
	if(!checkCharLength("jiao_se_ming_cheng",16))
	{
		$("notice").innerHTML = "经理名称输入过长！";
		return false;
	}
	var idx = $("di_qu").selectedIndex;
	//alert(idx);
	//alert($("di_qu").options[idx].value);
	//地区是否为空
	if($("di_qu").options[idx].value == -1)
	{
		$("di_qu_msg").innerHTML = "请选择地区！";
		return false;
	}
	//判断QQ号码
	$("qq_msg").innerHTML = "";
	if(isNull("qq"))
	{
		//alert("qq号");
		if(!isNum("qq"))
		{
			$("qq_msg").innerHTML = "QQ号码格式不合法！";
			return false;
		}
	}
	//判断Email格式
	$("email_msg").innerHTML = "";
	if(isNull("js_email"))
	{
		//alert("email地址");
		if(!checkIsEmail("js_email"))
		{
			$("email_msg").innerHTML = "Email格式不合法！";
			return false;
		}
	}
	//判断协议是否被阅读
	$("xie_yi_msg").innerHTML = "";
	if(!$("xie_yi").checked)
	{
		$("xie_yi_msg").innerHTML = "请确认是否阅读并同意注册协议！";
		return false;
	}
//	//判断QQ号码格式
//	if(!checkCharLength("qq",11))
//	{
//		$("qq").innerHTML = "QQ号码输入不合法！";
//		return false;
//	}
	return true;
}

/**
*函数名:nian_option
*功能：产生年
*参数：N      传入的年最大值
*	   str    HTML下拉框语句
*/
function nian_option(N)
{
	var i,str;
	str="";
	for(i=1960;i<=N;i++) 
	{
		if(i<10) str="<option value='0"+i+"'>"+"0"+i+"</option>";
		else str="<option value='"+i+"'>"+i+"</option>";   
		document.write(str);
	}  
}

/**
*函数名:yue_ri_option
*功能：产生月和日
*参数：N      传入的月和日的最大值
*	   str    HTML下拉框语句
*/
function yue_ri_option(N)
{
	var i,str;
	str="";
	for(i=1;i<=N;i++) 
	{
		if(i<10) str="<option value='0"+i+"'>"+"0"+i+"</option>";
		else str="<option value='"+i+"'>"+i+"</option>";   
		document.write(str);
	}  
}

/**
*函数名:ri_qi_onchange
*功能：选择年月是改变月日
*参数：nian   年
*      yue    月
*      ri     日
*/
function ri_qi_onchange()
{
	var nian,yue,ri;
	ri=31;
	yue=document.getElementById('yue').value;
	if(yue=="04" || yue=="06" || yue=="09" || yue=="11")
	ri=30;
	if(yue=="02") 
	{
		nian=document.getElementById('nian').value;
		if((nian%4==0 && nian%100!=0) || nian%400==0)
			ri=29;
		else ri=28;   
	}
	flen=document.getElementById('ri').length; 
	document.getElementById('ri').length =ri;
	i=flen+1;
	for(i;i<=ri;i++) 
	{
		document.getElementById('ri').options(i-1).text=i;
		document.getElementById('ri').options(i-1).value=i;
	}
	ri_qi_zu_he();                       //将日期组合
}

/**
*函数名:ri_qi_zu_he
*功能：将日期组合
*参数：nian   年
*      yue    月
*      ri     日
*/
function ri_qi_zu_he()
{
	nian=document.getElementById('nian').value;
	yue=document.getElementById('yue').value;
	ri=document.getElementById('ri').value;
	document.getElementById('sheng_ri').value = nian +"-"+ yue +"-"+ ri;
}

/*
*	添加好友的判断
*
*/
hao_you_check= function(myform)
{
  var mes = "";
  if($('can_shu').value==-1)
  {
	  mes = "该经理不存在！";
	  $('notice').innerHTML=mes;
	  return false;
  }

  if(!isNull('jiao_se_ming_cheng'))
  {
	  mes = "请输入经理名！";
	  $('notice').innerHTML=mes;
	  return false;
  }
  if($('can_shu').value==-2)
  {
	  return false;
  }
  return submit_to_div(myform,'popup_inner');
}