<?php
// $Id$

/**
 * Controller_Admin_User controller
 */
class Controller_Admin_User extends Controller_Abstract
{
	/*
	*用户管理页面
	*/
	function actionIndex()
	{
		$page_index = 1 ;
		if(isset($this->_context->page))
		{
			$page_index = $this->_context->page;
		}
		$query = User::find()->limitPage($page_index,10);

		$users =$query->query();
		$pagination = $query->getPagination();

		$udi = QContext::instance()->requestUDI('array');
		$page = new Page($udi,$pagination );

		$page->to_string();

		$this->_view['users'] = $users ;
		$this->_view['page'] = $page ;
		$this->_view['current_page'] = $page_index;
	}

	/*
	*删除用户
	*/
	function actionDelete()
	{
		/*实现删后返回原来页面*/
		$page_index = 1 ;
		if(isset($this->_context->page))
		{
			$page_index = $this->_context->page;
		}
		$id = $this->_context->id;
		$user = User::find('id=?',$id)->query();

		if(!empty($user->roles))
		{
			return $this->_redirectMessage('用户删失败,请先删除用户所有角色','',url('admin::user/index',array('page' =>$page_index)));
		}else
		{
			$user->destroy();
		}

		return $this->_redirectMessage('用户删成功','',url('admin::user/index',array('page' =>$page_index)));
	}
	/**
	 * 用户角色设定
	 **/
	function actionSetting()
	{
		if(!isset($this->_context->id))
		{
			return $this->_redirectMessage('用户ID为空','',url('admin::user/index',array('page' =>$page_index)));
		}
		$id = $this->_context->id;

		$user = User::find('id=?',$id)->query();

		$exist_roles = $user->roles;

		$roles = Role::find()->getAll();

		foreach ($roles as $role)
		{
			if(in_array($role,$exist_roles))
			{
				$index = array_search($role,$roles);
				unset($roles[$index]);
			}
		}
		reset($roles);

		$this->_view['exist_roles'] = $exist_roles;
		$this->_view['roles'] = $roles;
		$this->_view['user'] = $user;
	}
	/**
	 * 保存用户角色设定
	 */
	function actionSaveSetting()
	{
		$rolesStr = $this->_context->rolesStr;
		//删除最后的';'号
		if(strlen($rolesStr) > 0)
		{
			$rolesStr = substr($rolesStr,0,strlen($rolesStr)-1);
		}
		$roles_ids = array();
		if($rolesStr)
		{
			$roles_ids = split(";",$rolesStr);
		}
		QLog::log($rolesStr);
		$id = isset($this->_context->id)?$this->_context->id:0;
		if(!$id)
		{
			return $this->_redirectMessage('ID未定义','',url('admin::user/index'));
		}

		$user = User::find('id=?',$id)->query();
		if(!$user->id)
		{
			return $this->_redirectMessage('要编辑的用户不存在','id:'.$id,url('admin::user/index'));
		}

		$exist_roles = $user->roles;
		$exist_role_ids = array();
		foreach ($exist_roles as $exist_role)
		{
			array_push($exist_role_ids,$exist_role->id);
		}
		foreach ($roles_ids as $role_id)
		{
			$user_role_assoc = UserRoleAssoc::find('user_id=? and role_id=?',$id,$role_id)->query();

			//如果不存在则创建
			if(!$user_role_assoc->id)
			{
				Qlog::log('start to create user role assoce id:'.$id.'role_id:'.$role_id);
				$user_role_assoc->user_id = $id;
				$user_role_assoc->role_id = $role_id;
				$user_role_assoc->save();
			}
			else
			{
				//在原存在角色中做标记,不标记的都删除
				unset($exist_role_ids[array_search($role_id,$exist_role_ids)]);
			}
		}
		reset($exist_role_ids);

		foreach ($exist_role_ids as $remove_role_id)
		{
			$user_role_assoc = UserRoleAssoc::find('user_id=? and role_id=?',$id,$remove_role_id)->query();
			$user_role_assoc->destroy();
		}


		return $this->_redirectMessage('用户角色信息更新成功','',url('admin::user/index'));

	}


}


