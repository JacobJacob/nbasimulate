<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
   "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
<meta http-equiv="Content-type" content="text/html; charset=utf-8" />
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
       <a class="head">��������</a> 
       <ul>
        <li><a href="#">���߾���</a></li>
        <li><a href="#">�ҵı���</a></li>
        <li><a href="#">ѵw����</a></li>
        <li><a href="#">ʤ��Ϊ��</a></li>
      </ul>
    </li>  
     <li>
       <a class="head">��ӹ���</a>
         <ul> 
           <li><a href="<?php echo url('player/index');?>" target="center">��Ա����</a></li>
           <li><a href="#">��w�����</a></li>
           <li><a href="#">��Աѵw</a></li>
           <li><a href="#">��Ӳ���</a></li>
           <li><a href="#">��ӽ���</a></li>
           <li><a href="#">�������</a></li>
         </ul>
     </li>  
     <li>
         <a class="head">��̹���</a>
         <ul> 
           <li><a href="#">ְҵj��</a></li>
           <li><a href="#">��ѧj��</a></li>
           <li><a href="#">�ھ���</a></li>
           <li><a href="#">�Խ�����</a></li>
           <li><a href="#">����j��</a></li>
         </ul>
     </li> 
     <li>
         <a class="head">ս�����</a> 
         <ul> 
           <li><a href="#">ְҵս��</a></li>
           <li><a href="#">��ѧս��</a></li>
         </ul>
     </li> 
     <li>
         <a class="head">:��j��</a> 
         <ul> 
           <li><a href="#">j���б�</a></li>
           <li><a href="#">�ҵ�j��</a></li>
           <li><a href="#">j��ս��</a></li>
         </ul>
     </li>   
      <li>
         <a class="head">ת���г�</a>
         <ul> 
           <li><a href="#">��ע��Ա</a></li>
           <li><a href="#">������Ա</a></li>
           <li><a href="#">��ѧ��Ա</a></li>
           <li><a href="#">ְҵ��Ա</a></li>
           <li><a href="#">������Ա</a></li>
         </ul>
     </li> 
   </ul>
</div>
</body>
</html>

