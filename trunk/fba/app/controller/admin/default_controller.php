<?php
// $Id$

/**
 * Controller_Admin_Default controller
 */
class Controller_Admin_Default extends Controller_Abstract
{

	function actionIndex()
	{
		 $this->_view['text'] = 'Hello!';
	}
}


