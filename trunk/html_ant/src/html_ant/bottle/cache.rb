class Cache
  
  def initialize
   @obj_list = Hash.new
  end
  
  
  def has_key?(key)
    @obj_list.has_key?(key)
  end
  
  def try_load_object(id)
   if @obj_list.has_key?(id) 
     @obj_list[id]
   else
     return nil
   end
  end
  
  def put(key,value)
   @obj_list.store(key,value)
  end
  
  def move(key)
    @obj_list.delete(key)
  end
  
  def update(key,value)
    @obj_list[key]=value
  end
end