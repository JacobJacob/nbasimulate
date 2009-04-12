package com.dt.bottle.session;

import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;

import com.dt.bottle.cache.StartersCache;
import com.dt.bottle.connector.DBConnection;
import com.dt.bottle.constants.Constants;
import com.dt.bottle.exception.ObjectNotFoundException;
import com.dt.bottle.exception.SessionException;
import com.dt.bottle.helper.ObjectBuilder;
import com.dt.bottle.logger.Logger;
import com.dt.bottle.persistence.Persistence;
import com.dt.bottle.pool.ConnectionPool;
import com.dt.bottle.sql.SqlHelper;
import com.dt.bottle.util.StringConverter;

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

	public void save(Object obj) throws SessionException {

		long id = 0;

		if (!success) {
			return;
		}
		Logger.logger("start save");
		Hashtable<String, Object> table = SqlHelper.createInsertSql(obj);
		String sql = (String) table.get(Constants.DB_SQL);
		Object[] parm = (Object[]) table.get(Constants.DB_PARM);
		try {
			conn.execute(sql, parm);
		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		}
		String getIdSql = SqlHelper.getLastInsertSql(obj);
		try {
			ResultSet resultSet = conn.executeQuery(getIdSql, new Object[0]);
			if (!resultSet.next()) {
				throw new Exception("error occor while get last id");
			}
			id = resultSet.getLong("id");
			((Persistence) obj).setId(id);
		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		}
		Logger.logger("finish save");

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

		Logger.logger("start to load Object[" + cls.getClass().getName()
				+ "] id :" + id);

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

	public List<Persistence> query(Class<?> cla, String sql, Object[] parm)
			throws Exception {

		conn = ConnectionPool.instance().connection();

		List<Persistence> result = new ArrayList<Persistence>();

		try {
			ResultSet resultSet = conn.executeQuery(sql, parm);

			int rows = 0;
			while (resultSet.next()) {
				rows++;
				Persistence pesist = (Persistence) cla.newInstance();
				ObjectBuilder.builderObjFromResultSet(pesist, resultSet);
				result.add(pesist);
			}
			Logger.logger("Query:" + sql
					+ StringConverter.parmArray2String(parm)
					+ " select rows : " + rows);

		} catch (Exception e) {
			e.printStackTrace();
			throw new SessionException();
		} finally {
			ConnectionPool.instance().disConnection(conn);
		}
		return result;
	}

	public synchronized long getIdBySql(String sql, Object[] parm)
			throws ObjectNotFoundException, SessionException {

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
		// conn = null;
	}

	public void readCacheStatus() {

	}

	public void close() {

	}

}
