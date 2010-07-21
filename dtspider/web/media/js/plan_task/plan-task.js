//封装计划任务的数据交换 ---begin
var web_service = null; 
window.onload = function() {
	web_service = new rpc.ServiceProxy(
		"/services/plan_task/",{
			asynchronous: false,
			methods:[
						'get_tasks',
						'update_task',
						'delete_task',
						'get_task',
						'sync_tasks',
						'delete_current_task',
						'add_task',
						'get_defined_task'
					]
		});
};

var get_tasks_service = function(){
	try{
		var result = web_service.get_tasks();
		return result;
	}catch(e){
		return null;
	}
}

var get_defined_task_service = function(){//return object 格式:cmd:text
	try{
		var result = web_service.get_defined_task();
		if(result !== undefined){
			return result["cmds"];
		}
	}catch(e){}
	return null;
}

var get_task_service = function(task_id){
	try{
		var result = web_service.get_task(task_id);
		return result;
	}catch(e){
		return null;
	}
}

var save_task_service = function(task){
	var execute_ok = false;
	var reason = "";
	try{
		var result = web_service.add_task(task); //[result, {'id': taskid, 'message': msg}]
		execute_ok = result[0];
		reason = result[1]["message"];
	}catch(e){}
	if(!execute_ok){
		Ext.MessageBox.alert("错误","新建任务失败。" + reason);
	}
	return execute_ok;	
}

var update_task_service = function(task){
	var execute_ok = false;
	var reason = "";
	try{
		var result = web_service.update_task(task); //[result, {'id': taskid, 'message': msg}]
		execute_ok = result[0]; 
		reason = result[1]["message"];
	}catch(e){}
	if(!execute_ok){
		Ext.MessageBox.alert("错误","更新任务失败。" + reason);
	}
	return execute_ok;
}

var delete_task_service = function(task_id){
	var execute_ok = false;
	var reason = "";
	try{
		var result = web_service.delete_task(task_id);//[result, {'id': taskid, 'message': msg}]
		execute_ok = result[0]; 
		reason = result[1]["message"];
	}catch(e){}
	if(!execute_ok){
		Ext.MessageBox.alert("错误","删除任务失败。" + reason);
	}
	return execute_ok;	
}

var sync_task_service = function(){
	var execute_ok = false;
	var reason = "";
	try{
		var result = web_service.sync_tasks(); //[result,{'message': msg}] 
		execute_ok = result[0];
		reason = result[1]["message"];
	}catch(e){}
	if(!execute_ok){
		Ext.MessageBox.alert("错误","同步任务失败。" + reason);
	}
	return execute_ok;	
}

delete_current_task_service = function(task){
	var execute_ok = false;
	var reason = "";
	try{
		var result = web_service.delete_current_task(task); //[result,{'message': msg}]
		execute_ok = result[0];
		reason = result[1]["message"];
	}catch(e){}
	if(!execute_ok){
		Ext.MessageBox.alert("错误","删除当前任务失败。" + reason);
	}
	return execute_ok;	
}

