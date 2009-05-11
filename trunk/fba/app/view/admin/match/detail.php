<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
  <table style="width:900px;" cellspacing="2" cellpadding="1" border="0" id="dd">
   <?php foreach ($match->details as $detail):?>
    <tr>
       <td align="left">
          <?php echo $detail->time_msg;?>
       </td>
       <td align="left">
          <?php echo $detail->description;?>
       </td>
        <td align="left" width="10%">
          <?php echo $detail->point_msg;?>
       </td>
    </tr>
   <?php endforeach;?>
  </table>
</div>
<?php $this->_endblock('contents'); ?>
