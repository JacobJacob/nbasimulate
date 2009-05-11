<?php
// $Id$

/**
 * Controller_Admin_Player controller
 */
class Controller_Admin_Player extends Controller_Abstract
{

	function actionIndex()
	{
		# $this->_view['text'] = 'Hello!';
	}
	
	function actionCreate()
	{
		# $this->_view['text'] = 'Hello!';
	}
	/**
	 * 自由球员生成
	 *
	 */
	function actionFreeGen()
	{
		FreePlayer::build($_POST);

		return $this->_redirectMessage('球员生成成功','',url('admin::player/index'));
	}
}


