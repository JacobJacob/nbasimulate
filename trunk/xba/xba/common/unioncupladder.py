#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from xba.common import tenjin
from xba.common import stringutil
from xba.common import file_utility
from xba.config import DOMAIN



_result_tpl = None

def escape(s):
    return s

class RenderApple(object):
    
    def _render(self, data, template):
        """渲染
        @param data: 数据
        @param template: 模板
        @return: html
        """
        for k, v in data.items():
            data[k] = stringutil.ensure_utf8(v)

        global _result_tpl, escape, to_str
        _result_tpl = tenjin.Template(tostrfunc='to_str', smarttrim=True)
        _result_tpl.convert(template)
        escape = escape
        to_str = tenjin.helpers.to_str
        xml_data = _result_tpl.render(data)
        xml_data = xml_data.replace('\n', '').replace("\r", "")
        return stringutil.ensure_utf8(xml_data)
    
LADDER_ROUND_HEAD_PAIR_LAST_MATCH  = """<div id="${div_id}" class="CamClub" style="top:${css_top}px;left:${css_left}px;">
            <ul>
                <li style="width:100%;">
                    ${home_name}
                </li>
            </ul>
        </div>"""
    
LADDER_ROUND_HEAD_PAIR_NOT_MATCH = """
               <div id="${div_id}" class="RoundClub" style="top:${css_top}px;left:${css_left}px;">
                        <ul>
                            <li style="width:200px;">${home_name}</li>
                            <br>
                             <li style="width:200px;">${away_name}</li>
                        </ul>
            </div>"""

LADDER_ROUND_HEAD_PAIR = """
          <div id="${div_id}" class="RoundClub" style="top:${css_top}px;left:${css_left}px;">
                <ul>
                    <li style="width:120px;">
                        ${home_name}
                    </li>
                    <li style="width:40px;">${home_win}</li>
                    <li style="width:40px;">
                       
                    </li>
                    <br>
                    <li style="width:120px;">
                        ${away_name}
                    </li>
                    <li style="width:40px;">${away_win}</li>
                    <li style="width:40px;">
                    
                    </li>
                </ul>
<?py  
for match in matchs: 
?>
${match}
<?py #endfor ?>
</div>"""
           
class CupLadderRoundPair(RenderApple):
    
    def __init__(self, union_id_home, union_name_home, union_id_away=None, union_name_away=None, home_win=None, away_win=None, is_last_round=False):
        self.union_id_home = union_id_home
        self.union_name_home = union_name_home
        self.union_id_away = union_id_away
        self.union_name_away = union_name_away
        self.index = -1
        self.round = -1
        self.__home_win = home_win
        self.__away_win = away_win
        self.is_last_round = is_last_round
        self.__matchs = []
        
    def add_match(self, match):
        self.__matchs.append(match)
        
    @property   
    def home_win(self):
        return self.__home_win
        
    @property
    def away_win(self):
        return self.__away_win
    
    @property
    def club_type(self):
        club_type = 3
        if self.category == 6:
            club_type = 5
        return club_type
    
    @property
    def home_name(self):
        return self.union_name_home
    
    @property
    def away_name(self):
        if not self.union_id_away:
            return u"轮空"
        return self.union_name_away
        
    @property
    def div_id(self):
        return "divRound%s_%s_%s" % (self.round, self.union_id_away, self.union_id_home)
        
    @property
    def css_top(self):
        return 97 + (self.index) * 295
    
    @property
    def css_left(self):
        return 8 + (self.round - 1) * 206
        
    def __str__(self):
        data = {"css_top": self.css_top, "css_left": self.css_left, "home_name": self.home_name, "away_name": self.away_name, \
                "div_id": self.div_id, "home_win": self.home_win, "away_win": self.away_win, "matchs": self.__matchs}
        if self.union_id_away and self.union_id_home:
            return self._render(data, LADDER_ROUND_HEAD_PAIR)
        elif self.is_last_round:
            return self._render(data, LADDER_ROUND_HEAD_PAIR_LAST_MATCH)
        else:
            return self._render(data, LADDER_ROUND_HEAD_PAIR_NOT_MATCH)
        
