checkSelect = function(){
	var max_num = 8;
	var sum=0;		
	var name = document.getElementsByName('player[]');

	for(var i=0;i<name.length;i++){
	    if(name[i].checked==true){
	        sum += 1;
	    }
	}
	if(sum > max_num){
	    return false;
	}
	if(sum == max_num){
		document.getElementById('sub').disabled = false;
	}else{
		document.getElementById('sub').disabled = true;
    }
	var div = $("notice");
	div.innerHTML = "您已选择"+sum+",还可以选择"+(max_num-sum)+"";
	return true;
}