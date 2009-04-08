package com.ts.dt.facade;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

import javax.ejb.Remote;
import javax.ejb.Stateless;

import com.ts.dt.common.ICommand;
import com.ts.dt.constants.ContextConstant;
import com.ts.dt.context.RequestContext;
import com.ts.dt.context.ResponseContext;
import com.ts.dt.po.Transaction;
import com.ts.dt.pool.TransactionPool;

@Stateless
@Remote( { ServiceFacadeBean.class })
public class ServiceFacadeBeanImp implements ServiceFacadeBean {

	public ResponseContext processRequest(RequestContext reqCtx) {
		// TODO Auto-generated method stub
		ResponseContext resCtx = new ResponseContext();
		resCtx.init();
		String txnid = reqCtx.getTxnId();
		String txnCode = reqCtx.getTxnCode();
		String className = getServiceClassNameFromTxn(txnid, txnCode);

		Class<?> cls;
		Method method;
		Class<?>[] clss = { RequestContext.class, ResponseContext.class };
		ICommand commoand;
		Object[] parm = { reqCtx, resCtx };
		try {

			cls = Class.forName(className);
			commoand = (ICommand) cls.newInstance();
			method = cls.getMethod(ContextConstant.EXECUTE_METHOD_NAME, clss);
			method.invoke(commoand, parm);

		} catch (ClassNotFoundException ce) {
			ce.printStackTrace();
		} catch (NoSuchMethodException me) {
			me.printStackTrace();
		} catch (IllegalArgumentException ie) {
			ie.printStackTrace();
		} catch (InvocationTargetException inve) {
			inve.printStackTrace();
		} catch (IllegalAccessException illae) {
			illae.printStackTrace();
		} catch (InstantiationException inste) {
			inste.printStackTrace();
		}

		return resCtx;

	}

	public String getServiceClassNameFromTxn(String txnId, String txnCode) {

		TransactionPool pool = TransactionPool.getInstance();

		Transaction transaction = pool.getTransaction(txnId, txnCode);

		String className = transaction.getClassName();

		return className;
	}

}
