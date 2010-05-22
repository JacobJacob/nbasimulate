package com.ts.dt.loader;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.ActionDesc;

public interface ActionDescLoader {

	public ActionDesc loadWithNameAndResult(String actionNm, String result) throws MatchException;

	public ActionDesc loadWithNameAndResultAndFlg(String actionNm, String result, String flg) throws MatchException;

}
