<?php
class Common{

	const FIRST_NAME_KEY = 'FIRST_NAMES';
	const LAST_NAME_KEY = 'LAST_NAMES';

	static function create_name(){
		$first_names = Q::cache(Common::FIRST_NAME_KEY );
		if(empty($first_names)){
			$rs = Name::find('type=?','F')->getAll();
			$first_names = new ArrayObject();
			foreach ($rs as $name){
				$first_names->append($name->toArray());
			}
			if(!empty($first_names))
			{
				Q::writeCache(Common::FIRST_NAME_KEY, $first_names);
			}
		}
		$last_names = Q::cache(Common::LAST_NAME_KEY );
		if(empty($last_names)){
			$rs = Name::find('type=?','L')->getAll();
			$last_names = new ArrayObject();
			foreach ($rs as $name){
				$last_names->append($name->toArray());
			}
			if(!empty($last_names))
			{
				Q::writeCache(Common::LAST_NAME_KEY, $last_names);
			}
		}
		$randa = rand(0,count($first_names)-1);
		$randb = rand(0,count($last_names)-1);
		QLog::log('the name is '.json_encode($first_names));
		return $first_names[$randa]['name'].'.'.$last_names[$randb]['name'];
	}

	static function create_no(){

		$no = rand(0,100);
		return $no;
	}

	static function create_age($min=17,$max=25){

		$age = rand($min,$max);
		return $age;
	}

	static function create_stature($position){

		$inc = rand(0,20);
		switch($position){
			case 'C':$base = 200;break;
			case 'PF':$base = 190;break;
			case 'SF':$base = 185;break;
			case 'SG':$base = 175;break;
			case 'PG':$base = 165;break;
		}
		return $inc + $base;
	}

	static function create_avoirdupois($position){

		$inc = rand(0,20);
		switch($position){
			case 'C':$base = 100;break;
			case 'PF':$base = 95;break;
			case 'SF':$base = 85;break;
			case 'SG':$base = 80;break;
			case 'PG':$base = 70;break;
		}
		return $inc + $base;
	}

	static function check_color($player,$attr){

		$curr_val = $player->$attr;
		$max_attr = $attr.'_max';
		$max_val = $player->$max_attr;

		$potential = $max_val - $curr_val;

		if($potential>25){
			return 1 ;
		}elseif ($potential>20){
			return 2;
		}elseif ($potential>16){
			return 3;
		}elseif ($potential>12.5){
			return 4;
		}
		elseif ($potential>9.5){
			return 5;
		}
		elseif ($potential>7.0){
			return 6;
		}
		elseif ($potential>4.8){
			return 7;
		}
		elseif ($potential>3.0){
			return 8;
		}
		elseif ($potential>1.5){
			return 9;
		}
		elseif ($potential>0.7){
			return 10;
		}else{
			return 11;
		}
	}

}