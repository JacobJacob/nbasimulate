<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
  <table style="width:1100px;" cellspacing="2" cellpadding="1" border="0" id="dd">
    <tr>
      <td width="10%">动作名称</td>
      <td width="5%">动作结果</td>
      <td width="5%">动作标志</td>
      <td width="70%">动作描述</td>
      <td width="10%">操作</td>
    </tr>
   <?php foreach ($actiondescs as $actiondesc):?>
    <tr>
       <td align="left">
          <?php echo $actiondesc->action_name;?>
       </td>
       <td align="left">
          <?php echo $actiondesc->result;?>
       </td>
       <td align="left">
          <?php echo $actiondesc->flg;?>
       </td>
        <td align="left">
          <?php echo $actiondesc->action_desc;?>
       </td>
       <td>
         <a href="<?php echo url('admin::actiondesc/delete',array('id'=>$actiondesc->id,'page'=>$current_page));?>">删除</a>
         <a href="<?php echo url('admin::actiondesc/create',array('id'=>$actiondesc->id));?>">修改</a>
       </td>
    </tr>
   <?php endforeach;?>
  </table>
   <span style="float:left;margin-top:20px;margin-left:270px;"><?php echo $page->to_string();?></span>
  <span style="float:left;margin-top:20px;margin-left:310px;"><a href="<?php echo url('admin::actiondesc/create');?>">新增动作描述</a></span>
</div>
<?php $this->_endblock('contents'); ?>
