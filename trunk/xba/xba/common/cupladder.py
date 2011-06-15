#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common import tenjin

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
        global _result_tpl, escape, to_str
        _result_tpl = tenjin.Template(tostrfunc='to_str', smarttrim=True)
        _result_tpl.convert(template)
        escape = escape
        to_str = tenjin.helpers.to_str
        
        xml_data = _result_tpl.render(data)
        xml_data = xml_data.replace('\n', '').replace("\r", "")
        return xml_data

LADDER_ROUND_HEAD_MATCH = """
          <div id="%s" class="RoundClub" style="top:97px;left:214px;">
                <ul>
                    <li style="width:120px;">
                        <a href="http://xbam1.xba.dishun.net/ShowClub.aspx?UserID=22854&Type=3" target="Right">木-麒麟大帝</a>
                    </li>
                    <li style="width:40px;">43</li>
                    <li style="width:40px;">
                        <a href="../../SRep.aspx?Type=2&Tag=77672&A=63789&B=189565" target="_blank">战报</a>
                    </li>
                    <br>
                    <li style="width:120px;">
                        <a href="http://xbam1.xba.dishun.net/ShowClub.aspx?UserID=36009&Type=3" target="Right">QT-超越灵魂</a>
                    </li>
                    <li style="width:40px;">32</li>
                    <li style="width:40px;">
                        <a href="../../SStas.aspx?Type=2&Tag=77672&A=63789&B=189565" target="_blank">统计</a>
                    </li>
                </ul>
           </div>"""

class CupLadderRoundMatch(object):
    
    def __init__(self, club_home, club_away, match_id):
        self.__club_home = club_home
        self.__club_away = club_away
        self.__match_id = match_id
        
    @property
    def div_id(self):
        return "divRound1_63789_189565" % self.div_id
        
    def __str__(self):
        return LADDER_ROUND_HEAD_MATCH
        
LADDER_ROUND_HEAD = """
<div id="${div_id}" class="Round" style="left:${css_leff}px;">
    <ul>
        <li style="width:100%;">第%s轮比赛</li>
    </ul>
</div>
<?py
   for match in matchs: 
?>
${match}
<?py #endfor ?>
        """

class CupLadderRound(object):
    
    def __init__(self, round):
        self.__round = round
        self.__matchs = []
    
    def add_match(self, match):
        self.__matchs.append(match)
    
    @property    
    def get_css_left(self):
        return 9
        
    def __str__(self):
        return LADDER_ROUND_HEAD % (self.__round, self.get_css_left, self.__round)
    
CUP_LADDER_TEMPLATE = """
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN""http://www.w3.org/TR/html4/loose.dtd">
<html>
  <head>
      <title>大杯赛赛程安排</title>
      <style type="text/css">ul{ margin:0; list-style:none;}li{ float:left;
          padding:4px 0 0 0;/*上、右、下、左*/ text-align:center; overflow:hidden;
          text-overflow:clip; height:20px;}.CupName{ position:absolute;
          font-weight:bold; font-size:14px; left:0px; padding:0 0 0
          20px;}.Round{ width:200px; height:25px; background-color:#fcc6a4;
          position:absolute; font-weight:bold; top:68px;}.RoundClub{
          width:200px; height:50px; background-color:#fbe2d4;
          position:absolute;}.CamClub{ width:200px; height:25px;
          background-color:#fbe2d4; position:absolute;}</style>
      <LINK href="../../Css/Base.css" type="text/css" rel="stylesheet">
  </head>
  <body><!--fcc6a4,fbe2d4,fcf1eb -->
      <div id="divCupName" class="CupName">
          <ul>
              <li style="height:60px;width:50px;padding:5px 0 0 0;">
                  <img src="http://xbam1.xba.dishun.net//Images/Cup/BigBig.gif"
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

class CupLadder(RenderApple):
    
    def __init__(self, name):
        self.__name = name
        self.__rounds = []
        
    def add_match(self, round, match):
        """添加某一轮某场比赛"""
        while len(self.__rounds) <= round:
            self.__rounds.append(CupLadderRound(len(self.__rounds) + 1))
        self.__rounds[round].add_match(match)
    
    def render(self):
        data = {"name": self.__name, "rounds": self.__rounds}    
        return self._render(data, CUP_LADDER_TEMPLATE)

    
    def write(self, path):
        s = self.render()
        s = s.decode("utf8").encode("gb2312")
        print s
        f = open(path, "wb")
        try:
            f.write(s)
        finally:
            f.close()
        
    
if __name__ == "__main__":
    ladder = CupLadder("大师杯")
    
    from xba.common import cup_util
    
    club_ids = [i for i in range(17)]
    cup_util.create_cupLadder(32, club_ids)
    
    ladder.write("D:\\i.html")