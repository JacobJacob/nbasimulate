<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
  <table style="width:800px;" cellspacing="2" cellpadding="1" border="0" id="dd">
    <tr>
      <td width="30%">用户名</td>
      <td>昵称</td>
      <td>性别</td>
      <td>生日</td>
      <td>注册时间</td>
      <td>上次登录</td>
      <td width="20%">操作</td>
    </tr>
   <?php foreach ($users as $user):?>
    <tr>
       <td align="left">
          <?php echo $user->user_name;?>
       </td>
       <td>
          <?php echo $user->info->nickname;?>
       </td>
       <td>
          <?php echo $user->info->sex;?>
       </td>
       <td>
          <?php echo $user->info->birthday;?>
       </td>
        <td>
          <?php echo $user->create_at;?>
       </td>
        <td>
         <?php echo $user->create_at;?>
       </td>
       <td>
         <a href="<?php echo url('admin::user/delete',array('id'=>$user->id,'page'=>$current_page));?>">删除</a>
         <a href="<?php echo url('admin::user/edit',array('id'=>$user->id));?>">修改</a>
         <a href="<?php echo url('admin::user/setting',array('id'=>$user->id));?>">角色设置</a>
       </td>
    </tr>
   <?php endforeach;?>
  </table>
  <span style="float:left;margin-top:20px;margin-left:270px"><?php echo $page->to_string();?></span>
</div>
<?php $this->_endblock('contents'); ?>
