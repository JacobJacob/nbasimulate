package com.ts.dt.constants;

public class DefendTactical {

	public static final int TWO_THREE_DEFENSE = 10;// : u'2-3����',
	public static final int THREE_TWO_DEFENSE = 12;// : u'3-2����',
	public static final int TWO_ONE_TWO_DEFENS = 11;// : u'2-1-2����',
	public static final int MAN_MARKING_DEFENSE = 8;// : u'���˷���',
	public static final int MAN_MARKING_INSIDE = 7;// : u'��������',
	public static final int MAN_MARKING_OUTSIDE = 9;// : u'��������',

	public static String getDefendTacticalName(int tactical) {

		switch (tactical) {
		case TWO_THREE_DEFENSE:
			return "2-3����";
		case THREE_TWO_DEFENSE:
			return "3-2����";
		case TWO_ONE_TWO_DEFENS:
			return "2-1-2����";
		case MAN_MARKING_DEFENSE:
			return "���˷���";
		case MAN_MARKING_INSIDE:
			return "��������";
		case MAN_MARKING_OUTSIDE:
			return "��������";
		}
		return String.valueOf(tactical);
	}
}
