

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>Inar</title>

    <meta name="resource-type" content="document" />
    <meta name="distribution" content="global" />
    <meta name="author" content="uutan" />
    <meta name="generator" content="uutan.net" />
    <meta name="copyright" content="Copyright (c) 2008 UUTAN.NET. All Rights Reserved." />
    <script src="<?php echo $_BASE_DIR; ?>public/js/tabs.js" type="text/javascript"></script> 
    <link rel="shortcut icon" href="<?php echo $_BASE_DIR; ?>public/img/favicon.ico" />
    <link rel="stylesheet" href="<?php echo $_BASE_DIR; ?>public/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="<?php echo $_BASE_DIR; ?>public/css/menu.css" type="text/css" media="screen" />
</head>

<body>

<div id="utwrap">
    <div id="utcontent">
        <ul id="dashmenu">
            <?php if(isset($_SESSION[Q::ini('acl_session_key')])): ?>
            <li><a href="<?php echo url('center','index','','member'); ?>">个人空间</a></li>
            <li><a href="<?php echo url('passport','logout','','member'); ?>">退出</a></li>
            <?php else: ?>
            <li><a href="<?php echo url('passport','reg','','member'); ?>">注册</a></li>
            <li><a href="<?php echo url('passport','login','','member'); ?>">登录</a></li>
            <?php endif?>
            <li><a href="<?php echo url('admin::default/index'); ?>">后台管理</a></li>
        </ul>

        <div id="headmain">
            <a href="/" class="logo"></a>
             <div id="mainmenu">
                <ul>
                    <li id="tabnav_btn_1" onmouseover="tabit(this)"><a href="<?php echo url('default','index','','-'); ?>">首页</a></li>
                    <li id="tabnav_btn_2" onmouseover="tabit(this)"><a href="#">竞技中心</a></li>
                    <li id="tabnav_btn_3" onmouseover="tabit(this)"><a href="#" >球队管理</a></li>
                    <li id="tabnav_btn_4" onmouseover="tabit(this)"><a href="#">赛程管理</a></li>
                    <li id="tabnav_btn_5" onmouseover="tabit(this)"><a href="#">战术管理</a></li>
                    <li id="tabnav_btn_6" onmouseover="tabit(this)"><a href="#">篮球联盟</a></li>
                    <li id="tabnav_btn_7" onmouseover="tabit(this)"><a href="#">转会市场</a></li>
                </ul>
            </div>
        </div>
        <div id="utbody">
            <div class="clear"></div>
            <div id="uwrap">
            <div class="clear"></div>
  
            </div><!--/uwrap-->
        </div><!--/utbody-->
        <div id="submenu">
	        <div id="tabnav_div_1" class="submenu">
	                <ul>
	                    <li><a href="#">在线经理</a></li>
	                    <li><a href="#">我的比赛</a></li>
	                    <li><a href="#">训练中心</a></li>
	                    <li><a href="#">球队争霸</a></li>
	                    <li><a href="#">实力排行</a></li>
	                </ul>
	        </div>
	        <div id="tabnav_div_2" class="submenu">
	                <ul>
	                    <li><a href="#">在线经理</a></li>
	                    <li><a href="#">我的比赛</a></li>
	                    <li><a href="#">训练中心</a></li>
	                    <li><a href="#">球队争霸</a></li>
	                    <li><a href="#">实力排行</a></li>
	                </ul>
	        </div>
	        <div id="tabnav_div_3" class="submenu">
	                <ul>
	                    <li><a href="<?php echo url('player/index');?>" target="center">球员管理</a></li>
	                    <li><a href="#">教练组员</a></li>
	                    <li><a href="#">球员训练</a></li>
	                    <li><a href="#">球队经营</a></li>
	                    <li><a href="#">球队建设</a></li>
	                    <li><a href="#">球队荣誉</a></li>
	                </ul>
	        </div>
	        <div id="tabnav_div_4" class="submenu">
	                <ul>
	                    <li><a href="#">常规赛</a></li>
	                    <li><a href="#">季后赛</a></li>
	                    <li><a href="#">二级联赛</a></li>
	                    <li><a href="#">冠军杯赛</a></li>
	                    <li><a href="#">自建杯赛</a></li>
	                    <li><a href="#">表演赛</a></li>
	                </ul>
	        </div>
	        <div id="tabnav_div_5" class="submenu">
	                <ul>
	                    <li><a href="#">职业战术</a></li>
	                    <li><a href="#">二队战术</a></li>
	                </ul>
	        </div>
	        <div id="tabnav_div_6" class="submenu">
	                <ul>
	                    <li><a href="#">篮球公社</a></li>
	                    <li><a href="#">我的公社</a></li>
	                    <li><a href="#">公社战争</a></li>
	                </ul>
	        </div>
	        <div id="tabnav_div_7" class="submenu">
	                <ul>
	                    <li><a href="#">自由球员</a></li>
	                    <li><a href="#">受限自由球员</a></li>
	                    <li><a href="#">交易转会</a></li>
	                    <li><a href="<?php echo url('member::profselect/index');?>" target="center">职业选秀</a></li>
	                    <li><a href="#">苗子选拔</a></li>
	                </ul>
	        </div>
        </div>
    </div><!--/utcontent-->
</div><!--/utwrap-->
<div style="text-align:center"">
  <iframe name="center" id="center_frame" frameborder="0" scrolling="No" height="70%">
    
  </iframe>
</div>
<div id="footer">
    <div id="footertop">
        <div id="footerbody">
            <div class="footer">Powered by Inar</div>
        </div>
    </div>
</div>
</body>
</html>