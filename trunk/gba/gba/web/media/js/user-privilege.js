//封装计划任务的数据交换 ---begin
var web_service_privilege = null; 
window.onload = function() {
	web_service_privilege = new rpc.ServiceProxy(
		"/services/accounts/privilege/",{
			asynchronous: false,
			methods:[
			'add_user',
			'check_user_exist',
			'query_users',
			'delete_user',
			'query_pages',
			'add_page',
			'modify_page',
			'delete_page',
			'get_user_privilege',
			'get_user_privileges',
			'modify_privileges',
			'modify_privilege'
			]
		});
};

var get_pages_service = function(){
	try{
		var result = web_service_privilege.query_pages();
		return result;
	}catch(e){
		return null;
	}
}

var delete_page_service = function(page_id){
	var execute_ok = false;
	var reason = "";
	try{
		var result = web_service_privilege.delete_page(page_id); //[result, msg]
		execute_ok = result[0];
		reason = result[1];
	}catch(e){}
	if(!execute_ok){
		Ext.MessageBox.alert("错误","删除页面信息失败。" + reason);
	}
	return execute_ok;	
}

var add_page_service = function(page_name,page_descript){
	var execute_ok = false;
	var reason = "";
	try{
		var result = web_service_privilege.add_page(page_name,page_descript); //[result, msg]
		execute_ok = result[0];
		reason = result[1];
	}catch(e){}
	if(!execute_ok){
		Ext.MessageBox.alert("错误","新建页面信息失败。" + reason);
	}
	return execute_ok;	
}

var modify_page_service = function(page_id, page_name,page_descript){
	var execute_ok = false;
	var reason = "";
	try{
		var result = web_service_privilege.modify_page(page_id, page_name,page_descript); //[result, msg]
		execute_ok = result[0];
		reason = result[1];
	}catch(e){}
	if(!execute_ok){
		Ext.MessageBox.alert("错误","修改页面信息失败。" + reason);
	}
	return execute_ok;	
}

var get_users_service = function(){
	try{
		var result = web_service_privilege.query_users();
		return result;
	}catch(e){
		return null;
	}
}

var get_user_privileges_service = function(user_id){
	try{
		var result = web_service_privilege.get_user_privileges(user_id);
		return result;
	}catch(e){
		return null;
	}
}
var modify_user_privileges_service = function(user_id, page_id, privilege){
	try{
		var result = web_service_privilege.modify_privilege(user_id, page_id, privilege);
		return result;
	}catch(e){
		return null;
	}
}
var check_user_exist_service = function(username){
	try{
		var result = web_service_privilege.check_user_exist(username);
		return result;
	}catch(e){
		return null;
	}	
}
var delete_user_service = function(user_id){
	try{
		var result = web_service_privilege.delete_user(user_id);
		return result;
	}catch(e){
		return null;
	}	
}
var add_user_service = function(username){
	try{
		var result = web_service_privilege.add_user(username);
		return result;
	}catch(e){
		return null;
	}
}

/************** 网页处理部分************************/

