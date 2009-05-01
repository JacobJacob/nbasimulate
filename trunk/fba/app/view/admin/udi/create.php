<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<script type="text/javascript" src="<?php echo $_BASE_DIR; ?>public/js/jquery.js"></script>
<script type="text/javascript" src="<?php echo $_BASE_DIR; ?>public/js/jquery.validate.js"></script>
<script type="text/javascript">
$(document).ready(function(){	
	$("#udifrom").validate({
		rules: {
            controller:{required:true},
            action:{required:true}
		},
		messages: {
            controller: {required: "请输入控制器名称"},
            action: {required: "请输入动作名称"}
		}
	});
});
</script>
<div style="text-align:center">
<form name="udifrom" action="<?php echo url('admin::udi/save');?>" method="POST">
  <input type="hidden" name="id" value="<?php echo $udi->id;?>">
  <p style="text-align:left;width:450px;padding:2px">命名空间:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="namespace" value="<?php echo $udi->namespace;?>"></p>
  <p style="text-align:left;width:450px;padding:2px">控制器:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="controller" value="<?php echo $udi->controller;?>"></p>
  <p style="text-align:left;width:450px;padding:2px">动作:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="action" value="<?php echo $udi->action;?>"></p>
  <p style="text-align:left;width:450px;padding:2px">模型:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="module" value="<?php echo $udi->module;?>"></p>
  <p style="text-align:left;width:450px;padding:2px">验证类型:</p>
  <p style="text-align:left;width:450px;">
    <select name="check_type">
       <option value="ALLOW" <?php if($udi->check_type=='ALLOW'){ echo 'selected'; };?>>ALLOW</option>
       <option value="ROLE" <?php if($udi->check_type=='ROLE'){ echo 'selected'; };?>>ROLE</option>
       <option value="DENY" <?php if($udi->check_type=='DENY'){ echo 'selected'; };?>>DENY</option>
    </select>
  </p>
  <p style="text-align:left;width:450px;padding:2px;margin-top:10px">UDI描述:</p>
  <p style="text-align:left;width:450px;"><textarea type="text"  rows="8" cols="80" name="desc"><?php echo $udi->desc;?></textarea></p>
 <INPUT id=submit type=submit value="提交"> 
</form>
</div>
<?php $this->_endblock('contents'); ?>
