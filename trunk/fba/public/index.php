<?php
// $Id$

/**
 * 应用程序启动脚本
 */
global $g_boot_time;
$g_boot_time = microtime(true);

$app_config = require(dirname(__FILE__) . '/../config/boot.php');
require $app_config['QEEPHP_DIR'] . '/library/q.php';
require $app_config['APP_DIR'] . '/myapp.php';

echo MyApp::instance($app_config)->dispatching();

