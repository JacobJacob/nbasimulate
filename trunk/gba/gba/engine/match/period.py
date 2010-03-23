#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.business import match_operator
from gba.common.constants import MatchType

class Controller(object):
    
    def __init__(self, player, posistion, is_home=True):
        self._player = player
        self._is_home = is_home
        self._posistion = posistion

class Period(object):
    
    def __init__(self, context):
        self._context = context
        
    def execute(self):
        '''execute'''
        current_controller = self._context.current_controller;
        if not self._context.current_defender:
            check_current_defender()
        
        if not context.current_action:
            check_current_action()

        action = self._context.current_action;

        if action.type == MatchType.SHOUT:
            if self._context.current_offensive_timeout:
                current_controller.foul(self._context)
                self.check_next_controller(MatchType.FOUL)
            else:
                current_controller.shout(self._context);
                #handle after shout
                if not self._context.is_foul and not self._context.is_success and not self._context.is_outside:
                    pass
                    #BackboardCheckFactory.getInstance().createBackboardCheckFactory(context).check(context);
                self.check_next_controller(MatchType.SHOUT);
                context.has_pass_times = 0 #reset pass times

        elif action.type == MatchType.PASS:
            if context.current_offensive_timeout:
                current_controller.foul(self._context);
                self.check_next_controller(MatchType.FOUL)
            else:
                current_controller.pass_(self._context);
                self.check_next_controller(MatchType.PASS);
                after_pass = self._context.current_action;
                afterPass.after(self._context);
                has_pass_times = self._context.has_pass_times
                has_pass_times += 1
                context.has_pass_times = has_pass_times
                
        elif action == MatchType.REBOUND:
            current_controller.recovery(self._context);
            self.check_next_controller(MatchType.REBOUND);
            
        elif action == MatchType.SERVICE:
            current_controller.service(self._context);
            self.check_next_controller(MatchType.SERVICE);
            
        elif action == MatchType.SCRIMMAGE:
            current_controller.scrimmage(self._context);
            self.check_next_controller(MatchType.SCRIMMAGE);
            scrimmage = self._context.current_action;
            scrimmage.after(self._context);
        else:
            raise 'not defind action %s' % action
        
        self.check_next_action_type();
        self.check_next_defender();
    
    def next(self):       
        '''next'''
        current_controller = self._context.current_controller;
        next_controller = self._context.next_controller;
        
        self._context.previous_controller = current_controller
        self._context.current_controller = next_controller;
        self._context.BallRight();

        next_defender = self._context.next_defender;
        self._context.CurrentDefender(next_defender);

        #move action type: next = > current; current = > previous ;clear next

        current_action_type = self._context.current_action_type
        next_action_type = self._context.next_action_type;

        self._context.PreviousActionType(currentActionType);
        self._context.CurrentActionType(nextActionType);
        self._context.NextActionType(MatchType.NULL_INTEGER);
        self._context.Foul(false);
        self._context.Outside(false);

    def _check_next_controller(action):
    
    def _load_player(self):
        '''load player'''
        
    
