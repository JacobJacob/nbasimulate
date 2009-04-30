package com.ts.dt.key;

public class ActionDescKey {

	private String actionNm;
	private String result;
	private String flg;

	public ActionDescKey(String actionNm, String result, String flg) {
		this.actionNm = actionNm;
		this.result = result;
		this.flg = flg;
	}

	public String getActionNm() {
		return actionNm;
	}

	public void setActionNm(String actionNm) {
		this.actionNm = actionNm;
	}

	public String getResult() {
		return result;
	}

	public void setResult(String result) {
		this.result = result;
	}

	public String getFlg() {
		return flg;
	}

	public void setFlg(String flg) {
		this.flg = flg;
	}

	@Override
	public boolean equals(Object obj) {
		// TODO Auto-generated method stub
		if (!(obj instanceof ActionDescKey)) {
			return false;
		}
		ActionDescKey temp = (ActionDescKey) obj;
		boolean res;

		if (actionNm == null || result == null) {
			return false;
		}
		res = actionNm.equals(temp.getActionNm()) && result.equals(temp.getResult());
		res = res && ((flg == null && temp.getFlg() == null) || flg.equals(temp.getFlg()));

		return res;
	}

	@Override
	public int hashCode() {
		// TODO Auto-generated method stub
		int result = 14;
		int c = 0;
		c += this.actionNm == null ? 0 : this.actionNm.hashCode();
		c += this.result == null ? 0 : this.result.hashCode();
		c += this.flg == null ? 0 : this.flg.hashCode();

		result = 37 * result + c;

		return result;
	}

	@Override
	public String toString() {
		// TODO Auto-generated method stub
		StringBuffer sb = new StringBuffer();
		sb.append("Action Name:");
		sb.append(actionNm);
		sb.append("|Result:");
		sb.append(result);
		sb.append("|Flg:");
		sb.append(flg);
		return sb.toString();
	}

}