Ext.onReady(function(){
   // Ext.QuickTips.init();
	Ext.BLANK_IMAGE_URL = '/site_media/ext3/resources/images/default/s.gif'

	var current_page_id = 10;
	var current_page_privilege = 0;
    var modify_page_info = null;
    var current_user_id = "0";			//当前被分配权限的用户ID

    /************** 用户信息部分************************/
    var store_user_info = new Ext.data.ArrayStore({
		fields: ['username','userid'],
	    data: []
	});
	var user_title = "用户权限管理 (选中后单击可以修改权限)";
	
    var user_info_panel = new Ext.FormPanel({
        url:'',
        frame:true,
        title: user_title,
        bodyStyle:'padding:5px 5px 0',
        items: [{
            items: [{
            	columnWidth:600,
            	layout: 'column',
            	items:[{
            		xtype: 'combo',
            		name: "username",
	                store: store_user_info,
			        displayField: 'username',
			        valueField: 'userid',
			        typeAhead: true,
			        selectOnFocus:true,
			        mode: 'local',
			        editable: true,
			        triggerAction: 'all',
			        emptyText:'请选择或者输入用户名'
	            }, {
	                xtype: 'button',
	                text: ' 查询权限 ',
	                anchor:'95%',
	                handler: function(){
	                	refresh_user_privilege_by_click();
		            }
	            }, {
	                xtype: 'button',
	                text: ' 分配登录权限 ',
	                anchor:'95%',
	                handler: function(){
	                	add_user_login_rights();
		            }
	            },{
	                xtype: 'button',
	                text: ' 分配全部权限 ',
	                anchor:'95%',
	                handler: function(){
	                	var user_id = -1;
				    	var user_id_temp = user_info_panel.getForm().findField('username').getValue();
				    	var username_text = user_info_panel.getForm().findField('username').getEl().getValue();
				    	user_id = parseInt(user_id_temp);
				    	if(isNaN(user_id)){
				    		Ext.MessageBox.alert("用户"+user_id_temp+"目前没有登陆权限。");
				    		return;
				    	}
				    	current_user_id = user_id;
				    	var username_text = user_info_panel.getForm().findField('username').getEl().getValue(); 
    					user_info_panel.setTitle(user_title + "&   ------   " + username_text);
    					
	                	//当前用户信息 current_user_id
	                	var pages_info = get_pages_service(); //查询到页面信息，格式如：[[id,page_name,page_descript],[,,]]
				    	if(pages_info != null && pages_info !== undefined){
							for(var i=0;i<pages_info.length;i++){
								modify_user_privileges_service(current_user_id, pages_info[i][0], 3);
							}
				    	}
		            	load_user_privilege(current_user_id);
		            }
	            }, {
	                xtype: 'button',
	                text: '禁止登录 ',
	                anchor:'95%',
	                handler: function(){
	                	disable_user_login_rights();
		            }
	            }]
            }]
        }]
    });
    var disable_user_login_rights = function(){
    	var user_id = -1;
    	var user_id_temp = user_info_panel.getForm().findField('username').getValue();
    	var username_text = user_info_panel.getForm().findField('username').getEl().getValue();
    	user_id = parseInt(user_id_temp);
    	if(isNaN(user_id)){
    		Ext.MessageBox.alert("用户"+username_text+"目前没有登陆权限。");
    		return;
    	}
		var result = delete_user_service(user_id);
		if(!result[0]){
			Ext.MessageBox.alert("错误",result[1]);
			return;
		}else{
			Ext.MessageBox.alert("禁止"+username_text+"登录成功！");	
			user_info_panel.getForm().findField('username').setValue("");
		}
		refresh_user();	//这个要处理一下，防止列表中存在，又增加一次   		
    	user_info_panel.setTitle(user_title);
    	store_user_privilege_info.removeAll();
    	current_user_id = 0;
    }
    
    var add_user_login_rights = function(){
    	var user_id = -1;
    	var user_id_temp = user_info_panel.getForm().findField('username').getValue();
    	var username_text = user_info_panel.getForm().findField('username').getEl().getValue();
    	user_id = parseInt(user_id_temp);
    	if(isNaN(user_id)){
    		var result = add_user_service(user_id_temp);
    		if(!result[0]){
    			Ext.MessageBox.alert("错误",result[2]);
    			return;
    		}else{
				Ext.MessageBox.alert("分配"+username_text+"登录权限成功！");
				user_id = parseInt(result[1]);
    			refresh_user();	//这个要处理一下，防止列表中存在，又增加一次
    			user_info_panel.getForm().findField('username').setValue(user_id);	
    		}
    	}else{
    		Ext.MessageBox.alert("用户"+username_text+"已经有登陆权限了。");
    	}
    	user_info_panel.setTitle(user_title + "&   ------   " + username_text);
    	load_user_privilege(user_id);
    	current_user_id = user_id;
    }
    var refresh_user_privilege_by_click = function(){
    	var user_id = -1;
    	var user_id_temp = user_info_panel.getForm().findField('username').getValue();
    	user_id = parseInt(user_id_temp);
    	if(isNaN(user_id)){
    		var result = check_user_exist_service(user_id_temp);
    		if(!result){
    			store_user_privilege_info.removeAll();
    			Ext.MessageBox.alert(user_id_temp+ "不是from gba授权用户。");
    			return;
    		}
    	}
    	var username_text = user_info_panel.getForm().findField('username').getEl().getValue(); 
    	user_info_panel.setTitle(user_title + "&   ------   " + username_text);
    	load_user_privilege(user_id);
    	current_user_id = user_id;
    }
    
    user_info_panel.render(Ext.get("username_div"));
    user_info_panel.getForm().findField('username').on('select', function() {
    	refresh_user_privilege_by_click();
    });
    
    /************** 用户权限部分************************/
    var UserPrivilegeInfo = new Ext.data.Record.create([
		{name: 'page_id'},
		{name: 'page_name'}, 
		{name: 'select_privilege', type: 'bool'},
		{name: 'update_privilege', type: 'bool'}
		]);
 	
    var store_user_privilege_info = new Ext.data.GroupingStore({
        reader: new Ext.data.JsonReader({fields: UserPrivilegeInfo}),
		data: [],
        sortInfo: {field: 'page_name', direction: 'ASC'}
    });
    
    var editor = new Ext.ux.RowEditor({
        saveText: '更 新',
        cancelText: '取 消',
        kingsoft_enable: false
    });

    var grid_user_privilege_info = new Ext.grid.GridPanel({
        store: store_user_privilege_info,
        width: 600,
        height: 240,
        region:'center',
        margins: '0 5 5 5',
        loadMask: true,
        plugins: [editor],
        sm: new Ext.grid.RowSelectionModel({singleSelect:true}),
        view: new Ext.grid.GroupingView({
            markDirty: false  
        }),
        columns: 
        [new Ext.grid.RowNumberer(),
        {
            id: 'page_id',
            header: '页面编号',
            dataIndex: 'page_id',
            width: 85,
            sortable: true
        },{
			header: '页面名称',
			dataIndex: 'page_name',
			width: 200,
            sortable: true
        },{
        	xtype: 'booleancolumn',
			header: '允许查询',
			dataIndex: 'select_privilege',
			align: 'center',
			width: 85,
            trueText: '是',
            falseText: '否',
            editor: {
                xtype: 'checkbox'
            }
		 },{
		 	xtype: 'booleancolumn',
			header: '允许修改',
			dataIndex: 'update_privilege',
			align: 'center',
			width: 85,
            trueText: '是',
            falseText: '否',
            editor: {
                xtype: 'checkbox'
            }
		 }]
    });
	
    store_user_privilege_info.addListener('update', function(st, rds, opts){
    	// st 是当前的store, rds是读到的Record[], opts是store的配置
    	if(current_user_id <= 0) return;
    	var privilege = 0;
    	if(rds.get("select_privilege")) privilege += 1;
    	if(rds.get("update_privilege")) privilege += 2;
    	var page_id = parseInt(rds.get("page_id"));
    	if(isNaN(page_id)) return;
    	var result = modify_user_privileges_service(current_user_id, page_id, privilege);
    	if(!result[0]){
    		Ext.MessageBox.alert("错误", result[1]);
			load_user_privilege(current_user_id);
    	}
    });
    
	var user_privilege_info_panel = new Ext.Panel({
        title: '',
        layout: 'border',
        layoutConfig: {
            columns: 1
        },
        height:245,
        items: [grid_user_privilege_info]
    });
	
    user_privilege_info_panel.render(Ext.get("user_privilege_div"));
    
    var load_user_privilege = function(user_id){
    	store_user_privilege_info.removeAll();
    	if(user_id == 0 || user_id == "0") return;
    	var pages_info = get_pages_service(); //查询到页面信息，格式如：[[id,page_name,page_descript],[,,]]
    	var user_privilege = get_user_privileges_service(user_id); //查询用户权限信息，格式如：[[page_id, privilege],[,]]
    	
    	if(pages_info != null && pages_info !== undefined){
			for(var i=0;i<pages_info.length;i++){
				var privilege = 0;
				if(user_privilege != null && user_privilege !== undefined){
					for(var j=0;j<user_privilege.length;j++){
						if(pages_info[i][0] == user_privilege[j][0]){
							privilege = parseInt(user_privilege[j][1]);
							break;
						}
					}
				}

				var query_privilege = ((privilege & 1) == 0 ? false : true);
				var update_privilege = ((privilege & 2) == 0 ? false : true);
				
				var record_temp = new UserPrivilegeInfo({
					page_id: pages_info[i][0],
					page_name: pages_info[i][1],
					select_privilege: query_privilege,
					update_privilege: update_privilege
				});
				
				store_user_privilege_info.add(record_temp);
			}
    	}
    }
    
    /************** 页面维护部分************************/
    var page_info_formpanel = new Ext.FormPanel({
    	labelWidth: 75, // label settings here cascade unless overridden
        url:'',
        frame:true,
        title: '',
        bodyStyle:'padding:5px 5px 0',
        width: 350,
        defaults: {width: 250},
        defaultType: 'textfield',
        items: [{
                fieldLabel: '页面名称',
                name: 'page_name',
                allowBlank:false
            },{
                fieldLabel: '页面描述',
                name: 'page_descript'
            }],
        buttons: [{
            text: '保存',
            handler: function(){
            	var page_name_ = page_info_formpanel.getForm().findField('page_name').getValue();
            	var page_descript_ = page_info_formpanel.getForm().findField('page_descript').getValue();
            	if(page_name_.length < 1){
            		Ext.MessageBox.alert("错误","请输入正确的页面名称。");
            		return;
            	}
            	if(modify_page_info != null){//修改
            		var page_id_ = modify_page_info.get("id");
            		if(modify_page_service(page_id_, page_name_, page_descript_)){
            			refresh_page();
            			load_user_privilege(current_user_id);
            		}
            	}else{//新增
            		if(add_page_service(page_name_, page_descript_)){
            			refresh_page();
            			load_user_privilege(current_user_id);
            		}
            	}
            	modify_page_info = null;
	            task_operator_window.hide();
	        }
        },{
            text: '关闭',
            handler: function(){
            	modify_page_info = null;
	            task_operator_window.hide();
	        }
        }]
    });

    var task_operator_window = new Ext.Window({
	    id: "page_win",
	    title: "页面信息",
	    layout: "fit",
	    width: 400,
	    height: 150,
	    closeAction: 'hide',
	    items: page_info_formpanel,
	    listeners: {
	        "show": function(){
	        	if(modify_page_info != null){
	        		page_info_formpanel.getForm().findField('page_name').setRawValue(modify_page_info.get("page_name"));
					page_info_formpanel.getForm().findField('page_descript').setRawValue(modify_page_info.get("page_descript"));
	        	}else{
	        		page_info_formpanel.getForm().findField('page_name').setRawValue("");
	        		page_info_formpanel.getForm().findField('page_descript').setRawValue("");
	        	}
	        }
	    }
	});
    
	var PageInfo = new Ext.data.Record.create([
		{name: 'id'}, 
		{name: 'page_name'}, 
		{name: 'page_descript'}]);
 	
    var store_page_info = new Ext.data.GroupingStore({
        reader: new Ext.data.JsonReader({fields: PageInfo}),
		data: [],
        sortInfo: {field: 'page_name', direction: 'ASC'}
    });
    
    var grid_page_info = new Ext.grid.GridPanel({
        store: store_page_info,
        width: 600,
        region:'center',
        margins: '0 5 5 5',
        loadMask: true,
        height:200,
        sm: new Ext.grid.RowSelectionModel({singleSelect:true}),
        view: new Ext.grid.GroupingView({
            markDirty: false  
        }),
        tbar: [{
        	ref: '../addBtn',
            iconCls: 'task-add',
            text: '新建页面',
            handler: function(){
            	modify_page_info = null;
            	task_operator_window.show();
            }
        },{
            ref: '../removeBtn',
            iconCls: 'task-delete',
            text: '删除页面',
            disabled: true,
            handler: function(){
                var record_ = grid_page_info.getSelectionModel().getSelected();
                var page_name = record_.get("page_name");
                if(page_name != undefined){
                	Ext.MessageBox.confirm("确认","您确认要删除页面[" + page_name+"]吗？",
                		function(btn, text){
    						if (btn == 'yes'){
    							var page_id_ = record_.get("id");
        						if(page_id_ !== undefined && delete_page_service(page_id_)){
                		 			refresh_page();
                		 			load_user_privilege(current_user_id);
        						}
    						}   
    					}
                	);
                }
            }
        },{
			ref: '../modifyBtn',
            iconCls: 'task-modify',
            text: '修改页面',
            disabled: true,
            handler: function(){
                var record_ = grid_page_info.getSelectionModel().getSelected();
                modify_page_info = record_;
               	task_operator_window.show();
            }
        }],

        columns: [
        new Ext.grid.RowNumberer(),
        {
            id: 'id',
            header: '编号',
            dataIndex: 'id',
            width: 55,
            sortable: true
        },{
			header: '页面名称',
			dataIndex: 'page_name',
			width: 200,
            sortable: true
        },{
			header: '页面描述',
			dataIndex: 'page_descript',
			width: 300,
            sortable: true,
            renderer: function(v, p){	//v : value , p : cell
				p.attr += " ext:qtip='" + v + "'";
				return v;
			}
		 }],

        listeners:{
        	 rowdblclick :function(self,rowIndex, e){
        	 	if(current_page_privilege >= 2){
        	 		var record_ = grid_page_info.getSelectionModel().getSelected();
                	modify_page_info = record_;
               		task_operator_window.show();
        	 	}
    		}
        }
    });
	
	var page_info_panel = new Ext.Panel({
        title: '页面信息管理',
        layout: 'border',
        layoutConfig: {
            columns: 1
        },
        height:200,
        items: [grid_page_info]
    });
	
    page_info_panel.render(Ext.get("page_info_div"));

    grid_page_info.getSelectionModel().on('selectionchange', function(sm){
        grid_page_info.removeBtn.setDisabled(sm.getCount() < 1 || current_page_privilege < 2);
		grid_page_info.modifyBtn.setDisabled(sm.getCount() < 1 || current_page_privilege < 2);
    });
    
    /************** 其他用途部分************************/
    
    var refresh_privilege_enable = function(privilege){
    	//这里处理按钮是否是disable的
    	if(privilege < 2){
    		grid_page_info.addBtn.setDisabled(true);
    		grid_page_info.removeBtn.setDisabled(true);
    		grid_page_info.modifyBtn.setDisabled(true);
    	}else{
    		editor["kingsoft_enable"] = true;
    	}
    }
    
    var get_privilege_info = function(page_id){
    	current_page_privilege = 0;
    	var result = web_service_privilege.get_user_privilege(current_page_id);
    	if(result != null){
    		current_page_privilege = parseInt(result["privilege"]);
    	}
		refresh_privilege_enable(current_page_privilege);    	
    }
    
    var refresh_page = function(){
    	store_page_info.removeAll();
     	var pages_info = get_pages_service();
    	if(pages_info != null && pages_info !== undefined){
			for(var i=0;i<pages_info.length;i++){
				var record_ = new PageInfo({
					id: pages_info[i][0],
					page_name: pages_info[i][1],
					page_descript: pages_info[i][2]
				});
				store_page_info.add(record_);
			}
    	}
    }
    
    var refresh_user = function(){
    	store_user_info.removeAll();
    	var user_info = get_users_service();//格式：[[userid,username],[]]
    	if(user_info != null && user_info !== undefined){
			for(var i=0;i<user_info.length;i++){
				var user_info_temp = new Array();
				user_info_temp["username"] = user_info[i][1];
				user_info_temp["userid"] = user_info[i][0];
				var record_temp = new Ext.data.Record(user_info_temp);
				store_user_info.add(record_temp);
			}
    	}
    }
    
    refresh_user.defer(500, this, null);
    get_privilege_info.defer(600, this, null);//打开页面先延时500毫秒查询权限
    refresh_page.defer(700, this, null);//再延时500毫秒显示一下任务
});
