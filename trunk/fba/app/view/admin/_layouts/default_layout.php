<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>FBA网页游戏后台管理</title>

    <meta name="resource-type" content="document" />
    <meta name="distribution" content="global" />
    <meta name="author" content="uutan" />
    <meta name="generator" content="uutan.net" />
    <meta name="copyright" content="Copyright (c) 2008 UUTAN.NET. All Rights Reserved." />

    <link rel="shortcut icon" href="<?php echo $_BASE_DIR; ?>public/img/favicon.ico" />
    <link rel="stylesheet" href="<?php echo $_BASE_DIR; ?>public/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="<?php echo $_BASE_DIR; ?>public/css/admin.css" type="text/css" />
</head>

<body>

<div id="utwrap">
    <div id="utcontent">
        <ul id="dashmenu">
            <?php if(isset($_SESSION[Q::ini('acl_session_key')])): ?>
            <li><a href="<?php echo url('passport','logout','','member'); ?>">退出</a></li>
            <?php endif?>
        </ul>

        <div id="headmain">
            <a href="/" class="logo"></a>
             <div id="mainmenu">
                <ul>
                    <li><a href="<?php echo url('default','index','','-'); ?>">首页</a></li>
                    <li><a href="<?php echo url('admin::user/index'); ?>">用户管理</a></li>
                    <li><a href="<?php echo url('admin::role/index'); ?>">角色管理</a></li>
                    <li><a href="<?php echo url('admin::udi/index'); ?>">UDI管理</a></li>
                    <li><a href="<?php echo url('admin::actionDesc/index'); ?>">动作描述管理</a></li>
                    <li><a href="#">球员管理</a></li>
                    <li><a href="#">球队管理</a></li>
                    <li><a href="#">比赛管理</a></li>
                </ul>
            </div>
        </div>


        <div id="utbody">
            <div class="clear"></div>
            <div id="uwrap">
            <div class="clear"></div>
  
            </div><!--/uwrap-->
        </div><!--/utbody-->

    </div><!--/utcontent-->
</div><!--/utwrap-->
<div>
  <?php $this->_block('contents'); ?><?php $this->_endblock(); ?>
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