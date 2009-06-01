<?php
// $Id$

/**
 * Controller_Member_Match controller
 */
class Controller_Member_Match extends Controller_Abstract
{

	function actionIndex()
	{

		$page_index = 1 ;
		if(isset($this->_context->page))
		{
			$page_index = $this->_context->page;
		}
		$home_tame_id = 1 ;
		$query = MatchReq::find('home_team_id=?',$home_tame_id)->limitPage($page_index,12);

		$matchs = $query->query();
		$pagination = $query->getPagination();

		$udi = QContext::instance()->requestUDI('array');
		$page = new Page($udi,$pagination );

		$this->_view['matchs'] = $matchs ;
		$this->_view['navigation'] = $page->navigation() ;
		$this->_view['current_page'] = $page_index;
	}

	function actionMatchDetail(){
		$this-> _loadData();
	}
	/**
     * 比赛统计
     *
     */
	function actionMatchBulletin(){

		$match_id = $_GET['id'];
		$match = Match::find('id=?',$match_id)->query();
		$home_team_id = $match->home_team_id;
		$all_bulletins = Bulletin::find('match_id=?',$match_id)->getAll();

		$home_bulletins = array();
		$visiting_bulletins = array();
		$home_total = new Bulletin();
		$visiting_total = new Bulletin();
		foreach($all_bulletins as $bulletin){
			if($bulletin->team_id ==$home_team_id ){
				$home_total->point1_shoot_times += $bulletin->point1_shoot_times;
				$home_total->point1_doom_times += $bulletin->point1_doom_times;
				$home_total->point2_shoot_times += $bulletin->point2_shoot_times;
				$home_total->point2_doom_times += $bulletin->point2_doom_times;
				$home_total->point3_shoot_times += $bulletin->point3_shoot_times;
				$home_total->point3_doom_times += $bulletin->point3_doom_times;
				$home_total->offensive_rebound += $bulletin->offensive_rebound;
				$home_total->defensive_rebound += $bulletin->defensive_rebound;
				array_push($home_bulletins,$bulletin);
			}else{
				$visiting_total->point1_shoot_times += $bulletin->point1_shoot_times;
				$visiting_total->point1_doom_times += $bulletin->point1_doom_times;
				$visiting_total->point2_shoot_times += $bulletin->point2_shoot_times;
				$visiting_total->point2_doom_times += $bulletin->point2_doom_times;
				$visiting_total->point3_shoot_times += $bulletin->point3_shoot_times;
				$visiting_total->point3_doom_times += $bulletin->point3_doom_times;
				$visiting_total->offensive_rebound += $bulletin->offensive_rebound;
				$visiting_total->defensive_rebound += $bulletin->defensive_rebound;
				array_push($visiting_bulletins,$bulletin);
			}
		}
		$home_bulletins = Helper_Array::sortByMultiCols($home_bulletins,array('total_point'=>SORT_DESC,'total_rebound'=>SORT_DESC));
		$visiting_bulletins = Helper_Array::sortByMultiCols($visiting_bulletins,array('total_point'=>SORT_DESC,'total_rebound'=>SORT_DESC));

		$this->_view['match'] = $match;
		$this->_view['home_total'] = $home_total;
		$this->_view['visiting_total'] = $visiting_total;
		$this->_view['home_bulletins'] = $home_bulletins;
		$this->_view['visiting_bulletins'] = $visiting_bulletins;
	}
	/**
	 * 加载信息
	 */
	private function _loadData()
	{
		$match_id = $this->_context->id;
		$match = Match::find('id=?',$match_id)->query();

		$this->_view['match'] = $match;
	}


}


