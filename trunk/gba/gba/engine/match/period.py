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

        action = context.current_action;

        if action == MatchType.SHOUT:
             if context.current_offensive_timeout:
                current_controller.foul(context)
                self.check_next_controller(MatchType.FOUL)
            else:
                currentController.shout(context);

                if (! context.isFoul() && ! context.isSuccess() && ! context.isOutside()) {
                    BackboardCheckFactory.getInstance().createBackboardCheckFactory(context).check(context);
                }
                checkNextController(MatchConstant.ACTION_TYPE_SHOUT);
                context.put(MatchConstant.HAS_PASS_TIMES, 0);

        elif action == MatchType.PASS:
        
        elif action == MatchType.REBOUND:
            
        elif action == MatchType.SERVICE:
            
        elif action == MatchType.SCRIMMAGE:
            
        else:
            raise 'not defind action %s' % action
            
        if (action == MatchConstant.ACTION_TYPE_SHOUT) {
            if (context.currentOffensiveTimeOut()) {

                currentController.foul(context);
                checkNextController(MatchConstant.ACTION_TYPE_FOUL);
            } else {
                currentController.shout(context);

                if (! context.isFoul() && ! context.isSuccess() && ! context.isOutside()) {
                    BackboardCheckFactory.getInstance().createBackboardCheckFactory(context).check(context);
                }
                checkNextController(MatchConstant.ACTION_TYPE_SHOUT);
                context.put(MatchConstant.HAS_PASS_TIMES, 0);
            }

        } else if (action == MatchConstant.ACTION_TYPE_PASS) {
            if (context.currentOffensiveTimeOut()) {
                currentController.foul(context);
                checkNextController(MatchConstant.ACTION_TYPE_FOUL);
            } else {
                currentController.pass(context);
                checkNextController(MatchConstant.ACTION_TYPE_PASS);
                Pass afterPass = (Pass) context.getCurrentAction();
                afterPass.after(context);
                int hasPassTimes = (Integer) context.get(MatchConstant.HAS_PASS_TIMES);
                hasPassTimes + +;
                context.put(MatchConstant.HAS_PASS_TIMES, hasPassTimes);
            }

        } else if (action == MatchConstant.ACTION_TYPE_REBOUND) {
            currentController.loose(context);
            checkNextController(MatchConstant.ACTION_TYPE_REBOUND);

        } else if (action == MatchConstant.ACTION_TYPE_SERVICE) {
            currentController.service(context);
            checkNextController(MatchConstant.ACTION_TYPE_SERVICE);
        } else if (action == MatchConstant.ACTION_TYPE_SCRIMMAGE) {
            currentController.scrimmage(context);
            checkNextController(MatchConstant.ACTION_TYPE_SCRIMMAGE);
            Scrimmage scrimmage = (Scrimmage) context.getCurrentAction();
            scrimmage.after(context);
        }
        checkNextActionType();
        checkNextDefender();
    }


    public void next() {

        Controller currentController = context.getCurrentController();
        Controller nextController = context.getNextController();
        context.setPreviousController(currentController);
        context.setCurrentController(nextController);
        context.setBallRight();

        Controller nextDefender = context.getNextDefender();
        context.setCurrentDefender(nextDefender);

        /*********************************************************************** 
         * move action type: next = > current; current = > previous ;clear next
         **********************************************************************/ 
        int currentActionType = context.getCurrentActionType();
        int nextActionType = context.getNextActionType();

        context.setPreviousActionType(currentActionType);
        context.setCurrentActionType(nextActionType);
        context.setNextActionType(MatchConstant.NULL_INTEGER);

        context.setFoul(false);
        context.setOutside(false);
    
    def next(self):
        pass
    
    def _check_next_controller(action) {


    }
    
    def _load_player(self):
        '''load player'''
        
    
