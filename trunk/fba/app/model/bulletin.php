<?php
// $Id$

/**
 * Bulletin 封装来自 match_stat 数据表的记录及领域逻辑
 */
class Bulletin extends QDB_ActiveRecord_Abstract
{

	/**
     * 返回对象的定义
     *
     * @static
     *
     * @return array
     */
	static function __define()
	{
		return array
		(
		// 指定该 ActiveRecord 要使用的行为插件
		'behaviors' => '',

		// 指定行为插件的配置
		'behaviors_settings' => array
		(
		# '插件名' => array('选项' => 设置),
		),

		// 用什么数据表保存对象
		'table_name' => 'match_stat',

		// 指定数据表记录字段与对象属性之间的映射关系
		// 没有在此处指定的属性，QeePHP 会自动设置将属性映射为对象的可读写属性
		'props' => array
		(
		// 主键应该是只读，确保领域对象的“不变量”
		'id' => array('readonly' => true),
		'total_point' => array('getter'=>'total_point'),
		'total_rebound' => array('getter'=>'total_rebound'),
		'doom1rate' => array('getter'=>'doom1rate'),
		'doom2rate' => array('getter'=>'doom2rate'),
        'doom3rate' => array('getter'=>'doom3rate'),
		/**
                 *  可以在此添加其他属性的设置
                 */
		# 'other_prop' => array('readonly' => true),

		/**
                 * 添加对象间的关联
                 */
		# 'other' => array('has_one' => 'Class'),
		'player' => array(
		'has_one' => 'Player',
		'source_key' => 'player_id',
		'target_key' => 'id'
		)
		),


		/**
             * 允许使用 mass-assignment 方式赋值的属性
             *
             * 如果指定了 attr_accessible，则忽略 attr_protected 的设置。
             */
             'attr_accessible' => '',

             /**
             * 拒绝使用 mass-assignment 方式赋值的属性
             */
             'attr_protected' => 'id',

             /**
             * 指定在数据库中创建对象时，哪些属性的值不允许由外部提供
             *
             * 这里指定的属性会在创建记录时被过滤掉，从而让数据库自行填充值。
             */
             'create_reject' => '',

             /**
             * 指定更新数据库中的对象时，哪些属性的值不允许由外部提供
             */
             'update_reject' => '',

             /**
             * 指定在数据库中创建对象时，哪些属性的值由下面指定的内容进行覆盖
             *
             * 如果填充值为 self::AUTOFILL_TIMESTAMP 或 self::AUTOFILL_DATETIME，
             * 则会根据属性的类型来自动填充当前时间（整数或字符串）。
             *
             * 如果填充值为一个数组，则假定为 callback 方法。
             */
             'create_autofill' => array
             (
             # 属性名 => 填充值
             # 'is_locked' => 0,
             ),

             /**
             * 指定更新数据库中的对象时，哪些属性的值由下面指定的内容进行覆盖
             *
             * 填充值的指定规则同 create_autofill
             */
             'update_autofill' => array
             (
             ),

             /**
             * 在保存对象时，会按照下面指定的验证规则进行验证。验证失败会抛出异常。
             *
             * 除了在保存时自动验证，还可以通过对象的 ::meta()->validate() 方法对数组数据进行验证。
             *
             * 如果需要添加一个自定义验证，应该写成
             *
             * 'title' => array(
             *        array(array(__CLASS__, 'checkTitle'), '标题不能为空'),
             * )
             *
             * 然后在该类中添加 checkTitle() 方法。函数原型如下：
             *
             * static function checkTitle($title)
             *
             * 该方法返回 true 表示通过验证。
             */
             'validations' => array
             (
             'point2_shoot_times' => array
             (
             array('is_int', 'point2_shoot_times必须是一个整数'),

             ),

             'point2_doom_times' => array
             (
             array('is_int', 'point2_doom_times必须是一个整数'),

             ),

             'point3_shoot_times' => array
             (
             array('is_int', 'point3_shoot_times必须是一个整数'),

             ),

             'point3_doom_times' => array
             (
             array('is_int', 'point3_doom_times必须是一个整数'),

             ),

             'point1_shoot_times' => array
             (
             array('is_int', 'point1_shoot_times必须是一个整数'),

             ),

             'point1_doom_times' => array
             (
             array('is_int', 'point1_doom_times必须是一个整数'),

             ),

             'offensive_rebound' => array
             (
             array('is_int', 'offensive_rebound必须是一个整数'),

             ),

             'defensive_rebound' => array
             (
             array('is_int', 'defensive_rebound必须是一个整数'),

             ),

             'foul' => array
             (
             array('is_int', 'foul必须是一个整数'),

             ),

             'lapsus' => array
             (
             array('is_int', 'lapsus必须是一个整数'),

             ),


             ),
             );
	}


	/* ------------------ 以下是自动生成的代码，不能修改 ------------------ */

	/**
     * 开启一个查询，查找符合条件的对象或对象集合
     *
     * @static
     *
     * @return QDB_Select
     */
	static function find()
	{
		$args = func_get_args();
		return QDB_ActiveRecord_Meta::instance(__CLASS__)->findByArgs($args);
	}

	/**
     * 返回当前 ActiveRecord 类的元数据对象
     *
     * @static
     *
     * @return QDB_ActiveRecord_Meta
     */
	static function meta()
	{
		return QDB_ActiveRecord_Meta::instance(__CLASS__);
	}


	/* ------------------ 以上是自动生成的代码，不能修改 ------------------ */
	/**
	 * 总得分
	 *
	 * @return unknown
	 */
	function total_point(){

		$total = 0 ;
		$total += $this->point3_doom_times * 3 ;
		$total += $this->point2_doom_times * 2 ;
		$total += $this->point1_doom_times * 1 ;
		return $total;
	}
	/**
	 * 罚球命中率
	 *
	 * @return unknown
	 */
	function doom1rate(){
		if($this->point1_shoot_times==0){
			return '--';
		}
		$rate = $this->point1_doom_times / $this->point1_shoot_times * 100 ;
		return number_format($rate,2).'%';
	}
	/**
	 * 二分球命中率
	 *
	 * @return unknown
	 */
	function doom2rate(){
		if($this->point2_shoot_times==0){
			return '--';
		}
		$rate = $this->point2_doom_times / $this->point2_shoot_times * 100 ;
		return number_format($rate,2).'%';
	}
	/**
	 * 三分球命中率
	 *
	 * @return unknown
	 */
	function doom3rate(){
		if($this->point3_shoot_times==0){
			return '--';
		}
		$rate = $this->point3_doom_times / $this->point3_shoot_times * 100 ;
		return number_format($rate,2).'%';
	}
	/**
	 * 总篮板
	 */
	function total_rebound(){
		return $this->offensive_rebound + $this->defensive_rebound;
	}
}

