<?php
// $Id: group.php 2127 2009-01-21 07:16:22Z dualface $

/**
 * 定义 QForm_Group 类
 *
 * @link http://qeephp.com/
 * @copyright Copyright (c) 2006-2009 Qeeyuan Inc. {@link http://www.qeeyuan.com}
 * @license New BSD License {@link http://qeephp.com/license/}
 * @version $Id: group.php 2127 2009-01-21 07:16:22Z dualface $
 * @package form
 */

/**
 * 类 QForm_Group 是容纳多个元素或群组的集合
 *
 * @author YuLei Liao <liaoyulei@qeeyuan.com>
 * @version $Id: group.php 2127 2009-01-21 07:16:22Z dualface $
 * @package form
 */
class QForm_Group extends QForm_Element_Abstract implements ArrayAccess
{
    /**
     * 聚合的元素
     *
     * @var QColl
     */
    protected $_elements;

    /**
     * 构造函数
     *
     * @param string $id 表单 ID
     * @param array $attrs 属性
     */
    function __construct($id = null, array $attrs = null)
    {
        parent::__construct($id, $attrs);
        $this->_elements = new QColl('QForm_Element_Abstract');
    }

    /**
     * 添加一个元素，并返回该元素对象
     *
     * @code php
     * $form->add(QForm::ELEMENT, 'title', array('_ui' => 'textbox', 'size' => 40));
     * @endcode
     *
     * $type 参数只能是 QForm::ELEMENT 或 QForm::GROUP 两者之一。
     *
     * @param enum $type 要添加的元素类型
     * @param string $id 元素 ID
     * @param array $attrs 元素属性
     *
     * @return QForm_Element_Abstract
     */
    function add($type, $id, array $attrs = null)
    {
        if ($type == QForm::ELEMENT)
        {
            $item = new QForm_Element($id, $attrs);
        }
        elseif ($type == QForm::GROUP)
        {
            $item = new QForm_Group($id, $attrs);
        }
        else
        {
            throw new QForm_Exception(__('Invalid type "%s".', $type));
        }
        $this->_elements[$id] = $item->setGroup($this);
        return $item;
    }

    /**
     * 从配置批量添加元素
     *
     * 具体用法参考开发者手册关于表单的章节。
     *
     * @param array $config
     *
     * @return QForm_Group
     */
    function loadFromConfig(array $config)
    {
        foreach ($config as $id => $attrs)
        {
            if (!is_array($attrs))
            {
                $attrs = array();
            }
            if (isset($attrs['_group']) && $attrs['_group'])
            {
                if (isset($attrs['_elements']))
                {
                    $elements = $attrs['_elements'];
                }
                else
                {
                    $elements = null;
                }
                unset($attrs['_elements']);
                $group = new QForm_Group($id, $attrs);
                if (!empty($elements))
                {
                    $group->loadFromConfig($elements);
                }
                $this->_elements[$id] = $group;
            }
            else
            {
                if (isset($attrs['_filters']))
                {
                    $filters = $attrs['_filters'];
                    unset($attrs['_filters']);
                }
                else
                {
                    $filters = null;
                }
                if (isset($attrs['_validations']))
                {
                    $validations = $attrs['_validations'];
                    unset($attrs['_validations']);
                }
                else
                {
                    $validations = null;
                }

                $element = new QForm_Element($id, $attrs);

                if (!empty($filters))
                {
                    $element->addFilters($filters);
                }
                if (!empty($validations))
                {
                    $element->addValidations($validations);
                }

                $this->_elements[$id] = $element;
            }
        }

        return $this;
    }

    /**
     * 为群组的元素添加验证规则
     *
     * @param mixed $source
     *
     * @return QForm_Group
     */
    function addValidations($source)
    {
        if ($source instanceof QDB_ActiveRecord_Meta)
        {
            $validations = $source->allValidations();
            foreach ($validations as $id => $source)
            {
                if ($this->existsElement($id))
                {
                    $this->element($id)->addValidations($source['rules']);
                }
            }
        }
        elseif (is_array($source))
        {
            foreach ($source as $id => $validations)
            {
                if ($this->existsElement($id))
                {
                    $this->element($id)->addValidations($validations);
                }
            }
        }
        else
        {
            throw new QForm_Exception(__('Typemismatch, expected array, actual is "%s".', gettype($source)));
        }

        return $this;
    }

    /**
     * 返回指定 ID 的子元素
     *
     * @param string $id
     *
     * @return QForm_Element_Abstract
     */
    function element($id)
    {
        if (strpos($id, '/'))
        {
            $arr = explode('/', $id);
            $element = $this;
            foreach ($arr as $id)
            {
                $element = $element->element($id);
            }
            return $element;
        }

        return $this->_elements[$id];
    }

