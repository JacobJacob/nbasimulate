package com.ts.dt.match.desc.shoot;

import java.util.Random;

import com.ts.dt.constants.MatchConstant;
import com.ts.dt.context.MatchContext;
import com.ts.dt.match.desc.ActionDescription;
import com.ts.dt.match.helper.TechnicallyTraitHelper;
import com.ts.dt.po.Player;

public class ShortShootDescription implements ActionDescription {

	public String load(MatchContext context) {
		// TODO Auto-generated method stub
		String desc = null;
		String result = context.getShootActionResult();
		Random random = new Random();
		int a = random.nextInt(16);
		if (MatchConstant.RESULT_SUCCESS.equals(result)) {
			switch (a) {
			case 0:
				desc = "~1~等候在底线 ,出了空档,接到队友隐蔽的传球,出手,球进! ";
				break;
			case 1:
				desc = "~1~等候在底线 ,出了空档,接到队友隐蔽的传球,出手,球进! ";
				break;
			case 2:
				desc = "~1~接球直接出手,一气呵成,漂亮的弧线!球空心入网";
				break;
			case 3:
				desc = "~1~假装突破,骗过防守队员,突然出手投篮,球进";
				break;
			case 4:
				desc = "~1~倚住防守队员,强行出手! 球居然进了";
				break;
			case 5:
				desc = "~1~中圈附近得球后,晃过~2~,一个很不标准的投篮,命中!";
				break;
			case 6:
				desc = "~1~和~3~做了一个挡拆配合后,以一记精准的半截篮结束了本次进攻!";
				break;
			case 7:
				desc = "~1~底线切入,过了防守队员~2~,轻松的打篮入筐!";
				break;
			case 8:
				desc = "~1~背身单打,用身体挤开防守球员,一个右手勾手投篮,命中";
				break;
			case 9:
				desc = "~1~接~3~隐蔽的背后传球,突破上篮得手";
				break;
			case 10:
				desc = "~1~用身体强吃对手,在对手头顶打板命中";
				break;
			case 11:
				desc = "~1~跳投命中";
				break;
			case 12:
				desc = "~1~脚踩三分线,高高跃起投篮,命中";
				break;
			case 13:
				desc = "~1~抢在防守球员的封盖之前出手命中";
				break;
			case 14:
				desc = "~1~真是个投篮机器,接球不做任何调整,直接出手,皮球空心入网";
				break;
			case 15:
				desc = "~1~侧身运球过了一名防守球员,抢在补防球员之前将球投入篮筐";
				break;
			default:
				break;
			}

		} else {
			switch (a) {
			case 0:
				desc = "~1~等候在底线 ,出了空档,接到队友隐蔽的传球,出手,球偏筐而出! ";
				break;
			case 1:
				desc = "~1~接球直接出手,一气呵成,漂亮的弧线!但球还是差一点没进";
				break;
			case 2:
				desc = "~1~假装突破, 骗过防守队员,突然出手投篮,球被防守队员干扰了一下,没进";
				break;
			case 3:
				desc = "~1~倚住防守队员,强行出手! 投了个三不沾";
				break;
			case 4:
				desc = "~1~跑动中接球,在二分线附近得到队友传球,一个挑篮 ,力量有些大,球砸在篮板上";
				break;
			case 5:
				desc = "~1~跑动中接球,在二分线附近得到队友传球,一个挑篮 ,力量有些大,球砸在篮板上";
				break;
			case 6:
				desc = "~1~和~3~做了一个挡拆配合后,投篮不进!";
				break;
			case 7:
				desc = "~1~底线切入,过了防守队员~2~,但投篮还是被干扰了一下,没进!";
				break;
			case 8:
				desc = "~1~背身单打,用身体挤开防守球员,一个右手勾手投篮,球弹筐而出";
				break;
			case 9:
				desc = "~1~接~2~隐蔽的背后传球,突破上篮,不进";
				break;
			case 10:
				desc = "~1~想用身体强吃对手,球没进";
				break;
			case 11:
				desc = "~1~跳投,不中";
				break;
			case 12:
				desc = "~1~脚踩三分线,高高跃起投篮,投了个三不沾";
				break;
			case 13:
				desc = "~1~在对手严密的防守之前强行出手,不中";
				break;
			case 14:
				desc = "~1~今天的状态不是很好,连这种空位投篮也不进";
				break;
			case 15:
				desc = "~1~侧身运球过了一名防守球员,但投篮被补防的球员干扰了一下,没进";
				break;
			default:
				break;
			}
		}
		return desc;
	}

	public String failure(MatchContext context) {
		String desc = null;
		Random random = new Random();
		Player player = context.getCurrentController().getPlayer();
		int trait = TechnicallyTraitHelper.check(player);
		int a = random.nextInt(16);
		return desc;
	}

	public String traitOutSuccess() {
		return null;
	}

	public String traitOutfailure() {
		return null;
	}

	public String success(MatchContext context) {
		// TODO Auto-generated method stub
		return null;
	}

}
