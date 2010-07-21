'''
Created on 2009-11-11

@author: jackyshi
'''
from dtspider.common.bottle.persistable import Md5s
from dtspider.common import md5mgr

def main():
    
    for i1 in range(10):
        for i2 in range(10):
            for i3 in range(10):
                for i4 in range(10):
                    md5 = Md5s()
                    md5.password = '%s%s%s%s' % (i1, i2, i3, i4)
                    md5.md532 = md5mgr.mkmd5fromstr(md5.password)
                    md5.md516 = md5.md532[8:-8] 
                    md5.persist()
                    for i5 in range(10):
                        md5 = Md5s()
                        md5.password = '%s%s%s%s%s' % (i1, i2, i3, i4, i5)
                        md5.md532 = md5mgr.mkmd5fromstr(md5.password)
                        md5.md516 = md5.md532[8:-8] 
                        md5.persist()
                        for i6 in range(10):
                            md5 = Md5s()
                            md5.password = '%s%s%s%s%s%s' % (i1, i2, i3, i4, i5, i6)
                            md5.md532 = md5mgr.mkmd5fromstr(md5.password)
                            md5.md516 = md5.md532[8:-8] 
                            md5.persist()
                            for i7 in range(10):
                                md5 = Md5s()
                                md5.password = '%s%s%s%s%s%s%s' % (i1, i2, i3, i4, i5, i6, i7)
                                md5.md532 = md5mgr.mkmd5fromstr(md5.password)
                                md5.md516 = md5.md532[8:-8] 
                                md5.persist()
                                for i8 in range(10):
                                    md5 = Md5s()
                                    md5.password = '%s%s%s%s%s%s%s%s' % (i1, i2, i3, i4, i5, i6, i7, i8)
                                    md5.md532 = md5mgr.mkmd5fromstr(md5.password)
                                    md5.md516 = md5.md532[8:-8] 
                                    md5.persist()
                                    for i9 in range(10):
                                        md5 = Md5s()
                                        md5.password = '%s%s%s%s%s%s%s%s%s' % (i1, i2, i3, i4, i5, i6, i7, i8, i9)
                                        md5.md532 = md5mgr.mkmd5fromstr(md5.password)
                                        md5.md516 = md5.md532[8:-8] 
                                        md5.persist()
                                        for i10 in range(10):
                                            md5 = Md5s()
                                            md5.password = '%s%s%s%s%s%s%s%s%s%s' % (i1, i2, i3, i4, i5, i6, i7, i8, i9, i10)
                                            md5.md532 = md5mgr.mkmd5fromstr(md5.password)
                                            md5.md516 = md5.md532[8:-8] 
                                            md5.persist()
    
if __name__ == '__main__':
    main()
