package com.dt.bottle.helper;

import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;

import com.dt.bottle.constants.Constants;
import com.dt.bottle.persistence.Persistence;

public class SqlHelper {

	@SuppressWarnings("unchecked")
	public static Hashtable<String, Object> createInsertSql(Object obj) {

		Hashtable<String, Object> table = new Hashtable<String, Object>();

		StringBuffer insert = new StringBuffer();
		StringBuffer attribute = new StringBuffer();
		StringBuffer parameters = new StringBuffer();
		List<Object> values = new ArrayList<Object>();
		List<Persistence> oneToOne = new ArrayList<Persistence>();
		List<List<Persistence>> oneToMany = new ArrayList<List<Persistence>>();

		insert.append("INSERT INTO ");
		String tableName = className2TableName(obj);
		insert.append("`" + tableName + "`");

		Field[] fields = obj.getClass().getDeclaredFields();

		for (int i = 0; i < fields.length; i++) {

			Field field = fields[i];
			if ("serialVersionUID".equals(field.getName())) {
				continue;
			}
			Object value = getValueByField(obj, field.getName());

			// one to one
			if (value instanceof Persistence) {
				oneToOne.add((Persistence) value);
				continue;
			}
			// one to many
			if (value instanceof List) {
				oneToMany.add((List) value);
				continue;
			}

			if (value != null) {
				attribute.append(",");
				attribute.append(fieldName2ColumnName(field.getName()));
				parameters.append(",?");
				values.add(value);
			}
		}
		String attributeStr = attribute.substring(1, attribute.length());
		String parametersStr = parameters.substring(1, parameters.length());

		insert.append("(");
		insert.append(attributeStr);
		insert.append(")");
		insert.append(" VALUES ");
		insert.append("(");
		insert.append(parametersStr);
		insert.append(")");

		Object[] parm = values.toArray();
		table.put(Constants.DB_SQL, insert.toString());
		table.put(Constants.DB_PARM, parm);
		table.put(Constants.ONE_TO_ONE_ASSOC, oneToOne);
		table.put(Constants.ONE_TO_MANY_ASSOC, oneToMany);
		table.put(Constants.TABLE_NAME, tableName);
		return table;

	}

	public static Hashtable<String, Object> createUpdateSql(Object obj) {

		Hashtable<String, Object> table = new Hashtable<String, Object>();

		StringBuffer update = new StringBuffer();
		StringBuffer attribute = new StringBuffer();
		ArrayList<Object> values = new ArrayList<Object>();

		update.append("UPDATE ");
		update.append("`" + className2TableName(obj) + "`");
		update.append("SET ");

		Field[] fields = obj.getClass().getDeclaredFields();

		for (int i = 0; i < fields.length; i++) {

			Field field = fields[i];
			if ("serialVersionUID".equals(field.getName())) {
				continue;
			}
			Object value = getValueByField(obj, field.getName());

			if (value != null) {
				attribute.append(" ");
				attribute.append(fieldName2ColumnName(field.getName()));
				attribute.append(" = ? ,");

				values.add(value);
			}
		}
		Object id = getValueByField(obj, "id");
		values.add(id);

		String attributeStr = attribute.substring(0, attribute.length() - 1);

		update.append(attributeStr);
		update.append(" WHERE ID = ? ");

		Object[] parm = values.toArray();
		table.put(Constants.DB_SQL, update.toString());
		table.put(Constants.DB_PARM, parm);
		return table;

	}

	public static String getLastInsertSql(Object obj) {

		String sql = "select LAST_INSERT_ID() as id from `~` limit 1";

		return sql.replaceAll("~", className2TableName(obj));
	}

	public static String getLoaderSql(Object obj) {

		StringBuffer load = new StringBuffer();

		load.append(" SELECT * FROM ");
		load.append(className2TableName(obj));
		load.append(" WHERE ID = ? ");

		return load.toString();
	}

	public static String className2TableName(Object obj) {

		String className = obj.getClass().getName();
		className = className.substring(className.lastIndexOf(".") + 1, className.length());

		char[] chs = className.toCharArray();

		StringBuffer tableName = new StringBuffer();
		tableName.append(chs[0]);
		for (int i = 1; i < chs.length; i++) {
			byte bt = (byte) chs[i];
			if (bt >= 65 && bt <= 90) {
				tableName.append("_");
				tableName.append(chs[i]);
			} else {
				tableName.append(chs[i]);
			}
		}

		return tableName.toString();
	}

	public static String fieldName2ColumnName(String fieldName) {

		char[] chs = fieldName.toCharArray();

		StringBuffer columnName = new StringBuffer();
		columnName.append(chs[0]);
		for (int i = 1; i < chs.length; i++) {
			byte bt = (byte) chs[i];
			if (bt >= 65 && bt <= 90) {
				columnName.append("_");
				columnName.append(chs[i]);
			} else {
				columnName.append(chs[i]);
			}
		}
		return columnName.toString();
	}

	public static Object getValueByField(Object obj, String field) {
		Object value = null;
		try {
			String methodName = fieldName2GetMethod(field);
			Method method = obj.getClass().getMethod(methodName, new Class[0]);
			value = method.invoke(obj, new Object[0]);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return value;
	}

	public static String fieldName2GetMethod(String fieldName) {
		return "get" + fieldName.substring(0, 1).toUpperCase() + fieldName.substring(1, fieldName.length());
	}

	public static String fieldName2SetMethod(String fieldName) {
		return "set" + fieldName.substring(0, 1).toUpperCase() + fieldName.substring(1, fieldName.length());
	}
}
