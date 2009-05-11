<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
<form name="role_setting" action="<?php echo url('admin::actiondesc/save');?>" method="POST">
  <input type="hidden" name="id" value="<?php echo $actiondesc->id;?>">
  <p style="text-align:left;width:450px;padding:2px">动作名称:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="action_name" value="<?php echo $actiondesc->action_name;?>"></p>
  <p style="text-align:left;width:450px;padding:2px">动作结果:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="result" value="<?php echo $actiondesc->result;?>"></p>
  <p style="text-align:left;width:450px;padding:2px">动作标志:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="flg" value="<?php echo $actiondesc->flg;?>"></p>
  <p style="text-align:left;width:450px;padding:2px">动作概率:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="percent" value="<?php echo $actiondesc->percent;?>"></p>
  <p style="text-align:left;width:450px;padding:2px;margin-top:10px">动作描述:</p>
  <p style="text-align:left;width:450px;"><textarea type="text"  rows="8" cols="80" name="action_desc"><?php echo $actiondesc->action_desc;?></textarea></p>
 <INPUT id=submit type=submit value="提交"> 
</form>
</div>
<?php $this->_endblock('contents'); ?>
