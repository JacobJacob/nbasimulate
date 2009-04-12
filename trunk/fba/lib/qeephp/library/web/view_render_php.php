<?php
// $Id: view_render_php.php 2105 2009-01-19 14:05:48Z dualface $

/**
 * 定义 QView_Render_PHP 类
 *
 * @link http://qeephp.com/
 * @copyright Copyright (c) 2006-2009 Qeeyuan Inc. {@link http://www.qeeyuan.com}
 * @license New BSD License {@link http://qeephp.com/license/}
 * @version $Id: view_render_php.php 2105 2009-01-19 14:05:48Z dualface $
 * @package mvc
 */

/**
 * QView_Render_PHP 类实现了视图架构的基础
 *
 * @author YuLei Liao <liaoyulei@qeeyuan.com>
 * @version $Id: view_render_php.php 2105 2009-01-19 14:05:48Z dualface $
 * @package mvc
 */
class QView_Render_PHP
{
    /**
     * 视图文件所在目录
     *
     * @var string
     */
    public $view_dir;

    /**
     * 要输出的头信息
     *
     * @var array
     */
    public $headers;

    /**
     * 视图文件的扩展名
     *
     * @var string
     */
    public $file_extname = 'php';

    /**
     * 模板变量
     *
     * @var array
     */
    protected $_vars;

    /**
     * 视图
     *
     * @var string
     */
    protected $_viewname;

    /**
     * 要使用的布局视图
     *
     * @var string
     */
    protected $_view_layouts;

    protected $_parser;

    /**
     * 构造函数
     *
     * @param array $config
     */
    function __construct(array $config = null)
    {
        if (is_array($config))
        {
            foreach ($config as $key => $value)
            {
                $this->{$key} = $value;
            }
        }

        $this->cleanVars();
    }

    /**
     * 设置视图名称
     *
     * @param string $viewname
     *
     * @return QView_Render_PHP
     */
    function setViewname($viewname)
    {
        $this->_viewname = $viewname;
        return $this;
    }

    /**
     * 指定模板变量
     *
     * @param string|array $key
     * @param mixed $data
     *
     * @return QView_Render_PHP
     */
    function assign($key, $data = null)
    {
        if (is_array($key))
        {
            $this->_vars = array_merge($this->_vars, $key);
        }
        else
        {
            $this->_vars[$key] = $data;
        }
        return $this;
    }

	/**
     * 清除所有模板变量
     *
     * @return QView_Render_PHP
	 */
	function cleanVars()
    {
        $context = QContext::instance();
        $this->_vars = array(
            '_ctx'          => $context,
            '_BASE_DIR'     => $context->baseDir(),
            '_BASE_URI'     => $context->baseUri(),
            '_REQUEST_URI'  => $context->requestUri(),
        );
        return $this;
    }

    /**
     * 渲染视图
     *
     * @param string $viewname
     * @param array $vars
     * @param array $config
     */
    function display($viewname = null, array $vars = null, array $config = null)
    {
        if (empty($viewname))
        {
            $viewname = $this->_viewname;
        }
        echo $this->fetch($viewname, $vars, $config);
    }

    /**
     * 执行
     */
    function execute()
    {
        $this->display($this->_viewname);
    }

    /**
     * 渲染视图并返回渲染结果
     *
     * @param string $viewname
     * @param array $vars
     * @param array $config
     *
     * @return string
     */
    function fetch($viewname, array $vars = null, array $config = null)
    {
        $this->_before_render();
        $view_dir = isset($config['view_dir']) ? $config['view_dir'] : $this->view_dir;
        $extname = isset($config['file_extname']) ? $config['file_extname'] : $this->file_extname;
        $filename = "{$view_dir}/{$viewname}.{$extname}";

        if (file_exists($filename))
        {
            if (!is_array($vars))
            {
                $vars = $this->_vars;
            }
            if (is_null($this->_parser))
            {
                $this->_parser = new QView_Render_PHP_Parser($view_dir);
            }
            $output = $this->_parser->vars($vars)->parse($filename);
        }
        else
        {
            $output = '';
        }

        $this->_after_render($output);
        return $output;
    }

    /**
     * 渲染之前调用
     *
     * 继承类可以覆盖此方法。
     */
    protected function _before_render()
    {
    }

    /**
     * 渲染之后调用
     *
     * 继承类可以覆盖此方法。
     *
     * @param string $output
     */
    protected function _after_render(& $output)
    {
    }

}

/**
 * QView_Render_PHP_Parser 类实现了视图的分析
 *
 * @author YuLei Liao <liaoyulei@qeeyuan.com>
 * @version $Id: view_render_php.php 2105 2009-01-19 14:05:48Z dualface $
 * @package mvc
 */
