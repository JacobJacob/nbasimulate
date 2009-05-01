<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
  <table style="width:700px;" cellspacing="2" cellpadding="1" border="0" id="dd">
    <tr>
      <td width="30%">desc</td>
      <td>namespace</td>
      <td>controller</td>
      <td>action</td>
      <td>module</td>
      <td>check type</td>
      <td>操作</td>
    </tr>
   <?php foreach ($udis as $udi):?>
    <tr>
       <td align="left">
          <?php echo $udi->desc;?>
       </td>
       <td>
          <?php echo $udi->namespace;?>
       </td>
       <td>
          <?php echo $udi->controller;?>
       </td>
       <td>
          <?php echo $udi->action;?>
       </td>
        <td>
          <?php echo $udi->module;?>
       </td>
        <td>
         <?php echo $udi->check_type;?>
       </td>
       <td>
         <a href="<?php echo url('admin::udi/delete',array('id'=>$udi->id,'page'=>$current_page));?>">删除</a>
         <a href="<?php echo url('admin::udi/create',array('id'=>$udi->id));?>">修改</a>
       </td>
    </tr>
   <?php endforeach;?>
  </table>
  <span style="float:left;margin-top:20px;margin-left:270px;"><?php echo $page->to_string();?></span>
  <span style="float:left;margin-top:20px;margin-left:310px;"><a href="<?php echo url('admin::udi/create');?>">新增UDI</a></span>
</div>
<?php $this->_endblock('contents'); ?>
