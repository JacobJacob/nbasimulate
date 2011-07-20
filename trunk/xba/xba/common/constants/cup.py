#!/usr/bin/python
# -*- coding: utf-8 -*-

from constant_base import ConstantBase

class CupCategory(ConstantBase):
    """街球杯赛类型"""
    CUP_NEW = 21 #新人杯赛
    CUP_SMALL = 22 #小杯赛
    CUP_BIG = 23 #大杯赛
    
"""街球杯赛类型"""
CupCategoryMap = {
    CupCategory.CUP_NEW: '新人杯赛',
    CupCategory.CUP_SMALL: '小杯赛',
    CupCategory.CUP_BIG: '大杯赛',
}

"""街球杯赛小LOGO"""
CupCategorySmallLogoMap = {
    CupCategory.CUP_NEW: 'RookieSmall.gif',
    CupCategory.CUP_SMALL: 'SmallSmall.gif',
    CupCategory.CUP_BIG: 'BigSmall.gif',
}

"""街球杯赛大LOGO"""
CupCategoryBigLogoMap = {
    CupCategory.CUP_NEW: 'NewMember.gif',
    CupCategory.CUP_SMALL: 'SmallBig.gif',
    CupCategory.CUP_BIG: 'BigBig.gif',
}

"""街球杯赛大Requirement"""
CupCategoryRequirementMap = {
    CupCategory.CUP_NEW: '<Table>BTP_Account</Table><Condition>Levels = %s</Condition>',
    CupCategory.CUP_SMALL: '<Table>BTP_Account</Table><Condition>Levels = %s</Condition>',
    CupCategory.CUP_BIG: '<Table>BTP_Account</Table><Condition>Levels = %s</Condition>',
}

"""街球杯赛Cost Money"""
CupCategoryMoneyCostMap = {
    CupCategory.CUP_NEW: 0,
    CupCategory.CUP_SMALL: 400,
    CupCategory.CUP_BIG: 0,
}

"""街球杯赛创建数量"""
CupCategoryLevelCountMap = {
    CupCategory.CUP_NEW: {
      1: 2,
      2: 2,
      3: 2,
      4: 2,
      5: 2,
      6: 2,
      7: 2,
      8: 2,
      9: 2,
      10: 2,               
    },
    CupCategory.CUP_SMALL: {
      1: 2,
      2: 2,
      3: 2,
      4: 2,
      5: 2,
      6: 2,
      7: 2,
      8: 2,
      9: 2,
      10: 2,
    },
    CupCategory.CUP_BIG: {
      1: 2,
      2: 2,
      3: 2,
      4: 2,
      5: 2,
      6: 2,
      7: 2,
      8: 2,
      9: 2,
      10: 2,
    },
}



"""街球杯赛Capacity"""
CupCategoryCapacityMap = {
    CupCategory.CUP_NEW: 32,
    CupCategory.CUP_SMALL: 128,
    CupCategory.CUP_BIG: 512,
}

"""街球杯赛TicketCategory"""
CupCategoryTicketCategoryMap = {
    CupCategory.CUP_NEW: 6,
}

