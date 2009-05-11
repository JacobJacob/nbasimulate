<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
<form name="role_setting" action="<?php echo url('admin::player/freegen');?>" method="POST">
  <p style="text-align:left;width:450px;padding:2px">:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="number"></p>
 <INPUT id=submit type=submit value="提交"> 
</form>
</div>
<?php $this->_endblock('contents'); ?>
