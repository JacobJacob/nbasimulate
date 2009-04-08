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
		Logger.logger("finish save");
	}

	public Persistence load(Persistence pesist) throws Exception {

		if (cacheActive) {
			if (cache.containObject(pesist.getClass(), pesist.getId())) {
				return cache.load(pesist.getClass(), pesist.getId());
			}
		}
		conn = ConnectionPool.instance().connection();

		Logger.logger("start to load Object[" + pesist.getClass().getName() + "] id :" + pesist.getId());

		String sql = SqlHelper.getLoaderSql(pesist);
		Object[] parm = { String.valueOf(pesist.getId()) };

		try {
			ResultSet resultSet = conn.executeQuery(sql, parm);
			if (!resultSet.next()) {
				throw new ObjectNotFoundException();
			}
			ObjectBuilder.builderObjFromResultSet(pesist, resultSet);

		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		} finally {
			ConnectionPool.instance().disConnection(conn);
			conn = null;
		}
		if (cacheActive) {

			cache.store(pesist, pesist.getId());

		}

		return pesist;
	}

	public List<Persistence> query(Class<?> cla, String sql, Object[] parm) throws Exception {

		conn = ConnectionPool.instance().connection();

		List<Persistence> result = new ArrayList<Persistence>();

		try {
			ResultSet resultSet = conn.executeQuery(sql, parm);
			while (resultSet.next()) {
				Persistence pesist = (Persistence) cla.newInstance();
				ObjectBuilder.builderObjFromResultSet(pesist, resultSet);
				result.add(pesist);
			}

		} catch (Exception e) {
			e.printStackTrace();
			throw new SessionException();
		} finally {
			ConnectionPool.instance().disConnection(conn);
			conn = null;
		}
		return result;
	}

	public long getIdBySql(String sql, Object[] parm) throws ObjectNotFoundException, SessionException {

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
			conn = null;
		}

		return id;
	}

	public void beginTransaction() {
		conn = ConnectionPool.instance().connection();
	}

	public void endTransaction() {
		if (success) {
			conn.commit();
		} else {
			conn.rollback();
		}

		ConnectionPool.instance().disConnection(conn);
		conn = null;
	}

	public void readCacheStatus() {

	}

	public void close() {

	}

}
