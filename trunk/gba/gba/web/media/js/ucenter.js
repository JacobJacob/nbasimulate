YYYYMMDDstart = function(){

    MonHead = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    //先给年下拉框赋内容
    
    var ys = new Date().getFullYear();//初始年份
    var y   = 1984;
    //以今年为准，前30年，后30年
    for (var i = (ys-80); i < (ys); i++) {
        document.getElementById("yyyy").options.add(new Option(" "+ i +" ", i));
    }

    //赋月份的下拉框
    for (var i = 1; i < 13; i++){
        document.getElementById("mm").options.add(new Option(" " + i + " ", i));
    }

    document.getElementById("yyyy").value = y;
    document.getElementById("mm").value = 1;//初始月份new Date().getMonth() +
 
    var n = MonHead[new Date().getMonth()];
    if(new Date().getMonth() ==1 && isPinYear(y)) n++;
    writeDay(31); //赋日期下拉框Author:meizz
    document.getElementById("dd").value = 1;//初始日期new Date().getDate()
}

//年发生变化时日期发生变化(主要是判断闰平年)
yyyydd = function(str){
    var MMvalue = document.getElementById("mm").options[document.getElementById("mm").selectedIndex].value;
    if (MMvalue == ""){ 
        var e = document.getElementById("dd"); 
        optionsClear(e); 
        return;
    }
    var n = MonHead[MMvalue - 1];
    if (MMvalue ==2 && isPinYear(str)){
        n++;
    }
    writeDay(n);
}

//月发生变化时日期联动
mmdd = function(str){
    var YYYYvalue = document.getElementById("yyyy").options[document.getElementById("yyyy").selectedIndex].value;
    if (YYYYvalue == ""){ 
        var e = document.getElementById("dd"); 
        optionsClear(e); 
        return;
    }
    var n = MonHead[str - 1];
    if (str ==2 && isPinYear(YYYYvalue)){
        n++;
    }
    writeDay(n);
}

//判断是否闰平年
isPinYear = function(year){   
    return(0 == year%4 && (year%100 !=0 || year%400 == 0));
}

//据条件写日期的下拉框
writeDay = function(n){
    var e = document.getElementById("dd"); 
    optionsClear(e);
    for (var i=1; i<(n+1); i++){
        e.options.add(new Option(" "+ i + " ", i));
    }
}

optionsClear = function(e){
    for (var i=e.options.length; i>0; i--){
        e.remove(i);
    }
}