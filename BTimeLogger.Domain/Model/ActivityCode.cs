using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTimeLogger
{
	public partial class Activity
	{
		public class ActivityCode
		{
			private const char DELIM = '/';

			private ActivityCode(string value)
			{
				Value = value;
			}

			public string Value { get; set; }
			public string[] Parts { get => Value.Split(DELIM); }
			public string ActivityName
			{
				get
				{
					if (Parts.Length <= 0) return string.Empty;
					else return Parts[Parts.Length - 1];
				}
			}
			public string[] GroupNames
			{
				get
				{
					if (Parts.Length <= 0) return Array.Empty<string>();
					return Parts.Take(Parts.Length - 1).ToArray();
				}
			}
			public ActivityCode ParentCode
			{
				get
				{
					if (GroupNames.Length <= 0)
						return null;
					string[] groupNames = GroupNames;

					string parentName = groupNames[groupNames.Length - 1];
					string[] parentParentNames = groupNames.Take(groupNames.Length - 1).ToArray();
					return CreateCode(parentName, parentParentNames);
				}
			}

			public IEnumerable<ActivityCode> AncestorCodes
			{
				get
				{
					if (Parts.Length <= 0)
						return Array.Empty<ActivityCode>();

					List<ActivityCode> ancestors = new();

					ActivityCode currentAncestor = this;
					while (currentAncestor != null)
					{
						ancestors.Add(currentAncestor);
						currentAncestor = currentAncestor.ParentCode;
					}

					return ancestors;
				}
			}

			public override bool Equals(object obj)
			{
				ActivityCode other = obj as ActivityCode;
				if (other == null) return false;

				return other.Value.Equals(Value);
			}

			public override int GetHashCode()
			{
				return Value.GetHashCode();
			}

			public override string ToString()
			{
				return Value;
			}

			public static ActivityCode CreateCode(Activity activity)
			{
				string activityName = activity.Name;
				string[] groupNames = GetGroupNames(activity);
				return CreateCode(activityName, groupNames);
			}

			public static ActivityCode CreateCode(string activityName, string[] groupNames)
			{
				StringBuilder builder = new();
				for (int i = 0; i < groupNames.Length; i++)
				{
					builder.Append(groupNames[i]);
					builder.Append(DELIM);
				}
				builder.Append(activityName);
				return new ActivityCode(builder.ToString());
			}

			private static string[] GetGroupNames(Activity activity)
			{
				List<string> groupNames = new();

				Activity currentGroupAncestor = activity.Parent;
				while (currentGroupAncestor != null)
				{
					groupNames.Add(currentGroupAncestor.Name);
					currentGroupAncestor = currentGroupAncestor.Parent;
				}

				groupNames.Reverse();
				return groupNames.ToArray();
			}

		}
	}
}
