package com.ts.dt.constants;

public class OffensiveTactical {

	public static final int STRONG_INSIDE = 1;// : u'强打内线',
	public static final int CENTER_COORDINATE = 2;// : u'中锋策应',
	public static final int OUTSIDE_SHOT = 3;// : u'外线投篮',
	public static final int FAST_ATTACK = 4;// : u'快速进攻',
	public static final int OVERALL_CO_ORDINATION = 5;// : u'整体配合',
	public static final int COVER_SCREENS_FOR = 6;// : u'掩护挡拆',

	public static String getOffensiveTacticalName(int tactical) {

		switch (tactical) {
		case STRONG_INSIDE:
			return "强打内线";
		case CENTER_COORDINATE:
			return "中锋策应";
		case OUTSIDE_SHOT:
			return "外线投篮";
		case FAST_ATTACK:
			return "快速进攻";
		case OVERALL_CO_ORDINATION:
			return "整体配合";
		case COVER_SCREENS_FOR:
			return "掩护挡拆";
		}
		return "";
	}

}
