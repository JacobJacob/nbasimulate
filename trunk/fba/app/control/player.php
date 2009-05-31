<?php
class Control_Player extends QUI_Control_Abstract
{
	function render()
	{
		$this->_view['player'] = $this->_attrs['player'];
		return $this->_fetchView(dirname(__FILE__) . '/player_view.php');
	}
	
}
?>