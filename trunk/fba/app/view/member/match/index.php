<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
   "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
<meta http-equiv="Content-type" content="text/html; charset=utf-8" />
<script type="text/javascript" src="http://www.cssrain.cn/demo/JQuery+API/jquery-1[1].2.1.pack.js" charset="utf-8"></script>
<link rel="stylesheet" type="text/css" href="<?php echo $_BASE_DIR; ?>public/css/player.css">
<script type="text/javascript">
function loadTab(id)
{
	if ( $("ul a").length > 0){
		$.ajax({
			url: id,
			cache: false,
			success: function(message)
			{
				$("#tabcontent").empty().append(message);
			}
		});
	}
}


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
</head>
<body>
<div id = 'player_list'>
<table class="player_list">
  <tr>
       <td width="30%">发起</td>
       <td width="20%">接受</td>
       <td width="20%">比分</td>
       <td width="30%">战报</td>
  </tr>
<?php foreach($matchs as $match):?>
   <tr>
      <td><?php echo $match->home_team_id;?></td>
      <td><?php echo $match->visiting_team_id;?></td>
      <td><?php echo $match->match->point;?></td>
      <td>
        <a href="<?php echo url('member::match/matchdetail',array('id'=>$match->match->id));?>" target="_blank">比赛战报</a>
        <a href="<?php echo url('member::match/matchbulletin',array('id'=>$match->match->id));?>" target="_blank">技术统计</a>
      </td>
    </tr>
<?php endforeach;?>
<tr><td colspan="3"><?php echo $navigation;?></td></tr>
</table>
</div>
</body>
</html>
