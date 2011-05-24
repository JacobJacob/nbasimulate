#!/usr/bin/python
# -*- coding: utf-8 -*-

import re

EMPTY_MAIN_XML = "<ScoreH></ScoreH><ScoreA></ScoreA><ClubNameH></ClubNameH>" \
                 "<ClubNameA></ClubNameA><ClubLogoH></ClubLogoH><ClubLogoA>" \
                 "</ClubLogoA><ClubSayH></ClubSayH><ClubSayA></ClubSayA>" \
                 "<Tickets></Tickets><Income></Income><MVPName></MVPName><MVPStas>" \
                 "</MVPStas><NClubNameH></NClubNameH><NClubNameA></NClubNameA><NClubLogoH>" \
                 "</NClubLogoH><NClubLogoA></NClubLogoA><NClubSayH></NClubSayH><NClubSayA></NClubSayA>"

def build_main_xml(old_xml, new_info):
    """创建mina xml"""
    if not old_xml:
        old_xml = EMPTY_MAIN_XML
    for k, v in new_info.iteritems():
        old = r"<%s>[^<]*</%s>" % (k, k)
        new = "<%s>%s</%s>" % (k, v, k)
        old_xml = re.sub(old, new, old_xml)
       
    return old_xml

if __name__ == "__main__":
    print build_main_xml(None, {"ScoreH": 90})