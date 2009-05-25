package com.dt.bottle.exception;

public class SessionException extends Exception {

	public static final long serialVersionUID = -2805459034543427093L;

	public SessionException() {
		super();
	}

	public SessionException(String desc) {
		super(desc);
	}
}
