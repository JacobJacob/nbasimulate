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
}


