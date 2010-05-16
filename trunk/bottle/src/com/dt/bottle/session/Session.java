package com.dt.bottle.session;

import java.lang.reflect.Method;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.Date;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.List;

import com.dt.bottle.cache.StartersCache;
import com.dt.bottle.constants.Constants;
import com.dt.bottle.db.ConnectionPool;
import com.dt.bottle.exception.ObjectNotFoundException;
import com.dt.bottle.exception.SessionException;
import com.dt.bottle.helper.ObjectBuilder;
import com.dt.bottle.helper.SqlHelper;
import com.dt.bottle.logger.Logger;
import com.dt.bottle.persistence.Persistence;
import com.dt.bottle.util.DateConverter;
import com.dt.bottle.util.StringConverter;

public class Session {

	private Connection conn;
	private boolean success = true;

	private boolean cacheActive = false;

	private StartersCache cache;
	private boolean autoCommitTemp = false;

	public Session() {
		if (cacheActive) {
			cache = new StartersCache();
		}
	}

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
			this.execute(sql, parm);
		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		}
		String getIdSql = SqlHelper.getLastInsertSql(persist);
		try {
			ResultSet resultSet = this.executeQuery(conn, getIdSql, new Object[0]);
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
			this.execute(sql, parm);
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
		Logger.logger("start to load Object[" + cls.getClass().getName() + "] id :" + id);

		String sql = SqlHelper.getLoaderSql(persistence);
		Object[] parm = { String.valueOf(id) };
		Connection conn = ConnectionPool.getInstance().connection();
		try {
			ResultSet resultSet = this.executeQuery(conn, sql, parm);
			if (!resultSet.next()) {
				throw new ObjectNotFoundException();
			}
			ObjectBuilder.builderObjFromResultSet(persistence, resultSet);

		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		} finally {
			try {
				conn.close();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}

		if (cacheActive) {
			cache.store(persistence, id);
		}

		return persistence;
	}

	public Persistence load(Class<?> cls, String condition) throws Exception {

		Persistence persistence = (Persistence) cls.newInstance();

		Logger.logger("start to load Object[" + condition + "]");

		String sql = SqlHelper.getLoaderSql(persistence, condition);
		Object[] parm = {};
		Logger.logger(sql);
		Connection conn = ConnectionPool.getInstance().connection();
		try {
			ResultSet resultSet = this.executeQuery(conn, sql, parm);
			if (!resultSet.next()) {
				throw new ObjectNotFoundException();
			}
			ObjectBuilder.builderObjFromResultSet(persistence, resultSet);

		} catch (ObjectNotFoundException notfoundex) {
			return null;
		} catch (Exception e) {
			success = false;
			throw new SessionException();
		} finally {
			try {
				conn.close();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}

		return persistence;
	}

	@SuppressWarnings("unchecked")
	public List query(Class<? extends Persistence> cla, String sql, Object[] parm) throws SessionException {

		List<Persistence> result = new ArrayList<Persistence>();
		ResultSet resultSet = null;
		Connection conn = ConnectionPool.getInstance().connection();
		try {
			resultSet = this.executeQuery(conn, sql, parm);
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
				conn.close();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}

		return result;
	}

	public synchronized long getIdBySql(String sql, Object[] parm) throws ObjectNotFoundException, SessionException {

		long id = -1;
		Connection conn = ConnectionPool.getInstance().connection();
		try {
			ResultSet resultSet = this.executeQuery(conn, sql, parm);
			if (!resultSet.next()) {
				throw new ObjectNotFoundException();
			}
			id = resultSet.getLong(Constants.PRIMARY_KEY);

		} catch (Exception e) {
			success = false;
			e.printStackTrace();
			throw new SessionException();
		} finally {
			try {
				conn.close();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}

		return id;
	}

	public void beginTransaction() {
		conn = ConnectionPool.getInstance().connection();
		try {
			autoCommitTemp = conn.getAutoCommit();
			conn.setAutoCommit(false);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void endTransaction() {
		try {
			if (success) {
				conn.commit();
			} else {
				conn.rollback();
			}
			conn.setAutoCommit(autoCommitTemp);
			conn.close();
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	public void readCacheStatus() {

	}

	public void closeCache() {
		cacheActive = false;
	}

	public void execute(String sql, Object[] parm) {

		Logger.logger("SQL: " + sql);
		Logger.logger("PARAM:" + StringConverter.parmArray2String(parm));

		PreparedStatement prepareStatement = null;

		try {
			prepareStatement = conn.prepareStatement(sql);
			for (int i = 1; i <= parm.length; i++) {

				if (parm[i - 1] instanceof Date) {
					prepareStatement.setTimestamp(i, DateConverter.utilDate2Timestamp((Date) parm[i - 1]));
				} else if (parm[i - 1] instanceof Character) {
					prepareStatement.setString(i, parm[i - 1].toString());
				} else {
					prepareStatement.setObject(i, parm[i - 1]);
				}

			}
			prepareStatement.execute();

		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			try {
				prepareStatement.close();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}

	public ResultSet executeQuery(Connection conn, String sql, Object[] parm) {

		Logger.logger("SQL: " + sql);
		ResultSet resultSet = null;

		PreparedStatement prepareStatement = null;
		try {
			prepareStatement = conn.prepareStatement(sql);
			for (int i = 1; i <= parm.length; i++) {

				prepareStatement.setObject(i, parm[i - 1]);

			}
			resultSet = prepareStatement.executeQuery();

		} catch (Exception e) {
			e.printStackTrace();
		}

		return resultSet;
	}

	public void close() {

	}
}
