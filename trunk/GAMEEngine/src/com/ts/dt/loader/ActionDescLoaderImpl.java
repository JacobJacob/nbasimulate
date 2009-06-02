package com.ts.dt.loader;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import com.dt.bottle.persistence.Persistence;
import com.ts.dt.cache.ActionDescCache;
import com.ts.dt.dao.ActionDescDao;
import com.ts.dt.dao.impl.ActionDescDaoImpl;
import com.ts.dt.key.ActionDescKey;
import com.ts.dt.po.ActionDesc;

/**
 * 动作描述加载类
 * 
 */

public class ActionDescLoaderImpl implements ActionDescLoader {

	/** 缓存* */
	private ActionDescCache cache;

	private static ActionDescLoaderImpl actionDescLoaderImpl;

	private ActionDescLoaderImpl() {
		cache = new ActionDescCache();
	}

	public static ActionDescLoaderImpl getInstance() {

		if (actionDescLoaderImpl == null) {
			actionDescLoaderImpl = new ActionDescLoaderImpl();
		}
		return actionDescLoaderImpl;
	}

	public ActionDesc loadWithNameAndResult(String actionNm, String result) {
		// TODO Auto-generated method stub
		return null;
	}

	/**
	 * @param actionNm
	 *            the action name
	 * @param result
	 *            the action execute result
	 * @param flg
	 *            the flag,eg.blocked
	 * @return ActionDesc
	 */
	public ActionDesc loadWithNameAndResultAndFlg(String actionNm, String result, String flg) {
		// TODO Auto-generated method stub
		ActionDesc actionDesc = null;
		List<ActionDesc> actionDesclist = null;
		ActionDescKey key = new ActionDescKey(actionNm, result, flg);

		if (cache.containKey(key)) {
			// Logger.info("catch hit on,get from cache;key:" + key.toString());
			actionDesclist = cache.get(key);
		} else {
			actionDesclist = new ArrayList<ActionDesc>();
			ActionDescDao actionDescDao = new ActionDescDaoImpl();
			List<Persistence> list = actionDescDao.findWithActionAndResultAndFlg(actionNm, result, flg);
			for (Persistence p : list) {
				actionDesclist.add((ActionDesc) p);
			}
			cache.put(key, actionDesclist);
		}
		actionDesc = selectWithPercent(actionDesclist);
		return actionDesc;
	}

	private ActionDesc selectWithPercent(List<ActionDesc> actionDesclist) {

		ActionDesc actionDesc = null;
		boolean back = false;
		int totalPercent = 0;
		for (ActionDesc desc : actionDesclist) {
			totalPercent += desc.getPercent();
		}
		Random random = new Random();
		int ran = random.nextInt(totalPercent);
		int i = 0;
		int currentPoint = 0;
		while (!back) {
			currentPoint += actionDesclist.get(i).getPercent();
			if (ran < currentPoint) {
				actionDesc = actionDesclist.get(i);
				back = true;
			}
			i++;
		}
		return actionDesc;
	}

}
