#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.business.user_roles import UserManager
from gba.business import match_operator

user_infos = [
  ['test1', '花木兰1', '木兰女子队1'],
  ['test2', '花木兰2', '木兰女子队2'],
  ['test3', '花木兰3', '木兰女子队3'],
  ['test4', '花木兰4', '木兰女子队4'],
  ['test5', '花木兰5', '木兰女子队5'],
  ['test6', '花木兰6', '木兰女子队6'],
  ['test7', '花木兰7', '木兰女子队7'],
  ['test8', '花木兰8', '木兰女子队8'],
  ['test9', '花木兰9', '木兰女子队9'],
  ['test10', '花木兰10', '木兰女子队10'],
  ['test11', '花木兰11', '木兰女子队11'],
  ['test12', '花木兰12', '木兰女子队12'],
  ['test13', '花木兰13', '木兰女子队13'],
  ['test14', '花木兰14', '木兰女子队14'],       
]

def main():
    user_manager = UserManager()
    for user_info in user_infos:
        success, session_id = user_manager.register_user(user_info[0], '821015', user_info[1])
        print success, session_id
        if success:
            match_operator.init_team({'username': user_info[0], 'teamname': user_info[2]})


if __name__ == '__main__':
    main()