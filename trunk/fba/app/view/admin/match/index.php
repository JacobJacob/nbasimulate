<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
  <table style="width:700px;" cellspacing="2" cellpadding="1" border="0" id="dd">
    <tr>
      <td width="30%">比赛类型</td>
      <td>主队</td>
      <td>客队</td>
      <td>比分</td>
    </tr>
   <?php foreach ($matchs as $match):?>
    <tr>
       <td align="left">
          <?php echo $match->point;?>
       </td>
       <td align="left">
          <?php echo $match->home_team_name;?>
       </td>
       <td align="left">
          <?php echo $match->visiting_team_name;?>
       </td>
        <td align="left">
          <?php echo $match->point;?>
       </td>
       <td>
         <a href="<?php echo url('admin::role/delete',array('id'=>$match->id,'page'=>$current_page));?>">删除</a>
         <a href="<?php echo url('admin::match/detail',array('id'=>$match->id));?>">战报</a>
       </td>
    </tr>
   <?php endforeach;?>
  </table>
   <span style="float:left;margin-top:20px;margin-left:270px;"><?php echo $page->to_string();?></span>
  <span style="float:left;margin-top:20px;margin-left:310px;"><a href="<?php echo url('admin::role/create');?>">新增角色</a></span>
</div>
<?php $this->_endblock('contents'); ?>
