package com.dt.ejb.facade;

import javax.ejb.Remote;
import javax.ejb.Stateless;
import javax.ejb.TransactionAttribute;
import javax.ejb.TransactionAttributeType;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import com.dt.ejb.common.ICommand;
import com.dt.ejb.context.RequestContext;
import com.dt.ejb.context.ResponseContext;

@Stateless
@Remote( { ServiceFacadeBean.class })
public class ServiceFacadeBeanImp implements ServiceFacadeBean {

	@PersistenceContext
	public EntityManager em;

	@TransactionAttribute(TransactionAttributeType.REQUIRED)
	public ResponseContext processRequest(RequestContext reqCtx) throws Exception {
		// TODO Auto-generated method stub
		ResponseContext resCtx = new ResponseContext();
		String className = "com.ts.dt.service.impl.MatchServiceImpl";
		ICommand command = (ICommand) Class.forName(className).newInstance();

		command.execute(reqCtx, resCtx);

		return resCtx;
	}

}
