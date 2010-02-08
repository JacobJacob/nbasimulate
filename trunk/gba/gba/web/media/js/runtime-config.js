//封装Runtime Config的数据交换 ---begin
var web_service = null; 
window.onload = function() {
	web_service = new rpc.ServiceProxy(
		"/services/runtime_config/",{
			asynchronous: false,
			methods:[
						'get_all_runtime_config',
						'get_runtime_config_by_id',
						'add_runtime_config',
						'update_runtime_config',
						'delete_runtime_config'
					]
	});
};

var get_all_runtime_config_service = function(){
	try{
		var result = web_service.get_all_runtime_config();
		return result;
	}catch(e){
		return null;
	}
}
var get_runtime_config_by_id_service = function(id){
	try{
		var result = web_service.get_runtime_config_by_id(id);
		if(result !== undefined){
			return result;
		}
	}catch(e){}
	return null;
}

var add_runtime_config_service = function(runtime_config){
	var row_id = 0;
	try{
		var result = web_service.add_runtime_config(runtime_config);//return (lastrowid, row_effected)
		if(result[1] == "1"){
			row_id = result[0];
		}		
	}catch(e){}
	if(row_id == 0){
		Ext.MessageBox.alert("错误","新建运行时配置失败。");
	}
	return row_id;	
}

var update_runtime_config_service = function(runtime_config){
	var execute_ok = false;
	try{
		var result = web_service.update_runtime_config(runtime_config); //row_effected
		if(result == "1") execute_ok = true; 
	}catch(e){}
	if(!execute_ok){
		Ext.MessageBox.alert("错误","更新运行时配置失败。");
	}
	return execute_ok;
}

var delete_runtime_config_service = function(id){
	var execute_ok = false;
	try{
		var result = web_service.delete_runtime_config(id);//row_effected
		if(result == "1") execute_ok = true; 
	}catch(e){}
	if(!execute_ok){
		Ext.MessageBox.alert("错误","删除运行时配置失败。");
	}
	return execute_ok;	
}
//封装运行时配置的数据交换 ---end

var tigger_disable = false;  //ugly 用了一个全局变量来同步是否触发更新

