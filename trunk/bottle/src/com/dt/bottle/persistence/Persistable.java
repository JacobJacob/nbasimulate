package com.dt.bottle.persistence;

public interface Persistable {
	public long id = 0;

	public boolean save();

	public boolean delete();

	public void load(long id);

}
