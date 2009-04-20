require 'Session'
require 'Persistence'
require 'SystemInt'
require 'Cache'
class Tactic < Persistence
  
  attr_accessor :team
  attr_accessor :aggression_tactice_1,:defend_tactice_1
  attr_accessor :aggression_tactice_2,:defend_tactice_2
  attr_accessor :aggression_tactice_3,:defend_tactice_3
  attr_accessor :aggression_tactice_4,:defend_tactice_4
  
  @@cache = Cache.new
  
  def save
    super
	@@cache.put(@team,@id)
  end
  def update
    super
	@@cache.put(@team,@id)
  end
  def exist?
    sql = "SELECT ID FROM TACTIC WHERE TEAM = '" + @team.to_s + "'"
	res = DBConnection.new.query(sql)
	if  res == nil || res.length == 0
	  return false
	end
	@id = res[0]
	return true
  end
  def saveOrUpdate
     
	 if exist?
	   update
	 else
	   save
	 end
  end
  
  def Tactic.loadByTeamId(teamId)
    
	if Tactic.exist?(teamId)
	   if @@cache.has_key?(teamId)
	    id = @@cache.try_load_object(teamId)
	    Session.new.load(id,Tactic)
	   else
	    sql = "SELECT * FROM TACTIC WHERE TEAM = '" + teamId.to_s + "'"
        Session.new.loadBySql(sql,Tactic)
      end
	else
	  t = Tactic.new
	end
  end
  def Tactic.exist?(teamId)
  
    sql = "SELECT ID FROM TACTIC WHERE TEAM = '" + teamId.to_s + "'"
	res = DBConnection.new.query(sql)
	if  res == nil || res.length == 0
	  return false
	end
	return true
  end
end

=begin
t = Tactic.new
t.aggression_tactice_1 = "TEST"
t.team = 5
t.save
puts "============="
puts Tactic.loadByTeamId(5).aggression_tactice_1 

puts Tactic.loadByTeamId(5).aggression_tactice_1 

t.aggression_tactice_2 = "TEST"
t.update


#puts SQLHelper.create_update_sql(t)["sql"]
#puts SQLHelper.create_update_sql(t)["values"]
=end



