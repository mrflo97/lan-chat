using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
	public class ParticipantList : List<string>, IEquatable<ParticipantList>
	{
		private const string Delimiter = "|";

		public ParticipantList(IEnumerable<string> elements) : base(elements)
		{
		}

		public ParticipantList(string compareString)
		{
			var parts = compareString.Split(ParticipantList.Delimiter);
			this.AddRange(parts);
		}

		public string GetCompareString()
		{
			this.Sort();
			return string.Join("|", this);
		}

		public bool Equals(ParticipantList other)
		{
			if (object.ReferenceEquals(null, other)) return false;
			if (object.ReferenceEquals(this, other)) return true;
			return other.GetCompareString() == this.GetCompareString();
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj)) return false;
			if (object.ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return this.Equals((ParticipantList)obj);
		}

		public override int GetHashCode()
		{
			return unchecked(this.GetCompareString().Select<char, int>(x => x).Aggregate((x, y) => x + y));
		}
	}
}
