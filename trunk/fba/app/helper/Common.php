<?php
class common{

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

}