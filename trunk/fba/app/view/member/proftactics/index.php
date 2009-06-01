<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
   "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
<meta http-equiv="Content-type" content="text/html; charset=utf-8" />
<script type="text/javascript" src="http://www.cssrain.cn/demo/JQuery+API/jquery-1[1].2.1.pack.js" charset="utf-8"></script>
<link rel="stylesheet" type="text/css" href="<?php echo $_BASE_DIR; ?>public/css/player.css">
<script type="text/javascript">
		function loadTab(id)
		{
			if ( $("ul a").length > 0){
				$.ajax({
			        url: id, 
			        cache: false,
			        success: function(message) 
			        {			            	
			          $("#tabcontent").empty().append(message);      
			        }
			     });			        
				}
			}
		
			$(document).ready(function()
			{
				$("ul a").click(function()
				{
					 loadTab($(this).attr("rel"));
			});
	    });
</script>
</head>
<body>
<div>
   <div class="navcontainer">
    <ul>
        <li><a  href="javascript:void(0);" rel="<?php echo url('player/jayvee');?>">战术设置</a></li>
        <li><a  href="javascript:void(0);" rel="<?php echo url('player/professional');?>">战术一</a></li>
        <li><a  href="javascript:void(0);" rel="<?php echo url('player/jayvee');?>">战术二</a></li>
        <li><a  href="javascript:void(0);" rel="<?php echo url('player/professional');?>">战术三</a></li>
        <li><a  href="javascript:void(0);" rel="<?php echo url('player/jayvee');?>">战术四</a></li>
    </ul>
</div>
<div id="tabcontent">

</div>
</body>
</html>
