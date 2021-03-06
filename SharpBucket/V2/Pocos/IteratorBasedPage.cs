﻿using System.Collections.Generic;

namespace PortableBitBucketClient.V2.Pocos
{
	/// <summary>
	/// Models a page of data.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class IteratorBasedPage<T>
	{
		public int? pagelen { get; set; }
		public string next { get; set; }
		public List<T> values { get; set; }
		public ulong? size { get; set; }
	}
}
