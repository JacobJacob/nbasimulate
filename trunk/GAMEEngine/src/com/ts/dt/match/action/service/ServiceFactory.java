package com.ts.dt.match.action.service;

import com.ts.dt.context.MatchContext;

public class ServiceFactory {
	private static ServiceFactory serviceFactory;

	private ServiceFactory() {

	}

	public static ServiceFactory getInstance() {

		if (serviceFactory == null) {
			serviceFactory = new ServiceFactory();
		}
		return serviceFactory;
	}

	// check for player do which action
	// that must depends on the previous action and the
	// current player's position....
	public Service createServiceAction(MatchContext context) {

		Service service = null;
		service = new DefaultService();
		return service;
	}

}
