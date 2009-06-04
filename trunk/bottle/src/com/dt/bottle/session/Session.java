package com.dt.bottle.session;

import java.lang.reflect.Method;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.List;

import com.dt.bottle.cache.StartersCache;
import com.dt.bottle.connector.DBConnection;
import com.dt.bottle.constants.Constants;
import com.dt.bottle.exception.ObjectNotFoundException;
import com.dt.bottle.exception.SessionException;
import com.dt.bottle.helper.ObjectBuilder;
import com.dt.bottle.helper.SqlHelper;
import com.dt.bottle.logger.Logger;
import com.dt.bottle.persistence.Persistence;
import com.dt.bottle.pool.ConnectionPool;

public class Session {

	private DBConnection conn;
	private boolean success = true;

	private boolean cacheActive = true;

	private StartersCache cache;

	public Session() {
		if (cacheActive) {
			cache = new StartersCache();
		}
	}

	@SuppressWarnings("unchecked")
	public void save(Persistence persist) throws SessionException {

		long id = 0;

		if (!success) {
			return;
		}
		Logger.logger("start save");
		Hashtable<String, Object> table = SqlHelper.createInsertSql(persist);
		String sql = (String) table.get(Constants.DB_SQL);
		Object[] parm = (Object[]) table.get(Constants.DB_PARM);
		try {
			conn.execute(sql, parm);
		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		}
		String getIdSql = SqlHelper.getLastInsertSql(persist);
		try {
			ResultSet resultSet = conn.executeQuery(getIdSql, new Object[0]);
			if (!resultSet.next()) {
				throw new SessionException("error occor while get last id");
			}
			id = resultSet.getLong("id");
			((Persistence) persist).setId(id);
		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		}
		saveOneToOneAssoc(id, table);
		saveOneToManyAssoc(id, table);
		Logger.logger("finish save");

	}

	@SuppressWarnings("unchecked")
	private void saveOneToOneAssoc(long id, Hashtable<String, Object> table) {
		// save one to one associate
		String className = (String) table.get(Constants.CLASS_NAME);
		List<Persistence> one2one = (List) table.get(Constants.ONE_TO_ONE_ASSOC);
		Iterator<Persistence> iterator = one2one.iterator();
		while (iterator.hasNext()) {
			Persistence one2onepersist = iterator.next();
			saveSingleObject(id, className, one2onepersist);
		}
	}

	@SuppressWarnings("unchecked")
	private void saveOneToManyAssoc(long id, Hashtable<String, Object> table) {

		// save one to many associate
		String className = (String) table.get(Constants.CLASS_NAME);
		List<List<Persistence>> one2many = (List) table.get(Constants.ONE_TO_MANY_ASSOC);
		Iterator<List<Persistence>> iterator = one2many.iterator();
		while (iterator.hasNext()) {
			List<Persistence> one2onepersistList = iterator.next();
			Iterator<Persistence> innerIter = one2onepersistList.iterator();
			while (innerIter.hasNext()) {
				saveSingleObject(id, className, innerIter.next());
			}
		}
	}

	private void saveSingleObject(long assocId, String assocObjNm, Persistence persist) {

		Class<?> cls = persist.getClass();
		try {
			Method method = cls.getMethod("set" + assocObjNm + "Id", new Class[] { long.class });
			method.invoke(persist, assocId);
		} catch (Exception e) {
			e.printStackTrace();
		}
		persist.save();
	}

	public void update(Object obj) throws SessionException {

		if (!success) {
			return;
		}
		Logger.logger("start updaet");
		Hashtable<String, Object> table = SqlHelper.createUpdateSql(obj);
		String sql = (String) table.get(Constants.DB_SQL);
		Object[] parm = (Object[]) table.get(Constants.DB_PARM);
		try {
			conn.execute(sql, parm);
		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		}
		Logger.logger("finish update");

	}

	public Persistence load(Class<?> cls, long id) throws Exception {

		Persistence persistence = (Persistence) cls.newInstance();

		if (cacheActive) {
			if (cache.containObject(cls, id)) {
				return cache.load(cls, id);
			}
		}
		DBConnection conn = ConnectionPool.instance().connection();

		Logger.logger("start to load Object[" + cls.getClass().getName() + "] id :" + id);

		String sql = SqlHelper.getLoaderSql(persistence);
		Object[] parm = { String.valueOf(id) };

		try {
			ResultSet resultSet = conn.executeQuery(sql, parm);
			if (!resultSet.next()) {
				throw new ObjectNotFoundException();
			}
			ObjectBuilder.builderObjFromResultSet(persistence, resultSet);

		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		} finally {
			ConnectionPool.instance().disConnection(conn);
			conn = null;
		}
		if (cacheActive) {

			cache.store(persistence, id);

		}

		return persistence;
	}

	@SuppressWarnings("unchecked")
	public List query(Class<? extends Persistence> cla, String sql, Object[] parm) throws Exception {

		conn = ConnectionPool.instance().connection();
		List<Persistence> result = new ArrayList<Persistence>();
		ResultSet resultSet = null;
		try {
			resultSet = conn.executeQuery(sql, parm);

			int rows = 0;
			while (resultSet.next()) {
				rows++;
				Persistence pesist = (Persistence) cla.newInstance();
				ObjectBuilder.builderObjFromResultSet(pesist, resultSet);
				result.add(pesist);
			}

		} catch (Exception e) {
			e.printStackTrace();
			throw new SessionException();
		} finally {
			try {
				resultSet.close();
			} catch (Exception e) {
				e.printStackTrace();
			}
			ConnectionPool.instance().disConnection(conn);
		}
		return result;
	}

	public synchronized long getIdBySql(String sql, Object[] parm) throws ObjectNotFoundException, SessionException {

		long id = -1;
		conn = ConnectionPool.instance().connection();

		try {
			ResultSet resultSet = conn.executeQuery(sql, parm);
			if (!resultSet.next()) {
				throw new ObjectNotFoundException();
			}
			id = resultSet.getLong(Constants.PRIMARY_KEY);

		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		} finally {
			ConnectionPool.instance().disConnection(conn);
		}

		return id;
	}

	public void beginTransaction() {
		conn = ConnectionPool.instance().connection();
		if (conn == null) {
			System.out.println("get a null connection!");
		}
	}

	public void endTransaction() {
		if (success) {
			conn.commit();
		} else {
			conn.rollback();
		}

		ConnectionPool.instance().disConnection(conn);

	}

	public void readCacheStatus() {

	}

	public void close() {

	}

	public void closeCache() {
		cacheActive = false;
	}

}
