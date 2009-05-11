<?php
// $Id$

/**
 * Controller_Admin_Actiondesc controller
 */
class Controller_Admin_Actiondesc extends Controller_Abstract
{

	function actionIndex()
	{
		$page_index = 1 ;
		if(isset($this->_context->page))
		{
			$page_index = $this->_context->page;
		}
		$query = ActionDesc::find()->limitPage($page_index,10);

		$actiondescs =$query->query();
		$pagination = $query->getPagination();

		$udi = QContext::instance()->requestUDI('array');
		$page = new Page($udi,$pagination );

		$page->to_string();

		$this->_view['actiondescs'] = $actiondescs ;
		$this->_view['page'] = $page ;
		$this->_view['current_page'] = $page_index;
	}

	function actionCreate()
	{
		$id = $this->_context->id;

		$actiondesc = ActionDesc::find('id=?',$id)->query();

		$this->_view['actiondesc'] = $actiondesc ;
	}

	function actionSave()
	{

		$id = isset($this->_context->id)?$this->_context->id:0;

		if($id)
		{
			$actiondesc = ActionDesc::find('id=?',$id)->query();
			$message = '动作描述更新成功';
		}else{
			$actiondesc = new ActionDesc();
			$message = '动作描述创建成功';
		}
		$actiondesc->setProps($this->_context);

		$actiondesc->save();

		return $this->_redirectMessage($message,'',url('admin::actiondesc/index'));
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
		$actiondesc_id = $_SESSION[Q::ini('acl_session_key')]['id'];
		$actiondesc = ActionDesc::find('id=?',$actiondesc_id)->query();

		$this->_view['actiondesc'] = $actiondesc;
	}
}


