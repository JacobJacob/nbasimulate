require 'bottle/db_connection'
class ConnectionPool
   
   MAX_CONNECTIONS = 2
   @@connections = []
   @@connection_num = 0
   @@current_ind = -1
   MAX_CONNECTIONS.times do |i|
     c = DBConnection.new
	 @@connections += [c]
	 @@connection_num += 1
	 @@current_ind += 1
   end
   
   def ConnectionPool.connection
      until @@connection_num > 0
	    sleep(1)
	  end
	  @@current_ind -= 1
	  @@connection_num -= 1
	  @@connections.delete_at(@@current_ind)
   end
   
   def ConnectionPool.disconnection(con)
      @@current_ind += 1
	  @@connection_num += 1
	  @@connections += [con]
   end

end
