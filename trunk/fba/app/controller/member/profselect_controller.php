<?php
// $Id$

/**
 * Controller_Member_Profselect controller
 */
class Controller_Member_Profselect extends Controller_Abstract
{

	function actionIndex()
	{
		$this->actionList();
	}

	function actionList(){

		if(isset($_GET['position'])){
			$position = $_GET['position'];
		}else{
			$position = 'C';
		}

		$page_index = 1 ;
		if(isset($this->_context->page))
		{
			$page_index = $this->_context->page;
		}
		$query = FreePlayer::find('position=?',$position)->limitPage($page_index,12);

		$select_players = $query->query();
		$pagination = $query->getPagination();

		$udi = QContext::instance()->requestUDI('array');
		$page = new Page($udi,$pagination );

		$this->_view['players'] = $select_players ;
		$this->_view['navigation'] = $page->navigation(array('position'=>$position)) ;
		$this->_view['current_page'] = $page_index;
		$this->_view['position'] = $position;
	}

	function actionView()
	{
		$id = isset($this->_context->id) ? (int)$this->_context->id : 0;
		$player = FreePlayer::find(" id = ?",$id)->query();
		$this->_view['player'] = $player ;
		$this->_view['id'] = $id ;
	}
}


