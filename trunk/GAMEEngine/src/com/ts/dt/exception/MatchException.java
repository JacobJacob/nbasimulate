package com.ts.dt.exception;

public class MatchException extends Throwable {

	String message;
	/**
	 * 
	 */
	private static final long serialVersionUID = -11343756596180312L;

	public MatchException(Throwable cause) {
		super(cause);
	}

	public MatchException(String message) {
		super(message);
	}
}
