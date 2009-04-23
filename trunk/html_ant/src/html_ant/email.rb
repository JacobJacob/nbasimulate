require 'bottle'

class Email < Persistence

     attr_accessor :email
	 def exist?
	    sql = SQLHelper.create_query_by_column(self,'email',@email)
	    res = DBConnection.new.query(sql)		 
	    if  res == nil || res.length == 0
	      return false
	    end
	    return true
	 end
end