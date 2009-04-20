class StringConverter
   def StringConverter.arrayToString(arr)
     result = ""
	 arr.each do |a|
	   result += "," + a.to_s
	 end
	 result.slice!(0) 
	 "[" + result + "]"
   end
   
   def StringConverter.StringToNumber(arr)

     begin 
	   return arr.to_f
	 rescue => err
	   return -1
	 end
   end
end
