package com.dt.ejb.context;

import java.io.Serializable;
import java.util.Hashtable;

public class Context implements Serializable {

	public static final long serialVersionUID = -2805454943653427653L;

	private Hashtable<String, Object> msData;

	public Context() {
		init();
	}

	private void init() {
		msData = new Hashtable<String, Object>();
	}

	public Object getObject(String key) {
		return msData.get(key);
	}

	public Object setObject(String key, Object obj) {
		return msData.put(key, obj);
	}
}
