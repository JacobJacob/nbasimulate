
<div id = 'player_list'>
<table class="player_list">
  <tr>
       <td width="7%">号码</td>
       <td width="18%">姓名</td>
       <td width="7%">年龄</td>
       <td width="7%">位置</td>
       <td width="7%">体力</td>
       <td width="7%">状态</td>
       <td width="7%">身高</td>
       <td width="7%">体重</td>
       <td width="7%">综合</td>
       <td width="10%">当前选秀</td>
       <td width="10%">截止时间</td>
       <td width="7%">操作</td>
  </tr>
<?php foreach($players as $player):?>
   <tr>
      <td><?php echo $player->no;?></td>
      <td><a href="javascript:view('<?php echo url("member::profselect/view",array("id"=>$player->id));?>');"><?php echo $player->name;?></a></td>
      <td><?php echo $player->age;?></td>
      <td><?php echo $player->position;?></td>
      <td><?php echo $player->thew;?></td>
      <td><?php echo $player->status;?></td>
      <td><?php echo $player->stature;?></td>
      <td><?php echo $player->avoirdupois;?></td>
      <td><?php echo number_format($player->colligate(),1);?></td>
      <td><?php echo $player->avoirdupois;?></td>
      <td><?php echo date('y-m-d',strtotime($player->close_datetime));?></td>
      <td><a href="<?php echo url('member::profselect/select');?>">选秀</a></td>
    </tr>
<?php endforeach;?>
<tr><td colspan="12"><?php echo $navigation;?></td></tr>
</table>
</div>
<br>
<div id="player_detail">

</div>