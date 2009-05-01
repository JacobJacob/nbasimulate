<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
<p style="margin-bottom:20px">角色:<?php echo $role_name;?></p>
<form name="role_setting" action="<?php echo url('admin::role/saveSetting');?>" method="POST">
  <input type="hidden" name="id" value="<?php echo $role_id;?>">
  <table style="width:700px;" cellspacing="2" cellpadding="1" border="0" id="dd">
    <tr>
      <td width="30%">路径</td>
      <td>描述</td>
      <td>选择</td>
    </tr>
    <?php $rowid = 0;?>
    <?php foreach ($udis as $udi):?>
    <tr>
      <td align="left"><?php echo $udi['path'];?><input name="rowids[]" type="hidden" value="<?php echo $udi['id'];?>"></td>
      <td align="left"><?php echo $udi['desc'];?></td>
      <td><input type="checkbox" name="checks[]" value="<?php echo $rowid;?>" <?php echo $udi['checked'];?>></td>
    </tr>
    <?php $rowid += 1;?>
    <?php endforeach;?>
  </table>
 <INPUT id=submit type=submit value="提交"> 
</form>
</div>
<?php $this->_endblock('contents'); ?>
