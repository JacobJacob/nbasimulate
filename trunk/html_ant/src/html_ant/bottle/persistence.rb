require "bottle/sql_helper"
require "bottle/db_connection"
require "bottle/session"

class Persistence
  
  def initialize
   @id = 0
  end
  def save
    Logger.output_with_time  "start save object #{self.class.name}..."
	flg = Session.instance.save(self)
	Logger.output_with_time  "finish save object #{self.class.name}[#{@id}]..."
    flg
  end
  def update
    Logger.output_with_time  "start update object #{self.class.name}[#{@id}]..."
    flg = Session.instance.update(self)
	Logger.output_with_time  "finish update object #{self.class.name}[#{@id}]..."
    flg
  end
  attr_accessor :id
end

