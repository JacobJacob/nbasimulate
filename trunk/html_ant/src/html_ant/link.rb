require 'bottle'

class Link < Persistence

     attr_accessor :title
     attr_accessor :link
	 def exist?
	    sql = SQLHelper.create_query_by_column(self,'link',@link)
	    res = DBConnection.new.query(sql)		 
	    if  res == nil || res.length == 0
	      return false
	    end
	    return true
	 end
end