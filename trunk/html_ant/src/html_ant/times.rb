require "parsedate"
#require "date"  
include ParseDate  

str = '08-03 11:22'
time=parsedate(str)

puts time.class
puts "#{time}"

time.each do |t|
 puts t
end