<script type="text/javascript">
		function view(id)
		{
				$.ajax({
			        url: id, 
			        cache: false,
			        success: function(message) 
			        {			            	
			          $("#player_detail").empty().append(message);      
			        }
			     });			        
		}
</script>
<div id = 'player_list'>
<table class="player_list">
  <tr>
       <td width="7%">号码</td>
       <td width="23%">姓名</td>
       <td width="10%">年龄</td>
       <td width="10%">位置</td>
       <td width="10%">体力</td>
       <td width="10%">状态</td>
       <td width="10%">身高</td>
       <td width="10%">体重</td>
       <td width="10%">综合</td>
  </tr>
<?php foreach($players as $player):?>
   <tr>
      <td><?php echo $player->no;?></td>
      <td><a href="javascript:view('<?php echo url("player/view",array("id"=>$player->id));?>');"><?php echo $player->name;?></a></td>
      <td><?php echo $player->age;?></td>
      <td><?php echo $player->position;?></td>
      <td><?php echo $player->thew;?></td>
      <td><?php echo $player->status;?></td>
      <td><?php echo $player->stature;?></td>
      <td><?php echo $player->avoirdupois;?></td>
      <td><?php echo $player->avoirdupois;?></td>
    </tr>
<?php endforeach;?>
</table>
</div>
<div id="player_detail">

</div>