
import traceback

from dtspider.client.base import BaseClient
from dtspider.common.spider import Spider
from dtspider.common.constants import ClientType

class ZhuhaiPriceMonitor(BaseClient):
    
    def __init__(self):
        super(ZhuhaiPriceMonitor, self).__init__('t')
        self._start_url = 'http://esf.zh.soufun.com/000%s0___0_0_0_0_0_0_1_0_0_0_0_0/'
        self._spider = Spider()
    
    def run(self):
        
        print dir(ClientType)
        return
        for i in range(1, 100):
            url = self._start_url % i
            print url
            infos = self._get_infos(url)
            
    def _get_infos(self, url):
        while True:
            try:
                content = self._spider.read(url)
                print content
                return 
            except:
                self.current_info = traceback.format_exc()
            self._sleep()    
        
def main():
    monitor = ZhuhaiPriceMonitor()
    monitor.main()
        
if __name__ == '__main__':
    main()