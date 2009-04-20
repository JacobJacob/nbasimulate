require "logger"
class ConsoleLogger
   
   def ConsoleLogger.output_with_time(msg)
    puts "[" + Time.new.strftime("%Y-%m-%d %H:%M:%S") + "] " + msg
   end
   def ConsoleLogger.output(msg)
    puts  "[" + Time.new.strftime("%Y-%m-%d %H:%M:%S") + "] " + msg
   end
   def ConsoleLogger.print_msg(msg)
    print "[" + Time.new.strftime("%Y-%m-%d %H:%M:%S") + "] " + msg
   end
   def ConsoleLogger.print_wait_line(msg,times)
     print "[" + Time.new.strftime("%Y-%m-%d %H:%M:%S") + "] " + msg
	 i = 0
	 while i < times
	   print "."
	   sleep(0.1)
	   i += 1
	 end
	 puts "."
   end
   
   def ConsoleLogger.info(msg)
    begin
     log_file_name = "log/log" + Time.new.strftime("%Y%m%d%H") + ".log"
     log = Logger.new(log_file_name,5,10*1024)
     log.info(msg)
	rescue => err
	   ConsoleLogger.info  err
	 end
   end
   
   def ConsoleLogger.cache_on_info(msg)
    begin
	 log_file_name = "log/CACHE" + Time.new.strftime("%Y%m%d%H") + ".log"
     log = Logger.new(log_file_name,5,10*1024)
     log.info(msg)
	rescue => err
	   ConsoleLogger.info  err
	 end
   end
   
    def ConsoleLogger.exception(msg)
     begin
	  log_file_name = "log/Exception" + Time.new.strftime("%Y%m%d%H") + ".log"
      log = Logger.new(log_file_name,5,10*1024)
      log.info(msg)
	 rescue => err
	   ConsoleLogger.info  err
	 end
   end
   
   def ConsoleLogger.error(msg)
     begin
	  log_file_name = "log/error" + Time.new.strftime("%Y%m%d%H") + ".log"
      log = Logger.new(log_file_name,5,10*1024)
      log.error(msg)
	 rescue => err
	   ConsoleLogger.info  err
	 end
   end
   
    def ConsoleLogger.task(msg)
     begin
	  log_file_name = "log/task" + Time.new.strftime("%Y%m%d%H") + ".log"
      log = Logger.new(log_file_name,5,10*1024)
      log.info(msg)
	 rescue => err
	   ConsoleLogger.info  err
	 end
   end
   
end

#ConsoleLogger.output "test"