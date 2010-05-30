package com.ts.dt.cache;

import java.util.Hashtable;
import java.util.List;

import com.ts.dt.key.ActionDescKey;
import com.ts.dt.po.ActionDesc;
/*
 * 动作描述缓存类
 */
public class ActionDescCache {

	public static final String nameingMethod = "ACTION_RESULT_FLG";

	private Hashtable<ActionDescKey, List<ActionDesc>> data; 

	public ActionDescCache() {
		data = new Hashtable<ActionDescKey, List<ActionDesc>>();
	}

	public List<ActionDesc> get(ActionDescKey key) {

        return data.get(key);
	}

	public void put(ActionDescKey key, List<ActionDesc> actionDesclist) {
		data.put(key, actionDesclist);
	}

	public boolean containKey(ActionDescKey key) {
		return data.containsKey(key);
	}

}
