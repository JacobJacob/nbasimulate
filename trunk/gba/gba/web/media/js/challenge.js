
	//比赛的时间
	count_down_all_jian = function()
	{
		count_down_one("yz_time","yz_value");
	}	
	//正在等待对手的时的时间处理
	count_down_waiting = function()
	{
		count_down_two("yz_time","waiting");
	}

	//比赛前的时间
	count_down_all_jian_sai_qian = function()
	{
		count_down_three("yz_time","yz_value");
	}



	count_down_two = function(status_div_id,waiting)
	{
		var a = document.getElementById(waiting).value;
		a ++;
		document.getElementById(waiting).value = a;
		document.getElementById(status_div_id).innerHTML = "等待对手中……已等待"+a+"秒";
	}

	count_down_one = function(yz_div_id,yz_value)
	{
		var a = document.getElementById(yz_value).value;
		a --;
		document.getElementById(yz_value).value = a;
		document.getElementById(yz_div_id).innerHTML =a;
	}

	count_down_three = function(yz_div_id,yz_value)
	{
		var a = document.getElementById(yz_value).value;
		a --;
		document.getElementById(yz_value).value = a;
		if(a>30 && a<=120)
		{	
			document.getElementById(yz_div_id).innerHTML = "赛前准备中……您还有"+a+"秒时间进行战术安排";
		}
		else if(a>0 && a<=30)
		{	
			document.getElementById(yz_div_id).innerHTML = "球员入场中……比赛将在"+a+"秒后开始";
		}
		else
		{
			window.parent.location.reload();
		}
	}