LADDER_ROUND_HEAD_MATCH = """
                <ul>
                    <li style="width:120px;">
                        ${home_name}
                    </li>
                    <li style="width:40px;">${home_point}</li>
                    <li style="width:40px;">
                        ${report}
                    </li>
                    <br>
                    <li style="width:120px;">
                        ${away_name}
                    </li>
                    <li style="width:40px;">${away_point}</li>
                    <li style="width:40px;">
                        ${stat}
                    </li>
                </ul>"""

class CupLadderRoundMatch(RenderApple):
    
    CLUB_LINK_TEMPLATE = """<a href="%sShowClub.aspx?UserID=%s&Type=%s" target="Right">%s</a>"""
    REPORT_TEMPLAGE = """<a href="../../%sRep.aspx?Type=%s&Tag=%s&A=%s&B=%s" target="_blank">战报</a>"""
    STAT_TEMPLAGE = """<a href="../../%sStas.aspx?Type=%s&Tag=%s&A=%s&B=%s" target="_blank">统计</a>"""
    
    def __init__(self, user_id_home, club_name_home, user_id_away, club_name_away, match=None):
        self.category = 7
        self.user_id_home = user_id_home
        self.club_name_home = club_name_home
        self.user_id_away = user_id_away
        self.club_name_away = club_name_away
        self.match = match
        self.index = -1
        self.round = -1
   
    @property
    def report(self):
        if self.match:
            #cup_id = self.match["UnionCupID"]
            asp_prefix = "V"
            return CupLadderRoundMatch.REPORT_TEMPLAGE %  (asp_prefix, self.category, self.match["UnionCupMatchID"], self.match["ClubAID"], self.match["ClubBID"])
    
    @property
    def stat(self):
        if self.match:
            #cup_id = self.match["UnionCupID"]
            asp_prefix = "V"
            return CupLadderRoundMatch.STAT_TEMPLAGE % (asp_prefix, self.category, self.match["UnionCupMatchID"], self.match["ClubAID"], self.match["ClubBID"])
     
    @property   
    def home_point(self):
        if self.match:
            return self.match["ScoreA"]
        
    @property
    def away_point(self):
        if self.match:
            return self.match["ScoreB"]
    
    @property
    def club_type(self):
        club_type = 3
        if self.category == 6:
            club_type = 5
        return club_type
    
    @property
    def home_name(self):
        return CupLadderRoundMatch.CLUB_LINK_TEMPLATE % (DOMAIN, self.user_id_home, self.club_type, "<font color='red'>%s</font>" % self.club_name_home)
    
    @property
    def away_name(self):
        if not self.user_id_away:
            return u"轮空"
        return CupLadderRoundMatch.CLUB_LINK_TEMPLATE % (DOMAIN, self.user_id_away, self.club_type, self.club_name_away)
        
    @property
    def div_id(self):
        return "divRound%s_%s_%s" % (self.round, self.user_id_away, self.user_id_home)
        
    @property
    def css_top(self):
        return 97 + (self.index) * 54
    
    @property
    def css_left(self):
        return 8 + (self.round - 1) * 206
    
    def __str__(self):
        data = {"css_top": self.css_top, "css_left": self.css_left, "home_name": self.home_name, "away_name": self.away_name, \
                "div_id": self.div_id, "home_point": self.home_point, "away_point": self.away_point, "report": self.report, \
                "stat": self.stat}
        return self._render(data, LADDER_ROUND_HEAD_MATCH)

        
LADDER_ROUND_HEAD = """
<div id="${div_id}" class="Round" style="left:${css_left}px;">
    <ul>
        <li style="width:100%;">${title}</li>
    </ul>
</div>
<?py
for round_pair in round_pairs: 
?>
${round_pair}
<?py #endfor ?>
"""

class CupLadderRound(RenderApple):
    
    def __init__(self, round):
        self.__round = round
        self.__is_last_round = False
        self.__round_pairs = []
        self.__index = 0
    
    def add_round_pair(self, round_pair):
        round_pair.index = self.__index
        round_pair.round = self.__round
        if hasattr(round_pair, "is_last_round"):
            self.__is_last_round = getattr(round_pair, "is_last_round")
        self.__index += 1
        self.__round_pairs.append(round_pair)
    
    @property    
    def css_left(self):
        return 8 + (self.__round - 1) * 206
    
    @property
    def title(self):
        if self.__is_last_round:
            return "总冠军"
        else:
            return "第%s轮比赛" % self.__round
        
    def __str__(self):
        data = {"div_id": 1, "css_left": self.css_left, "round_pairs": self.__round_pairs, "title": self.title}
        return self._render(data, LADDER_ROUND_HEAD)
    
