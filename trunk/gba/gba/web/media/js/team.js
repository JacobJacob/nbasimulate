var service = new rpc.ServiceProxy("/services/user/", {asynchronous:false, methods: ['check_teamname_exist']});

//失去焦点的时候执行此方法，发送请求到后台php
team_name_check = function(){
  var team_name = $('team_name').value;
  var result = service.check_teamname_exist(team_name);
  if(result==0){
		$('can_shu').value=1;
		if($('message').innerHTML != ""){
			$('message').innerHTML = "恭喜您，该俱乐部名可以使用！" ;
	    }
  }else{
	  $('can_shu').value=-2;
	  $('message').innerHTML = "俱乐部名已存在，请重新输入！";
  } 
}

manager_name_check = function(){
  var manager_name = $('manager_name').value;
  var result = service.check_teamname_exist(manager_name);
  if(result==0){
		$('can_shu').value=1;
		if($('message').innerHTML != ""){
			$('message').innerHTML = "恭喜您，该经理名称可以使用！" ;
	    }
  }else{
	  $('can_shu').value=-2;
	  $('message').innerHTML = "经理名称已存在，请重新输入！";
  } 
}

//--------------------------------填充页面-------------------------------------


var strClothesURL="/site_media/images/team/";
var i;

function ChooseClothes(i)
{
	var div2 = document.getElementById("div2"); 
	parent.document.getElementById("imgClothes2").src=strClothesURL+i+".gif";
	parent.document.getElementById("hidClothes").value=strClothesURL+i+".gif";
	window.parent.document.getElementById("div2").style.display="none";
}
function ShowMenu2()
{
	if(document.getElementById("div2").style.display=="none")
	{
		document.getElementById("div2").style.display="";
	}
	else
	{
		document.getElementById("div2").style.display="none";
	}		
}
// 添加俱乐部判断
function checkform()
{
	if($('can_shu').value==-2)
	{
	  mes = "俱乐部名已存在，请重新输入！";
	  $('message').innerHTML=mes;
	  return false;
	}

	if(!isNull('team_name'))
	{
		$('message').innerHTML = "俱乐部名称不能为空！";
		return false;
	}
	//过滤内容
	if(is_political_words($('team_name').value)||is_dirty_words($('team_name').value))
	{
		$("message").innerHTML = "俱乐部名称不合法！";
		return false;
	}
	if(!isNaN($('team_name').value))
	{
		$('message').innerHTML = "俱乐部名称不能全是数字！";
		return false;
	}
	if(!checkCharLength('team_name',16))
	{
		$('message').innerHTML = "填写的俱乐部字符太长！";
		return false;
	}
	return true;
}
// 编辑俱乐部判断
check_edit_form = function (myform)
{
	if($('can_shu').value==-2)
	{
	  mes = "俱乐部名已存在，请重新输入！";
	  $('message').innerHTML=mes;
	  return false;
	}

	if(!isNull('team_name'))
	{
		$('message').innerHTML = "俱乐部名称不能为空！";
		return false;
	}
	//过滤内容
	if(is_political_words($('team_name').value)||is_dirty_words($('team_name').value))
	{
		$("message").innerHTML = "俱乐部名称不合法！";
		return false;
	}
	if(!isNaN($('team_name').value))
	{
		$('message').innerHTML = "俱乐部名称不能全是数字！";
		return false;
	}
	if(!checkCharLength('team_name',16))
	{
		$('message').innerHTML = "填写的俱乐部字符太长！";
		return false;
	}

	return submit_to_div(myform,'popup_inner');
}

function check_file() 
{
	var strFileName=$('myfile').value;
	if (strFileName=="")
	{
		alert("请选择要上传的文件");
		return false;
	}
	if(document.getElementById("vip").value != "is_vip")
	{
		alert("该功能只对vip用户开放！");
		return false;	
	}
	var strtype=strFileName.substring(strFileName.length-3,strFileName.length);
	strtype=strtype.toLowerCase();
	if (strtype=="jpg"||strtype=="gif")
	{}
	else{
		alert("这种文件类型不允许上传！\r\n只允许上传这几种文件：jpg、gif\r\n请选择别的文件并重新上传。");
		$('myfile').focus();
		return false;
	}
	return true;
}

check_manager_form = function (myform){

	if($('can_shu').value==-2){
	  mes = "经理名称已存在，请重新输入！";
	  $('message').innerHTML=mes;
	  return false;
	}

	if(!isNull('manager_name')){
		$('message').innerHTML = "经理名称不能为空！";
		return false;
	}
	//过滤内容
	if(is_political_words($('manager_name').value)||is_dirty_words($('manager_name').value))
	{
		$("message").innerHTML = "经理名称不合法！";
		return false;
	}
	if(!isNaN($('manager_name').value)){
		$('message').innerHTML = "经理名称不能全是数字！";
		return false;
	}
	if(!checkCharLength('manager_name',16)){
		$('message').innerHTML = "填写的经理名称字符太长！";
		return false;
	}

	return true;
}