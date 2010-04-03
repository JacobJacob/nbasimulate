
function show_popup(ahref){
    
	var isIE = (document.all) ? true : false;
	var isIE6 = isIE && ([/MSIE (\d)\.0/i.exec(navigator.userAgent)][0][1] == 6);
	var isGoogle = (navigator.userAgent.indexOf('Chrome')!=-1) ? true : false; // 判断是否是chrome内核
	
	
	var navID = 'floatBox';
	temp = document.getElementById(navID).style.display;
	if (temp == "block")
		result = "none";
	else
		result = "block";
	document.getElementById(navID+"Bg").style.display=result;
	if(isIE6){
		document.getElementById(navID+"Bg").style.position="absolute";
		document.getElementById(navID+"Bg").style.height=Math.max(document.documentElement.scrollHeight, document.documentElement.clientHeight) + "px";
		document.getElementById(navID+"Bg").style.width=Math.max(document.documentElement.scrollWidth, document.documentElement.clientWidth) + "px";
	}
	else
	{
		document.getElementById(navID+"Bg").style.position="fixed";
		document.getElementById(navID+"Bg").style.height="100%";
		document.getElementById(navID+"Bg").style.width="100%";
	}
	
	href_to_div(ahref,'popup_inner');
	document.getElementById(navID).style.display=result;
	if(isIE)
	{
		window.focus();
	}
	else if(isGoogle)
	{
	}
	else
	{
		document.activeElement.blur();
	}
	return false;
}

function hide_popup()
{
	var navID = 'floatBox';
	document.getElementById(navID+"Bg").style.display='none';
	document.getElementById(navID).style.display='none';
}

function display_popup()
{
	var isIE = (document.all) ? true : false;
	var isIE6 = isIE && ([/MSIE (\d)\.0/i.exec(navigator.userAgent)][0][1] == 6);
	var isGoogle = (navigator.userAgent.indexOf('Chrome')!=-1) ? true : false; // 判断是否是chrome内核,不支持（activeElement）
	
	var navID = 'floatBox';
	document.getElementById(navID+"Bg").style.display='block';
	if(isIE6){
		document.getElementById(navID+"Bg").style.position="absolute";
		document.getElementById(navID+"Bg").style.height=Math.max(document.documentElement.scrollHeight, document.documentElement.clientHeight) + "px";
		document.getElementById(navID+"Bg").style.width=Math.max(document.documentElement.scrollWidth, document.documentElement.clientWidth) + "px";
	}
	else
	{
		document.getElementById(navID+"Bg").style.position="fixed";
		document.getElementById(navID+"Bg").style.height="100%";
		document.getElementById(navID+"Bg").style.width="100%";
	}
	document.getElementById(navID).style.display='block';
	if(isIE)
	{
		window.focus();
	}
	else if(isGoogle)
	{
	}
	else
	{
		document.activeElement.blur();
	}
}

function show_dialog(mes,path){
	var navID  = 'floatBox';
	var button = '';
	temp = document.getElementById(navID).style.display;

	if (temp == "block")
		result = "none";
	else
		result = "block";
	if(path==-1)
		button = '<div class="btn_3"><a href="#" onclick="return show_popup(\'floatBox\');">确定</a></div>';
	else
		button = '<div class="btn_3"><a href="'+path+'" onclick="href_to_div(this,\'main_content\');hide_popup();return false;">确定</a></div>';

	mes='<br><br><br><br><div align="center">'+mes+'</div><br><br><br><br><div   align="center">'+button+'</div><br>';

	document.getElementById(navID+"Bg").style.display=result;
	var pageHeight = document.compatMode=="CSS1Compat" ? document.documentElement.clientHeight : document.body.clientHeight;
	document.getElementById(navID+"Bg").style.height=pageHeight;
	document.getElementById(navID).style.display=result;
	document.getElementById('popup_inner').innerHTML = mes;
	
	return false;
}
