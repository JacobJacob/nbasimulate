<?php
// $Id$

/**
 * Controller_Admin_Match controller
 */
class Controller_Admin_Match extends Controller_Abstract
{

	function actionIndex()
	{
		$page_index = 1 ;
		if(isset($this->_context->page))
		{
			$page_index = $this->_context->page;
		}
		$query = Match::find()->limitPage($page_index,10);

		$matchs =$query->query();
		$pagination = $query->getPagination();

		$udi = QContext::instance()->requestUDI('array');
		$page = new Page($udi,$pagination );

		$page->to_string();

		$this->_view['matchs'] = $matchs ;
		$this->_view['page'] = $page ;
		$this->_view['current_page'] = $page_index;
	}

	function actionDetail()
	{
		$this-> _loadData();
	}

	/**
	 * 加载信息
	 */
	private function _loadData()
	{
		$match_id = $this->_context->id;
		$match = Match::find('id=?',$match_id)->query();

		$this->_view['match'] = $match;
	}
}


