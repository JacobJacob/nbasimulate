package com.dt.bottle.cache;

import java.util.Hashtable;

public class SocketCache {

	private Hashtable<Object, Object> cache;

	public SocketCache() {
		cache = new Hashtable<Object, Object>();
	}

	public boolean containObject(Object key) {
		if (cache.containsKey(key)) {
			return true;
		} else {
			return false;
		}
	}

	public Object load(Object key) {

		if (cache.containsKey(key)) {
			return cache.get(key);
		} else {
			return null;
		}
	}

	public void store(Object key, Object obj) {
		cache.put(key, obj);
	}

}
