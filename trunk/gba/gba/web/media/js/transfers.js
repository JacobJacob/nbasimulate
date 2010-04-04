

count_down_all = function()
{
	for (i=1;i<100;i++)
	{
		if (document.getElementById("time_"+i))
			count_down_one("time_"+i,"value_"+i);
	}
}	

count_down_one = function(div_id,value_id)
{
	var spantime = document.getElementById(value_id).value;
	spantime --;
	document.getElementById(value_id).value = spantime;
	var d = Math.floor(spantime / (24 * 3600));
	var h = Math.floor((spantime % (24*3600))/3600);
	var m = Math.floor((spantime % 3600)/(60));
	var s = Math.floor(spantime%60);
	var clock = "";
	if(d>0)
	{
		clock+=d+"天";
		if(h!=0)
			clock+=h+"小时";
		else
			clock+=m+"分钟";
	}
	else{
		if(h>0)
		{
			clock+=h+"小时";
			clock+=m+"分钟";
		}else
		{
			if(m>0)
			{
				clock+=m+"分钟";
				clock+=s+"秒";
			}else
			{
				if(s==0)
					window.parent.location.reload();
				if(s>0)
					clock+=s+"秒";
				else
				    clock = "已截止";//，截止时间:"+time
			}

		}
	}
	document.getElementById(div_id).innerHTML = clock;
}
window.onload = function()
{
	setInterval(count_down_all,1000);
}


// 判断出价是否合理
check_form_bid = function (myform)
{
	if(!isNull('price'))
	{
		$('notice').innerHTML = "价格不能为空！";
		return false;
	}
	if(!isNum('price'))
	{
		$('notice').innerHTML = "价格必须是数字！";
		return false;
	}
	if(parseInt($('price').value)< parseInt($('min_price').value))
	{
		$('notice').innerHTML = "出价需大于最低价！";
		return false;
	}

	if(parseInt($('price').value)>parseInt($('funds').value))
	{
		$('notice').innerHTML = "出价超出您俱乐部资金!";
		return false;
	}
	return submit_to_div(myform,"popup_inner");
}

// 委托拍卖出价是否合理
check_wei_tuo = function (myform)
{
	if(!isNull('zui_gao_xian_jia'))
	{
		$('notice').innerHTML = "起拍价格不能为空！";
		return false;
	}
	if(!isNum('zui_gao_xian_jia'))
	{
		$('notice').innerHTML = "最高价位必须是数字！";
		return false;
	}
	if(parseInt($('zui_gao_xian_jia').value)<=parseInt($('jia_ge').value))
	{
		$('notice').innerHTML = "出价需大于起拍价！";
		return false;
	}
	if(parseInt($('zui_gao_xian_jia').value)>parseInt($('zi_jin').value))
	{
		$('notice').innerHTML = "出价超出您俱乐部资金!";
		return false;
	}
	return submit_to_div(myform,"popup_inner");
}
// 业余出售
check_yy_chu_jia = function (myform)
{
	if(!isNum('jia_ge'))
	{
		$('notice').innerHTML = "请输入数字！";
		return false;
	}
	if(parseInt($('jia_ge').value)< parseInt($('jia_ge_a').value))
	{
		$('notice').innerHTML = "出价需大于指定价！";
		return false;
	}
	return submit_to_div(myform,"popup_inner");
}