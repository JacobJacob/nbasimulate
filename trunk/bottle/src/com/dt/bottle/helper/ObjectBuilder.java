package com.dt.bottle.helper;

import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.sql.ResultSet;
import java.util.Date;

import com.dt.bottle.persistence.Persistence;
import com.dt.bottle.sql.SqlHelper;

public class ObjectBuilder {

	public static void builderObjFromResultSet(Object obj, ResultSet resultSet)
			throws Exception {

		Class<?> cls = obj.getClass();

		Field[] fields = cls.getDeclaredFields();

		for (int i = 0; i < fields.length; i++) {

			Field field = fields[i];

			if ("serialVersionUID".equals(field.getName())) {
				continue;
			}

			String fieldName = field.getName();
			Class<?>[] parmTypes = { field.getType() };
			Object value = getValueWithField(field, resultSet);
			Object[] args = { value };
			Method method = cls.getMethod(SqlHelper
					.fieldName2SetMethod(fieldName), parmTypes);

			method.invoke(obj, args);

		}
		((Persistence) obj).setId(resultSet.getLong("id"));

	}

	public static Object getValueWithField(Field field, ResultSet resultSet)
			throws Exception {

		// if(!resultSet.next()){
		// throw new ObjectNotFoundException();
		// }
		Class<?> fieldType = field.getType();
		String fieldTypeName = fieldType.getName();
		if (fieldTypeName.endsWith("String")) {
			return resultSet.getString(SqlHelper.fieldName2ColumnName(field
					.getName()));
		} else if (fieldTypeName.endsWith("Integer")
				|| fieldTypeName.endsWith("int")) {
			return resultSet.getInt(SqlHelper.fieldName2ColumnName(field
					.getName()));
		} else if (fieldTypeName.endsWith("Long")
				|| fieldTypeName.endsWith("long")) {
			return resultSet.getLong(SqlHelper.fieldName2ColumnName(field
					.getName()));
		} else if (fieldTypeName.endsWith("Double")
				|| fieldTypeName.endsWith("double")) {
			return resultSet.getDouble(SqlHelper.fieldName2ColumnName(field
					.getName()));
		} else if (fieldTypeName.endsWith("Float")
				|| fieldTypeName.endsWith("float")) {
			return resultSet.getFloat(SqlHelper.fieldName2ColumnName(field
					.getName()));
		} else if (fieldTypeName.endsWith("Boolean")
				|| fieldTypeName.endsWith("boolean")) {
			return resultSet.getBoolean(SqlHelper.fieldName2ColumnName(field
					.getName()));
		} else if (fieldTypeName.endsWith("Date")) {
			Date date = new Date(resultSet.getTimestamp(
					SqlHelper.fieldName2ColumnName(field.getName())).getTime());
			return date;
		}
		return "";

	}
}
