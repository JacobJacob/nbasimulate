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
require 'timeout'
require 'email'
MAX_DEPTH = 30
tree = Tree.new
#get the page's all link with the give url
def get_links(url)
	agent = WWW::Mechanize.new
    page = agent.get(url)
    begin
	 links = page.links
    rescue
      return []
    end
end
def get_html(url)
   begin
     agent = WWW::Mechanize.new
     page = agent.get(url)
     return page.body
   rescue RuntimeError
     return '';
   end
end
#handle the current page
def handle_curt_page(url,title)
   puts url
   #linkBo= Link.new
   #linkBo.link = url
   #puts title
   #linkBo.title = title
   #puts link.href
   html = get_html(url)
   emails = html.scan(/\w+@[a-zA-Z_-]+?\.[a-zA-Z]{2,3}/)
   if emails == nil or emails.size == 0
      puts 'not email match'
      return 
   end
   emails.each{ |e|
      emailBo = Email.new
      emailBo.email = e
      Session.instance.begin_transaction
      emailBo.save
      Session.instance.end_transaction 
   }
   #Session.instance.begin_transaction
   #linkBo.save
   #Session.instance.end_transaction 
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
            if(link.href == url)
              next
            end
            handle_link(link.href,link.text,next_dep)
         }
      end
    rescue Timeout::Error,RuntimeError,Errno::ETIMEDOUT
      puts 'time out'
    end
end

URL = 'http://www.baidu.com/'
#URL = "http://www.phome.net/doc/ecmsedu/base/EmpireCMS-BaseDoc.zip"
#agent = WWW::Mechanize.new
#page = agent.get(URL)
#puts page.body.class.name
handle_link(URL,"",0)
#html = get_html('http://www.3-ya.com')
#puts html
#regx_b = /^\w+@[a-zA-Z_-]+?\.[a-zA-Z]{2,3}$/
#regx = /\w+@[a-zA-Z_-]+?\.[a-zA-Z]{2,3}/
#emails = '<td align=\"center\">QQ:133917117 email:3yaportlets@gmail.com</td>'.scan(regx)
#puts "emails is :#{emails}"
#if emails == nil or emails.size == 0
#   puts 'not email match'
#end
