require "bottle/sql_helper"
require "bottle/db_connection"
class IdGenerater
  
   def IdGenerater.generater(obj)
     sql = SQLHelper.create_query_max_id_sql(obj)
	 conn = ConnectionPool.connection
     res = conn.query(sql)[0]
	 ConnectionPool.disconnection conn
	 res
   end

end