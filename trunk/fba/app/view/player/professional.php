<table>
    <th>
       <td>号码</td>
       <td>姓名</td>
       <td>年龄</td>
       <td>位置</td>
       <td>体力</td>
       <td>状态</td>
       <td>身高</td>
       <td>体重</td>
       <td>综合</td>
    </th>
<?php foreach($players as $player):?>
   <tr>
      <td><?php echo $player->no;?></td>
      <td><a href="<?php echo url("player/view",array("id"=>$player->id));?>" target="left"><?php echo $player->name;?></a></td>
      <td><?php echo $player->age;?></td>
      <td><?php echo $player->id;?></td>
      <td><?php echo $player->name;?></td>
      <td><?php echo $player->name;?></td>
      <td><?php echo $player->name;?></td>
      <td><?php echo $player->name;?></td>
    </tr>
<?php endforeach;?>
</table>