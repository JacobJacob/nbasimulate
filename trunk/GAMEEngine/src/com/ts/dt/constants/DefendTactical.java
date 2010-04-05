package com.ts.dt.constants;

public class DefendTactical {

	public static final int TWO_THREE_DEFENSE = 10;// : u'2-3联防',
	public static final int THREE_TWO_DEFENSE = 12;// : u'3-2联防',
	public static final int TWO_ONE_TWO_DEFENS = 11;// : u'2-1-2联防',
	public static final int MAN_MARKING_DEFENSE = 8;// : u'盯人防守',
	public static final int MAN_MARKING_INSIDE = 7;// : u'盯人内线',
	public static final int MAN_MARKING_OUTSIDE = 9;// : u'盯人外线',

	public static String getDefendTacticalName(int tactical) {

		switch (tactical) {
		case TWO_THREE_DEFENSE:
			return "2-3联防";
		case THREE_TWO_DEFENSE:
			return "3-2联防";
		case TWO_ONE_TWO_DEFENS:
			return "2-1-2联防";
		case MAN_MARKING_DEFENSE:
			return "盯人防守";
		case MAN_MARKING_INSIDE:
			return "盯人内线";
		case MAN_MARKING_OUTSIDE:
			return "盯人外线";
		}
		return String.valueOf(tactical);
	}
}
