<?php
// $Id$

/**
 * Controller_Admin_Udi controller
 */
class Controller_Admin_Udi extends Controller_Abstract
{

	function actionIndex()
	{
		$page_index = 1 ;
		if(isset($this->_context->page))
		{
			$page_index = $this->_context->page;
		}
		$query = UDI::find()->limitPage($page_index,10);

		$udis = $query->query();
		$pagination = $query->getPagination();

		$udi = QContext::instance()->requestUDI('array');
		$page = new Page($udi,$pagination );

		$page->to_string();

		$this->_view['udis'] = $udis ;
		$this->_view['page'] = $page ;
		$this->_view['current_page'] = $page_index;
	}

	/**
	 * 创建/编辑一个UDI
	 **/
	function actionCreate()
	{
		$id = isset($this->_context->id)?$this->_context->id:0;
		if($id)
		{
			QLog::log('start to edit the udi with id '.$id,QLog::DEBUG );
			$udi = UDI::find('id=?',$id)->query();
		}
		else
		{
			QLog::log('start to create a new udi',QLog::DEBUG );
			$udi = new UDI();
		}

		$this->_view['udi'] = $udi;
	}
	/**
	 * 保存或更新一个UDI
	 **/
	function actionSave()
	{
		$id = isset($this->_context->id)?$this->_context->id:0;
		if($id)
		{
			$udi = UDI::find('id=?',$id)->query();
			$message = 'UDI更新成功';
		}
		else
		{
			$udi = new UDI();
			$message = 'UDI创建成功';
		}
		$udi->namespace = $this->_context->post('namespace');
		$udi->controller = $this->_context->post('controller');
		$udi->action = $this->_context->post('action');
		$udi->module = $this->_context->post('module');
		$udi->check_type = $this->_context->post('check_type');
		$udi->desc = $this->_context->post('desc');

		$udi->save();

		return $this->_redirectMessage($message,'',url('admin::udi/index'));

	}

}


