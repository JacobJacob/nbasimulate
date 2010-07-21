from django.shortcuts import render_to_response
from dtspider.business import spider_task

def index(request):
    
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 1))
    spider_tasks, total = spider_task.get_spider_tasks(page, pagesize=10)

    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    return render_to_response("spider/index.html", locals())




