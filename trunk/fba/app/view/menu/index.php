<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
   "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
<meta http-equiv="Content-type" content="text/html; charset=utf8" />
<script type="text/javascript" src="<?php echo $_BASE_DIR; ?>public/js/jquery.js"></script>
<script type="text/javascript" src="<?php echo $_BASE_DIR; ?>public/js/jquery.dimensions.js"></script>
<script type="text/javascript" src="<?php echo $_BASE_DIR; ?>public/js/jquery.accordion.js"></script> 
<link rel="stylesheet" type="text/css" href="<?php echo $_BASE_DIR; ?>public/css/menu.css">
<script type="text/javascript"> 
  jQuery().ready(
    function()
    { 
         jQuery('#navigation').accordion({
			header: '.head'
		});

    }
  ); 
</script> 
</head>
<body>
<div  style="height:250px;margin-bottom:1em;">
 <ul id="navigation">
    <li><a href="#"> 
       <a class="head">竞技中心</a> 
       <ul>
        <li><a href="#">我的比赛</a></li>
        <li><a href="#">训练中心</a></li>
        <li><a href="#">胜者为王</a></li>
        <li><a href="#">实力排行</a></li>
      </ul>
    </li>  
     <li>
       <a class="head">球队管理</a>
         <ul> 
           <li><a href="<?php echo url('player/index');?>" target="center">球员管理</a></li>
           <li><a href="#">教练组员</a></li>
           <li><a href="#">球员训练</a></li>
           <li><a href="#">球队财政</a></li>
           <li><a href="#">球队建设</a></li>
           <li><a href="#">球队荣誉</a></li>
         </ul>
     </li>  
     <li>
         <a class="head">赛程管理</a>
         <ul> 
           <li><a href="#">FBA联赛</a></li>
           <li><a href="#">CFBA联赛</a></li>
           <li><a href="#">冠军杯赛</a></li>
           <li><a href="#">自建杯赛</a></li>
           <li><a href="#">胜者为王</a></li>
         </ul>
     </li> 
     <li>
         <a class="head">战术设定</a> 
         <ul> 
           <li><a href="#">职业战术</a></li>
           <li><a href="#">二队战术</a></li>
         </ul>
     </li> 
     <li>
         <a class="head">篮球联盟</a> 
         <ul> 
           <li><a href="#">我的联盟</a></li>
           <li><a href="#">联盟战术</a></li>
           <li><a href="#">联盟列表</a></li>
         </ul>
     </li>   
      <li>
         <a class="head">转会市场</a>
         <ul> 
           <li><a href="#">职业转会</a></li>
           <li><a href="#">职业选秀</a></li>
           <li><a href="#">外授市场</a></li>
           <li><a href="#">自由球员</a></li>
           <li><a href="#">高中球员</a></li>
         </ul>
     </li> 
   </ul>
</div>
</body>
</html>

