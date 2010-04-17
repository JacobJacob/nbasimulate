var rpc = new PHPRPC_Client('ajax/jlb.php', ['jlb_ming_cheng_check']);

//失去焦点的时候执行此方法，发送请求到后台php
function usernameResult(result) {
  var mes;

	if(result==1)
	{
		$('can_shu').value=1;
		if($('message').innerHTML != "")
			$('message').innerHTML = "恭喜您，该俱乐部名可以使用！" ;
	}
	if(result==3)
	{
		$('can_shu').value=1;
		if($('message').innerHTML != "")
			$('message').innerHTML = "该俱乐部名可以使用！" ;
	}

  if(result==-2)
  {
	  mes = "俱乐部名已存在，请重新输入！";
	  $('can_shu').value=-2;
	  $('message').innerHTML = mes;
  } 
}

function jlb_ming_chengCheck() {						//判断是否执行此方法
  var input = $('ju_le_bu_ming').value;
  rpc.jlb_ming_cheng_check(input, usernameResult);
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

	if(!isNull('ju_le_bu_ming'))
	{
		$('message').innerHTML = "俱乐部名称不能为空！";
		return false;
	}
	//过滤内容
	if(is_political_words($('ju_le_bu_ming').value)||is_dirty_words($('ju_le_bu_ming').value))
	{
		$("message").innerHTML = "俱乐部名称不合法！";
		return false;
	}
	if(!isNaN($('ju_le_bu_ming').value))
	{
		$('message').innerHTML = "俱乐部名称不能全是数字！";
		return false;
	}
	if(!checkCharLength('ju_le_bu_ming',16))
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

	if(!isNull('ju_le_bu_ming'))
	{
		$('message').innerHTML = "俱乐部名称不能为空！";
		return false;
	}
	//过滤内容
	if(is_political_words($('ju_le_bu_ming').value)||is_dirty_words($('ju_le_bu_ming').value))
	{
		$("message").innerHTML = "俱乐部名称不合法！";
		return false;
	}
	if(!isNaN($('ju_le_bu_ming').value))
	{
		$('message').innerHTML = "俱乐部名称不能全是数字！";
		return false;
	}
	if(!checkCharLength('ju_le_bu_ming',16))
	{
		$('message').innerHTML = "填写的俱乐部字符太长！";
		return false;
	}
	if($('type').value=='upload')
	{
		if(!check_file())
			return false;
	}
	//submit_to_div(myform,"popup_inner")
	return true;
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