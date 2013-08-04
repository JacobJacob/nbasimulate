#!/usr/bin/python
# -*- coding: utf-8 -*-

from rrlog.server import filewriter

def main():

    log = filewriter.createLocalLog(
            filePathPattern = "./demo-log-%s.txt", # "pattern" because %s (or %d) is required for the rotate-number
            tsFormat = "%Y-%m-%d %H:%M:%S",
            rotateCount = 3, # rotate over 3 files
            rotateLineMin = 10, #at least 10 lines in each file before rotating
            drop = False,
            )
    
    log("test_%s")
    log("中文")
    try:
        1 / 0
    except:
        import traceback
        log(traceback.format_exc())
    
if __name__ == "__main__":
    main()