class QView_Render_PHP_Parser
{
    protected $_extname;
    protected $_stacks;
    protected $_curr_stack;
    protected $_blocks_stack;
    protected $_block_suffix;
    protected $_vars = array();
    protected $_view_dir;

    /**
     * 构造函数
     */
    function __construct($view_dir, array $vars = array())
    {
        $this->_view_dir = $view_dir;
        $this->_vars = $vars;
    }

    /**
     * 设置或返回分析器已经指定的变量
     *
     * @param array $vars
     *
     * @return array|QView_Render_PHP_Parser
     */
    function vars(array $vars = null)
    {
        if (!is_null($vars))
        {
            $this->_vars = $vars;
            return $this;
        }
        return $this->_vars;
    }

    /**
     * 返回分析器使用的视图文件的扩展名
     *
     * @return string
     */
    function extname()
    {
        return $this->_extname;
    }

    /**
     * 分析一个视图文件并返回结果
     *
     * @string $filename
     *
     * @return string
     */
    function parse($filename)
    {
        $this->_extname = pathinfo($filename, PATHINFO_EXTENSION);
        $this->_stacks = array();
        $this->_blocks_stack = array();
        $this->_block_suffix = mt_rand();

        $this->___includeFile($filename);
        $contents = '';
        $max = count($this->_stacks) - 1;
        for ($i = $max; $i >= 0; $i--)
        {
            $stack = $this->_stacks[$i];
            if ($i > 0)
            {
                $prev_stack = $this->_stacks[$i - 1];
            }
            else
            {
                $prev_stack = null;
            }

            $search = array();
            $replace = array();
            foreach ($stack['blocks'] as $block_name => $block)
            {
                $b = strtoupper($block_name);
                $search[] = "%BLOCK_{$b}_{$this->_block_suffix}%";
                if (isset($prev_stack['blocks'][$block_name]))
                {
                    $replace[] = $prev_stack['blocks'][$block_name];
                    $this->_stacks[$i - 1]['blocks'][$block_name] = '';
                }
                else
                {
                    $replace[] = $block;
                }
            }
            unset($prev_stack);

            foreach ((array)$stack['vars'] as $var => $value)
            {
                $search[] = $var;
                $replace[] = $value;
            }

            $contents .= str_replace($search, $replace, $stack['contents']);
            unset($this->_stacks[$i]);
            unset($stack);
            unset($search);
            unset($replace);
        }
        unset($this->_stacks);
        unset($this->_blocks_stack);

        return $contents;
    }

    /**
     * 载入一个视图文件
     *
     * @access private
     */
    protected function ___includeFile($___filename, $___vars = null)
    {
        $this->_current = count($this->_stacks);
        $this->_stacks[$this->_current] = array(
            'blocks'    => array(),
            'filename'  => $___filename,
            'vars'      => $___vars,
            'contents'  => '',
        );

        ob_start();
        extract($this->_vars);
        if (is_array($___vars))
        {
            extract($___vars);
        }
        include $___filename;
        $contents = ob_get_clean();

        $this->_stacks[$this->_current]['contents'] = $contents;
        $this->_current--;
    }

    /**
     * 视图的继承
     *
     * @param string $tplname
     * @param array $vars
     *
     * @access public
     */
    protected function _extends($tplname, array $vars = null)
    {
        $this->___includeFile("{$this->_view_dir}/{$tplname}.{$this->_extname}", $vars);
    }

    /**
     * 开始定义一个区块
     *
     * @param string $block_name
     *
     * @access public
     */
    protected function _block($block_name)
    {
        array_push($this->_blocks_stack, $block_name);
        ob_start();
    }

    /**
     * 结束一个区块
     *
     * @access public
     */
    protected function _endblock()
    {
        $content = ob_get_clean();
        $block_name = array_pop($this->_blocks_stack);
        $this->_stacks[$this->_current]['blocks'][$block_name] = $content;
        $block_name = strtoupper($block_name);
        echo "%BLOCK_{$block_name}_{$this->_block_suffix}%";
    }

    /**
     * 构造一个控件
     *
     * @param string $control_type
     * @param string $id
     * @param array $args
     *
     * @access public
     */
    protected function _control($control_type, $id = null, array $args = array())
    {
        Q::control($control_type, $id, $args)->display($this);
    }

    /**
     * 载入一个视图片段
     *
     * @param string $element_name
     * @param array $___vars
     *
     * @access public
     */
    protected function _element($element_name, array $___vars = null)
    {
        $___filename = "{$this->_view_dir}/_elements/{$element_name}_element.{$this->_extname}";
        extract($this->_vars);
        if (is_array($___vars))
        {
            extract($___vars);
        }

        include $___filename;
    }
}