get_description = function(values) {
	var vals = [];
	var every = false;
	if(values['month'].indexOf('*/') > -1) {
		vals.push('每隔', values['month'].replace('*/', '') + '个月');
		every = true;
	} else if(values['month'] != '*') {
		vals.push('每年', values['month'] + '月');
	}
	
	if(values['week'].indexOf('*/') > -1) {
		if(!every) {
				  vals.push('每隔');
			  }
			  vals.push(values['week'].replace('*/', '') + '个星期');
		every = true;
		  } else if(values['week'] != '*') {
		if(vals.length > 0 && values['week'].indexOf(',') > -1) {
				  vals.push('其中的');
			  }
			  vals.push('星期' + values['week'] + ' ');
		  }
	
	if(values['day'].indexOf('*/') > -1) {
		if(vals.length > 0) {
				  vals.push('每隔');
			  }
			  vals.push(values['day'].replace('*/', '') + '天');
		every = true;
		  } else if(values['day'] != '*') {
		if(vals.length == 0) {
				  vals.push('每月');
			  }
		if(values['day'].indexOf(',') > -1) {
				  vals.push('其中的');
			  }
			  vals.push(values['day'] + '日');
		  }
	
	if(values['hour'].indexOf('*/') > -1) {
		if(!every) {
				  vals.push('每隔');
			  }
			  vals.push(values['hour'].replace('*/', '') + '小时');
		every = true;
		  } else if(values['hour'] != '*') {
		if(vals.length == 0) {
				  vals.push('每天');
			  }
		if(values['hour'].indexOf(',') > -1) {
				  vals.push('其中的');
			  } 
			  vals.push(values['hour'] + '时');
		  }
	
	if(values['minute'].indexOf('*/') > -1) {
		if(!every) {
				  vals.push('每隔');
			  }
			  vals.push(values['minute'].replace('*/', '') + '分钟');
		every = true;
		  } else if(values['minute'] != '*') {
		if(vals.length == 0) {
				  vals.push('每小时');
			  }
		if(values['minute'].indexOf(',') > -1) {
				  vals.push('其中的');
			  }
			  vals.push(values['minute'] + '分');
		  }
	vals.push('执行一次 [' + values['name'] + ']');
	return vals.join('');
}
//封装计划任务的数据交换 ---end

var task_id_keys = {'minute': ['task_minute_3', 'task_minute_2', 'task_minute_1'],
    'hour': ['task_hour_3', 'task_hour_2', 'task_hour_1'],
	'day': ['task_day_3', 'task_day_2', 'task_day_1'],
	'month': ['task_month_3', 'task_month_2', 'task_month_1'],
	'week': ['task_week_3', 'task_week_2', 'task_week_1']
}

var get_form_values = function(){
	var values = choice_best_values();
	var j_cmd = $('#task_cmds');
    values['cmd'] = j_cmd.val();
	values['name'] = j_cmd.attr('options')[j_cmd.attr('selectedIndex')].text;
	return values;
}

var refresh_descript = function(element){
	if(element !== undefined && element != null)
		value_on_blur(element);
	var values =  get_form_values();
	var descript = get_description(values);
	$('#taskDesc').html(descript);
}

var choice_best_values = function(){// 选择最佳的值
	var values = {isvaild: false};
	for(var k in task_id_keys) {
		ids = task_id_keys[k];
		for(var i=0;i<ids.length;i++){
			var v = $('#' + ids[i]).val().trim();
               if(v) {
				if(i == 0) {
					v = '*/' + v;
				}
				values[k] = v;
				if(v != '*' && v != '*/1') { // 只要有一时间值不为*，则算输入正确
					values.isvaild = true;
				}
				break;
			}
		}
	}
	return values;
}

var number_check = function(e){//间隔文本框的校验
    if (e.keyCode != 8) {
        var key = window.event ? e.keyCode : e.which;
        var keychar = String.fromCharCode(key);
        reg = /\d/;
        return reg.test(keychar);
    }
    return true;
}

var number_or_symbol_check = function(e){
	if(window.event == undefined && e.keyCode == 8){
		return true;
	}
    var key = window.event ? e.keyCode : e.which;   
	var keychar = String.fromCharCode(key);
	var reg1 = /[-]|[,]/;
	var reg2 = /\d/;
	return reg1.test(keychar) || reg2.test(keychar);
}

var value_on_blur = function(element){
	if(element.value == "" || element.value == "*"){ //如果没有变化，就不检查了
		return;
	}
	for(var k in task_id_keys) {
		if(k != element.name)
			continue;

		ids = task_id_keys[k];
		for(var i=0;i<ids.length;i++){
			if(ids[i] != element.id){
				var v = Ext.get(ids[i]);
	          	if(v){
					if(i == 2) {
						v.dom.selectedIndex = 0;
					}else{
						v.dom.value = "";
					}
				}
			}
		}
	}
}

var reset_form_values = function(){
	for(var k in task_id_keys) {
		ids = task_id_keys[k];
		for(var i=0;i<ids.length;i++){
			var v = Ext.get(ids[i]);
          	if(v){
				if(i == 2) {
					v.dom.selectedIndex = 0;
				}else{
					v.dom.value = "";
				}
			}
		}
	}
	Ext.get('task_cmds').dom.selectedIndex = 0;
	$('#taskDesc').html("");
}