CUP_LADDER_TEMPLATE = """
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN""http://www.w3.org/TR/html4/loose.dtd">
<html>
  <head>
      <title>赛程安排</title>
      <LINK href="${css_path}" type="text/css" rel="stylesheet">
      ${css}
  </head>
  <body><!--fcc6a4,fbe2d4,fcf1eb -->
      <div id="divCupName" class="CupName">
          <ul>
              <li style="height:60px;width:50px;padding:5px 0 0 0;">
                  <img src="${logo}"
                      width="40" height="50" border="0">
              </li>
              <li style="height:60px;width:100px;padding:28px 0 0 0;">${name}</li>
          </ul>
      </div>
<?py
for round in rounds: 
?>
${round}
<?py #endfor ?>
  </body>
</html>"""

CATEGORY_3_CSS = """<style type="text/css">ul{ margin:0; list-style:none;}li{ float:left;
    padding:4px 0 0 0;/*上、右、下、左*/ text-align:center; overflow:hidden;
    text-overflow:clip; height:20px;}.CupName{ position:absolute;
    font-weight:bold; font-size:14px; left:0px; padding:0 0 0 20px;}.Round{
    width:200px; height:25px; background-color:#fcc6a4; position:absolute;
    font-weight:bold; top:68px;}.RoundClub{ width:200px; height:200px;
    background-color:#fbe2d4; position:absolute;}.CamClub{ width:200px;
    height:25px; background-color:#fbe2d4; position:absolute;}
    </style>"""
    
CATEGORY_5_CSS = """<style type="text/css">body{background-color:#fcf6df;}ul{ margin:0;
                list-style:none;}li{ float:left; padding:4px 0 0 0;/*上、右、下、左*/
                text-align:center; overflow:hidden; text-overflow:clip;
                height:20px;}.CupName{ position:absolute; font-weight:bold;
                font-size:14px; left:0px; padding:0 0 0 20px;}.Round{ width:200px;
                height:25px; background-color:#fcdb7c; position:absolute;
                font-weight:bold; top:68px;}.RoundClub{ width:200px; height:290px;
                background-color:#faedbc; position:absolute;}.CamClub{ width:200px;
                height:25px; background-color:#faedbc; position:absolute;}
      </style>"""

class UnionCupLadder(RenderApple):
    
    def __init__(self, name, logo, category=5, css_path="../Css/Base.css"):
        self.__name = name
        self.__logo = logo
        self.__rounds = []
        self.__css_path = css_path
        self.__category = category
        
    def add_round_pair(self, round, round_pair):
        """添加某一轮某场对阵"""
        while len(self.__rounds) <= round:
            self.__rounds.append(CupLadderRound(len(self.__rounds) + 1))
        self.__rounds[round].add_round_pair(round_pair)
    
    @property
    def logo(self):
        return "%sImages/Cup/%s" % (DOMAIN, self.__logo)
    
    def render(self):
        data = {"name": stringutil.ensure_utf8(self.__name), "rounds": self.__rounds, "logo": self.logo, "css_path": self.__css_path}
        if self.__category == 5:
            data['css'] = CATEGORY_5_CSS
        else:
            data['css'] = CATEGORY_3_CSS
            
        return self._render(data, CUP_LADDER_TEMPLATE)

    
    def write(self, path):
        file_utility.ensure_dir_exists(os.path.dirname(path))
        s = self.render()
        try:
            s = s.decode("utf8").encode("gbk")
        except:
            s = s.decode("utf8").encode("gbk", "replace")
            
        f = open(path, "wb")
        print path
        try:
            f.write(s)
        finally:
            f.close()
            
if __name__ == "__main__":
    ladder = UnionCupLadder("大师杯")
    
    ladder.write("D:\\i.html")