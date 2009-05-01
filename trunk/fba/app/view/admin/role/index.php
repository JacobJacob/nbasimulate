<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
  <table style="width:700px;" cellspacing="2" cellpadding="1" border="0" id="dd">
    <tr>
      <td width="30%">角色名称</td>
      <td>角色描述</td>
      <td>操作</td>
    </tr>
   <?php foreach ($roles as $role):?>
    <tr>
       <td align="left">
          <?php echo $role->role_name;?>
       </td>
        <td align="left">
          <?php echo $role->role_desc;?>
       </td>
       <td>
         <a href="<?php echo url('admin::role/delete',array('id'=>$role->id,'page'=>$current_page));?>">删除</a>
         <a href="<?php echo url('admin::role/create',array('id'=>$role->id));?>">修改</a>
         <a href="<?php echo url('admin::role/setting',array('id'=>$role->id));?>">权限设置</a>
       </td>
    </tr>
   <?php endforeach;?>
  </table>
   <span style="float:left;margin-top:20px;margin-left:270px;"><?php echo $page->to_string();?></span>
  <span style="float:left;margin-top:20px;margin-left:310px;"><a href="<?php echo url('admin::role/create');?>">新增角色</a></span>
</div>
<?php $this->_endblock('contents'); ?>
