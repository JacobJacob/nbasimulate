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

//创建联盟判断
function checkform()
{
	if($('name_check').value==-2)
	{
	  mes = "联盟已存在，请重新输入！";
	  $('message').innerHTML=mes;
	  return false;
	}

	if(!isNull('name'))
	{
		$('message').innerHTML = "联盟名称不能为空！";
		return false;
	}
	//过滤内容
	if(is_political_words($('name').value)||is_dirty_words($('name').value))
	{
		$("message").innerHTML = "联盟名称不合法！";
		return false;
	}
	if(!isNaN($('name').value))
	{
		$('message').innerHTML = "联盟名称不能全是数字！";
		return false;
	}
	if(!checkCharLength('name',12))
	{
		$('message').innerHTML = "填写的俱乐部字符太长！";
		return false;
	}
	return true;
}