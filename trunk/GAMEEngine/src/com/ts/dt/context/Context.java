package com.ts.dt.context;

import java.io.Serializable;
import java.util.Hashtable;

import com.ts.dt.constants.ContextConstant;

public class Context implements Serializable {

	public static final long serialVersionUID = -2805454943653427093L;

	private Hashtable<String, Object> msData;

	public void init() {
		msData = new Hashtable<String, Object>();
	}

	public void setObject(String key, Object value) {

		msData.put(key, value);
	}

	public Object getObject(String key) {
		return msData.get(key);
	}

	public String getTxnId() {

		return getObject(ContextConstant.TXN_ID) == null ? "" : getObject(
				ContextConstant.TXN_ID).toString();
	}

	public void setTxnId(String txnId) {
		if (txnId != null) {
			msData.put(ContextConstant.TXN_ID, txnId);
		}
	}

	public String getTxnCode() {

		return getObject(ContextConstant.TXN_CODE) == null ? "" : getObject(
				ContextConstant.TXN_CODE).toString();
	}

	public void setTxnCode(String txnCode) {
		if (txnCode != null) {
			msData.put(ContextConstant.TXN_CODE, txnCode);
		}
	}

	public String getReturnCode() {
		return msData.get(ContextConstant.RETURN_CODE) == null ? "9999"
				: msData.get(ContextConstant.RETURN_CODE).toString();
	}

	public void setReturnCode(String returnCode) {
		msData.put(ContextConstant.RETURN_CODE, returnCode);
	}

}
