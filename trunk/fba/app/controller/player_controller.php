<?php
// $Id$

/**
 * Controller_Player controller
 */
class Controller_Player extends Controller_Abstract
{

	function actionIndex()
	{
		# $this->_view['text'] = 'Hello!';
		$players = Player::find("teamid = ?",1)->getAll();
		
		
		$this->_view['players'] = $players ;
	}
	
	function actionJayvee()
	{
	   $players = Player::find("teamid = ?",1)->getAll();
       $this->_view['players'] = $players ;
	}
	function actionProfessional()
	{
	   $players = Player::find("teamid = ?",1)->getAll();
       $this->_view['players'] = $players ;
	}
    
    function actionView()
	{
	   $id = isset($this->_context->id) ? (int)$this->_context->id : 0;
       $player = Player::find(" id = ?",$id)->query();
       $this->_view['player'] = $player ;
       $this->_view['id'] = $id ;
	}
}