Ext.onReady(function(){
   // Ext.QuickTips.init();
	Ext.BLANK_IMAGE_URL = '/site_media/ext3/resources/images/default/s.gif'

	var RuntimeConfig = new Ext.data.Record.create([
		{name: 'id',type:'string'},
		{name: 'ip',type:'string'}, 
		{name: 'program',type:'string'}, 
		{name: 'config_key',type:'string'}, 
		{name: 'config_value',type:'string'}, 
		{name: 'remark',type:'string'}, 
		{name: 'created_time',type:'string'}]);
 	
    var store_runtime_config = new Ext.data.GroupingStore({
        reader: new Ext.data.JsonReader({fields: RuntimeConfig}),
		data: [],
        sortInfo: {field: 'id', direction: 'ASC'}
    });
    
    var editor = new Ext.ux.RowEditor({
        saveText: '更 新',
        cancelText: '取 消',
        kingsoft_enable: true
    });
    
    var grid_runtime_config = new Ext.grid.GridPanel({
        store: store_runtime_config,
        width: 600,
        region:'center',
        margins: '0 5 5 5',
        loadMask: true,
        plugins: [editor],
        sm: new Ext.grid.RowSelectionModel({singleSelect:true}),
        view: new Ext.grid.GroupingView({
            markDirty: false  
        }),        
        tbar: [{
        	 ref: '../addBtn',
            iconCls: 'task-add',
            text: '新建配置',
            handler: function(){
            	editor.stopEditing();
            	var rc = new RuntimeConfig({
                    id: "",
                    ip: "",
                    program: "",
                    config_key: "",
                    config_value: "",
                    remark: "",
                    created_time: (new Date()).format('y-m-d') 
                });
                store_runtime_config.insert(0, rc);
                grid_runtime_config.getView().refresh();
                grid_runtime_config.getSelectionModel().selectRow(0);
                editor.startEditing(0);
            }
        },{
			ref: '../modifyBtn',
            iconCls: 'task-modify',
            text: '修改配置',
            disabled: true,
            handler: function(){
            	var selectedIndex = -1;
            	for(var i=0;i < store_runtime_config.getCount();i++){
            		if(grid_runtime_config.getSelectionModel().isSelected(i)){
            			selectedIndex = i;
            			break;
            		}
            	}
            	if(selectedIndex >= 0){
            		editor.startEditing(selectedIndex);
            	}
            }
        },{
            ref: '../removeBtn',
            iconCls: 'task-delete',
            text: '删除配置',
            disabled: true,
            handler: function(){
            	editor.stopEditing();
                var record_ = grid_runtime_config.getSelectionModel().getSelected();
                var config_id = record_.get("id");
                if(config_id != undefined){
                	Ext.MessageBox.confirm("确认","您确认要删除编号为[" + config_id+"]的配置吗？",
                		function(btn, text){
    						if (btn == 'yes'){
   								if(delete_runtime_config_service(config_id)){
                		 			refresh_runtime_config();
        						}	
    						}   
    					}
                	);
                }
            }
        },{
        	iconCls: 'task-refresh',
            text: '刷新',
            disabled: false,
            handler: function(){
            	editor.stopEditing();
                refresh_runtime_config();
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
            id: 'ip',
            header: '客户端ip',
            dataIndex: 'ip',
            width: 80,
            sortable: true,
            editor: {
                xtype: 'textfield'
            }
        },{
			header: '程序',
			dataIndex: 'program',
			width: 150,
            sortable: true,
            editor: {
                xtype: 'textfield'        
            }
        },{
			header: '配置关键字',
			dataIndex: 'config_key',
			width: 100,
            sortable: true,
            editor: {
                xtype: 'textfield',
                allowBlank: true
            }
            
		 },{
			header: '配置值',
			dataIndex: 'config_value',
			width: 200,
            sortable: true,
            editor: {
                xtype: 'textfield'
            }
		 },{
			header: '备注',
			dataIndex: 'remark',
			width: 100,
            sortable: true,
            editor: {
                xtype: 'textfield'
            }
		 },{
			header: '创建时间',
			dataIndex: 'created_time',
			width: 80,
            sortable: true
        }]
    });

 	store_runtime_config.addListener('update', function(st, rds, opts){
    	// st 是当前的store, rds是读到的Record[], opts是store的配置
    	if(tigger_disable) return;
    	var current_config_id = parseInt(rds.get("id"));
    	var config_info = {};
    	config_info["ip"] = rds.get("ip");
    	config_info["program"] = rds.get("program");
    	config_info["config_key"] = rds.get("config_key");
    	config_info["config_value"] = rds.get("config_value");
    	config_info["remark"] = rds.get("remark");
    	if(isNaN(current_config_id)){//新增
    		config_info["created_time"] = Date();
    		var row_id = add_runtime_config_service(config_info);
	    	if(row_id > 0){
	    		tigger_disable = true;
	    		rds.data["id"] = "" + row_id;
	    		rds.commit();
	    		tigger_disable = false;
	    	}
    	}else{ //更新    	
	    	config_info["id"] = rds.get("id");
	    	var result = update_runtime_config_service(config_info);
	    	if(!result){
	    		refresh_runtime_config();
	    	}
    	}
    });

    var runtime_config_panel = new Ext.Panel({
        title: '运行时参数配置管理',
        layout: 'border',
        layoutConfig: {
            columns: 1
        },
        height:500,
        items: [grid_runtime_config]
    });

    runtime_config_panel.render(Ext.get("runtime_config_list_div"));
	
    grid_runtime_config.getSelectionModel().on('selectionchange', function(sm){
        grid_runtime_config.removeBtn.setDisabled(sm.getCount() < 1);
        grid_runtime_config.modifyBtn.setDisabled(sm.getCount() < 1);
    });
    
       
    var refresh_runtime_config = function(){
    	store_runtime_config.removeAll();
    	var config_info = get_all_runtime_config_service();
    	if(config_info != null){
			for(var i=0;i<config_info.length;i++){
				var record_ = new RuntimeConfig({
					id: config_info[i]["id"],
					ip: config_info[i]["ip"],
					program: config_info[i]["program"],
					config_key: config_info[i]["config_key"],
					config_value: config_info[i]["config_value"],
					remark: config_info[i]["remark"],
					created_time: config_info[i]["created_time"]
				});
				store_runtime_config.add(record_);
			}
    	}
    }
    refresh_runtime_config.defer(500, this, null);//再延时500毫秒显示一下任务
});
