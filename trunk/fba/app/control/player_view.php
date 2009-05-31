
  <div id="info">
    姓名:<?php echo $player->name;?><br>
    年龄:<?php echo $player->age;?><br>
    位置:<?php echo $player->position;?><br>
    身高:<?php echo $player->stature;?>cm<br>
    体重:<?php echo $player->avoirdupois;?>kg<br>
    <br>
  </div>
  <div id="ability">
    <TABLE id="tblDetail" cellSpacing="0" cellPadding="0" width="201" border="0">
      <TBODY>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">速度</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'speed');?>.gif" width="<?php echo $player->speed;?>" />
                    </TD>
                    <TD align="center" width="41"><A title="85.7+11.5" style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->speed;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">弹跳</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'bounce');?>.gif" width="<?php echo $player->bounce;?>" />
                    </TD>
                    <TD align="center" width="41"><A style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->bounce;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">强壮</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'strength');?>.gif" width="<?php echo $player->strength;?>" />
                    </TD>
                    <TD align="center" width="41"><A title="85.7+11.5" style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->strength;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">耐力</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'stamina');?>.gif" width="<?php echo $player->stamina;?>" />
                    </TD>
                    <TD align="center" width="41"><A title="85.7+11.5" style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->stamina;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">投篮</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'shooting');?>.gif" width="<?php echo $player->shooting;?>" />
                    </TD>
                    <TD align="center" width="41"><A title="85.7+11.5" style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->shooting;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">三分</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'trisection');?>.gif" width="<?php echo $player->trisection;?>" />
                    </TD>
                    <TD align="center" width="41"><A title="85.7+11.5" style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->trisection;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">运球</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'dribble');?>.gif" width="<?php echo $player->dribble;?>" />
                    </TD>
                    <TD align="center" width="41"><A title="85.7+11.5" style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->dribble;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">传球</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'pass');?>.gif" width="<?php echo $player->pass;?>" />
                    </TD>
                    <TD align="center" width="41"><A title="85.7+11.5" style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->pass;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">篮板</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'backboard');?>.gif" width="<?php echo $player->backboard;?>" />
                    </TD>
                    <TD align="center" width="41"><A title="85.7" style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->backboard;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">抢断</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'steal');?>.gif" width="<?php echo $player->steal;?>" />
                    </TD>
                    <TD align="center" width="41"><A title="85.7+11.5" style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->steal;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
        <TR>
          <TD align="center" width="44" height="22"><A title="" style="CURSOR: pointer; COLOR: #a600b9">封盖</A></TD>
          <TD width="157">
             <TABLE cellSpacing="0" cellPadding="0" width="157" border="0">
               <TBODY>
                 <TR>
                    <TD width="116" bgColor="#eeeeee" height="18">
                      <IMG height="8" src="http://xbam1.web.17173.com/Images/Player/Ability/Color<?php echo Common::check_color($player,'blocked');?>.gif" width="<?php echo $player->blocked;?>" />
                    </TD>
                    <TD align="center" width="41"><A title="85.7+11.5" style="CURSOR: pointer; COLOR: #a600b9"><?php echo $player->blocked;?></A></TD>
                 </TR>
               </TBODY>
            </TABLE>
          </TD>
        </TR>
   </TABLE>
  </div>
