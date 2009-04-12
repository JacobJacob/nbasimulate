package com.dt.bottle.exception;

public class SessionException extends Exception {
	private String description;

	public SessionException() {
		super();
	}

	public SessionException(String desc) {
		super(desc);
	}
}
