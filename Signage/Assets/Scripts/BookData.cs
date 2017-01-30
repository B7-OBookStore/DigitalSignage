using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BookData {
	public string title;
	public string author;
	public string date;
	public string description;
	public string img;

	public BookData(string ti, string au, string da, string de, string im)
	{
		this.title = ti;
		this.author = au;
		this.date = da;
		this.description = de;
		this.img = "http://books.google.com/books/content?id=" + im + "&printsec=frontcover&img=1&zoom=5&source=gbs_api";
	}
}
