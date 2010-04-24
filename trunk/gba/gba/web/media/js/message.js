function checkdel()
{
	//js中取页面控件是数组的情况需要加上中括号
	if(!isCheckDel('ch[]'))
		return false;
	var frm = document.getElementById('frm');
	return submit_to_div(frm,"popup_inner");
}

shan_chu = function(url)
{
	if(isDel())
	{
		return show_popup(url);
	}
	else
		return false;
}
