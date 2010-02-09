#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from xml.dom.minidom import parse, parseString
from xml.dom.minidom import Element

from gba.config import PathSettings
from gba.entity import FreePlayer

class AttributeConfig(object):
    '''格式:{'shoot': {'grade-c': [xx, xx xx, xx, xx, xx, xx, xx, xx]}}'''
    
    _LOADED = False
    _DATA = {}
    
    
    @classmethod
    def get_attribute_config(cls, attribute, location, level=1):
        if level > 9 or level < 1:
            return None
        index = level - 1
        location = 'grade-%s' % location.lower() 
        if not cls._LOADED:
            cls._load()
        if attribute in cls._DATA:
            if location in cls._DATA[attribute]:
                return cls._DATA[attribute][location][index]
        return None

    @classmethod
    def _load(cls):
        config_dom = parse(os.path.join(PathSettings.PROJECT_FOLDER, 'config', 'attribute-config.xml'))
        root = config_dom._get_firstChild()
        for node in root.childNodes:
            if not isinstance(node, Element):
                continue
            name = node.getAttribute('name')
            child_map = {}
            for child_node in node.childNodes:
                if not isinstance(child_node, Element):
                    continue
                child_name = child_node.tagName
                text_node = child_node._get_firstChild()
                attributes = text_node.data.split(';')
                child_map[child_name] = attributes
            cls._DATA[name] = child_map
                
class PlayerCreator(object):
    
    
    def run(self):
        
        player = FreePlayer()
        player.no = 'free_player:2'
        player.persist()





if __name__ == '__main__':
    creator = PlayerCreator()
    creator.run()
    #print AttributeConfig.get_attribute_config('shooting', 'c', 1)
    
    