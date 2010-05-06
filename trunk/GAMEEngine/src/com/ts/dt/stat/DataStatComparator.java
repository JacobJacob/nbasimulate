package com.ts.dt.stat;

import java.io.Serializable;
import java.util.Comparator;

public class DataStatComparator<T> implements Comparator<PlayerDataStat>, Serializable {

	public int compare(PlayerDataStat playerDataStat1, PlayerDataStat playerDataStat2) {
		// TODO Auto-generated method stub
		int point1 = playerDataStat1.getTotalPoint();
		int point2 = playerDataStat2.getTotalPoint();
		if (point1 < point2) {
			return -1;
		} else if (point1 == point2) {
			return 0;
		} else {
			return 1;
		}
	}

}