    /**
     * 检查指定的元素是否存在
     *
     * @param string $id
     *
     * @return boolean
     */
    function existsElement($id)
    {
        if (strpos($id, '/'))
        {
            $arr = explode('/', $id);
            $element = $this;
            foreach ($arr as $id)
            {
                if (!$element->existsElement($id)) return false;
                $element = $element->element($id);
            }
            return true;
        }

        return isset($this->_elements[$id]);
    }

    /**
     * 返回包含所有元素的集合
     *
     * @return QColl
     */
    function elements()
    {
        return $this->_elements;
    }

    /**
     * 指示当前元素是一个组
     *
     * @return boolean
     */
    function isGroup()
    {
        return true;
    }

    /**
     * 导入数据并验证，返回验证结果
     *
     * @param array $data
     * @param array $failed
     *
     * @return boolean
     */
    function validate($data, & $failed = null)
    {
        // TODO: 添加对嵌套元素的支持
        if (!is_array($data) && !($data instanceof ArrayAccess))
        {
            throw new QForm_Exception(__('Argument $data typemismatch.'));
        }

        $failed = array();
        $is_valid = true;

        foreach ($this->_elements as $id => $element)
        {
            $value = isset($data[$id]) ? $data[$id] : null;
            $ret = $element->validate($value);
            $is_valid &= $ret;
            if (!$ret) $failed[] = $id;
        }

        return $is_valid;
    }

    /**
     * 导入数据，但不进行过滤和验证
     *
     * @param array $data
     *
     * @return QForm_Group
     */
    function import($data)
    {
        // TODO: 添加对嵌套元素的支持
        if (!is_array($data) && !($data instanceof ArrayAccess))
        {
            throw new QForm_Exception(__('Argument $data typemismatch.'));
        }

        foreach ($this->_elements as $id => $element)
        {
            $element->value = isset($data[$id]) ? $data[$id] : null;
        }

        return $this;
    }

    /**
     * 确认群组所有元素的有效性
     *
     * @return boolean
     */
    function isValid()
    {
        $is_valid = true;
        foreach ($this->elements() as $element)
        {
            $is_valid &= $element->isValid();
        }
        return $is_valid;
    }

    /**
     * 指示群组的数据为无效状态
     *
     * @param mixed $error
     *
     * @return QForm_Group
     */
    function invalidate($error)
    {
        if ($error instanceof QValidator_ValidateFailedException)
        {
            $errors = $error->validate_errors;
        }
        elseif (!is_array($error))
        {
            $keys = Q::normalize($error);
            $errors = array();
            foreach ($keys as $key) $errors[$key] = null;
        }

        foreach ($errors as $id => $msg)
        {
            if ($this->existsElement($id))
            {
                $this->element($id)->invalidate($msg);
            }
        }
        return $this;
    }

    /**
     * 返回包含群组所有元素值的数组
     *
     * @return array
     */
    function values()
    {
        $ret = array();
        foreach ($this->_elements as $id => $element)
        {
            if ($element->isGroup())
            {
                $ret[$id] = $element->values();
            }
            else
            {
                $ret[$id] = $element->value();
            }
        }
        return $ret;
    }

    /**
     * 返回包含群组所有元素未过滤值得数组
     *
     * @return array
     */
    function unfilteredValues()
    {
        $ret = array();
        foreach ($this->_elements as $id => $element)
        {
            if ($element->isGroup())
            {
                $ret[$id] = $element->unfilteredValues();
            }
            else
            {
                $ret[$id] = $element->unfilteredValue();
            }
        }
        return $ret;
    }

    /**
     * ArrayAccess 接口实现：检查指定键名是否存在
     *
     * @param string $id
     *
     * @return boolean
     */
    function offsetExists($id)
    {
        return isset($this->_elements[$id]);
    }

    /**
     * ArrayAccess 接口实现：取得指定键名的元素
     *
     * @param string $id
     *
     * @return QForm_Element_Abstract
     */
    function offsetGet($id)
    {
        return $this->_elements[$id];
    }

    /**
     * ArrayAccess 接口实现：设置指定键名的元素
     *
     * @param string $id
     * @param QForm_Element_Abstract $element
     */
    function offsetSet($id, $element)
    {
        $this->_elements[$id] = $element;
    }

    /**
     * ArrayAccess 接口实现：删除指定键名的元素
     *
     * @param string $id
     */
    function offsetUnset($id)
    {
        unset($this->_elements[$id]);
    }
}

