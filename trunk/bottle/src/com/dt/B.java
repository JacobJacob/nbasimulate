package com.dt;

public class B extends A {

	public int b;

	public static void main(String[] args) {

		byte b1 = -8; // 
		byte b2 = 8;
		byte b3 = 0;
		byte b4 = 0;
		byte b5 = 98;
		byte b6 = -0;

		System.out.println(byte2short(b1, b2));
		System.out.println(byte2short(b1, b3));
		System.out.println(byte2short(b2, b3));
		System.out.println(byte2short(b4, b5));
		System.out.println(byte2short(b2, b2));
		System.out.println(byte2short(b4, b2));
		System.out.println(byte2short(b6, b2));

	}

	public static short byte2short(byte b1, byte b2) {

		int retVal;

		int temp = b1 << 8;

		if (temp >= 0) {
			retVal = temp + b2;
		} else {
			retVal = temp - b2 ;
		}
		return (short) retVal;
	}
}
