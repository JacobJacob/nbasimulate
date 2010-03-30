package com.ts.dt.constants;

public class OffensiveTactical {

	public static final int STRONG_INSIDE = 1;// : u'ǿ������',
	public static final int CENTER_COORDINATE = 2;// : u'�з��Ӧ',
	public static final int OUTSIDE_SHOT = 3;// : u'����Ͷ��',
	public static final int FAST_ATTACK = 4;// : u'���ٽ���',
	public static final int OVERALL_CO_ORDINATION = 5;// : u'�������',
	public static final int COVER_SCREENS_FOR = 6;// : u'�ڻ�����',

	public static String getOffensiveTacticalName(int tactical) {

		switch (tactical) {
		case STRONG_INSIDE:
			return "ǿ������";
		case CENTER_COORDINATE:
			return "�з��Ӧ";
		case OUTSIDE_SHOT:
			return "����Ͷ��";
		case FAST_ATTACK:
			return "���ٽ���";
		case OVERALL_CO_ORDINATION:
			return "�������";
		case COVER_SCREENS_FOR:
			return "�ڻ�����";
		}
		return "";
	}

}
