package com.ts.dt.db;

import java.beans.PropertyVetoException;
import java.sql.Connection;
import java.sql.SQLException;
import com.mchange.v2.c3p0.ComboPooledDataSource;

public final class ConnectionManager {
	private static ConnectionManager instance;
	private static ComboPooledDataSource dataSource;

	private ConnectionManager() throws SQLException, PropertyVetoException {
		dataSource = new ComboPooledDataSource();
		dataSource.setUser("gba");
		dataSource.setPassword("gba123");
		dataSource.setJdbcUrl("jdbc:mysql://192.168.1.152/gba?characterEncoding=utf-8");
		dataSource.setDriverClass("com.mysql.jdbc.Driver");
		dataSource.setInitialPoolSize(10);
		dataSource.setMinPoolSize(1);
		dataSource.setMaxPoolSize(20);
		dataSource.setMaxStatements(100);
		dataSource.setMaxIdleTime(60);
	}

	public static final ConnectionManager getInstance() {
		if (instance == null) {
			try {
				instance = new ConnectionManager();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		return instance;
	}

	public synchronized final Connection getConnection() {
		Connection conn = null;
		try {
			conn = dataSource.getConnection();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return conn;
	}
}