var format_form_values = function(task_info) {
	reset_form_values();
	
	for(var key in task_id_keys) {
		var task_value = task_info[key];
		var key_string = "task_"+ key;
		if(task_value.indexOf('*/') > -1) {
			Ext.fly(task_id_keys[key][0]).dom.value = task_value.replace('*/', '');
	   	}else if(task_value.indexOf(',') > -1 || task_value.indexOf('-') > -1) {
			Ext.fly(task_id_keys[key][1]).dom.value = task_value;   
	   	}else {
			var selected_index = 0;
			
			if(task_value != "*"){
				try{
					selected_index = parseInt(task_value);
				}catch(e){}
			}
			
			if((key == "minute" || key == "hour") && task_value != "*"){ //小时和分钟有0分
				selected_index += 1; 
			}
			Ext.fly(task_id_keys[key][2]).dom.selectedIndex = selected_index;
	   	}
	}
	$('#task_cmds').val(task_info["name"]);
	refresh_descript();}


Ext.onReady(function(){
   // Ext.QuickTips.init();
	Ext.BLANK_IMAGE_URL = '/site_media/ext3/resources/images/default/s.gif'
    var current_task_id = "0";
     
	var tipfs = new Ext.form.FieldSet({
	    title: "提示",
	    labelWidth: 105,
	    autoHeight: true,
	    layout: 'form',
	    html: '你可以指定程序在 月、日、星期、小时、分的组合中执行，每行的第一个下拉框是选择具体的时间，如:在分钟行的下拉框中选择 5 ，那么就表示在第五分钟；第二个文本框中可以指定其中的几项,' +
	    '如:1,2,8,9 表示在第1,2,8,9分执行，数字之间用逗号隔开，也可以指定为:5-10,表示在第5到10分钟期间都会执行，也就是5,6,7,8,9,10;' +
	    '第三个文本框表示间隔，如在分钟行的第三个文本框中指定10，表示每隔10分钟执行一次。' +
	    '<span class="fcRed">*</span>为必填项'
	});
	
	var task_operator_panel = new Ext.FormPanel({
	    labelAlign: 'right',
	    buttonAlign: 'right',
	    bodyStyle: 'padding:5px',
	    width: 620,
	    frame: true,
	    items: [tipfs, {
	        html: '\
			<table width="102%" border="0" cellpadding="0" cellspacing="0" except="true">\
			  <tr>\
				<td style="text-align:center" width="132">任务描述</td>\
				<td style="text-align:left" name="taskDesc" id="taskDesc"></td>\
			  </tr>\
			  <tr><td><br/></td><td></td></tr>\
			  <tr>\
				<td style="text-align:center" width="132" valign="right">分钟</td>\
				<td width="466" style="text-align:left">\
				  <select  style="width:8.9%" id="task_minute_1" name="minute" onChange="refresh_descript(this)" width="60px";>\
					<option value="*">*</option>\
				  </select>\
				或其中的\
				<input name="minute" type="text" id="task_minute_2" onKeyUp="refresh_descript(this)" onKeyPress="return number_or_symbol_check(event);"/>\
				间隔\
				<input name="minute" type="text" id="task_minute_3" onKeyUp="refresh_descript(this)"  onkeypress="return number_check(event);" />\
				<span id="error_minute"></span>        </td>\
			  </tr>\
			  <tr>\
				<td style="text-align:center" width="132">时</td>\
				<td style="text-align:left"><select style="width:8.9%" name="hour" id="task_hour_1" onChange="refresh_descript(this)" width="60px">\
					 <option value="*">*</option>\
				</select>\
				或其中的\
				<input name="hour" type="text" id="task_hour_2"  onkeyup="refresh_descript(this)" onKeyPress="return number_or_symbol_check(event);"/>\
				间隔\
				<input name="hour" type="text" id="task_hour_3" onKeyUp="refresh_descript(this)" onKeyPress="return number_check(event);"/><span id="error_hour"></span></td>\
			  </tr>\
			  <tr>\
				<td style="text-align:center" width="132">日</td>\
				<td style="text-align:left"><select style="width:8.9%" name="day" id="task_day_1" onChange="refresh_descript(this)" width="60px">\
					 <option value="*">*</option>\
				</select>\
				或其中的\
				<input name="day" type="text" id="task_day_2" onKeyUp="refresh_descript(this)" onKeyPress="return number_or_symbol_check(event);" />\
				间隔\
				<input name="day" type="text" id="task_day_3" onKeyUp="refresh_descript(this)" onKeyPress="return number_check(event);" />\
				<span id="errir_dayofmonth"></span></td>\
			  </tr>\
			  <tr>\
				<td style="text-align:center" width="132">星期</td>\
				<td style="text-align:left"><select style="width:8.9%" name="week" id="task_week_1" onChange="refresh_descript(this)" >\
					 <option value="*">*</option>\
				</select>\
				或其中的\
				<input name="week" type="text" id="task_week_2" onKeyUp="refresh_descript(this)" onKeyPress="return number_or_symbol_check(event);"/>\
				间隔\
				<input name="week" type="text" id="task_week_3" onKeyUp="refresh_descript(this)" onKeyPress="return number_check(event);" />\
				<span id="error_dayofweek"></span></td>\
			  </tr>\
			  <tr>\
				<td style="text-align:center" width="132">月</td>\
				<td style="text-align:left"><select  style="width:8.9%" name="month" id="task_month_1" onChange="refresh_descript(this)">\
					 <option value="*">*</option>\
				</select>\
				  或其中的\
				  <input name="month" type="text" id="task_month_2" onKeyUp="refresh_descript(this)" onKeyPress="return number_or_symbol_check(event);"/>\
				  间隔\
				  <input name="month" type="text" id="task_month_3"  onkeyup="refresh_descript(this)" onKeyPress="return number_check(event);" />\
				  <span id="error_month"></span></td>\
			  </tr>\
			   <tr><td><br/></td><td></td></tr>\
			  <tr>\
				<td style="text-align:center" width="132">执行的程序(<span class="fcRed">*</span>)</td>\
				<td style="text-align:left"><select style="width:60%" name="task_cmds" id="task_cmds" onChange="refresh_descript(this)" width="60px">\
			</td>\
			  </tr>\
			</table>\
			'
	    }],
	    buttons: [{
	        text: "提交",
	        type: 'button',
	        handler: function(btn){
	        	var task_info_ = get_form_values();
	        	if(!task_info_["isvaild"]){
	        		Ext.MessageBox.alert("错误","您至少要输入一个时间项！")
		        	return;
	        	} 
	        	task_operator_window.hide();
	        	if(current_task_id == "0"){
	        		save_task_service(task_info_);
	        	}else{
	        		task_info_["id"] = current_task_id;
	        		update_task_service(task_info_);
	        	}
	        	refresh_task();
	        }
	    }, {
	        text: "关闭",
	        handler: function(){
	            task_operator_window.hide();
	        }
	    }]
	});
	
	var task_operator_window = new Ext.Window({
	    id: "htopwin",
	    title: "计划任务配置",
	    layout: "fit",
	    width: 620,
	    height: 398,
	    closeAction: 'hide',
	    items: task_operator_panel,
	    listeners: {
	        "show": function(){
	            if ($("#task_minute_1")[0].options.length < 2) {
	            	var commands = get_defined_task_service();
	            	if(commands !== undefined && commands != null){
	            		for(var p in commands){
	            			var opt = document.createElement('option');
	                    	opt.value = commands[p];
	                    	opt.innerHTML = p.safe();
	                    	$("#task_cmds").append(opt);
	            		}
	            	}
	            	
	                var m = $('#task_minute_1');
	                var h = $('#task_hour_1');
	                var dom = $('#task_day_1');
	                var dow = $('#task_week_1');
	                var month = $('#task_month_1');
	                __f = function(from, to, selectBox){
	                    if (selectBox) {
	                        for (var i = from; i < to; i++) {
	                            var opt = document.createElement('option');
	                            opt.value = i + '';
	                            opt.innerHTML = i;
	                            selectBox.append(opt);
	                        }
	                    }
	                }
	                __f(0, 60, m);
	                __f(0, 24, h);
	                __f(1, 32, dom);
	                __f(1, 8, dow);
	                __f(1, 13, month);
	            }
	        }
	    }
	});

	var PlanTask = new Ext.data.Record.create([
		{name: 'id'}, 
		{name: 'minute'}, 
		{name: 'hour'}, 
		{name: 'dayofmonth'}, 
		{name: 'month'}, 
		{name: 'dayofweek'},
		{name: 'taskname'}, 
		{name: 'taskdesc'}]);
 	
    var store_plan_task = new Ext.data.GroupingStore({
        reader: new Ext.data.JsonReader({fields: PlanTask}),
		data: [],
        sortInfo: {field: 'id', direction: 'ASC'}
    });
	
	var CurrentTask = new Ext.data.Record.create([
		{name: 'taskname'}]);
 
	var store_current_task = new Ext.data.GroupingStore({
        reader: new Ext.data.JsonReader({fields: CurrentTask}),
		data: [],
        sortInfo: {field: 'taskname', direction: 'ASC'}
    });
    
    var grid_plan_task = new Ext.grid.GridPanel({
        store: store_plan_task,
        width: 600,
        region:'center',
        margins: '0 5 5 5',
        loadMask: true,
        autoHeight: true,
        sm: new Ext.grid.RowSelectionModel({singleSelect:true}),
        view: new Ext.grid.GroupingView({
            markDirty: false  
        }),
        tbar: [{
            iconCls: 'task-add',
            text: '新建任务',
            handler: function(){
            	current_task_id = "0";
            	task_operator_window.show();
            	reset_form_values();
            }
        },{
            ref: '../removeBtn',
            iconCls: 'task-delete',
            text: '删除任务',
            disabled: true,
            handler: function(){
                var record_ = grid_plan_task.getSelectionModel().getSelected();
                var task_id = record_.get("id");
                if(task_id != undefined){
                	Ext.MessageBox.confirm("确认","您确认要删除编号为[" + task_id+"]的任务吗？",
                		function(btn, text){
    						if (btn == 'yes'){
        						if(delete_task_service(task_id)){
                		 			refresh_task();
        						} 
    						}   
    					}
                	);
                }
            }
        },{
			ref: '../modifyBtn',
            iconCls: 'task-modify',
            text: '修改任务',
            disabled: true,
            handler: function(){
                var record_ = grid_plan_task.getSelectionModel().getSelected();
                current_task_id = record_.get("id");
                if(current_task_id != undefined){
                	var task_info = get_task_service(current_task_id);
                	task_operator_window.show();
                	format_form_values(task_info);
                }
            }
        },{
            iconCls: 'task-sync',
            text: '同步任务',
            disabled: false,
            handler: function(){
                sync_task_service();
                refresh_task();
            }
        },{
        	iconCls: 'task-refresh',
            text: '刷新',
            disabled: false,
            handler: function(){
                refresh_task();
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
			header: '分钟',
			dataIndex: 'minute',
			width: 60,
            sortable: true
        },{
			header: '小时',
			dataIndex: 'hour',
			width: 60,
            sortable: true
		 },{
			header: '每月第几日',
			dataIndex: 'dayofmonth',
			width: 60,
            sortable: true
		 },{
			header: '月份',
			dataIndex: 'month',
			width: 60,
            sortable: true
		 },{
			header: '每周第几日',
			dataIndex: 'dayofweek',
			width: 60,
            sortable: true
        },{
            header: '任务名称',
            dataIndex: 'taskname',
            width: 120,
            sortable: true,
			renderer: function(v, p){	//v : value , p : cell
				p.attr += " ext:qtip='" + v + "'";
				return v;
			}
        },{
            header: '任务描述',
            dataIndex: 'taskdesc',
            width: 200,
			sortable: true,
			renderer: function(v, p){	//v : value , p : cell
				p.attr += " ext:qtip='" + v + "'";
				return v;
			}
        }],
        
        listeners:{
        	 rowdblclick :function(self,rowIndex, e){
        	 	var record_ = grid_plan_task.getSelectionModel().getSelected();
                current_task_id = record_.get("id");
                if(current_task_id != undefined){
                	var task_info = get_task_service(current_task_id);
                	task_operator_window.show();
                	format_form_values(task_info);
                }
    		}
        }
    });
	
	var grid_current_task = new Ext.grid.GridPanel({
	    store: store_current_task,
        width: 600,
        region:'center',
        margins: '0 5 5 5',
        autoHeight: true,
        sm: new Ext.grid.RowSelectionModel({singleSelect:true}),
        view: new Ext.grid.GroupingView({
            markDirty: false  
        }),
        tbar: [{
            ref: '../removeCurrentBtn',
            iconCls: 'task-delete',
            Cls:'x-btn-text',
            text: '删除任务',
            disabled: true,
            handler: function(){
                var record_ = grid_current_task.getSelectionModel().getSelected();
                var task_name = record_.get("taskname");
                if(task_name != undefined){
                	Ext.MessageBox.confirm("确认","您确认要删除任务[" + task_name+"]吗？",
                		function(btn, text){
    						if (btn == 'yes'){
        						if(delete_current_task_service(task_name))
                		 		refresh_task(); 
    						}   
    					}
                	);
                }
            }
        }],

        columns: [
        new Ext.grid.RowNumberer(),
        {
            header: '任务名称',
            dataIndex: 'taskname',
            width: 700,
            sortable: true,
			renderer: function(v, p){
				//v : value , p : cell
				p.attr += " ext:qtip='" + v + "'";
				return v;
			}
        }]
    });
	
    var plan_task_panel = new Ext.Panel({
        title: 'linux计划任务管理',
        layout: 'border',
        layoutConfig: {
            columns: 1
        },
        height:245,
        items: [grid_plan_task]
    });
	
	var current_task_panel = new Ext.Panel({
        title: '当前执行计划任务',
        layout: 'border',
        layoutConfig: {
            columns: 1
        },        
		height:245,
        items: [grid_current_task]
    });
	
    plan_task_panel.render(Ext.get("plan_task_div"));
	current_task_panel.render(Ext.get("current_task_div"));

    grid_plan_task.getSelectionModel().on('selectionchange', function(sm){
        grid_plan_task.removeBtn.setDisabled(sm.getCount() < 1);
		grid_plan_task.modifyBtn.setDisabled(sm.getCount() < 1);
    });
    
    grid_current_task.getSelectionModel().on('selectionchange', function(sm){
        grid_current_task.removeCurrentBtn.setDisabled(sm.getCount() < 1);
    });
    
    var refresh_task = function(){
    	store_plan_task.removeAll();
    	store_current_task.removeAll();
    	var task_info = get_tasks_service();
    	if(task_info != null){
    		if(task_info["tasks"] !== undefined){
    			for(var i=0;i<task_info["tasks"].length;i++){
    				var record_ = new PlanTask({
    					id: task_info["tasks"][i]["id"],
    					minute: task_info["tasks"][i]["minute"],
    					hour: task_info["tasks"][i]["hour"],
    					dayofmonth: task_info["tasks"][i]["day"],
    					month: task_info["tasks"][i]["month"],
    					dayofweek: task_info["tasks"][i]["week"],
    					taskname: task_info["tasks"][i]["name"].safe(),
    					taskdesc: get_description(task_info["tasks"][i])
    				});
    				store_plan_task.add(record_);
    			}
    		}
    		
    		if(task_info["current_tasks"] !== undefined){
     			for(var i=0;i<task_info["current_tasks"].length;i++){
    				var record_ = new CurrentTask({taskname:task_info["current_tasks"][i].safe()});
    				store_current_task.add(record_);
    			}
    		}
    	}
    }
    refresh_task.defer(1000, this, null);//打开页面先延时1秒显示一下任务
});
