<?php $this->_extends('_layouts/default_layout'); ?>
<script type="text/javascript" src="<?php echo $_BASE_DIR; ?>public/js/jquery.js"></script>
 <script type="text/javascript">
 $(function(){
 	//移到右边
 	$('#add').click(function() {
 		$('#select1 option:selected').remove().appendTo('#select2');
 	});
 	//移到左边
 	$('#remove').click(function() {
 		$('#select2 option:selected').remove().appendTo('#select1');
 	});
 	//双击选项
 	$('#select1').dblclick(function(){
 		$("option:selected",this).remove().appendTo('#select2');
 	});
 	//双击选项
 	$('#select2').dblclick(function(){
 		$("option:selected",this).remove().appendTo('#select1');
 	});
 });

 $(document).ready(function() {
 	$("#submit").click(function() {
 		var str = '';
 		$("#select2 option").each(function(){
 			str += this.value;
 			str += ';';
 		});
 		$("#rolesStr").val(str);
 	});
 });


 </script>
<?php $this->_block('contents'); ?>
<div style="margin-bottom:20px">
  <span style="float:left;margin-left:480px;">用户名:</span>
  <span><?php echo $user->user_name;?></span>
</div>
<form method="POST" id="setForm" action="<?php echo url('admin::user/saveSetting');?>">
<input type="hidden" name="rolesStr" id="rolesStr">
<input type="hidden" name="id" value="<?php echo $user->id;?>">
<div style="text-align:center">
 <div style="float:left;margin-left:240px;width:150px">
  <div style="float:left;margin-bottom:10px">备选角色:</div>
  <div>
	  <select multiple id="select1" style="width:150px;height:200px;">
	     <?php foreach ($roles as $role):?>
	       <option value="<?php echo $role->id;?>"><?php echo $role->role_name;?></option>
	     <?php endforeach;?>
	  </select>
  </div>
 </div>
 <div style="float:left;width:100px">
  <a href="#" id="add" style="display:block;margin-top:100px;text-decoration: none">添加角色</a>
  <a href="#" id="remove" style="display:block;text-decoration: none">删除角色</a>
 </div>
 <div style="float:left;width:150px">
  <div style="float:left;margin-bottom:10px">当前角色:</div>
  <div>
	  <select multiple id="select2" style="width: 150px;height:200px;">
	     <?php foreach ($exist_roles as $exist_role):?>
	       <option value="<?php echo $exist_role->id;?>"><?php echo $exist_role->role_name;?></option>
	     <?php endforeach;?>
	  </select>
  </div>
 </div>
</div>
<div style="clear:both;"></div>
<div style="float:left;margin-left:220px;">
   <input type="submit" id="submit" value="提交">
</div>
</form>
<?php $this->_endblock('contents'); ?>
