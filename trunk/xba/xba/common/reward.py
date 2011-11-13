#!/usr/bin/python
# -*- coding: utf-8 -*-

from xml.dom.minidom import parseString
from xml.dom.minidom import Element

class Money(object):
    
    def __init__(self, money):
        self.money = money

class Score(object):
    
    def __init__(self, score):
        self.score = score
        
class Tool(object):
    
    def __init__(self, category, ticket_category):
        self.category = category
        self.ticket_category = ticket_category
        
class Reputation(object):
    
    def __init__(self, reputation):
        self.reputation = reputation
        
class Wealth(object):
    
    def __init__(self, wealth):
        self.wealth = wealth
        
class Reward(object):
    
    def __init__(self, reward_xml, round_key="round"):
        self.__reward_xml = '<Rewards>%s</Rewards>' % reward_xml
        self.__reward_dict = None
        self.__round_key = round_key
        
    def __parse(self):
        if self.__reward_dict:
            return
        self.__reward_dict = {}
        config_dom = parseString(self.__reward_xml)
        root = config_dom._get_firstChild()
        for node in root.childNodes:
            if not isinstance(node, Element):
                continue
            map = {}
            for child_node in node.childNodes:
                if not isinstance(child_node, Element):
                    continue
                text_node = child_node._get_firstChild()
                map[child_node.tagName.lower()] = int(text_node.data)
            
            round = map[self.__round_key]
            
            info = {}
            if map.get("money"):
                info['money'] = Money(map.get("money"))
            
            if map.get("score"):
                info['score'] = Score(map.get("score"))
                
            if map.get("reputation"):
                info['reputation'] = Reputation(map.get("reputation"))
                
            if map.get("wealth"):
                info['wealth'] = Wealth(map.get("wealth"))
                
            if map.get("tool") and map.get("ticketcategory"):
                info['tool'] = Tool(map.get("tool"), map.get("ticketcategory"))
                
            self.__reward_dict[round] = info
                
    def get_reward(self, round):
        self.__parse()
        return self.__reward_dict.get(round, {})
    
if __name__ == "__main__":

    s = '<Reward><Round>2</Round><Money>1000</Money><Score>1</Score></Reward><Reward><Round>3</Round><Money>' \
                          '2000</Money><Score>2</Score></Reward><Reward><Round>4</Round><Money>5000</Money><Score>3</Score></Reward>' \
                          '<Reward><Round>5</Round><Money>10000</Money><Score>5</Score><Tool>1</Tool><TicketCategory>5</TicketCategory>' \
                          '</Reward><Reward><Round>6</Round><Money>20000</Money><Score>10</Score><Tool>1</Tool>' \
                          '<TicketCategory>5</TicketCategory></Reward><Reward><Round>7</Round><Money>30000</Money><Score>15</Score>' \
                          '<Tool>1</Tool><TicketCategory>5</TicketCategory></Reward><Reward><Round>8</Round><Money>40000</Money>' \
                          '<Score>20</Score><Tool>1</Tool><TicketCategory>5</TicketCategory></Reward><Reward><Round>9</Round>' \
                          '<Money>60000</Money><Score>30</Score><Tool>1</Tool><TicketCategory>5</TicketCategory></Reward><Reward>' \
                          '<Round>100</Round><Money>100000</Money><Score>40</Score><Tool>1</Tool><TicketCategory>5</TicketCategory></Reward>'

    #reward = Reward(s)
    #print reward.get_reward(5)
    ss = "<Reward><Place>1</Place><Wealth>100</Wealth></Reward><Reward><Place>2</Place><Wealth>0</Wealth></Reward><Reward><Place>3</Place><Wealth>0</Wealth></Reward><Reward><Place>4</Place><Wealth>0</Wealth></Reward>"
    reward = Reward(ss, "place")
    print reward.get_reward(1)
        