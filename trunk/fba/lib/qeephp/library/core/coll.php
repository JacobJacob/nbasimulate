<?php
// $Id: coll.php 2008 2009-01-08 18:49:30Z dualface $

/**
 * 定义 QColl 类
 *
 * @link http://qeephp.com/
 * @copyright Copyright (c) 2006-2009 Qeeyuan Inc. {@link http://www.qeeyuan.com}
 * @license New BSD License {@link http://qeephp.com/license/}
 * @version $Id: coll.php 2008 2009-01-08 18:49:30Z dualface $
 * @package core
 */

/**
 * QColl 实现了一个类型安全的集合
 *
 * QColl 会检查每一个元素的类型是否符合预期，
 * 以便于将同一类型的元素组织在一起。
 *
 * QColl 具有和 PHP 内置数组相似的性质，因此可以按照使用数组的方式来使用 QColl 集合。
 *
 * 在构造一个集合时，必须指定该集合能够容纳的元素类型。
 *
 * @code php
 * $coll = new QColl('MyObject');
 * $coll[] = new MyObject();
 *
 * // 在尝试存入 MyObject2 类型的对象到 $coll 中时将抛出异常
 * $coll[] = new MyObject2();
 *
 * // 指定一个元素
 * $coll[$offset] = $item;
 *
 * // 遍历一个集合
 * foreach ($coll as $offset => $item)
 * {
 *     dump($item, $offset);
 * }
 * @endcode
 *
 * @author YuLei Liao <liaoyulei@qeeyuan.com>
 * @version $Id: coll.php 2008 2009-01-08 18:49:30Z dualface $
 * @package core
 */
class QColl implements Iterator, ArrayAccess, Countable
{
    /**
     * 集合元素的类型
     *
     * @var string
     */
    protected $_type;

    /**
     * 保存元素的数组
     *
     * @var array
     */
    protected $_coll = array();

    /**
     * 构造函数
     *
     * @param string $type 集合元素类型
     */
    function __construct($type)
    {
        $this->_type = $type;
    }

    /**
     * 从数组创建一个集合
     *
     * QColl::createFromArray() 方法从一个包含指定类型元素的数组创建集合。
     * 新建的集合包含数组中的所有元素，并且确保元素的类型符合要求。
     *
     * @param array $arr 创建集合的数组
     * @param string $type 集合元素类型
     *
     * @return QColl 新创建的集合对象
     */
    static function createFromArray(array $arr, $type)
    {
        $coll = new QColl($type);
        foreach ($arr as $offset => $item)
        {
            $coll[$offset] = $item;
        }
        return $coll;
    }

    /**
     * 遍历集合中的所有对象，返回包含特定属性值的数组
     *
     * @code php
     * $coll = new QColl('Post');
     * $coll[] = new Post(array('title' => 't1'));
     * $coll[] = new Post(array('title' => 't2'));
     *
     * // 此时 $titles 中包含 t1 和 t2 两个值
     * $titles = $coll->values('title');
     * @endcode
     *
     * @param string $prop_name 要获取集合元素的哪一个属性
     *
     * @return array 包含所有集合元素指定属性值的数组
     */
    function values($prop_name)
    {
        $return = array();
        foreach (array_keys($this->_coll) as $offset)
        {
            if (isset($this->_coll[$offset]->{$prop_name}))
            {
                $return[] = $this->_coll[$offset]->{$prop_name};
            }
        }
        return $return;
    }

    /**
     * 检查指定索引的元素是否存在，实现 ArrayAccess 接口
     *
     * @code php
     * echo isset($coll[1]);
     * @endcode
     *
     * @param mixed $offset
     *
     * @return boolean
     */
    function offsetExists($offset)
    {
        return isset($this->_coll[$offset]);
    }

    /**
     * 返回指定索引的元素，实现 ArrayAccess 接口
     *
     * @code php
     * $item = $coll[1];
     * @endcode
     *
     * @param mixed $offset
     *
     * @return mixed
     */
    function offsetGet($offset)
    {
        if (isset($this->_coll[$offset]))
        {
            return $this->_coll[$offset];
        }
        // LC_MSG: Undefined offset: "%s".
        throw new QException(__('Undefined offset: "%s".', $offset));
    }

    /**
     * 设置指定索引的元素，实现 ArrayAccess 接口
     *
     * @code php
     * $coll[1] = $item;
     * @endcode
     *
     * @param mixed $offset
     * @param mixed $value
     */
    function offsetSet($offset, $value)
    {
        if (is_null($offset))
        {
            $offset = count($this->_coll);
        }
        if (is_array($value))
        {
            foreach (array_keys($value) as $key)
            {
                $this->_checkType($value[$key]);
            }
        }
        else
        {
            $this->_checkType($value);
        }
        $this->_coll[$offset] = $value;
    }

