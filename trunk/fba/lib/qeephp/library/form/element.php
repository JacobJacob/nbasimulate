<?php
// $Id: element.php 2106 2009-01-19 16:09:39Z dualface $

/**
 * 定义 QForm_Element 类
 *
 * @link http://qeephp.com/
 * @copyright Copyright (c) 2006-2009 Qeeyuan Inc. {@link http://www.qeeyuan.com}
 * @license New BSD License {@link http://qeephp.com/license/}
 * @version $Id: element.php 2106 2009-01-19 16:09:39Z dualface $
 * @package form
 */

/**
 * QForm_Element 类封装了表单中的一个值元素
 *
 * @author YuLei Liao <liaoyulei@qeeyuan.com>
 * @version $Id: element.php 2106 2009-01-19 16:09:39Z dualface $
 * @package form
 */
class QForm_Element extends QForm_Element_Abstract
{
    /**
     * 未过滤的值
     *
     * @var mixed
     */
    protected $_unfiltered_value = null;

    /**
     * 过滤器链
     *
     * @var array
     */
    protected $_filters = array();

    /**
     * 验证规则
     *
     * @var array
     */
    protected $_validations = array();

    /**
     * 数据的验证结果
     *
     * @var boolean
     */
    protected $_is_valid = false;

    /**
     * 验证失败的信息
     *
     * @var string
     */
    protected $_error_msg = array();

    /**
     * 该元素所属的群组
     *
     * @var QForm_Group
     */
    protected $_group;

    /**
     * 设置元素的所属的群组，由 QeePHP 内部使用
     *
     * @param QForm_Group $group
     *
     * @return QForm_Element
     */
    function setGroup(QForm_Group $group)
    {
        $this->_group = $group;
        return $this;
    }

    /**
     * 调用该元素所属群组的 add() 方法，以便在连贯接口中连续添加元素
     *
     * @param enum $type
     * @param string $id
     * @param array $attrs
     *
     * @return QForm_Element_Abstract
     */
    function add($type, $id, array $attrs = null)
    {
        if (!is_null($this->_group))
        {
            return $this->_group->add($type, $id, $attrs);
        }
        throw new QForm_Exception(__('Current element not child.'));
    }

    /**
     * 指示该元素是否是一个群组
     *
     * @return boolean
     */
    function isGroup()
    {
        return false;
    }

    /**
     * 添加过滤器
     *
     * 多个过滤器可以使用以“,”分割的字符串来表示：
     *
     * @code php
     * $element->addFilters('trim, strtolower');
     * @endcode
     *
     * 或者以包含多个过滤器名的数组表示：
     *
     * @code php
     * $element->addFilters(array('trim', 'strtolower'));
     * @endcode
     *
     * 如果是需要附加参数的过滤器 ，则必须采用下面的格式：
     *
     * @code php
     * $element->addFilters(array(
     *     array('substr', 0, 5),
     *     'strtolower',
     * ));
     * @endcode
     *
     * @param string|array $filters 要添加的过滤器
     *
     * @return QForm_Element
     */
    function addFilters($filters)
    {
        if (!is_array($filters)) $filters = Q::normalize($filters);

        foreach ($filters as $filter)
        {
            if (!is_array($filter)) $filter = array($filter);
            $this->_filters[] = $filter;
        }
        return $this;
    }

    /**
     * 添加验证规则
     *
     * 每一个验证规则是一个数组，可以采用两种方式添加：
     *
     * @code php
     * $element->addValidations('max_length', 5, '不能超过5个字符');
     * // 或者
     * $element->addValidations(array('max_length', 5, '不能超过5个字符'));
     * @endcode
     *
     * 如果要添加一个 callback 方法作为验证规则，必须这样写：
     *
     * @code php
     * $element->addValidations(array($obj, 'method_name'), $args, 'error_message'));
     * @endcode
     *
     * 如果要一次性添加多个验证规则，需要使用二维数组：
     *
     * @code php
     * $element->addValidations(array(
     *     array('min', 3, '不能小于3'),
     *     array('max', 9, '不能大于9'),
     * ));
     * @endcode
     *
     * 如果要将 ActiveRecord 模型的验证规则添加给元素，可以使用：
     *
     * @code php
     * // 将 Post 模型中与该表单元素同名属性的验证规则添加到表单元素中
     * $element->addValidations(Post::meta());
     * // 或者将指定属性的验证规则添加到表单元素中
     * $element->addValidation(Post::meta(), 'propname');
     * @endcode
     *
     * @param mixed $validations 要添加的验证规则
     *
     * @return QForm_Element
     */
    function addValidations($validations)
    {
        $args = func_get_args();
        if ($validations instanceof QDB_ActiveRecord_Meta)
        {
            if (isset($args[1]))
            {
                $validations = $validations->propValidations($args[1]);
            }
            else
            {
                $validations = $validations->propValidations($this->id);
            }
            foreach ($validations['rules'] as $v)
            {
                $this->_validations[] = $v;
            }
        }
        elseif (!is_array($validations))
        {
            $this->_validations[] = $args;
        }
        else
        {
            // 如果没有提供第二个参数，并且 $validations 的第一个元素是数组，则视为二维数组
            if (!isset($args[1]) && is_array(reset($validations)))
            {
                foreach ($validations as $v)
                {
                    $this->_validations[] = $v;
                }
            }
            else
            {
                // 否则视为一个验证规则
                $this->_validations[] = $args;
            }
        }

        return $this;
    }

    /**
     * 导入数据后进行过滤，并返回验证结果
     *
     * 通常调用 QForm 对象的 validate() 方法一次性导入整个表单的数据。
     *
     * @param mixed $data
     *
     * @return boolean
     */
    function validate($data)
    {
        $this->_unfiltered_value = $data;
        $data = QFilter::filterBatch($data, $this->_filters);
        $this->value = $data;

        if (!empty($this->_validations))
        {
            $failed = null;
            $this->_is_valid = QValidator::validateBatch($data, $this->_validations, QValidator::CHECK_ALL, $failed);
            if (!$this->_is_valid)
            {
                $this->_error_msg = array();
                foreach ($failed as $v)
                {
                    $this->_error_msg[] = array_pop($v);
                }
            }
        }
        else
        {
            $this->_error_msg = array();
            $this->_is_valid = true;
        }

        return $this->_is_valid;
    }

    /**
     * 指示表单元素的值是否有效
     *
     * @return boolean
     */
    function isValid()
    {
        return $this->_is_valid;
    }

    /**
     * 设置一个元素为无效状态，以及错误消息
     *
     * 设置一个元素为无效状态后，整个表单的状态都会无效。
     * 为了能够通过 errorMsg() 取得导致表单元素无效的错误信息，可以指定 $msg 参数。
     *
     * @code php
     * $element->invalidate('order 的值不能小于 0');
     * @endcode
     *
     * @param string|array $msg 错误消息，如果有多个可以用数组
     *
     * @return QForm_Element
     */
    function invalidate($msg = null)
    {
        $this->_is_valid = false;
        if (!is_array($msg)) $msg = array($msg);
        $this->_error_msg = $msg;
        return $this;
    }

    /**
     * 返回验证错误信息
     *
     * @return array
     */
    function errorMsg()
    {
        return $this->_error_msg;
    }

    /**
     * 获得表单元素的值
     *
     * @return mixed
     */
    function value()
    {
        return $this->value;
    }

    /**
     * 返回未过滤的值
     *
     * @return mixed
     */
    function unfilteredValue()
    {
        return $this->_unfiltered_value;
    }
}

