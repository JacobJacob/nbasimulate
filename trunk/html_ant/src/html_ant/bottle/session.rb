require 'singleton'
require 'bottle/sql_helper'
require 'bottle/db_connection'
require 'bottle/object_not_found_exception'
require 'bottle/cache'
require 'bottle/init'
require 'bottle/id_generater'
require 'bottle/logger'
require 'bottle/connection_pool'
class Session
   
   include Singleton
   def initialize
     
   end
   
   def load(id,cla)
     
	 obj = $cache.try_load_object(id)
	 if obj != nil
	   Logger.cache_on_info "cache hit on ID:" + id.to_s
	   return obj
	 end
     begin
	   obj = cla.new
	   sql = SQLHelper.create_query_sql(obj,id)
	   fields_value = conn.execute_query_sql_whit_id(sql)
	   ConnectionPool.disconnection conn
	   if fields_value == nil
	    return nil
	   end
	   combination(obj,fields_value)
	   $cache.put(id,obj)
	   return obj
	 rescue Mysql::Error => e 
	   puts "Error code: #{e.errno}"
	 end
   end
   def loadBySql(sql,cla)
   
     begin
	   obj = cla.new
	   conn = ConnectionPool.connection
	   fields_value = conn.execute_query_sql_whit_id(sql)
	   ConnectionPool.disconnection conn
	   if fields_value == nil
	    return nil
	   end
	   combination(obj,fields_value)
	   id = obj.id
	   $cache.put(id,obj)
	   return obj
	 rescue Mysql::Error => e 
	   Logger.output_with_time "Error code: #{e.errno}"
	 end
   end
   def save(obj)
   
     Logger.output_with_time  "save start..."
	 sql,parm = SQLHelper.create_insert_sql(obj)
	 flg = @conn.execute(sql,parm)
	 id = IdGenerater.generater(obj)
	 obj.id = id
	 $cache.put(id,obj)
	 Logger.output_with_time  "save success..."
     flg
   end
   def update(obj)

     Logger.output_with_time "update start..."
	 sql,parm = SQLHelper.create_update_sql(obj)
	 flg = @conn.execute(sql,parm)
	 id = obj.id
	 $cache.update(id,obj)
	 Logger.output_with_time  "update success..."
     flg
   end
   def combination(obj,hash)
     hash.each_key do |key|
       obj.method(key.to_s + "=").call hash[key]
	 end
   end
   
   def begin_transaction
     @conn = ConnectionPool.connection
   end
   
   def end_transaction(mode=true)
      if mode
	    @conn.commit
      else
	    @conn.rollback
	  end
	  ConnectionPool.disconnection @conn
   end
end


