package com.ts.dt.test;

import com.ts.dt.loader.ActionDescLoaderImpl;
import com.ts.dt.po.ActionDesc;

public class ActionDescTest {

	public static void main(String[] args) {
		ActionDesc actionDesc = null;

		for (int i = 0; i < 100; i++) {
			try {
				actionDesc = ActionDescLoaderImpl.getInstance().loadWithNameAndResultAndFlg("test", "success", "block");
				System.out.println(actionDesc.getActionName() + "/" + actionDesc.getPercent());

			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}
}
