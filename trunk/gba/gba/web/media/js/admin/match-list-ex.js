
Ext.onReady(function(){
    
	var data_record = new Ext.data.Record.create([
		{name: 'id'},
		{name: 'home_team_id'},
		{name: 'guest_team_id'},
		{name: 'status'},
		{name: 'show_status'},
		{name: 'next_show_status'},
		{name: 'expired_time'},
		{name: 'point'},
		{name: 'type'},
		{name: 'overtime'},
		{name: 'created_time'},
		{name: 'start_time'},
	]);
 	
	var data_read = new Ext.data.JsonReader({totalProperty: 'total', root: 'infos', id: 'id'}, data_record);
	
	var store = new Ext.data.Store({proxy: new Ext.data.HttpProxy({url: '/admin/match_list_json/'}),
        						    reader: data_read,
        							remoteSort: true ,
        					       });
        							         
    var data_grid_panel = new Ext.grid.GridPanel({
        store: store,
        width: 600,
        height: 645,
        region:'center',
        loadMask: true,
        columns: [
           new Ext.grid.RowNumberer()
        ,{
            id: 'id',
            header: '编号',
            dataIndex: 'id',
            width: 55,
        },{
            id: 'home_team_id',
            header: '主队id',
            dataIndex: 'home_team_id',
            width: 80,
        },{
            id: 'guest_team_id',
            header: '客队id',
            dataIndex: 'guest_team_id',
            width: 80,
        },{
            id: 'type',
            header: '比赛类型',
            dataIndex: 'type',
            width: 80,
        },{
            id: 'created_time',
            header: '创建时间',
            dataIndex: 'created_time',
            width: 150,
        },{
            id: 'start_time',
            header: '开始时间',
            dataIndex: 'start_time',
            width: 150,
        },{
            id: 'expired_time',
            header: '超时时间',
            dataIndex: 'expired_time',
            width: 150,
        },{
            id: 'status',
            header: '状态',
            dataIndex: 'status',
            width: 60,
        },{
            id: 'show_status',
            header: '显示状态',
            dataIndex: 'show_status',
            width: 60,
        },{
            id: 'next_show_status',
            header: '下一显示状态',
            dataIndex: 'next_show_status',
            width: 60,
        },{
            id: 'point',
            header: '比分',
            dataIndex: 'point',
            width: 80,
        },{
            id: 'overtime',
            header: '加时次数',
            dataIndex: 'overtime',
            width: 40,
        }],
        bbar: new Ext.PagingToolbar({
	        pageSize: 20,
	        store: store,
	        displayInfo: true,
	        displayMsg: '显示第 {0} 条到 {1} 条记录，一共 {2} 条',
	        emptyMsg: "没有记录"
	    })
      
    });
    var downurl_panel = new Ext.Panel({
        title: '批处理日志',
        layout: 'border',
        renderTo: 'data_div',
        layoutConfig: {
            columns: 1
        },
        height:650,
        items: [data_grid_panel]
    });
    
    store.load();
    
});