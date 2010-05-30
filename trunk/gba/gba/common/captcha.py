#!/usr/bin/env python
#coding=utf8

import os
import datetime
import glob


from PIL import Image
from PIL import ImageDraw
from PIL import ImageFont

import random
from gba.common import cache
from gba.common import md5mgr, file_utility
from gba.web.settings import CAPTCHA_PATH, CAPTCHA_M


class CustomDefie:
    '''自用常量定义'''
    captcha = 'ckey__'    # 存在客户端验证码的hash_key的key
    captcha_total = 10 #验证码个数 

CAPTCHA_RI = random.Random() 
def get_captcha():
    '''获取验证码
    返回 (hash_key, image_path)'''
    
    captcha_cnt = CustomDefie.captcha_total
    m_captcha_key, fn = '', ''
    
    b_memcache = False
    # 从memcache中获得一个验证码文件
    memecache_captcha = cache.get(CAPTCHA_M)
    if memecache_captcha and len(memecache_captcha) == captcha_cnt:
        captcha_index = CAPTCHA_RI.randint(0, len(memecache_captcha) - 1)
        captcha_i = memecache_captcha[captcha_index]
        m_captcha_key, fn = captcha_i['m_captcha_key'], captcha_i['fn']
        if os.path.exists(os.path.join(CAPTCHA_PATH, fn)):
            b_memcache = True
    if not b_memcache: 
        # 新建验证码文件
        file_ls = glob.glob1(CAPTCHA_PATH, '*.jpg')
        if captcha_cnt > len(file_ls):
            __make_captcha(captcha_cnt - len(file_ls))
            file_ls = glob.glob1(CAPTCHA_PATH, '*.jpg')
        __set_captcha_memcache(file_ls)
        
        # 获取验证码
        captcha_index = CAPTCHA_RI.randint(0, len(file_ls) - 1)
        fn = file_ls[captcha_index]
        hash_v = __get_captcha_hash_by_fn(file_ls[captcha_index])
        m_captcha_key = __get_mcaptchahash_by_captchahash(hash_v)
    
    return m_captcha_key, "/site_media/captcha/%s" % fn

    
def valid_captcha_str(captcha_str, captcha_key):
    '''验证验证码
    @cpacha_str : 验证码
    @cpacha_key : 验证码的hash_key'''

    if captcha_str and captcha_key:
        if isinstance(captcha_str, unicode) : captcha_str = captcha_str.encode('utf8')
        captcha_str = captcha_str.strip().lower()
        if isinstance(captcha_key, unicode) : captcha_key = captcha_key.encode('utf8')
        captcha_key = captcha_key.strip().lower()
        
        look_captcha_hash_val = __get_captcha_hash_by_str(captcha_str)
        captcha_hash_val = cache.get(captcha_key)
        if captcha_hash_val is not None:
            # memcache valid
            return captcha_hash_val == look_captcha_hash_val
        else:
            # local file valid
            return captcha_key == __get_mcaptchahash_by_captchahash(look_captcha_hash_val)

def __set_captcha_memcache(file_ls):
    cache.delete(CAPTCHA_M)
    if file_ls:
        captcha_ls = []    
        for i in file_ls:
            hash_v = __get_captcha_hash_by_fn(i)
            captcha_ls.append({'m_captcha_key' : __get_mcaptchahash_by_captchahash(hash_v),
                'captcha_hash' : hash_v,
                'fn' : os.path.split(i)[1]})
        time_out = 60 * 60 * 24    
        for i in captcha_ls:
            cache.set(i['m_captcha_key'], i['captcha_hash'], time_out)
        cache.set(CAPTCHA_M, captcha_ls, time_out)
        
def __make_captcha(cnt):
    file_utility.ensure_dir_exists(CAPTCHA_PATH)

    for i in range(cnt):
        im = Image.new('RGBA', (90, 26), (50, 50, 50, 50))
        draw = ImageDraw.Draw(im)
        rands = []
        for i in range(4):
            rands.append('%s' % random.randint(0, 9))
        draw.text((2, 0), rands[0], font=ImageFont.truetype("tahomabd.TTF", random.randrange(18, 24)), fill='red')
        draw.text((24, 0), rands[1], font=ImageFont.truetype("tahomabd.TTF", random.randrange(18, 24)), fill='yellow')
        draw.text((43, 0), rands[2], font=ImageFont.truetype("tahomabd.TTF", random.randrange(18, 24)), fill='blue')
        draw.text((64, 0), rands[3], font=ImageFont.truetype("tahomabd.TTF", random.randrange(18, 24)), fill='white')
        
        rand_str = "".join(rands)
        rand_str_hash = __get_captcha_hash_by_str(rand_str)
        
        fpath = os.path.join(CAPTCHA_PATH, '%s.jpg' % rand_str_hash)
        fp = file(fpath, 'wb')
        im.save(fp, "GIF")
        fp.close()
        if (os.stat(fpath).st_size == 0):
            os.remove(fpath)
            
def __get_captcha_str(str_val):
    if isinstance(str_val, unicode):
        str_val = str_val.encode('utf8')
    return md5mgr.mkmd5fromstr(str_val)[:2].capitalize()

def __get_captcha_hash_by_str(rand_str):
    if isinstance(rand_str, unicode):
        rand_str = rand_str.encode('utf8')
    return md5mgr.mkmd5fromstr('%s%s' % (rand_str.lower(), CAPTCHA_M))

def __get_captcha_hash_by_fn(filename):
    if isinstance(filename, unicode):
        filename = filename.encode('utf8')
    return os.path.splitext(filename)[0]

def __get_mcaptchahash_by_captchahash(captcha_hash):
    if isinstance(captcha_hash, unicode):
        captcha_hash = captcha_hash.encode('utf8')
    return md5mgr.mkmd5fromstr('%s%s' % (CAPTCHA_M, captcha_hash.lower()))

def is_captcha(captcha_str, captcha_key):
    b = valid_captcha_str(captcha_str, captcha_key)
    if b: 
        m_captcha_key, fpath = "", ""
    else:
        m_captcha_key, fpath = get_captcha()
    return b, m_captcha_key, fpath

if __name__ == '__main__':
    print get_captcha()
    print valid_captcha_str("6691", "0f457d38866d1a2ecb49c9f3a95a750d")
