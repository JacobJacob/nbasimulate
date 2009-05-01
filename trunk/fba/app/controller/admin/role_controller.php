<?php
// $Id$

/**
 * Controller_Admin_Role controller
 */
class Controller_Admin_Role extends Controller_Abstract
{

	function actionIndex()
	{
		$page_index = 1 ;
		if(isset($this->_context->page))
		{
			$page_index = $this->_context->page;
		}
		$query = Role::find()->limitPage($page_index,10);

		$roles = $query->query();
		$pagination = $query->getPagination();

		$udi = QContext::instance()->requestUDI('array');
		$page = new Page($udi,$pagination );

		$page->to_string();

		$this->_view['roles'] = $roles ;
		$this->_view['page'] = $page ;
		$this->_view['current_page'] = $page_index;
	}
	/**角色权限设定**/
	function actionSetting()
	{
		/***/
		$role_id = $this->_context->id;

		$role = Role::find('id=?',$role_id)->query();

		$udis = UDI::find()->all()->query();

		$temp_array = array();

		foreach ($udis as $udi)
		{
			$path = QContext::instance()->UDIString(array('namespace'=>$udi->namespace,
			'controller'=>$udi->controller,'action'=>$udi->action,'module'=>$udi->module));
			$row = array();

			$row['id'] = $udi->id;
			$row['path'] = $path;
			$row['desc'] = $udi->desc;


			/**判断当所设置的角色是否拥有当前UDI的权限**/
			$role_udi_assoc = RoleUDIAssoc::find('role_id=? and udi=?',$role_id,$udi->id)->query();
			if($role_udi_assoc->id)
			{
				$row['checked'] = 'checked';
			}else
			{
				$row['checked'] = '';
			}

			array_push($temp_array,$row);
		}
		$udis = $temp_array;

		$this->_view['role_id'] = $role_id ;
		$this->_view['role_name'] = $role->role_name ;
		$this->_view['udis'] = $udis ;
	}
	/**保存角色权限设定**/
	function actionSaveSetting()
	{
		$role_id = $this->_context->id;
		$checks = $this->_context->checks;
		$rowids = $this->_context->rowids;

		foreach ($checks as $check)
		{
			$udi = $rowids[$check];
			$role_udi_assoc = RoleUDIAssoc::find('role_id=? and udi=?',$role_id,$udi)->query();
			/**如果原表中没有关联,则新建关联**/
			if(!$role_udi_assoc->id)
			{
				$role_udi_assoc->role_id = $role_id;
				$role_udi_assoc->udi = $udi;
				$role_udi_assoc->save();
			}
			/**处理完某一个选中的UDI后,我们将要把它删除,因为我们将在后面删除掉没有选择的项**/
			unset($rowids[$check]);
		}
		reset($rowids);
		/**下面处理未给权限的UDI关联**/
		foreach ($rowids as $udi)
		{
			$role_udi_assoc = RoleUDIAssoc::find('role_id=? and udi=?',$role_id,$udi)->query();
			/**如果原表中存在关联,则删除该关联**/
			if($role_udi_assoc->id)
			{
				$role_udi_assoc->destroy();
			}

		}
		return $this->_redirectMessage('角色权限设置成功','',url('admin::role/setting',array('id'=>$role_id)));

	}
	/**
	*创建或编辑角色
	**/
	function actionCreate()
	{
		$id = isset($this->_context->id)?$this->_context->id:0;

		if($id)
		{
			$role = Role::find('id=?',$id)->query();
		}
		else
		{
			$role = new Role();
		}

		$this->_view['role'] = $role;
	}
	/**
	 * 保存角色,存在更则更新,不存在则创建
	 * **/
	function actionSave()
	{
		$id = isset($this->_context->id)?$this->_context->id:0;

		if($id)
		{
			$role = Role::find('id=?',$id)->query();
			$message = '角色信息更新成功';
		}else{
			$role = new Role();
			$message = '角色信息创建成功';
		}
		$role->role_name =  $this->_context->name ;
		$role->role_desc =  $this->_context->desc ;

		$role->save();

		return $this->_redirectMessage($message,'',url('admin::role/index'));
	}
}