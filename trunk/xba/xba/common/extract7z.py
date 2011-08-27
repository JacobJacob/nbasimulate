#!/usr/bin/python
# -*- coding: utf-8 -*-
"""7z tool
linux的7z需要安装p7zip-rar
"""

import subprocess
import os

class NotInstall7zError(Exception):pass
class ExtractError(Exception):pass

if os.name == 'nt':
    FileNameTooLongError = WindowsError
    FileNameTooLongCode = (206, 3, 234)
else:
    FileNameTooLongError = OSError
    FileNameTooLongCode = (36,)

_7z_path = None

def _get7z():
    global _7z_path    
    if _7z_path != None:
        return _7z_path
    if os.name == 'nt':
        # 使用绿色版
        _7z_path = os.path.join(os.path.dirname(__file__), '7z', '7z.exe')
    else:
        if os.path.isfile("/usr/bin/7z"):
            _7z_path = "/usr/bin/7z"
    if not _7z_path:
        raise NotInstall7zError("7z is not installed")
    return _7z_path

def extract(filepath, target_folder=None):
    if not filepath or not os.path.isfile(filepath):
        return None
    if isinstance(filepath, unicode):
        filepath = filepath.encode('utf-8')
    if isinstance(target_folder, unicode):
        target_folder = target_folder.encode('utf-8')
    tool = _get7z()
    if not target_folder:
        target_folder = '%s_dir' % filepath
    elif os.path.exists(target_folder) and os.listdir(target_folder):
        raise ExtractError('%s exists and it is not empty')
    cmd = [tool, 'x', '"%s"' % filepath, '-o"%s"' % target_folder, '-y']
#    print ' '.join(cmd)
    p = subprocess.Popen(' '.join(cmd), shell=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    stdout, _ = p.communicate()
    if p.returncode != 0:
        raise ExtractError(stdout.strip())
    return target_folder

def zip(filepath):
    if not filepath or not os.path.isfile(filepath):
        return None
    tool = _get7z()
    cmd = [tool, 'a', '"%s.7z"' % filepath, filepath]
#    print ' '.join(cmd)
    p = subprocess.Popen(' '.join(cmd), shell=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    stdout, _ = p.communicate()
    if p.returncode != 0:
        raise ExtractError(stdout.strip())
    return '%s.7z' % filepath

    
if __name__ == "__main__":
    test_f = os.path.join(os.path.dirname(os.path.abspath(__file__)), '7z', 'extract7z.py.tar.gz')
    print extract(test_f)