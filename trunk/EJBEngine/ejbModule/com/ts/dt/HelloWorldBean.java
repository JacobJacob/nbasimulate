package com.ts.dt;

import javax.ejb.Remote;
import javax.ejb.Stateless;


@Stateless
@Remote({HelloWorld.class})
public class HelloWorldBean implements HelloWorld {

	public String sayHello(String name) {
		// TODO Auto-generated method stub
		return "Hello" + name;
	}

}
