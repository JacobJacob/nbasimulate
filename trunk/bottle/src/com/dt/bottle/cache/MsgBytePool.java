/*
 * @(#)ReceiveMsgBytePool.java 2009-3-2 Shi Jacky@TechnoSolve
 *
 * Copyright (c) 2003 TechnoSolve Limited, Hong Kong. All Rights Reserved.
 *
 */
package com.dt.bottle.cache;

/**
 * @author Shi Jacky@TechnoSolve
 * 
 */
public class MsgBytePool {

	private int start_cursor = 0;
	private int end_cursor = 0;
	private byte[] msg = new byte[8096];

	private final Object lock = new Object();

	protected void append(byte[] b, int offset, int len) {
		synchronized (lock) {

			if (len + end_cursor > msg.length) {
				if (len + end_cursor - start_cursor > msg.length) {
					int mul = (int) ((len + end_cursor - start_cursor - msg.length) / 1024) + 1;
					expand(mul * 1024);
				} else {
					reset();
				}
			}
			System.arraycopy(b, offset, msg, end_cursor, len);
			end_cursor += len;

			lock.notifyAll();

		}

	}

	private void expand(int size) {
		if (start_cursor > 0) {
			reset();
		}
		byte[] temp = new byte[msg.length + size];
		System.arraycopy(msg, 0, temp, 0, end_cursor);
		msg = temp;
	}

	private void reset() {

		byte[] temp = new byte[msg.length];
		int length = end_cursor - start_cursor;
		System.arraycopy(msg, start_cursor, temp, 0, length);
		msg = temp;
		start_cursor = 0;
		end_cursor = length;

	}

	public String divMsg() {
		String message = null;
		while (true) {
			synchronized (lock) {
				int curLen = end_cursor - start_cursor;
				if (curLen >= 4) {
					byte[] msgHead = new byte[] { msg[start_cursor], msg[start_cursor + 1], msg[start_cursor + 2],
							msg[start_cursor + 3] };
					int msgSize = 0;//MessageFormat.decodeHeader(msgHead);

					if (curLen >= msgSize + 4) {

						String content = new String(msg, start_cursor + 4, msgSize);
						return content;
					}

					lock.notifyAll();
					start_cursor += (msgSize + 4);
					return message;
				}

				try {
					lock.wait();
				} catch (InterruptedException ie) {
					ie.printStackTrace();
				}

			}

		}
	}
}
