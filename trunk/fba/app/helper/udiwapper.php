<?php
  /*
    *UDI包装类,负责验证用户对某个资源的访问权限
    */
  class UDIWapper{
    
    private $_udi_list = array();
    
    private $_udi_role_assoc_list = array();
    
    private function __construct(){
       
    }
    static function instance()
    {
        $instance;
        Q::cleanCache('UIDWapper');
        if (!Q::cache('UIDWapper'))
        {
            QLog::log('start init UIDWapper....');
        	$instance = new UDIWapper();
            $instance->_loadUDISetting();
            $instance-> _loadRoleUDIAssoc();
            Q::writeCache('UIDWapper',$instance);
        }
        else 
        {
        	QLog::log('UIDWapper Exist get from cache....');
        	$instance = Q::cache('UIDWapper');
        }
        return $instance;
    }
    /*
         *加载UDI,保存在array中,以key => value的形式保存
         *key:该UDI对应的String 表现形式
         *value:UDI对应模型的hash表
         */
    private function _loadUDISetting()
    {
       $udis = UDI::find()->getAll();
       foreach($udis as $udi)
       {
       	  $udi_str_key = $this->_UDIString($udi);
       	  
       	  $udi_hash = array();
       	  $udi_hash['namespace'] = $udi->namespace;
       	  $udi_hash['controller']=$udi->controller;
       	  $udi_hash['action']=$udi->action;
       	  $udi_hash['module']=$udi->module;
       	  $udi_hash['check_type']=$udi->check_type;
       	  
          $this->_udi_list[$udi_str_key] = $udi_hash;
       }
    }
    /*
          *加载UDI与角色关联表,以key=>value形式保存
          *key:该UDI对应的String表现形式
          *value:为一数组,保存了对该UDI具有访问权限的所有角色
          */
    private function _loadRoleUDIAssoc()
    {
       $role_udi_assocs = RoleUDIAssoc::find();
       
       foreach($role_udi_assocs as $role_udi_assoc)
       {
          $uid = Uid::find('id = ?',$role_udi_assoc->udi)->query();
          $uid_str_key = $this->_UDIString($udi);
          if(isset($this->_udi_role_assoc_list[$uid_str_key ]))
          {
            array_push($this->_udi_role_assoc_list[$uid_str_key],$udi_role_assoc->role_id);
          }
          else
          {
            $udi_role = array($udi_role_assoc->role_id);
            $this->_udi_role_assoc_list[$uid_str_key] = $udi_role;
          }
       }   
    }
    /*
          *验证用户对某个UDI的权限
          *
          *@param  array $roles
          *@param  string $udi_str_key
          *
          *@return Boolean
          */
    function rolesBasedCheck($roles,$udi_str_key)
    {
       if(empty($roles))
       {
       	  /*just for debug*/ 
       	  array_push($roles,'member');
       }
       /*如果该UDI不存在列表中,返回False*/
       if(!isset($this->_udi_list[$udi_str_key]))
       {
           $key_list = array_keys($this->_udi_list); 
           QLog::log('udi '.$udi_str_key.' not in udi list');
       	   return false;
       }
       $udi_hash = $this->_udi_list[$udi_str_key];
       $check_type = $udi_hash['check_type'];
       if($check_type == 'ALLOW')
       {
         /*如果该UDI的验证类型为ALLOW,则返回True*/ 
         return true;
       }else if($check_type == 'ROLE'){
         /*如果该UDI的验证类型为ROLE,则当用户角色表不为空,则返回True*/
         if(!empty($roles))
         {
         	return true;
         }else{
         	QLog::log('user has not roles');
            return false;
         }
       }else if($check_type == 'DENY'){
            /*如果该UDI的验证类型为DENY,则对用户的角色列表和对该UDI拥有权限的所有角色列表求交集
            **如果存在交集则返回True,反之返回False
            */
            $allow_roles = $this->_udi_role_assoc_list[$udi_str_key];
            if(array_intersect($roles,$allow_roles))
            {
               return true;
            }            
       }else {
       	   QLog::log('not defined check type');
       }
       return false;
    }
    /*
           *生成UDI的string形式
           *
           *@param Udi $udi
           *
           *@return string
          */
    private function _UDIString($udi)
    {
       $string = '';

        if (!empty($udi->namespace))
        {
            $string = $udi->namespace . '::';
        }
        if (!empty($udi->controller))
        {
            $string .= $udi->controller;
        }
        if (!empty($udi->action))
        {
            $string .= '/' . $udi->action;
        }
        if (!empty($udi->module))
        {
            $string .= '@' . $udi->module;
        }

        return $string;
    }
  }

