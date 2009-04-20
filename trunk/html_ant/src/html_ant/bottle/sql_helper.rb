
module SQLHelper
  
  def SQLHelper.create_insert_sql(obj)
    attribute = ""
	parameters = ""
	values = []
    insert = "INSERT INTO #{create_table_name(obj.class.to_s)}" 
    obj.instance_variables.each do |attr|
      value = obj.method(attr.delete("@")).call
	  if value != nil and value != ""
	    attribute += "," + attr.to_s.delete("@")
		parameters += ",?"
		values += [value]
	  end
	end
	attribute.slice!(0) 
	parameters.slice!(0) 
	insert << "(" + attribute + ") VALUES (#{parameters})"
	return insert , values 
  end
  def SQLHelper.create_update_sql(obj)
    attribute = ""
	values = []
    update = "UPDATE #{create_table_name obj.class.to_s} SET " 
	superVariables = obj.class.superclass.new.instance_variables
    (obj.instance_variables - superVariables).each do |attr|
      value = obj.method(attr.delete("@")).call
	  if value != nil and value != ""
	    attribute += "," + attr.to_s.delete("@") + "=?"
		values += [value]
	  end
	end
	attribute.slice!(0) 
	id = obj.method("id").call.to_s
	update << attribute << " WHERE  ID = '#{id.to_s}'"
	return update , values 
  end
  def SQLHelper.create_query_by_column(obj,column_name,column_value,column_all = false)
    if column_all
      select = "SELECT * FROM " + create_table_name(obj.class.to_s)  + " WHERE #{column_name} = '#{column_value}'"
	else 
	  select = "SELECT 1 FROM " + create_table_name(obj.class.to_s)  + " WHERE #{column_name} = '#{column_value}'"  
	end
	return select
  end
  def SQLHelper.create_table_name(class_name)
    table_name = ''
	first = true
	class_name.each_byte do |byte|
	  if byte >= 65 and byte <= 90 and !first
	    table_name << '_' << byte
      else
	    table_name << byte
	  end
	  first = false
	end
	table_name << 's'
  end
  def SQLHelper.create_query_sql(obj,id)
    select = "SELECT *  FROM " + obj.class.to_s  + " WHERE id = " + id.to_s
  end
  def SQLHelper.create_query_max_id_sql(obj)
    select = "SELECT MAX(id)  FROM #{create_table_name obj.class.to_s}"  
  end
end



