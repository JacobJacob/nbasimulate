<?php $this->_extends('_layouts/default_layout'); ?>

<?php $this->_block('contents'); ?>
<div style="text-align:center">
<form name="role_setting" action="<?php echo url('admin::player/freegen');?>" method="POST">
  <p style="text-align:left;width:450px;padding:2px">位置:</p>
  <p style="text-align:left;width:450px;">
    <select  name="position">
       <option value="C">中锋</option>
       <option value="PF">大前锋</option>
       <option value="SF">小前锋</option>
       <option value="SG">分卫</option><br>
       <option value="PG">控卫</option>
    </select>
  </p>
  <p style="text-align:left;width:450px;padding:2px">A1:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="A1" value="1"></p>
  <p style="text-align:left;width:450px;padding:2px">A2:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="A2" value="2"></p>
  <p style="text-align:left;width:450px;padding:2px">A1:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="A3" value="12"></p>
  <p style="text-align:left;width:450px;padding:2px">B1:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="B1" value="40"></p>
  <p style="text-align:left;width:450px;padding:2px">B2:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="B2" value="60"></p>
  <p style="text-align:left;width:450px;padding:2px">B3:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="B3" value="70"></p>
  <p style="text-align:left;width:450px;padding:2px">C1:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="C1" value="80"></p>
  <p style="text-align:left;width:450px;padding:2px">C2:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="C2" value="100"></p>
  <p style="text-align:left;width:450px;padding:2px">C3:</p>
  <p style="text-align:left;width:450px;"><input type="text" name="C3" value="150"></p>
 <INPUT id=submit type=submit value="提交"> 
</form>
</div>
<?php $this->_endblock('contents'); ?>
