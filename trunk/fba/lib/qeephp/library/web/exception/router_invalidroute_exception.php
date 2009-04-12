<?php
// $Id: router_invalidroute_exception.php 2010 2009-01-08 18:56:36Z dualface $

/**
 * 定义 QRouter_InvalidRouteException 异常
 *
 * @link http://qeephp.com/
 * @copyright Copyright (c) 2006-2009 Qeeyuan Inc. {@link http://www.qeeyuan.com}
 * @license New BSD License {@link http://qeephp.com/license/}
 * @version $Id: router_invalidroute_exception.php 2010 2009-01-08 18:56:36Z dualface $
 * @package exception
 */

/**
 * QRouter_InvalidRouteException 异常指示无效的路由规则
 *
 * @author YuLei Liao <liaoyulei@qeeyuan.com>
 * @version $Id: router_invalidroute_exception.php 2010 2009-01-08 18:56:36Z dualface $
 * @package exception
 */
class QRouter_InvalidRouteException extends QException
{
    public $route_name;
    public $rule;

    function __construct($route_name, $rule)
    {
        $this->route_name = $route_name;
        $this->rule = $rule;
        parent::__construct(__('Invalid route "%s".', $route_name));
    }
}

