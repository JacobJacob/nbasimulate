<?php
// $Id: log.php 2008 2009-01-08 18:49:30Z dualface $

/**
 * 定义 QLog 类
 *
 * @link http://qeephp.com/
 * @copyright Copyright (c) 2006-2009 Qeeyuan Inc. {@link http://www.qeeyuan.com}
 * @license New BSD License {@link http://qeephp.com/license/}
 * @version $Id: log.php 2008 2009-01-08 18:49:30Z dualface $
 * @package core
 */

/**
 * 类 QLog 实现了一个简单的日志记录服务
 *
 * @author YuLei Liao <liaoyulei@qeeyuan.com>
 * @version $Id: log.php 2008 2009-01-08 18:49:30Z dualface $
 * @package core
 */
class QLog
{
	/**
	 * 优先级
	 */
	const EMERG   = 'EMERG';   // Emergency: system is unusable
	const ALERT   = 'ALERT';   // Alert: action must be taken immediately
	const CRIT    = 'CRIT';    // Critical: critical conditions
	const ERR     = 'ERR';     // Error: error conditions
	const WARN    = 'WARN';    // Warning: warning conditions
	const NOTICE  = 'NOTICE';  // Notice: normal but significant condition
	const INFO    = 'INFO';    // Informational: informational messages
	const DEBUG   = 'DEBUG';   // Debug: debug messages

	/**
	 * 日期格式
	 *
	 * @var string
	 */
	protected $_date_format = 'Y-m-d H:i:s';

    /**
     * 要记录的日志优先级
     *
     * @var array
     */
    protected $_priorities = array(
		self::EMERG  => true,
		self::ALERT  => true,
		self::CRIT   => true,
		self::ERR    => true,
		self::WARN   => true,
		self::NOTICE => true,
		self::INFO   => true,
		self::DEBUG  => true,
    );

	/**
	 * 保存运行期间的日志
	 *
	 * @var array
	 */
	protected $_log = array();

	/**
	 * 已缓存日志内容的大小
	 *
	 * @var int
	 */
	protected $_cached_size = 0;

	/**
	 * 日志缓存块大小
	 *
	 * @var int
	 */
	protected $_cache_chunk_size = 65536;

    /**
     * 日志文件名
     *
     * @var string
     */
    protected $_filename;

    /**
     * 日志对象是否已经做好写入准备
     *
     * @var boolean
     */
    protected $_writeable = false;

	/**
	 * 析构函数
	 */
	function __destruct()
	{
		$this->flush();
	}

	/**
	 * 追加日志到日志缓存
	 *
	 * @param string $msg
	 * @param int $type
	 */
	static function log($msg, $type = self::DEBUG)
	{
		static $instance;

        if (is_null($instance))
        {
			$instance = Q::singleton('QLog');
		}
		/* @var $instance QLog */
		$instance->append($msg, $type);
    }

	/**
	 * 追加日志到日志缓存
	 *
	 * @param string $msg
	 * @param int $type
	 */
	function append($msg, $type = self::DEBUG)
	{
		if (!isset($this->_priorities[$type]))
		{
			return;
		}

        $this->_log[] = array(time(), $msg, $type);
        $this->_cached_size += strlen($msg);

        if ($this->_writeable && $this->_cached_size >= $this->_cache_chunk_size)
        {
            $this->flush();
        }
    }

    /**
     * 将缓存的日志信息写入实际存储，并清空缓存
     */
    function flush()
    {
        $log_writer_dir = Q::ini('log_writer_dir');
        if (empty($this->_log) || empty($log_writer_dir))
        {
            return;
        }

        if (!$this->_writeable)
        {
        	$keys = Q::normalize(Q::ini('log_priorities'));
        	$arr = array();
        	foreach ($keys as $key)
        	{
        		if (!isset($this->_priorities[$key]))
        		{
        			continue;
        		}
        		$arr[$key] = true;
        	}
        	$this->_priorities = $arr;

	        $dir = realpath(Q::ini('log_writer_dir'));
	        if (empty($dir))
	        {
	            $dir = realpath(Q::ini('runtime_cache_dir'));
	            if (empty($dir))
	            {
	                // LC_MSG: 指定的日志文件保存目录不存在 "%s".
	                throw new QLog_Exception(__('指定的日志文件保存目录不存在 "%s".', Q::ini('log_writer_dir')));
	            }
	        }

	        $filename = Q::ini('log_writer_filename');
	        $this->_filename = rtrim($dir, '/\\') . DS . $filename;
	        $chunk_size = intval(Q::ini('log_cache_chunk_size'));
	        if ($chunk_size < 1)
	        {
	        	$chunk_size = 64;
	        }
	        $this->_cache_chunk_size = $chunk_size * 1024;
	        $this->_writeable = true;

	        #IFDEF DEBUG
	        $this->append('QLog switch to wriable mode.', self::DEBUG);
	        #ENDIF
        }

        $string = '';
        foreach ($this->_log as $offset => $item)
        {
            list($time, $msg, $type) = $item;
            unset($this->_log[$offset]);
            // 过滤掉不需要的日志条目
            if (!isset($this->_priorities[$type]))
            {
            	continue;
            }

            $string .= date('c', $time) . " {$type}: {$msg}\n";
        }

        if ($string)
        {
            $fp = fopen($this->_filename, 'a');
            if ($fp && flock($fp, LOCK_EX))
            {
                fwrite($fp, $string);
                flock($fp, LOCK_UN);
                fclose($fp);
            }
        }

        $this->_log = array();
        $this->_cached_size = 0;
    }
}

