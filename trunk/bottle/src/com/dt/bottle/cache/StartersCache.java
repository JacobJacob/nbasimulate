package com.dt.bottle.cache;

import java.util.Hashtable;

import com.dt.bottle.logger.Logger;
import com.dt.bottle.persistence.Persistence;

/**
 * 
 * @author Jacky Shi
 * Class StartersCache is a Starters Cache
 *
 */
public class StartersCache {

	private Hashtable<Class<?>, Hashtable<String, Persistence>> cache;

	public StartersCache() {
		cache = new Hashtable<Class<?>, Hashtable<String, Persistence>>();
	}

	public boolean containObject(Class<?> cls, long id) {
		if (cache.containsKey(cls)) {
			Hashtable<String, Persistence> tmpTable = (Hashtable<String, Persistence>) cache
					.get(cls);
			if (tmpTable.containsKey(String.valueOf(id))) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	public Persistence load(Class<?> cls, long id) {

		if (cache.containsKey(cls)) {
			Hashtable<String, Persistence> tmpTable = (Hashtable<String, Persistence>) cache
					.get(cls);
			if (tmpTable.containsKey(String.valueOf(id))) {
				Logger.logger("Building Object from Cache : " + cls.getName()
						+ " : " + id);
				return tmpTable.get(String.valueOf(id));
			} else {
				return null;
			}
		} else {
			return null;
		}
	}

	public void store(Persistence obj, long id) {
		Class<?> cls = obj.getClass();
		if (containObject(obj.getClass(), id)) { 
			update(obj, id);
		} else {
			if (cache.containsKey(cls)) {
				Hashtable<String, Persistence> tmpTable = (Hashtable<String, Persistence>) cache
						.get(cls);
				tmpTable.put(String.valueOf(id), obj);
			} else {
				Hashtable<String, Persistence> tmpTable = new Hashtable<String, Persistence>();
				cache.put(cls, tmpTable);
			}
		}
	}

	public void update(Persistence obj, long id) {

		Class<?> cls = obj.getClass();
		Hashtable<String, Persistence> tmpTable = (Hashtable<String, Persistence>) cache
				.get(cls);
		tmpTable.put(String.valueOf(id), obj);

	}
}
