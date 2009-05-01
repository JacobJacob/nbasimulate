<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
<form name="role_setting" action="<?php echo url('admin::role/save');?>" method="POST">
  <input type="hidden" name="id" value="<?php echo $role->id;?>">
  <p style="text-align:left;width:450px;padding:2px">角色名:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="name" value="<?php echo $role->role_name;?>"></p>
  <p style="text-align:left;width:450px;padding:2px;margin-top:10px">角色描述:</p>
  <p style="text-align:left;width:450px;"><textarea type="text"  rows="8" cols="80" name="desc"><?php echo $role->role_desc;?></textarea></p>
 <INPUT id=submit type=submit value="提交"> 
</form>
</div>
<?php $this->_endblock('contents'); ?>
