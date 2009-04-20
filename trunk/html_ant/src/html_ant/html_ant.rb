# 
# To change this template, choose Tools | Templates
# and open the template in the editor.
require 'parsedate'
require 'rubygems'
require 'mechanize'
require 'open-uri'
require 'hpricot'
require 'iconv'
require 'link'
require 'tree'
MAX_DEPTH = 30
tree = Tree.new
#get the page's all link with the give url
def get_links(url)
	agent = WWW::Mechanize.new
    page = agent.get(url)
	links = page.links
end
#handle the current page
def handle_curt_page(url,title)
   puts url
   linkBo= Link.new
   linkBo.link = url
   puts title
   linkBo.title = title
   #puts link.href
   Session.instance.begin_transaction
   linkBo.save
   Session.instance.end_transaction 
end
#depth handle the link
def handle_link(url,title,curt_dep)
    if(url == nil or url.scan(/^http:\/\//).size == 0 or url.scan(/.([zZ][iI][pP]|[rR][aA][rR])$/).size >  0)
      return 
    end
    next_dep = curt_dep + 1
    begin
      handle_curt_page(url,title)
      links = get_links(url)
      puts "current depth is #{curt_dep}"
      if(curt_dep > MAX_DEPTH)
         return 
      else
         links.each{ |link|
            handle_link(link.href,link.text,next_dep)
         }
      end
    rescue => err
      puts err
    end
end

URL = 'http://www.phome.net/'
#URL = "http://www.phome.net/doc/ecmsedu/base/EmpireCMS-BaseDoc.zip"

handle_link(URL,"",0)
