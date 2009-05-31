
<?php $this->_block('contents'); ?>
<div style="text-align:center">
  <table style="width:900px;" cellspacing="2" cellpadding="1" border="0" id="dd">
   <tr><td colspan="17"><?php echo $match->home_team_name;?></td></tr>
   <tr>
       <td >姓名</td>
       <td >号码</td>
       <td >位置</td>
       <td >得分</td>
       <td >篮板</td>
       <td >助攻</td>
       <td >抢断</td>
       <td >封盖</td>
       <td >失误</td>
       <td >犯规</td>
       <td >二分</td>
       <td >二分%</td>
       <td >罚球</td>
       <td >罚球%</td>
       <td >三分</td>
       <td >三分%</td>
       <td >篮板</td>
  </tr>
   <?php foreach ($home_bulletins as $bulletin):?>
    <tr>
       <td><?php echo $bulletin->player->name;?></td>
       <td><?php echo $bulletin->player->no;?></td>
       <td><?php echo $bulletin->player->position;?></td>
       <td><?php echo $bulletin->total_point;?></td>
       <td><?php echo $bulletin->total_rebound ;?></td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td><?php echo $bulletin->point2_doom_times;?>/<?php echo $bulletin->point2_shoot_times;?></td>
       <td><?php echo $bulletin->doom2rate;?></td>
       <td><?php echo $bulletin->point1_doom_times;?>/<?php echo $bulletin->point1_shoot_times;?></td>
       <td><?php echo $bulletin->doom1rate;?></td>
       <td><?php echo $bulletin->point3_doom_times;?>/<?php echo $bulletin->point3_shoot_times;?></td>
       <td><?php echo $bulletin->doom3rate;?></td>
       <td><?php echo $bulletin->offensive_rebound;?>/<?php echo $bulletin->defensive_rebound;?></td>
    </tr>
   <?php endforeach;?>
    <tr>
       <td colspan="3" align="center">合计</td>
       <td><?php echo $home_total->total_point;?></td>
       <td><?php echo $home_total->offensive_rebound + $home_total->defensive_rebound ;?></td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td><?php echo $home_total->point2_doom_times;?>/<?php echo $home_total->point2_shoot_times;?></td>
       <td><?php echo $home_total->doom2rate;?></td>
       <td><?php echo $home_total->point1_doom_times;?>/<?php echo $home_total->point1_shoot_times;?></td>
       <td><?php echo $home_total->doom1rate;?></td>
       <td><?php echo $home_total->point3_doom_times;?>/<?php echo $home_total->point3_shoot_times;?></td>
       <td><?php echo $home_total->doom3rate;?></td>
       <td><?php echo $home_total->offensive_rebound;?>/<?php echo $home_total->defensive_rebound;?></td>
    </tr>
   <tr><td colspan="17"><?php echo $match->visiting_team_name;?></td></tr>
   <?php foreach ($visiting_bulletins as $bulletin):?>
    <tr>
       <td><?php echo $bulletin->player->name;?></td>
       <td><?php echo $bulletin->player->no;?></td>
       <td><?php echo $bulletin->player->position;?></td>
       <td><?php echo $bulletin->total_point;?></td>
       <td><?php echo $bulletin->offensive_rebound + $bulletin->defensive_rebound ;?></td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td><?php echo $bulletin->point2_doom_times;?>/<?php echo $bulletin->point2_shoot_times;?></td>
       <td><?php echo $bulletin->doom2rate;?></td>
       <td><?php echo $bulletin->point1_doom_times;?>/<?php echo $bulletin->point1_shoot_times;?></td>
       <td><?php echo $bulletin->doom1rate;?></td>
       <td><?php echo $bulletin->point3_doom_times;?>/<?php echo $bulletin->point3_shoot_times;?></td>
       <td><?php echo $bulletin->doom3rate;?></td>
       <td><?php echo $bulletin->offensive_rebound;?>/<?php echo $bulletin->defensive_rebound;?></td>
    </tr>
   <?php endforeach;?>
   <tr>
       <td colspan="3" align="center">合计</td>
       <td><?php echo $visiting_total->total_point;?></td>
       <td><?php echo $visiting_total->offensive_rebound + $visiting_total->defensive_rebound ;?></td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td>0</td>
       <td><?php echo $visiting_total->point2_doom_times;?>/<?php echo $visiting_total->point2_shoot_times;?></td>
       <td><?php echo $visiting_total->doom2rate;?></td>
       <td><?php echo $visiting_total->point1_doom_times;?>/<?php echo $visiting_total->point1_shoot_times;?></td>
       <td><?php echo $visiting_total->doom1rate;?></td>
       <td><?php echo $visiting_total->point3_doom_times;?>/<?php echo $visiting_total->point3_shoot_times;?></td>
       <td><?php echo $visiting_total->doom3rate;?></td>
       <td><?php echo $visiting_total->offensive_rebound;?>/<?php echo $visiting_total->defensive_rebound;?></td>
    </tr>
  </table>
</div>
<?php $this->_endblock('contents'); ?>
