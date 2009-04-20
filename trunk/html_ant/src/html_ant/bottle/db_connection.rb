require "mysql"
require "bottle/logger"
require "bottle/string_converter"

class DBConnection
  HOST = "localhost"
  DB = "ant"
  USER_NAME = "root"
  PASSWORD = "821015"
  def initialize
    @dbh = Mysql.real_connect(HOST, USER_NAME, PASSWORD, DB) 
  end
  
  def connection
   @dbh
  end
  
  def execute(sql,parm)
    execute_result = false
	@dbh.autocommit false
	Logger.info(sql)
	Logger.info(StringConverter.arrayToString(parm))
    begin  
	   de = @dbh.prepare("SET NAMES GB2312")
	   de.execute
	   de = @dbh.prepare(sql);
       de.execute *parm
       de.close
	   execute_result = true
	rescue Mysql::Error => e
       Logger.output_with_time "Error code: #{e.errno}"
       Logger.output_with_time "Error message: #{e.error}"
       Logger.output_with_time "Error SQLSTATE: #{e.sqlstate}" if e.respond_to?("sqlstate")
	   execute_result = false
	rescue => e
	   Logger.output_with_time "Error code: #{e}" 
	   Logger.output_with_time "SQL: #{sql}" 
	   execute_result = false
	end
	execute_result
  end
  def query(sql)
    begin  
	   res = @dbh.query(sql)
	   res.fetch_row
	rescue Mysql::Error => e
       Logger.output_with_time "Error code: #{e.errno}"
       Logger.output_with_time "Error message: #{e.error}"
       Logger.output_with_time "Error SQLSTATE: #{e.sqlstate}" if e.respond_to?("sqlstate")
	end
  end
  def execute_query_sql_whit_id(sql)

    begin  
	   res = @dbh.query(sql)
	   res.fetch_hash
	rescue Mysql::Error => e
       Logger.output_with_time "Error code: #{e.errno}"
       Logger.output_with_time "Error message: #{e.error}"
       Logger.output_with_time "Error SQLSTATE: #{e.sqlstate}" if e.respond_to?("sqlstate")
	end
  end
  def autocommit(mode)
   @dbh.autocommit(mode)
  end
  def commit
   @dbh.commit
  end
  def rollback
   @dbh.rollback
  end

end
