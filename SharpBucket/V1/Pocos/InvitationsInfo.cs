﻿using System.Collections.Generic;

namespace PortableBitBucketClient.V1.Pocos
{
	public class InvitationsInfo
	{
		public List<string> groups { get; set; }
		public User invited_by { get; set; }
		public string utc_sent_on { get; set; }
		public string email { get; set; }
	}
}