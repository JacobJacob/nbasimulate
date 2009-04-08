package com.ts.dt.loader;

import com.ts.dt.po.ActionDesc;

public interface ActionDescLoader {

	public ActionDesc loadWithNameAndResult(String actionNm, String result);

	public ActionDesc loadWithNameAndResultAndFlg(String actionNm, String result, String flg);

}
