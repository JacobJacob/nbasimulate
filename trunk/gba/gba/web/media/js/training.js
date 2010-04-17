function jia_dian_onclick(shu_xing)
{	
	var attr_array = new Array("sd","tt","qz","nli","tlan","sf","yq","cq","lb","qd","fg");
	var sx_dang_qian_zhi = parseFloat(Math.round($(shu_xing+'_width').value*10)/10);   //属性当前值 --截取小数后一位
	var sx_add = parseFloat(Math.round($(shu_xing+'_add').value*10)/10);   //属性当前已经增加值，最多为3
	var sx_max_add = parseFloat(Math.round($(shu_xing+'_max_add').value*10)/10);   //属性最多能增加的最大值值，最多为3
	var ti_li = parseInt($('ti_li_zhi').value);	
	var ability = parseFloat($('ability').value);
	var total_add = parseFloat($('total_add').value);										   //体力当前值
	var zong_he = parseFloat(Math.round($('zong_he_zhi').value*10)/10);                //综合当前值 --截取小数后一位
	var xun_lian_dian = parseInt($('xun_lian_dian_zhi').value);						   //训练点当前值
	var ti_li_zui_xiao_zhi = 30;   //体力最小值限制
	var shu_xing_zeng_zhi = 1;     //每次属性增加值
	var xld_xiao_hao_zhi = 200;       //每次消耗训练点值
	var yuan_xld_xiao_hao_zhi = xld_xiao_hao_zhi;                       //这个训练点消耗值用于IF判断
	
	var tiao_html = $(shu_xing+'_tiao').innerHTML;
	var color_re = /color(\d+).gif/i
	var color = tiao_html.match(color_re)[1];

	shu_xing_zeng_zhi_1 = 1;
	if(sx_add + 1 >= sx_max_add){
	   $(shu_xing +'_jia').innerHTML = '<img id="'+ shu_xing + '" style="CURSOR: pointer" height="12" title="训练提升该项目值" src="/site_media/images/zengjia_c.gif" width="12" align="absmiddle">';
	   shu_xing_zeng_zhi_1 = sx_max_add - sx_add;
	}
	
	if(xun_lian_dian <= 200 && xun_lian_dian >= 20)
	{
		shu_xing_zeng_zhi = parseFloat(parseInt((xun_lian_dian/xld_xiao_hao_zhi)*10)/10);
	}
	if (shu_xing_zeng_zhi_1 < shu_xing_zeng_zhi){
	  shu_xing_zeng_zhi = shu_xing_zeng_zhi_1; //取最小的
	}
	
	xld_xiao_hao_zhi = xld_xiao_hao_zhi * shu_xing_zeng_zhi;
	
	if(ti_li <= 30 || xun_lian_dian < 20)
	{
		for(var i=0; i<attr_array.length; i++)
	    {
		    $(attr_array[i]+'_jia').innerHTML = '<img id="'+attr_array[i]+'" style="CURSOR: pointer" height="12" title="训练提升该项目值" src="/site_media/images/zengjia_c.gif" width="12" align="absmiddle">';
	    }
	}
	else
	{
	    new_total_add = total_add + shu_xing_zeng_zhi ;
	    new_total = ability*10/10 + new_total_add/14;
	    zong_he = parseFloat(Math.round(new_total*10)/10);
		sx_dang_qian_zhi = sx_dang_qian_zhi + shu_xing_zeng_zhi;
		ti_li = ti_li - 1;

		xun_lian_dian = xun_lian_dian - xld_xiao_hao_zhi;
		$(shu_xing+'_tiao').innerHTML = '<img height=8 src="/site_media/images/qiu_yuan/color' + color + '.gif" width="'+sx_dang_qian_zhi * 1.6 +'"><input type="hidden" id="'+shu_xing+'_width" name="'+shu_xing+'_width" value="'+sx_dang_qian_zhi+'">';
		$(shu_xing+'_zhi').innerHTML = parseFloat(parseInt(sx_dang_qian_zhi*10)/10);
		$('ti_li').innerHTML = ti_li+'<input type="hidden" id="ti_li_zhi" name="ti_li_zhi" value="'+ti_li+'">';
		$('xun_lian_dian').innerHTML = xun_lian_dian+'<input type="hidden" id="xun_lian_dian_zhi" name="xun_lian_dian_zhi" value="'+xun_lian_dian+'">';
	    $('zong_he').innerHTML = zong_he + '<input type="hidden" id="zong_he_zhi" name="zong_he_zhi" value="'+zong_he+'">' ;
	    $(shu_xing+'_add').value = sx_add + shu_xing_zeng_zhi ;
	    $('total_add').value = new_total_add ;
	}
	//判断扣除后的训练点不足时，无法再点
	if(xun_lian_dian < 20)
	{
		for(var i=0; i<attr_array.length; i++)
	    {
		    $(attr_array[i]+'_jia').innerHTML = '<img id="'+attr_array[i]+'" style="CURSOR: pointer" height="12" title="训练提升该项目值" src="/site_media/images/zengjia_c.gif" width="12" align="absmiddle">';
	    }
	}
}