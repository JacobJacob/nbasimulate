#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
Also see: django.core.context_processors
A set of request processors that return dictionaries to be merged into a
template context. Each function takes the request object as its only parameter
and returns a dictionary to add to the context.

These are referenced from the setting TEMPLATE_CONTEXT_PROCESSORS and used by
RequestContext.
"""

from webauth.common import cache
from webauth.business import black_url
from webauth.business import url_source
from webauth.business import url_task
from webauth.business import url_result
from webauth.business import url_third_data


def data_total(request):
    """统计数据的上下文，目前包括url总数, black url总数"""
    cache_time = 600
    # black url total count
    black_url_key = 'black_url_count'
    black_url_total = cache.get(black_url_key)
    if not black_url_total:
        black_url_total = black_url.get_total_count()
        cache.set(black_url_key, black_url_total, cache_time)
        
    # url result total count
    url_check_result_key = 'url_check_result_count'
    url_check_result_total = cache.get(url_check_result_key)
    if not url_check_result_total:
        url_check_result_total = url_result.get_total_count()
        cache.set(url_check_result_key, url_check_result_total, cache_time)
    
    # url total count
    url_source_key = 'url_count'
    url_source_total = cache.get(url_source_key)
    if not url_source_total:
        url_source_total = url_source.get_total_count()
        cache.set(url_source_key, url_source_total, cache_time)
    # url task count
    url_task_key = 'url_task_count'
    url_task_total = cache.get(url_task_key)
    if not url_task_total:
        url_task_total = url_task.get_total_count()
        cache.set(url_task_key, url_task_total, cache_time)
        
    # url task history count
    url_task_history_key = 'url_task_history_count'
    url_task_history_total = cache.get(url_task_history_key)
    if not url_task_history_total:
        url_task_history_total = url_task.get_history_total_count()
        cache.set(url_task_history_key, url_task_history_total, cache_time)
    
    # third data count
    url_third_data_key = 'url_third_data_count'
    url_third_data_total = cache.get(url_third_data_key)
    if not url_third_data_total:
        url_third_data_total = url_third_data.get_total_count()
        cache.set(url_third_data_key, url_third_data_total, cache_time)
        
    
    total_info = {black_url_key: black_url_total,
                  url_source_key: url_source_total,
                  url_task_key: url_task_total,
                  url_third_data_key: url_third_data_total,
                  url_check_result_key: url_check_result_total,
                  url_task_history_key: url_task_history_total,
                  }
    return {'data_total': total_info}