    /**
     * 注销指定索引的元素，实现 ArrayAccess 接口
     *
     * @code php
     * unset($coll[1]);
     * @endcode
     *
     * @param mixed $offset
     */
    function offsetUnset($offset)
    {
        unset($this->_coll[$offset]);
    }

    /**
     * 返回当前位置的元素，实现 Iterator 接口
     *
     * @code php
     * // 遍历集合的每一个元素
     * foreach ($coll as $offset => $item)
     * {
     *     dump($item);
     * }
     * @endcode
     *
     * @return mixed
     */
    function current()
    {
        return current($this->_coll);
    }

    /**
     * 返回遍历时的当前索引，实现 Iterator 接口
     *
     * @return mixed
     */
    function key()
    {
        return key($this->_coll);
    }

    /**
     * 遍历下一个元素，实现 Iterator 接口
     *
     * @return mixed
     */
    function next()
    {
        return next($this->_coll);
    }

    /**
     * 重置遍历索引，返回第一个元素，实现 Iterator 接口
     *
     * @return mixed
     */
    function rewind()
    {
        return reset($this->_coll);
    }

    /**
     * 判断是否是调用了 rewind() 或 next() 之后获得的有效元素，实现 Iterator 接口
     *
     * @return boolean
     */
    function valid()
    {
        return current($this->_coll) !== false;
    }

    /**
     * 返回元素总数，实现 Countable 接口
     *
     * @code php
     * echo count($coll);
     * @endcode
     *
     * @return int
     */
    function count()
    {
        return count($this->_coll);
    }

    /**
     * 追加数组或 QColl 对象的内容到集合中
     *
     * @code php
     * $data = array(
     *     $item1,
     *     $item2,
     *     $item3
     * );
     *
     * $coll->append($data);
     * @endcode
     *
     * QColl::append() 在追加数据时不会保持键名。
     *
     * @param array|QColl $data 要追加的数据
     *
     * @return QColl 返回集合对象本身，实现连贯接口
     */
    function append($data)
    {
        if (is_array($data) || ($data instanceof Iterator))
        {
            foreach ($data as $item)
            {
                $this->offsetSet(null, $item);
            }
        }
        else
        {
            // LC_MSG: Type mismatch. expected "%s", but actual is "%s".
            throw new QException(__('Type mismatch. expected "%s", but actual is "%s".',
                                    'array or Iterator', gettype($data)));
        }

        return $this;
    }

    /**
     * 返回包含所有元素内容的数组
     *
     * @code php
     * dump($coll->toArray());
     * @endcode
     *
     * 该方法要求集合中的元素实现了 toArray() 方法。
     *
     * @param int $recursion 包含多深层次的数据
     *
     * @return array 包含元素内容的数组
     */
    function toArray($recursion = 99)
    {
        $arr = array();
        foreach ($this->_coll as $obj)
        {
            $arr[] = $obj->toArray($recursion);
        }
        return $arr;
    }

    /**
     * 返回包含所有元素内容的 JSON 字符串
     *
     * 该方法要求集合中的元素实现了 toArray() 方法。
     *
     * @param int $recursion 包含多深层次的数据
     *
     * @return string 元素内容的 JSON 字符串
     */
    function toJSON($recursion = 99)
    {
        return json_encode($this->toArray($recursion));
    }

    /**
     * 查找符合指定属性值的元素，没找到返回 NULL
     *
     * @code php
     * // 在 $coll 集合中搜索 title 属性等于 T1 的第一个元素
     * $item = $coll->search('title', 'T1');
     * @endcode
     *
     * @param string $prop_name 要搜索的属性名
     * @param mixed $needle 需要的属性值
     *
     * @return mixed
     */
    function search($prop_name, $needle)
    {
        foreach ($this->_coll as $item)
        {
            if ($item->{$prop_name} == $needle) return $item;
        }
        return null;
    }

    /**
     * 检查值是否符合类型要求
     *
     * @param mixed $value
     */
    protected function _checkType($value)
    {
        if (is_object($value))
        {
            if ($value instanceof $this->_type)
            {
                return;
            }
            $type = get_class($value);
        }
        elseif (gettype($value) == $this->_type)
        {
            return;
        }
        else
        {
            $type = gettype($value);
        }
        // LC_MSG: Type mismatch. expected "%s", but actual is "%s".
        throw new QException(__('Type mismatch. expected "%s", but actual is "%s".', $this->_type, $type));
    }

}

