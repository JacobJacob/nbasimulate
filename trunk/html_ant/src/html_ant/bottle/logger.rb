require "logger"
class Logger
   
   def Logger.output_with_time(msg)
    puts "[" + Time.new.strftime("%Y-%m-%d %H:%M:%S") + "] " + msg
   end
   def Logger.output(msg)
    puts  "[" + Time.new.strftime("%Y-%m-%d %H:%M:%S") + "] " + msg
   end
   def Logger.print_msg(msg)
    print "[" + Time.new.strftime("%Y-%m-%d %H:%M:%S") + "] " + msg
   end
   def Logger.print_wait_line(msg,times)
     print "[" + Time.new.strftime("%Y-%m-%d %H:%M:%S") + "] " + msg
	 i = 0
	 while i < times
	   print "."
	   sleep(0.1)
	   i += 1
	 end
	 puts "."
   end
   
   def Logger.info(msg)
    begin
     log_file_name = "log/info/SQL" + Time.new.strftime("%Y%m%d%H") + ".log"
     log = Logger.new(log_file_name,5,10000*1024)
     log.info(msg)
	rescue  => err
	  exception err
	end
   end
   
   def Logger.cache_on_info(msg)
     log_file_name = "log/info/CACHE" + Time.new.strftime("%Y%m%d%H") + ".log"
     log = Logger.new(log_file_name,5,10000*1024)
     log.info(msg)
   end
   
   def Logger.exception(msg)
	 begin 
      log_file_name = "log/error/error" + Time.new.strftime("%Y%m%d%H") + ".log"
      log = Logger.new(log_file_name,5,10000*1024)
      log.error(msg)
	 rescue 
	  
	 end
   end
   def Logger.error(msg)
     begin
	  log_file_name = "log/error/error" + Time.new.strftime("%Y%m%d%H") + ".log"
      log = Logger.new(log_file_name,5,10*1024)
      log.error(msg)
	 rescue => err
	   Logger.info  err
	 end
   end
   def Logger.task(msg)
     begin
	  log_file_name = "log/info/task" + Time.new.strftime("%Y%m%d%H") + ".log"
      log = Logger.new(log_file_name,5,10*1024)
      log.info(msg)
	 rescue => err
	   Logger.info  err
	 end
   end
   
end

#Logger.output "test"