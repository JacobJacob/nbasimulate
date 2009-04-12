package com.dt.bottle.connector;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.Date;

import com.dt.bottle.logger.Logger;
import com.dt.bottle.util.DateConverter;

public class DBConnection {

	private Connection conn = null;
	private PreparedStatement prepareStatement = null;

	public synchronized void connect() {
		try {
			Class.forName("com.mysql.jdbc.Driver").newInstance();
			conn = DriverManager
					.getConnection("jdbc:mysql://localhost/xba?user=root&password=821015");
			conn.setAutoCommit(false);

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public synchronized void execute(String sql, Object[] parm) {

		Logger.logger("SQL: " + sql);
		if (conn == null) {
			connect();
		}

		try {
			prepareStatement = conn.prepareStatement(sql);
			for (int i = 1; i <= parm.length; i++) {

				if (parm[i - 1] instanceof Date) {
					prepareStatement.setTimestamp(i, DateConverter
							.utilDate2Timestamp((Date) parm[i - 1]));
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

	/*
	 * 
	 */
	public synchronized ResultSet executeQuery(String sql, Object[] parm) {

		Logger.logger("SQL: " + sql);

		ResultSet resultSet = null;

		if (conn == null) {
			connect();
		}
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

	public synchronized void commit() {

		try {
			conn.commit();
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	public synchronized void rollback() {

		try {
			conn.rollback();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public synchronized void destory() {

		try {
			if (prepareStatement != null && !prepareStatement.isClosed()) {
				prepareStatement.close();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}

	}
}
