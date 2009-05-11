<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
  <table style="width:700px;" cellspacing="2" cellpadding="1" border="0" id="dd">
    <tr>
      <td width="30%">角色名称</td>
      <td>角色描述</td>
      <td>操作</td>
    </tr>
  </table>
  <span style="float:left;margin-top:20px;margin-left:310px;"><a href="<?php echo url('admin::player/create');?>">生成球员</a></span>
</div>
<?php $this->_endblock('contents'); ?>
