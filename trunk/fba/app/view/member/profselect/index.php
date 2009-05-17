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
			
			
		function view(id)
		{
				$.ajax({
			        url: id, 
			        cache: false,
			        success: function(message) 
			        {			            	
			          $("#player_detail").empty().append(message);      
			        }
			     });			        
		}
</script>
</head>
<body>
<div>
   <div class="navcontainer">
    <ul>
        <li><a  href="javascript:loadTab('<?php echo url('member::profselect/list',array('position'=>'C'));?>')">中锋</a></li>
        <li><a  href="javascript:loadTab('<?php echo url('member::profselect/list',array('position'=>'PF'));?>')">大前锋</a></li>
        <li><a  href="javascript:loadTab('<?php echo url('member::profselect/list',array('position'=>'SF'));?>')">小前锋</a></li>
        <li><a  href="javascript:loadTab('<?php echo url('member::profselect/list',array('position'=>'SG'));?>')">得分后卫</a></li>
        <li><a  href="javascript:loadTab('<?php echo url('member::profselect/list',array('position'=>'PG'));?>')">控球后卫</a></li>
    </ul>
</div>
<div id="tabcontent">

</div>
</body>
</html>
