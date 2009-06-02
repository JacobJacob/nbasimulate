package com.ts.dt.factory;

import com.ts.dt.context.MatchContext;
import com.ts.dt.match.action.Action;
import com.ts.dt.match.check.block.BlockCheck;
import com.ts.dt.match.check.block.DefaultBlockCheck;
import com.ts.dt.util.StringUtil;

/**
 * shoot whether be block check factory class
 * 
 * @author Administrator
 * 
 */
public class BlockCheckFactory {

	public static final String CHECK_CLASS_SUFFIXAL = "BlockCheck";
	public static final String CHECK_CLASS_PACKAGE_NAME = "com.ts.dt.block";

	private static BlockCheckFactory blockcheckFactory;

	private BlockCheckFactory() {

	}

	public static BlockCheckFactory getInstance() {

		if (blockcheckFactory == null) {
			blockcheckFactory = new BlockCheckFactory();
		}
		return blockcheckFactory;
	}

	public BlockCheck createBlockCheckFactory(MatchContext context) {

		BlockCheck blockCheck = null;

		String claFulNm;
		Action action = context.getCurrentAction();
		String clsNm = StringUtil.className2ShortName(action);

		claFulNm = CHECK_CLASS_PACKAGE_NAME + "." + clsNm + CHECK_CLASS_SUFFIXAL;

		try {
			blockCheck = (BlockCheck) Class.forName(claFulNm).newInstance();
		} catch (Exception e) {
			blockCheck = new DefaultBlockCheck();
		}
		return blockCheck;

	}
}
