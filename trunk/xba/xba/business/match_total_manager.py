#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.model import MatchTotal
from xba.common import json

def view_total_with_path(file_path):
    f = open(file_path, "rb")
    try:
        content = f.read()
    finally:
        f.close()
    
    return view_total(content)

def view_total_with_match_id(type_id, match_id):
    match_total = MatchTotal.load(match_id=match_id)
    if match_total:
        data = match_total.data
        return view_total(data)
    return None

def get_report_info(report_url):
    if not report_url:
        return 0, 0
    m = {}
    report_url = report_url[report_url.find("?")+1:]
    datas = report_url.split("&")

    for data in datas:
        ds = data.split("=")
        if len(ds) == 2:
            m[ds[0]] = ds[1]

    return m.get("Type", 0), m.get("Tag", 0)
    
    
def view_total(data):
    info = json.loads(data)
    
    datas = {}
    
    arrange_info = info["arrangeInfo"]
    arrange_desc_list = get_arrange_list(arrange_info)
    datas["arrange_desc_list"] = arrange_desc_list

    homeAblityMap = info["homeAblityMap"]
    home_ability_list, _, _ = get_team_ability(homeAblityMap)
    datas["home_ability_list"] = home_ability_list
              
    awayAblityMap = info["awayAblityMap"]
    away_ability_list, _, _ = get_team_ability(awayAblityMap)
    datas["away_ability_list"] = away_ability_list

    homeActionInfoList = info.get("homeActionInfoList")
    if homeActionInfoList:
        home_action_desc_list = get_action_list(homeActionInfoList)
        datas["home_action_desc_list"] = home_action_desc_list
            
    awayActionInfoList = info.get("awayActionInfoList")
    if awayActionInfoList:
        away_action_desc_list = get_action_list(awayActionInfoList)
        datas["away_action_desc_list"] = away_action_desc_list
        
    return datas

def get_arrange_list(arrange_info):
    arrange_desc_list = []
    for i in range(6):
        key = str(i)
        if key in arrange_info:
            home_add = int(arrange_info[key]["homeArrangeAdd"])
            if home_add > 0:
                home_add = "+%s" % home_add
            away_add = int(arrange_info[key]["awayArrangeAdd"])
            if away_add > 0:
                away_add = "+%s" % away_add
            s = u"第%s节.主队战术攻克值[%s%%],客队战术攻克值[%s%%]" % (key, home_add, away_add)
            arrange_desc_list.append(s)

    return arrange_desc_list

def get_team_ability(ablityMap):
    total_off, total_def = 0, 0
    ability_list = []
    for i in range(6):
        key = str(i)
        if key in ablityMap:
            ability_list.append("-----------------------------第%s节-----------------------------" % i)
            datas = ablityMap[key]
            for data in datas:
                off_ability = data["OffAbility"]
                def_ability = data["DefAbility"]
                total_off += off_ability
                total_def += def_ability
                s = u"名字[%s], 位置[%s], 进攻能力值[%s], 防守能力值[%s]" % (get_name(data["Name"]), data["Pos"], off_ability, def_ability)
                ability_list.append(s)
    ability_list.append(u"<p style='color:#EE4000;'>总进攻值[%s], 总防守值[%s]</p>" % (total_off, total_def)) 
    return ability_list, total_off, total_def
    
def get_action_list(action_list):
    action_desc_list = []
    total_rate_all = 0
    success_times = 0
    times = 0
    for action_info in action_list:
        action_desc, total_rate, success = get_action_info(action_info)
        total_rate_all += total_rate
        times += 1
        if success:
            success_times += 1
        action_desc_list.append(action_desc)
    s = u"<p style='color:#EE4000;'>球队总进攻次数[%s], 球队平均综合系数[%0.3f], 球队实际发挥[%0.3f]</p>" % (times, total_rate_all / times, float(success_times) / times)
    action_desc_list.append(s)
    
    return action_desc_list

def get_name(name):
    if name:
        return u"<span style='font-weight:bold'>%s</span>" % name.replace("&lt;u&gt;", "").replace("&lt;/u&gt;", "")

def get_action_info(info):
    playerValueCheckRnd = info["playerValueCheckRnd"]
    offCheckValue = info["offCheckValue"]
    defCheckValue = info["defCheckValue"]
    teamValueCheckRnd = info["teamValueCheckRnd"]
    currentOffCheckValue = info["currentOffCheckValue"]
    name = info["name"]
    
    attack_team_rate = (float)(offCheckValue) / (offCheckValue + defCheckValue)
    attack_player_rate = (float)(currentOffCheckValue) / (currentOffCheckValue + defCheckValue)
    total_rate = attack_team_rate * attack_player_rate
    
    if offCheckValue > teamValueCheckRnd and currentOffCheckValue > playerValueCheckRnd:
        success = True
    else:
        success = False
    
    action_desc = u"进攻球员[%s], 本次进攻球队能力系数[%0.3f], 球员能力系数[%0.3f], 综合系数[%0.3f], 进攻结果[%s]" % \
             (get_name(name), attack_team_rate, attack_player_rate, total_rate, u"<span style='color:#006400;'>成功</span>" if success else u"<span style='color:#EE3B3B;'>失败</span>")
    return action_desc, total_rate, success
        
if __name__ == "__main__":
    view_total_with_match_id(2, 324308)
    #view_total_with_path("E:\\xba_log\\match_total\\99_1677379.json")
    #print get_report_info("http://n1.113388.net/VStas.aspx?Type=1&Tag=1677379&A=2&B=38")
