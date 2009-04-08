package com.dt.bottle.connector;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import com.dt.bottle.logger.Logger;

public class DBConnection {

	private Connection conn = null;

	public void connect() {
		try {
			Class.forName("com.mysql.jdbc.Driver").newInstance();
			conn = DriverManager.getConnection("jdbc:mysql://localhost/xba?user=root&password=821015");
			conn.setAutoCommit(false);

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void execute(String sql, Object[] parm) {

		Logger.logger("SQL: " + sql);
		if (conn == null) {
			connect();
		}

		try {
			PreparedStatement prepareStatement = conn.prepareStatement(sql);
			for (int i = 1; i <= parm.length; i++) {
				prepareStatement.setObject(i, parm[i - 1]);

			}
			prepareStatement.execute();

		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	/*
	 * 
	 */
	public ResultSet executeQuery(String sql, Object[] parm) {

		Logger.logger("SQL: " + sql);

		ResultSet resultSet = null;

		if (conn == null) {
			connect();
		}
		try {
			PreparedStatement prepareStatement = conn.prepareStatement(sql);
			for (int i = 1; i <= parm.length; i++) {

				prepareStatement.setObject(i, parm[i - 1]);

			}
			resultSet = prepareStatement.executeQuery();

		} catch (Exception e) {
			e.printStackTrace();
		}

		return resultSet;
	}

	public void commit() {

		try {
			conn.commit();
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	public void rollback() {

		try {
			conn.rollback();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}
