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
       <a class="head">��������</a> 
       <ul>
        <li><a href="#">�ҵı���</a></li>
        <li><a href="#">ѵ������</a></li>
        <li><a href="#">ʤ��Ϊ��</a></li>
        <li><a href="#">ʵ������</a></li>
      </ul>
    </li>  
     <li>
       <a class="head">��ӹ���</a>
         <ul> 
           <li><a href="<?php echo url('player/index');?>" target="center">��Ա����</a></li>
           <li><a href="#">������Ա</a></li>
           <li><a href="#">��Աѵ��</a></li>
           <li><a href="#">��Ӳ���</a></li>
           <li><a href="#">��ӽ���</a></li>
           <li><a href="#">�������</a></li>
         </ul>
     </li>  
     <li>
         <a class="head">���̹���</a>
         <ul> 
           <li><a href="#">FBA����</a></li>
           <li><a href="#">CFBA����</a></li>
           <li><a href="#">�ھ�����</a></li>
           <li><a href="#">�Խ�����</a></li>
           <li><a href="#">ʤ��Ϊ��</a></li>
         </ul>
     </li> 
     <li>
         <a class="head">ս���趨</a> 
         <ul> 
           <li><a href="#">ְҵս��</a></li>
           <li><a href="#">����ս��</a></li>
         </ul>
     </li> 
     <li>
         <a class="head">��������</a> 
         <ul> 
           <li><a href="#">�ҵ�����</a></li>
           <li><a href="#">����ս��</a></li>
           <li><a href="#">�����б�</a></li>
         </ul>
     </li>   
      <li>
         <a class="head">ת���г�</a>
         <ul> 
           <li><a href="#">ְҵת��</a></li>
           <li><a href="#">ְҵѡ��</a></li>
           <li><a href="#">�����г�</a></li>
           <li><a href="#">������Ա</a></li>
           <li><a href="#">������Ա</a></li>
         </ul>
     </li> 
   </ul>
</div>
</body>
</html>

