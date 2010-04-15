function jia_dian_onclick(shu_xing)
{	
	var shu_xing_array = new Array("sd","tt","qz","nli","tlan","sf","yq","cq","lb","qd","fg");
	var sx_dang_qian_zhi = parseFloat(Math.round($(shu_xing+'_width').value*10)/10);   //属性当前值 --截取小数后一位
	var ti_li = parseInt($('ti_li_zhi').value);										   //体力当前值
	//var zong_he = parseFloat(Math.round($('zong_he_zhi').value*10)/10);                //综合当前值 --截取小数后一位
	var xun_lian_dian = parseInt($('xun_lian_dian_zhi').value);						   //训练点当前值
	var ti_li_zui_xiao_zhi = parseInt($('ti_li_zui_xiao_zhi').value);   //体力最小值限制
	var shu_xing_zeng_zhi = parseInt($('shu_xing_zeng_zhi').value);     //每次属性增加值
	var tl_xiao_hao_zhi = parseInt($('tl_xiao_hao_zhi').value);         //每次消耗体力值
	var xld_xiao_hao_zhi = parseInt($('xld_xiao_hao_zhi').value);       //每次消耗训练点值
	var yuan_xld_xiao_hao_zhi = xld_xiao_hao_zhi;                       //这个训练点消耗值用于IF判断
	if(xun_lian_dian <= yuan_xld_xiao_hao_zhi && xun_lian_dian >= yuan_xld_xiao_hao_zhi/10)
	{
		shu_xing_zeng_zhi = parseFloat(parseInt((xun_lian_dian/xld_xiao_hao_zhi)*10)/10);
		//alert('属性增加值：'+shu_xing_zeng_zhi);
		xld_xiao_hao_zhi = xld_xiao_hao_zhi * shu_xing_zeng_zhi;
		//alert('训练点消耗值：'+xld_xiao_hao_zhi);
	}
	if(ti_li <= ti_li_zui_xiao_zhi || xun_lian_dian < yuan_xld_xiao_hao_zhi/10)
	{
		for(var i=0; i<shu_xing_array.length; i++)
	    {
		    $(shu_xing_array[i]+'_jia').innerHTML = '<img id="'+shu_xing_array[i]+'" style="CURSOR: pointer" height="12" title="训练提升该项目值" src="images/zengjia_C.gif" width="12" align="absmiddle">';
	    }
	}
	else
	{
		sx_dang_qian_zhi = sx_dang_qian_zhi + shu_xing_zeng_zhi;
		ti_li = ti_li - tl_xiao_hao_zhi;
		//zong_he ++;
		xun_lian_dian = xun_lian_dian - xld_xiao_hao_zhi;
		$(shu_xing+'_tiao').innerHTML = '<img height=8 src="/site_media/images/qiu_yuan/color1.gif" width="'+sx_dang_qian_zhi+'"><input type="hidden" id="'+shu_xing+'_width" name="'+shu_xing+'_width" value="'+sx_dang_qian_zhi+'">';
		$(shu_xing+'_zhi').innerHTML = parseFloat(parseInt(sx_dang_qian_zhi*10)/10);
		//$(shu_xing+'_zhi').innerHTML = sx_dang_qian_zhi;
		$('ti_li').innerHTML = ti_li+'<input type="hidden" id="ti_li_zhi" name="ti_li_zhi" value="'+ti_li+'">';
		//$('zong_he').innerHTML = zong_he+'<input type="hidden" id="zong_he_zhi" name="zong_he_zhi" value="'+zong_he+'">';
		$('xun_lian_dian').innerHTML = xun_lian_dian+'<input type="hidden" id="xun_lian_dian_zhi" name="xun_lian_dian_zhi" value="'+xun_lian_dian+'">';
		//alert('改变后属性值：'+sx_dang_qian_zhi);
		//alert('改变后体力值：'+ti_li);
				//alert('改变后综合值：'+zong_he);
		//alert('改变后训练点值：'+xun_lian_dian);
	}
	//判断扣除后的训练点不足时，无法再点
	if(xun_lian_dian < yuan_xld_xiao_hao_zhi/10)
	{
		for(var i=0; i<shu_xing_array.length; i++)
	    {
		    $(shu_xing_array[i]+'_jia').innerHTML = '<img id="'+shu_xing_array[i]+'" style="CURSOR: pointer" height="12" title="训练提升该项目值" src="images/zengjia_C.gif" width="12" align="absmiddle">';
	    }
	}
}