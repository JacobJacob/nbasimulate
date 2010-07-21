@echo off 垃圾清理 
echo 正在清除系统垃圾文件，请稍等...... 
del /f /s /q "%userprofile%\Local Settings\Temporary Internet Files\*.*" 
echo 清理完毕！ 