"""街球杯赛说明"""
CupCategoryDescMap = {
    CupCategory.CUP_NEW: '杯赛介绍：<br>六轮挑战，三万大奖！这里是新球队成长的天堂！<br>杯赛奖励：<br>第一轮：奖金1000。<br>第二轮：' \
                           '奖金5000。<br>第三轮：奖金10000。<br>第四轮：奖金15000。<br>第五轮：奖金20000。<br>第六轮：奖金30000。<br><br>' \
                           '报名截止时间：%s<br>赛程安排时间：%s<br>需持新人杯邀请函报名！<br>',
    CupCategory.CUP_SMALL: '杯赛介绍：<br>胜利让你变成强者！<br><br>杯赛奖励：<br>第二轮：积分1个，奖金1000。<br>第三轮：积分2个，' \
                            '奖金5000。<br>第四轮：积分3个，奖金7000。<br>第五轮：积分5个，奖金10000，大师杯邀请函。<br>第六轮：积分7个，' \
                            '奖金15000，大师杯邀请函。<br>第七轮：积分10个，奖金20000，大师杯邀请函。<br>第八轮：积分15个，奖金30000，' \
                            '大师杯邀请函。<br><br>报名截止时间：%s<br>赛程安排时间：%s<br>',
    CupCategory.CUP_BIG: '杯赛介绍：<br>每个赛季街球队的盛会。<br>你将面对十轮挑战！你将有机会获得10万大奖！<br>杯赛奖励：<br>第二轮：' \
                          '积分1个，奖金1000。<br>第三轮：积分2个，奖金2000。<br>第四轮：积分3个，奖金5000。<br>第五轮：积分5个，' \
                          '奖金10000，大师杯邀请函。<br>第六轮：积分10个，奖金20000，大师杯邀请函。<br>第七轮：积分15个，奖金30000，' \
                          '大师杯邀请函。<br>第八轮：积分20个，奖金40000，大师杯邀请函。<br>第九轮：积分30个，奖金60000，大师杯邀请函。' \
                          '<br>第十轮：积分40个，奖金100000，大师杯邀请函。<br><br>报名截止时间：%s<br>' \
                          '赛程安排时间：%s<br>',
}
"""街球杯赛Reward"""
CupCategoryRewardMap = {
    CupCategory.CUP_NEW: '<Reward><Round>1</Round><Money>1000</Money></Reward><Reward><Round>2</Round><Money>5000</Money>' \
                          '</Reward><Reward><Round>3</Round><Money>10000</Money></Reward><Reward><Round>4</Round>' \
                          '<Money>15000</Money></Reward><Reward><Round>5</Round><Money>20000</Money></Reward><Reward>' \
                          '<Round>100</Round><Money>30000</Money></Reward>',
    CupCategory.CUP_SMALL: '<Reward><Round>2</Round><Money>1000</Money><Score>1</Score></Reward><Reward><Round>3</Round>' \
                            '<Money>5000</Money><Score>2</Score></Reward><Reward><Round>4</Round><Money>7000</Money><Score>3</Score>' \
                            '</Reward><Reward><Round>5</Round><Money>10000</Money><Score>5</Score><Tool>1</Tool>' \
                            '<TicketCategory>5</TicketCategory></Reward><Reward><Round>6</Round><Money>15000</Money>' \
                            '<Score>7</Score><Tool>1</Tool><TicketCategory>5</TicketCategory></Reward><Reward><Round>7</Round>' \
                            '<Money>20000</Money><Score>10</Score><Tool>1</Tool><TicketCategory>5</TicketCategory></Reward>' \
                            '<Reward><Round>100</Round><Money>30000</Money><Score>15</Score><Tool>1</Tool><TicketCategory>5</TicketCategory>' \
                            '</Reward>',
    CupCategory.CUP_BIG: '<Reward><Round>2</Round><Money>1000</Money><Score>1</Score></Reward><Reward><Round>3</Round><Money>' \
                          '2000</Money><Score>2</Score></Reward><Reward><Round>4</Round><Money>5000</Money><Score>3</Score></Reward>' \
                          '<Reward><Round>5</Round><Money>10000</Money><Score>5</Score><Tool>1</Tool><TicketCategory>5</TicketCategory>' \
                          '</Reward><Reward><Round>6</Round><Money>20000</Money><Score>10</Score><Tool>1</Tool>' \
                          '<TicketCategory>5</TicketCategory></Reward><Reward><Round>7</Round><Money>30000</Money><Score>15</Score>' \
                          '<Tool>1</Tool><TicketCategory>5</TicketCategory></Reward><Reward><Round>8</Round><Money>40000</Money>' \
                          '<Score>20</Score><Tool>1</Tool><TicketCategory>5</TicketCategory></Reward><Reward><Round>9</Round>' \
                          '<Money>60000</Money><Score>30</Score><Tool>1</Tool><TicketCategory>5</TicketCategory></Reward><Reward>' \
                          '<Round>100</Round><Money>100000</Money><Score>40</Score><Tool>1</Tool><TicketCategory>5</TicketCategory></Reward>',
}