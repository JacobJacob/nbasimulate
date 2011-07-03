#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import shutil

from datetime import datetime

from xba.config import NGINX_ACCESS_LOG, PathSettings

    
if __name__ == "__main__":
    if os.path.exists(NGINX_ACCESS_LOG) and os.path.getsize(NGINX_ACCESS_LOG) > 1024 * 1024:
        now = datetime.now()
        dist = os.path.join(PathSettings.LOG, "nginx", now.strftime("%Y_%m_%d"), now.strftime("access_%H_%M_%S.log"))
        shutil.move(NGINX_ACCESS_LOG, dist)