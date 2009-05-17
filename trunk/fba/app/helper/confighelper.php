<?php 

class ConfigHelper{

	const  ATTR_CONFIG_C = 'grade-c';
	const  ATTR_CONFIG_PF = 'grade-pf';
	const  ATTR_CONFIG_SF= 'grade-sf';
	const  ATTR_CONFIG_SG = 'grade-sg';
	const  ATTR_CONFIG_PG= 'grade-pg';

	static function readFreePlayerConfig(){

		$app_config = Q::ini('app_config');
		$ROOT_DIR = $app_config['ROOT_DIR'];

		require_once($ROOT_DIR.'/lib/domit/xml_domit_include.php');

		$cdCollection = new DOMIT_Document();
		$success = $cdCollection->loadXML($ROOT_DIR.'/config/attribute-config.xml');

		$attributes = array();
		if($success){

			$myDocumentElement = $cdCollection->documentElement;
			$mynodes = $myDocumentElement->childNodes;

			foreach ($mynodes as $node){
				if($node instanceof DOMIT_Comment) {
					continue;
				}
				$attributes[$node->getAttribute('name')] = ConfigHelper::readAttr($node);
			}
		}
		return $attributes;

	}
	private static function readAttr($node){

		$attribute_config = array();
		$position_att_nodes = $node->childNodes;
		foreach ($position_att_nodes as $position_node){
			$row = split(';',$position_node->getText());
			$attribute_config[$position_node->nodeName] = $row;
		}

		return $attribute_config;

	}


}
